<script setup>
import axios from 'axios';
</script>

<template>
    <div class="bg-dark">
   <nav class="navbar navbar-expand-lg bg-dark text-uppercase fixed-top">
      <a class="navbar-brand text-light h1" id="logo" href="/">
         <h1><span style="color: #1db954;">S</span><span style="color: #1db954;">t</span><span style="color: #1db954;">a</span><span style="color: #1db954;">t</span>ify</h1>
      </a>
      <div id="navbarCollapse">
         <button class="navbar-toggler"  type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
         <span class="navbar-toggler-icon" id="navbarIcon"></span>
         </button>
      </div>
      <div class="collapse navbar-collapse" id="navbarNav">
         <ul class="navbar-nav ml-auto">
            <li  class="nav-item mx-0 mx-lg-1" id="padding-left">
               <router-link class="nav-link py-3 px-0 px-lg-3 rounded text-light"  to="/other-users">
                  <h5><span style="color: #1db954;">other</span> Users</h5>
               </router-link>
            </li>
            <li  class="nav-item mx-0 mx-lg-1" id="padding-left">
               <router-link class="nav-link py-3 px-0 px-lg-3 rounded text-light"  to="/song">
                  <h5><span style="color: #1db954;">Top</span> Songs</h5>
               </router-link>
            </li>
            <li  class="nav-item mx-0 mx-lg-1" id="padding-left">
               <router-link class="nav-link py-3 px-0 px-lg-3 rounded text-light" to="/artist">
                  <h5><span style="color: #1db954;">Top</span> Artists</h5>
               </router-link>
            </li>
            <li  class="nav-item mx-0 mx-lg-1" id="padding-left">
               <router-link class="nav-link py-3 px-0 px-lg-3 rounded text-light" to="/my-account">
                  <h5><span style="color: #1db954;">My</span> Account</h5>
               </router-link>
            </li>
            <li class="nav-item mx-0 mx-lg-1" id="login">
               <router-link class="nav-link py-3 px-0 px-lg-3 rounded text-light" to="/login">
                  <h5>Login</h5>
               </router-link>
            </li>
         </ul>
      </div>
   </nav>
   <main class="container">
      <router-view></router-view>
   </main>
</div>
</template>

<style>
main {
    margin-top: 6em;
}
</style>
<style scoped>
.logout {
    opacity: 0.5;
    cursor: pointer;
}

.navbar-toggler-icon {
  background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 30 30'%3e%3cpath stroke='white' stroke-linecap='round' stroke-miterlimit='10' stroke-width='2' d='M4 7h22M4 15h22M4 23h22'/%3e%3c/svg%3e");
}

.navbar-toggler {
    border-color: white;
}

#padding-left {

    padding-left: 30px;
}

#navbarCollapse {
   padding-right: 20px;

}

#logo {
   padding-left: 30px;
}

#navbarIcon {
   border-color: white;
}

#login {
   padding-left: 30px; 
   padding-right: 20px;
}
</style>
<script>
import HomeView from './views/HomeView.vue';
import ArtistView from './views/ArtistVIew.vue';
import SongView from './views/SongView.vue';
import MyAccountView from './views/MyAccountView.vue';

export default {
    name: 'App',
    components: {
        HomeView,
        ArtistView,
        SongView,
        MyAccountView
    },
    methods: {
        deleteToken() {
            delete axios.defaults.headers.common['Authorization'];
            this.$store.commit('authenticate', null);
        },
    },
    computed: {
        authenticated() {
            return this.$store.state.user != null;
        },
        username() {
            return this.$store.state.user?.username;
        },
    },
};
$(function(){ 
     var navMain = $("#navbarNav");
     navMain.on("click", "a", null, function () {
         navMain.collapse('hide');
     });
 });
</script>





