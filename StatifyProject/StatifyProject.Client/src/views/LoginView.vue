<script setup>
import axios from 'axios';
</script>

<template>
    <div class="loginView container">
        <template v-if="!authenticated">
            <div class="loginForm">
                <div>Username:</div>
                <div class="mb-2"><input type="text" v-model="model.username"/></div>
                <div>Password:</div>
                <div class="mb-2"><input type="password" v-model="model.password" /></div>
            </div>
                <button class="btn btn-primary" type="button" v-on:click="sendLoginData">Submit</button>
            <p>Hint: Username matrix, Passwort 1111</p>
        </template>
        <template v-else>
            <div>Logged in as {{ username }}</div>
            <div><a href="javascript:void(0)" v-on:click="deleteToken">Abmelden</a></div>
        </template>
        <div v-if="message">
            {{ message }}
        </div>
    </div>
</template>

<style scoped>
.loginForm {

}
</style>
<script>
export default {
    data() {
        return {
            model: {
                username: 'matrix',
                password: '1111',
            },
            message: ""
        };
    },
    methods: {
        deleteToken() {
            delete axios.defaults.headers.common['Authorization'];
            this.$store.commit('authenticate', null);
        },
        async sendLoginData() {
            this.message = "";
            try {
                const userdata = (await axios.post('user/login', this.model)).data;
                axios.defaults.headers.common['Authorization'] = `Bearer ${userdata.token}`;
                this.$store.commit('authenticate', userdata);
            } catch (e) {
                if (e.response.status == 401) {
                    this.message = "Login failed. Invalid credentials.";
                }
            }
        },
    },
    computed: {
        authenticated() {
            return this.$store.state.user != null;
        },
        username() {
            return this.$store.state.user.username;
        },
    },
};
</script>
