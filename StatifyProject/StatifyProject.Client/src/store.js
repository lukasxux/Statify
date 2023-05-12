import { createStore } from 'vuex'   // npm install vuex

export default createStore({
    state() {
        return {
            user:{
                name: '',
                guid:'',
                isLoggedIn: false,
                spotifyToken: '',
            }
        }
    },
    mutations: {
        authenticate(state, userdata) { 
            if (!userdata) {
                state.user = { name: "", guid: "", isLoggedIn: false, spotifyToken: "" };
                return;
            }
            state.user.name = userdata.username;
            state.user.guid = userdata.userGuid;
            state.user.isLoggedIn = true;
            state.user.spotifyToken = userdata.spotifyToken;
        }
    }
});
