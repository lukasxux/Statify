import { createStore } from 'vuex'   // npm install vuex

export default createStore({
    state() {
        return {
            user:{
                name: '',
                guid:'',
                isLoggedIn: false,
            }

        }
    },
    mutations: {
        authenticate(state, userdata) { //selber gemacht
            if (!userdata) {
                state.user = { name: "", guid: "", isLoggedIn: false };
                return;
            }
            state.user.name = userdata.username;
            state.user.guid = userdata.userGuid;
            state.user.isLoggedIn = true;
        }

    }
});
