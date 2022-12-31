using MISA.CUKCUK.API.Common.Entities;
using MISA.CUKCUK.API.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using System.Data;

namespace MISA.CUKCUK.DL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        /// <summary>
        /// Ham check trung ma
        /// Nguyen Ba Hai - 26/12/2022
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int CheckDuplicateCode(string code)
        {
            //Chuan bi store Proc
            string storeProcName = String.Format(Procedures.CHECK_DUPLICATE, typeof(Dish).Name);

            int duplicateRow = 0;

            //Chuan bi tham so dau vao, va tham so tra ve
            var parameters = new DynamicParameters();
            parameters.Add($"@{typeof(T).Name}Code", code);

            //Goi vao DB
            using (var mySqlConnection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                if (mySqlConnection != null && mySqlConnection.State != ConnectionState.Open)
                {
                    mySqlConnection.Open();
                    duplicateRow = mySqlConnection.QueryFirstOrDefault<int>(storeProcName, parameters, commandType: CommandType.StoredProcedure);
                    mySqlConnection.Close();
                }
            }
            return duplicateRow; 
        }

        /// <summary>
        /// Lấy thông tin 1 bản ghi theo id
        /// Author: Nguyễn Bá Hải
        /// Date: 22/12/2022
        /// </summary>
        /// <param name="recordID">ID bản ghi muốn lấy</param>
        /// <returns>Thông tin 1 bản ghi</returns>
        public T GetRecordByID(Guid? recordID)
        {
            //Chuan bi Store Proc
            string storeProcName = String.Format(Procedures.GET_BY_ID, typeof(T).Name);

            //Chuan bi tham so
            var parameters = new DynamicParameters();
            parameters.Add($"@{typeof(T).Name}ID", recordID);

            //Thực hiện gọi vào DB muộn nhất và hủy gọi vào sớm nhất
            using (var mySqlConnection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                if (mySqlConnection != null && mySqlConnection.State != ConnectionState.Open) 
                {
                    mySqlConnection.Open();
                }
                T records = mySqlConnection.QueryFirstOrDefault<T>(storeProcName, parameters, commandType: CommandType.StoredProcedure);
                if (mySqlConnection != null &&  mySqlConnection.State == ConnectionState.Open) 
                {
                    mySqlConnection.Close();
                }
                return records;
            }
        }
    }
}
