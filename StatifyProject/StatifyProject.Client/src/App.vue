<script setup>
import axios from 'axios';
</script>

<template>
    <div class="bg-dark">
        <nav class="navbar navbar-expand-lg bg-dark text-uppercase fixed-top" id="mainNav">
            <div class="container">
                <a class="navbar-brand text-light h1" href="/"> <h1>Statify</h1></a>
                <p v-if="authenticated">Logged in as {{ username }}. <span class="logout" v-on:click="deleteToken()">Logout</span></p>
                <div class="collapse navbar-collapse" id="navbarResponsive">
                    <ul class="navbar-nav ms-auto">
                        <li class="nav-item mx-0 mx-lg-1">
                            <router-link class="nav-link py-3 px-0 px-lg-3 rounded text-light" to="/song"><h5>Songs</h5></router-link>
                        </li>
                        <li class="nav-item mx-0 mx-lg-1">
                            <router-link class="nav-link py-3 px-0 px-lg-3 rounded text-light" to="/artist"><h5>Artists</h5></router-link>
                        </li>
                        <li v-if="!authenticated" class="nav-item mx-0 mx-lg-1">
                            <router-link class="nav-link py-3 px-0 px-lg-3 rounded text-light" to="/login"><h5>Login</h5></router-link>
                        </li>
                    </ul>
                </div>
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
</style>
<script>
import HomeView from './views/HomeView.vue';
import ArtistView from './views/ArtistView.vue';
import SongView from './views/SongView.vue';

export default {
    name: 'App',
    components: {
        HomeView,
        ArtistView,
        SongView,
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
</script>





