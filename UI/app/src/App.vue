<template>
  <dish-view></dish-view>
</template>

<script>
import DishView from './views/dish/DishView.vue'

export default {
  name: 'App',
  components: {
    DishView,
  }
}
</script>

<style>
@import url('~/src/style/common.css');
@import url('~/src/style/icon.css');
#app {
  box-sizing: border-box;
}
</style>
