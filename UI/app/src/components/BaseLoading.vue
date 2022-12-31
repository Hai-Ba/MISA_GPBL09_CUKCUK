<template>
<div class="cukcuk-loading">
    <div class="cukcuk-loading-container">
        <div class="sprite-loading"></div>
        <div>Đang lấy dữ liệu</div>
    </div>
</div>
</template>

<script>
export default {
    name: 'BaseLoading',
}
</script>

<style lang="css" scoped>
.cukcuk-loading {
    position: absolute;
    z-index: 30;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(10, 10, 10, .4) !important;
}

.cukcuk-loading-container{
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    height: 50px;
    width: 100px;
    display: flex;
    flex-direction: column;
    border-radius: 2px;
    align-items: center;
    justify-content: space-around;
    background-color: #ccc;
}
</style>
