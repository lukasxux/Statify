import { createStore } from 'vuex';
import createPersistedState from 'vuex-persistedstate';

export default createStore({
  state() {
    return {
      user: {
        data: null,
        isLoggedIn: false
      }
    };
  },
  mutations: {
    store(state, data) {
      state.user.data = data;
    },
    setLoggedInStatus(state) {
      state.user.isLoggedIn = true;
    }
  },
  actions: {
    storeData({ commit }, data) {
      commit('store', data);
    },
    setLoggedIn({ commit }) {
      commit('setLoggedInStatus');
    }
  },
  plugins: [createPersistedState()]
});
