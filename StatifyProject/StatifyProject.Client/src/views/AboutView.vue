<script setup>
import axios from 'axios';
import store from '../store.js';
</script>

<template>
  <div class="StatifyAccountView">
    <div v-if="!$store.state.user.isLoggedIn" class="login-container">
      <h1>Login</h1>
      <form @submit.prevent="loginUser">
        <div class="form-group">
          <label for="email">Email:</label>
          <input type="email" id="email" v-model="model.email" />
        </div>
        <div class="form-group">
          <label for="password">Password:</label>
          <input type="password" id="password" v-model="model.password" />
        </div>
        <button type="submit">Log in</button>
      </form>
    </div>

    <div v-else class="user-info">
      <p>Welcome, {{ accessToken }}!</p>
      <button @click="logoutUser">Log out</button>
    </div>

    <div v-if="!$store.state.user.isLoggedIn" id="or">
      <span><h5 id="or-text">or</h5></span>
    </div>

    <div v-if="!$store.state.user.isLoggedIn" class="registration-container">
      <h1>Register</h1>
      
        <div class="form-group">
          <label for="username">Username:</label>
          <input type="text" id="username" v-model="regmodel.regUsername" />
        </div>
        <div class="form-group">
          <label for="email">Email:</label>
          <input type="email" id="email" v-model="regmodel.regEmail" />
        </div>
        <div class="form-group">
          <label for="password">Password:</label>
          <input type="password" id="password" v-model="regmodel.regPassword" />
        </div>
        <div v-if="!isLinked" class="form-group">
          <label for="password">Link Spotify Account:</label>
          <button style="color: white" @click="linkSpotifyAcc">Click to Link</button>
        </div>
        <div v-else class="form-group">
          <label for="password">Linked Spotify Account:</label>
          <label for="password">Username: {{SpotifyAccount.display_name}}</label>
        </div>
        <button @click="registerUser" type="submit">Register</button>
      
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      regmodel: {
        regusername: '',
        regemail: '',
        regPassword: '',
      },
      model: {
        email: '',
        password: '',
      },
      userGuid: {},
      SpotifyAccount: {},
      isLinked: false,
      UseraccessToken: '',
      UserrefreshToken: ''

    };
  },
  methods: {
      async registerUser() {
       try {
        const response = await axios.post('https://localhost:5001/api/users', {
          username: this.regmodel.regUsername,
          email: this.regmodel.regEmail,
          password: this.regmodel.regPassword,
          accessToken: this.UseraccessToken,
          refreshToken: this.UserrefreshToken,
          Bio: "This is my bio"
        });
        alert('Registration successful!');
      } catch (error) {
        console.error(error);
        alert('Registration failed.');
      }
    },
    async UpdateUserData(){
      const response3 = await axios.get("https://localhost:5001/api/users/"+this.userGuid);
      console.log(response3.data);
      localStorage.setItem('access_token', response3.data.accessToken);
      localStorage.setItem('refresh_token', response3.data.refreshToken);
      this.user = response3.data; 
    },
    async loginUser() {
      try {
        const response2 = await axios.post('https://localhost:5001/api/users/login', {
          email: this.model.email,
          password: this.model.password,
        });
        this.$store.commit('authenticate', response2.data);
        this.userGuid = response2.data.userGuid;
        console.log(response2);
        await this.UpdateUserData();
        this.loggedIn = true;
        alert('Login successful!');
      } catch (error) {
        console.error(error);
        alert('Login failed.');
      }
    },
    logoutUser() {
      this.$store.state.user.isLoggedIn = false;
      this.$store.state.user.userGuid = "";
      this.$store.state.user.username = "";
      this.username = "";
    },
    linkSpotifyAcc(){
      this.fetchProfileData();
    },
    
    //------------------------------------Spotify------------------------------------
        // Update the data properties 
        async fetchProfileData() {
          const clientId = 'afc8cff8760e496a82a85b2cf42ff99b'; 
          const params = new URLSearchParams(window.location.search);
          const code = params.get('code');
          if (!code) {
            this.redirectToAuthCodeFlow(clientId);
          } else {
            const accessToken = await this.getAccessToken(clientId, code);
            this.isLinked = true;
          } 
        },
        //---------------------------------Authentication---------------------------------
        async redirectToAuthCodeFlow(clientId) {
          const verifier = this.generateCodeVerifier(128);
          const challenge = await this.generateCodeChallenge(verifier);
  
          localStorage.setItem('verifier', verifier);
  
          const params = new URLSearchParams();
          params.append('client_id', clientId);
          params.append('response_type', 'code');
          params.append('redirect_uri', 'http://localhost:5173/about');
          params.append('scope', 'user-read-private user-read-email user-library-read playlist-read-private playlist-read-collaborative user-follow-read user-follow-modify user-top-read');
          params.append('code_challenge_method', 'S256');
          params.append('code_challenge', challenge);
  
          document.location = `https://accounts.spotify.com/authorize?${params.toString()}`;
        },
        async getAccessToken(clientId, code) {
          const verifier = localStorage.getItem('verifier');

          const params = new URLSearchParams();
          params.append('client_id', clientId);
          params.append('grant_type', 'authorization_code');
          params.append('code', code);
          params.append('redirect_uri', 'http://localhost:5173/about');
          params.append('code_verifier', verifier);
          params.append('scope', 'user-read-private user-read-email user-library-read playlist-read-private playlist-read-collaborative user-follow-read user-follow-modify user-top-read');

          const result = await fetch('https://accounts.spotify.com/api/token', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/x-www-form-urlencoded'
            },
            body: params,
          });

          const { access_token, refresh_token } = await result.json();
          this.UseraccessToken = access_token;
          this.UserrefreshToken = refresh_token;
          return access_token;
        },
        generateCodeVerifier(length) {
          let text = '';
          let possible = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
  
          for (let i = 0; i < length; i++) {
            text += possible.charAt(Math.floor(Math.random() * possible.length));
          }
  
          return text;
        },
        async generateCodeChallenge(verifier) {
          const hashedVerifier = await this.sha256(verifier);
          const base64Url = btoa(String.fromCharCode(...new Uint8Array(hashedVerifier)))
            .replace('+', '-')
            .replace('/', '_')
            .replace(/=+$/, '');
  
          return base64Url;
        },
        async sha256(plain) {
          const encoder = new TextEncoder();
          const data = encoder.encode(plain);
          const hashBuffer = await crypto.subtle.digest('SHA-256', data);
          return hashBuffer;
        },
  
       
        //---------------------------------Statify---------------------------------
      },
   } ;

</script>

<style>
/*OR THING CSS----------------------------*/
#or-text{
  font-weight: bold;
  font-size: 18px;
  color: #ffffff94;
  text-transform: uppercase;
}
#or {
  display: flex;
  align-items: center;
  text-align: center;
  width: 100%;
  margin: 20px 0;
}

#or::before,
#or::after {
  content: "";
  flex: 1;
  border-bottom: 1px solid #ffffff94;
  margin: 0 10px;
}

#or span {
  font-weight: bold;
  font-size: 18px;
  color: #666;
  text-transform: uppercase;
  margin: 0 10px;
}

/*Login CSS----------------------------*/
.login-container {
  max-width: 400px;
  margin: auto;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.3);
  display: flex;
  flex-direction: column;
  align-items: center;
}

h1 {
  text-align: center;
  margin-bottom: 20px;
}

.form-group {
  margin-bottom: 20px;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}

label {
  margin-bottom: 5px;
  font-weight: bold;
}

input[type="email"],
input[type="password"] {
  padding: 10px;
  font-size: 16px;
  border-radius: 5px;
  border: 1px solid #ccc;
  width: 100%;
  box-sizing: border-box;
}

button[type="submit"] {
  padding: 10px;
  font-size: 16px;
  background-color: #1ed760;
  color: #fff;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}
/*Register CSS----------------------------*/
#spotify-link-button{
  width: 200px;
  height: 40px;

}


.registration-container {
  max-width: 400px;
  margin: auto;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.3);
  display: flex;
  flex-direction: column;
  align-items: center;
}

h1 {
  text-align: center;
  margin-bottom: 20px;
}

.form-group {
  margin-bottom: 20px;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}

label {
  margin-bottom: 5px;
  font-weight: bold;
}

input[type="text"],
input[type="email"],
input[type="password"] {
  padding: 10px;
  font-size: 16px;
  border-radius: 5px;
  border: 1px solid #ccc;
  width: 100%;
  box-sizing: border-box;
}

button[type="submit"] {
  padding: 10px;
  font-size: 16px;
  background-color: #1ed760;
  color: #fff;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}


</style>