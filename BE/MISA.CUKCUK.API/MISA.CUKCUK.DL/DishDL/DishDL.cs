using Dapper;
using MISA.CUKCUK.API.Common;
using MISA.CUKCUK.API.Common.Entities;
using MISA.CUKCUK.API.Common.Entities.DTO;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.DL
{
    public class DishDL : BaseDL<Dish>, IDishDL
    {
        /// <summary>
        /// API lay danh sach DTODish hien thi va tong so ban ghi khong ke phan trang
        /// Nguyen Ba Hai - 22/12/2022
        /// </summary>
        /// <param name="limit">So ban ghi 1 trang</param>
        /// <param name="offset">Bat dau tu ban ghi nao</param>
        /// <param name="filterAndSortQuery">Query lay danh sach ban ghi bang filter, paging</param>
        /// <param name="totalQuery"> Query lay tong so ban ghi</param>
        /// <returns></returns>
        public DTOPagingData<DTODish> GetDishByPagingAndFiltering(int limit, int offset, string filterAndSortQuery, string totalQuery)
        {
            //Chuan bi store proc
            string storeProcName = String.Format(Procedures.GET_PAGING, typeof(Dish).Name);

            //Chuan bi tham so dau vao
            var parameters = new DynamicParameters();
            parameters.Add("@Limit", limit);
            parameters.Add("@Offset", offset);
            parameters.Add("@FilterAndSortQuery", filterAndSortQuery);
            parameters.Add("@TotalQuery", totalQuery);

            //Thuc hien goi vao DB
            var dishPagingInfo = new DTOPagingData<DTODish>();
            using (var mySqlConnection = new MySqlConnection(DatabaseConfig.ConnectionString)) 
            {
                if (mySqlConnection != null && mySqlConnection.State != ConnectionState.Open)
                {
                    mySqlConnection.Open();
                    var result = mySqlConnection.QueryMultiple(storeProcName, parameters, commandType: CommandType.StoredProcedure);
                    if (result != null) 
                    {
                        var listDish = result.Read<DTODish>().ToList();
                        var totalRecord = result.ReadSingle<int>();
                        dishPagingInfo = new DTOPagingData<DTODish>(listDish, totalRecord);
                    }
                    mySqlConnection.Close();
                }
            }
            return dishPagingInfo;
        }

        /// <summary>
        /// API them moi hoac sua ban ghi mon an
        /// Nguyen Ba Hai - 22/12/2022
        /// </summary>
        /// <param name="dish"></param>
        /// <returns></returns>
        public Guid? InsertUpdateDish(DTODishAndHobbyList dishObject)
        {
            string storeProcName;
            Guid? returnId;
            if (dishObject.Dish.DishID == null || dishObject.Dish.DishID == Guid.Empty)   //Case INSERT
            {
                //GUID moi sinh ra trong qua trinh them moi
                returnId = Guid.NewGuid();
            }

            else //Case UPDATE
            {
                returnId = dishObject.Dish.DishID;
            }

            //Chuan bi tham so dau vao cho dish
            var dishParameters = PrepareDishParameters(dishObject, returnId);

            //Chuan bi tham so dau vao cho HobbyServeDish
            var hobbyServeDishParameters = new DynamicParameters();
            string insertQueryHobby = "";
            foreach (var hobby in dishObject.HobbyList)
            {
                var hobbyServeDishID = Guid.NewGuid();
                var hobbyID = hobby.HobbyID;
                var dishID = returnId;
                insertQueryHobby += $"('{hobbyServeDishID}', '{hobbyID}', '{dishID}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', 'Nguyễn Bá Hải'),";
            }
            if (insertQueryHobby.Length > 0)
            {
                insertQueryHobby = insertQueryHobby.Remove(insertQueryHobby.Length - 1) + ";";
            }

            hobbyServeDishParameters.Add("@Text", insertQueryHobby);
            int numberOfAffectedRows = 0;
            //Thuc hien goi vao DB
            //Chia case sua va them
            //Them thi them Dish rooif them Hobby
            if (dishObject.Dish.DishID == null || dishObject.Dish.DishID == Guid.Empty) //Case INSERT
            {
                //Chuan bi store proc cho them moi cho dish
                using (var mySqlConnection = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    if (mySqlConnection != null && mySqlConnection.State != ConnectionState.Open)
                    {
                        mySqlConnection.Open();
                        using (MySqlTransaction transaction = mySqlConnection.BeginTransaction())//Bat dau transaction
                        {
                            storeProcName = String.Format(Procedures.INSERT, typeof(Dish).Name);//Store them moi Dish
                            try
                            {
                                numberOfAffectedRows = mySqlConnection.Execute(storeProcName, dishParameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                                if (insertQueryHobby.Trim() == "")
                                {
                                    transaction.Commit();
                                }
                                else //Them ban ghi cho HobbyServeDish
                                {
                                    storeProcName = String.Format(Procedures.INSERT, typeof(HobbyServeDish).Name);//Store them moi HobbyServeDish
                                    try
                                    {
                                        mySqlConnection.Execute(storeProcName, hobbyServeDishParameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                                        transaction.Commit();
                                    }
                                    catch
                                    {
                                        transaction.Rollback();
                                    }
                                }
                            }
                            catch
                            {
                                transaction.Rollback();
                            }
                        }
                        mySqlConnection.Close();
                    }
                }
            }

            else //CASE UPDATE
            {
                //Xoa het du lieu bang HobbyServeDish ma co id la DishID duoc chon
                //Goi store sua Dish
                //Goi store insert vao bang HobbyServeDish
                using (var mySqlConnection = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    if (mySqlConnection != null && mySqlConnection.State != ConnectionState.Open)
                    {
                        mySqlConnection.Open();
                        using (MySqlTransaction transaction = mySqlConnection.BeginTransaction()) //Bat dau transaction
                        {
                            storeProcName = String.Format(Procedures.UPDATE, typeof(Dish).Name);//Store sua Dish
                            try
                            {
                                numberOfAffectedRows = mySqlConnection.Execute(storeProcName, dishParameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                                //Xoa het Hobby
                                int deleteNumber = DeleteHobbiesByDishID(dishObject.Dish.DishID.ToString());
                                if (deleteNumber == -1)
                                {
                                    transaction.Rollback();
                                }
                                else
                                {
                                    if (insertQueryHobby.Trim() == "")
                                    {
                                        transaction.Commit();
                                    }
                                    else //Them ban ghi cho HobbyServeDish
                                    {
                                        storeProcName = String.Format(Procedures.INSERT, typeof(HobbyServeDish).Name);//Store them moi HobbyServeDish
                                        try
                                        {
                                            mySqlConnection.Execute(storeProcName, hobbyServeDishParameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                                            transaction.Commit();
                                        }
                                        catch
                                        {
                                            transaction.Rollback();
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                transaction.Rollback();
                            }
                        }
                        mySqlConnection.Close();
                    }
                }
            }
            if (numberOfAffectedRows > 0)
            {
                return returnId;
            }
            else return Guid.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dishObject"></param>
        /// <param name="returnId"></param>
        /// <returns></returns>
        private static DynamicParameters PrepareDishParameters(DTODishAndHobbyList dishObject, Guid? returnId)
        {
            var dishParameters = new DynamicParameters();
            dishParameters.Add("@DishID", returnId);
            dishParameters.Add("@DishCode", dishObject.Dish.DishCode);
            dishParameters.Add("@DishName", dishObject.Dish.DishName);
            dishParameters.Add("@SalePrice", dishObject.Dish.SalePrice);
            dishParameters.Add("@PrimeCost", dishObject.Dish.PrimeCost);
            dishParameters.Add("@Description", dishObject.Dish.Description);
            dishParameters.Add("@ShowOnMenu", dishObject.Dish.ShowOnMenu);
            dishParameters.Add("@DishTypeID", dishObject.Dish.DishTypeID);
            if (dishObject.Dish.CookPlaceID == Guid.Empty)
            {
                dishParameters.Add("@CookPlaceID", null);
            }
            else
            {
                dishParameters.Add("@CookPlaceID", dishObject.Dish.CookPlaceID);
            }
            dishParameters.Add("@UnitID", dishObject.Dish.UnitID);
            if (dishObject.Dish.CategoryID == Guid.Empty)
            {
                dishParameters.Add("@CategoryID", null);
            }
            else
            {
                dishParameters.Add("@CategoryID", dishObject.Dish.CategoryID);
            }

            return dishParameters;
        }

        /// <summary>
        /// API lay so thich phuc vu theo DishID
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns></returns>
        public List<Hobby> GetHobbiesByDishID(string dishID) 
        {
            var list = new List<Hobby>();
            //Chuan bi store proc
            string storeProcName = String.Format(Procedures.GET_BY_ORTHER_ID, typeof(HobbyServeDish).Name, typeof(Dish).Name);

            //Chuan bi tham so dau vao
            var parameter = new DynamicParameters();
            parameter.Add("@DishID", dishID);

            //Goi vao DB
            using (var mySqlConnection = new MySqlConnection(DatabaseConfig.ConnectionString)) 
            {
                if (mySqlConnection != null && mySqlConnection.State != ConnectionState.Open) 
                {
                    mySqlConnection.Open();
                    list = (List<Hobby>)mySqlConnection.Query<Hobby>(storeProcName, parameter, commandType: CommandType.StoredProcedure);
                    mySqlConnection.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// API xóa mot hoac nhieu bản ghi Dish
        /// Author: Nguyễn Bá Hải
        /// Date:22/12/2022
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        public int DeleteRecord(string[] listID)
        {
            int numberOfAffectedRows = -1;
            //Chuan bi store Proc
            string storeProcName = String.Format(Procedures.DELETE, typeof(Dish).Name);

            //Xu li param
            string listInString = "";
            var formatListString = new List<String>();
            if (listID != null)
            {
                foreach (string id in listID)
                {
                    formatListString.Add($"\"{id}\"");
                }
            }
            if (formatListString.Count > 0)
            {
                listInString = $"{string.Join(", ", formatListString)}";
            }

            //Chuan bi tham so dau vao, va tham so tra ve
            var parameters = new DynamicParameters();
            parameters.Add("@ListID", listInString);

            //Goi vao DB
            using (var mySqlConnection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                if (mySqlConnection != null && mySqlConnection.State != ConnectionState.Open)
                {
                    mySqlConnection.Open();
                    using (MySqlTransaction transaction = mySqlConnection.BeginTransaction())  //Dung transaction
                    {
                        try
                        {
                            //Xoa Hobby theo id
                            foreach (string id in listID)
                            {
                                int affectedRows = DeleteHobbiesByDishID(id);
                                if (affectedRows == -1) 
                                {
                                    transaction.Rollback();
                                }
                            }
                            //Thanh cong xoa het hobby roi xoa DIsh
                            numberOfAffectedRows = mySqlConnection.Execute(storeProcName, parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: transaction);
                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                        }
                    }
                    mySqlConnection.Close();
                }
            }
            return numberOfAffectedRows;
        }

        /// <summary>
        /// Xoa ban ghi so thich phuc vuj theo id
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns></returns>
        public static int DeleteHobbiesByDishID(string dishID) 
        {
            int numberOfAffectedRows = -1;
            string storeProcName = String.Format(Procedures.DELETE_BY_ORTHER_ID, typeof(HobbyServeDish).Name, typeof(Dish).Name);
            var param = new DynamicParameters();
            param.Add("@DishID", dishID);
            using (var mySqlConnection = new MySqlConnection(DatabaseConfig.ConnectionString)) 
            {
                if (mySqlConnection != null && mySqlConnection.State != ConnectionState.Open) 
                {
                    mySqlConnection.Open();
                    using (MySqlTransaction transaction = mySqlConnection.BeginTransaction()) //Bat dau transaction
                    {
                        try
                        {
                            numberOfAffectedRows = mySqlConnection.Execute(storeProcName, param, commandType: CommandType.StoredProcedure, transaction: transaction);
                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                        }
                    }
                    mySqlConnection.Close();
                }
            }
            return numberOfAffectedRows;
        }
    }
}
