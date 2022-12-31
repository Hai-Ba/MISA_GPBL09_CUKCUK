// import Vue from 'vue';
// import Vuex from 'vuex';

// import dish from './modules/dish';

// Vue.use(Vuex);

// export default new Vuex.Store({
//     modules: {
//         dish,
//     },
// });

import { createStore } from "vuex";

import dish from './modules/dish';

export const store = createStore({
    modules: {
        dish,
    },
});