<script setup>
import axios from "axios";
</script>

<template>
  <br />
  <br />

  <div class="container">
    <div class="row">
      <div class="col">
        <img
          class="img-statifyLogo"
          src="../assets/img/StatifyLogo.png"
          alt=""
        />
      </div>
      <div id="welcome" class="col d-flex align-items-center">
        <div>
          <h1>Welcome to Statify</h1>
          <h5>
            Discover the beat of your musical journey with our Spotify API
            integration. Explore your top songs, favorite artists, and other
            users' musical tastes with ease!
          </h5>
        </div>
      </div>
    </div>
  </div>
  <br />
  <br />
  <br />
  <br />
  <div class="container" id="container">
    <div class="form-container sign-up-container">
      <form @submit.prevent="loginUser">
        <h1 id="sign-account">Sign in</h1>
        <span>or register with us</span>
        <input type="email" placeholder="Email" v-model="model.email" />
        <input
          type="password"
          placeholder="Password"
          v-model="model.password"
        />
        <button type="submit">Sign In</button>
      </form>
    </div>
    <div class="form-container sign-in-container">
      <form action="#">
        <h1 id="create-account">Create Account</h1>
        <span>or Sign in</span>
        <input
          type="text"
          placeholder="Username"
          v-model="regmodel.regUsername"
        />
        <input type="email" placeholder="Email" v-model="regmodel.regEmail" />
        <input
          type="password"
          placeholder="Password"
          v-model="regmodel.regPassword"
        />
        <input
          @click="linkSpotifyAcc"
          type="button"
          value="Link your Spotfiy Account"
          v-if="!isLinked"
          style="
            border-radius: 5px;
            display: flex;
            justify-content: left;
            color: ##9eb6ce;
          "
        />
        <input
          @click="linkSpotifyAcc"
          type="button"
          value="Spotify Account Linked!"
          v-else
          style="
            border-radius: 5px;
            display: flex;
            justify-content: left;
            color: #222;
          "
          disabled
        />
        <button @click="registerUser">Sign Up</button>
      </form>
    </div>
    <div class="overlay-container">
      <div class="overlay">
        <div class="overlay-panel overlay-left">
          <h1>Welcome Back!</h1>
          <p>To keep connected with us please login with your personal info</p>
          <button class="ghost" id="signIn">Sign Up</button>
        </div>
        <div class="overlay-panel overlay-right">
          <h1>Hello, Friend!</h1>
          <p>Enter your personal details and start journey with us</p>
          <button class="ghost" id="signUp">Sign in</button>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.img-statifyLogo {
  width: 400px;
}

#forgot-password {
  color: white;
}

#create-account,
#sign-account {
  color: #ffffff;
}

@import url("https://fonts.googleapis.com/css?family=Montserrat:400,800");

* {
  box-sizing: border-box;
}

body {
  background: #f6f5f7;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
  font-family: "Montserrat", sans-serif;
  height: 100vh;
  margin: -20px 0 50px;
}

h1 {
  font-weight: bold;
  margin: 0;
}

h2 {
  text-align: center;
}

p {
  font-size: 14px;
  font-weight: 100;
  line-height: 20px;
  letter-spacing: 0.5px;
  margin: 20px 0 30px;
}

span {
  font-size: 12px;
}

a {
  color: #333;
  font-size: 14px;
  text-decoration: none;
  margin: 15px 0;
}

button {
  border-radius: 20px;
  border: 1px solid #1ed760;
  background-color: #1ed760;
  color: #222;
  font-size: 12px;
  font-weight: bold;
  padding: 12px 45px;
  letter-spacing: 1px;
  text-transform: uppercase;
  transition: transform 80ms ease-in;
}

button:active {
  transform: scale(0.95);
}

button:focus {
  outline: none;
}

button.ghost {
  background-color: transparent;
  border-color: #1ed760;
  background-color: #1ed760;
}

form {
  background-color: #222;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  padding: 0 50px;
  height: 100%;
  text-align: center;
}

input {
  background-color: #eee;
  border: none;
  padding: 12px 15px;
  margin: 8px 0;
  width: 100%;
}

.container {
  background-color: #1ed760;
  border-radius: 10px;
  box-shadow: 0 14px 28px rgba(0, 0, 0, 0.25), 0 10px 10px rgba(0, 0, 0, 0.22);
  position: relative;
  overflow: hidden;
  width: 760px;
  max-width: 100%;
  min-height: 480px;
}

.form-container {
  position: absolute;
  top: 0;
  height: 100%;
  transition: all 0.6s ease-in-out;
}

.sign-in-container {
  left: 0;
  width: 50%;
  z-index: 2;
}

.container.right-panel-active .sign-in-container {
  transform: translateX(100%);
}

.sign-up-container {
  left: 0;
  width: 50%;
  opacity: 0;
  z-index: 1;
}

.container.right-panel-active .sign-up-container {
  transform: translateX(100%);
  opacity: 1;
  z-index: 5;
  animation: show 0.6s;
}

@keyframes show {
  0%,
  49.99% {
    opacity: 0;
    z-index: 1;
  }

  50%,
  100% {
    opacity: 1;
    z-index: 5;
  }
}

.overlay-container {
  position: absolute;
  top: 0;
  left: 50%;
  width: 50%;
  height: 100%;
  overflow: hidden;
  transition: transform 0.6s ease-in-out;
  z-index: 100;
}

.container.right-panel-active .overlay-container {
  transform: translateX(-100%);
}

.overlay {
  background: #222;
  background-repeat: no-repeat;
  background-size: cover;
  background-position: 0 0;
  color: #ffffff;
  position: relative;
  left: -100%;
  height: 100%;
  width: 200%;
  transform: translateX(0);
  transition: transform 0.6s ease-in-out;
}

.container.right-panel-active .overlay {
  transform: translateX(50%);
}

.overlay-panel {
  position: absolute;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  padding: 0 40px;
  text-align: center;
  top: 0;
  height: 100%;
  width: 50%;
  transform: translateX(0);
  transition: transform 0.6s ease-in-out;
}

.overlay-left {
  transform: translateX(-20%);
}

.container.right-panel-active .overlay-left {
  transform: translateX(0);
}

.overlay-right {
  right: 0;
  transform: translateX(0);
}

.container.right-panel-active .overlay-right {
  transform: translateX(20%);
}

.social-container {
  margin: 20px 0;
}

.social-container a {
  border: 1px solid #1ed760;
  border-radius: 50%;
  display: inline-flex;
  justify-content: center;
  align-items: center;
  margin: 0 5px;
  height: 40px;
  width: 40px;
}
</style>

<script>
export default {
  data() {
    return {
      regmodel: {
        regusername: "",
        regemail: "",
        regPassword: "",
      },
      model: {
        email: "",
        password: "",
      },
      userGuid: {},
      SpotifyAccount: {},
      isLinked: false,
      UseraccessToken: "",
      UserrefreshToken: "",
    };
  },
  mounted() {
    this.message = "";
    const signUpButton = document.getElementById("signUp");
    const signInButton = document.getElementById("signIn");
    const container = document.getElementById("container");

    signUpButton.addEventListener("click", () => {
      container.classList.add("right-panel-active");
    });

    signInButton.addEventListener("click", () => {
      container.classList.remove("right-panel-active");
    });
  },
  methods: {
    async registerUser() {
      try {
        const response = await axios.post("/users", {
          username: this.regmodel.regUsername,
          email: this.regmodel.regEmail,
          password: this.regmodel.regPassword,
          accessToken: this.UseraccessToken,
          refreshToken: this.UserrefreshToken,
          Bio: "This is my bio",
        });
        alert("Registration successful!");
      } catch (error) {
        console.error(error);
        alert("Registration failed.");
      }
    },
    async UpdateUserData() {
      const response3 = await axios.get("/users/" + this.userGuid);
      this.user = response3.data;
    },
    async loginUser() {
      try {
        const response2 = await axios.post("/users/login", {
          email: this.model.email,
          password: this.model.password,
        });
        this.userGuid = response2.data.userGuid;
        localStorage.setItem("access_token", response2.data.accessToken);
        localStorage.setItem("refresh_token", response2.data.refreshToken);
        await this.UpdateUserData();
        this.$store.dispatch("storeData", response2.data);
        console.log();
        this.$store.dispatch("setLoggedIn").then(() => {
          const isLoggedIn = this.$store.state.user.isLoggedIn;
          console.log("setLoggedIn:", isLoggedIn);
        });

        alert("Login successful!");
      } catch (error) {
        console.error(error);
        alert("Login failed.");
      }
    },
    logoutUser() {
      this.$store.state.user.isLoggedIn = false;
      this.$store.state.user.userGuid = "";
      this.$store.state.user.username = "";
      this.username = "";
    },
    linkSpotifyAcc() {
      this.fetchProfileData();
    },

    //------------------------------------Spotify------------------------------------

    // Update the data properties
    async fetchProfileData() {
      const clientId = "afc8cff8760e496a82a85b2cf42ff99b";
      const params = new URLSearchParams(window.location.search);
      const code = params.get("code");
      if (!code) {
        this.redirectToAuthCodeFlow(clientId);
      } else {
        const accessToken = await this.getAccessToken(clientId, code);
        console.log(accessToken);
        this.isLinked = true;
      }
    },
    //---------------------------------Authentication---------------------------------
    async redirectToAuthCodeFlow(clientId) {
      const verifier = this.generateCodeVerifier(128);
      const challenge = await this.generateCodeChallenge(verifier);

      localStorage.setItem("verifier", verifier);

      const params = new URLSearchParams();
      params.append("client_id", clientId);
      params.append("response_type", "code");
      params.append(
        "redirect_uri",
        "https://statify-hyvzewj0usmbnyiu.azurewebsites.net"
      );
      params.append(
        "scope",
        "user-read-private user-read-email user-library-read playlist-read-private playlist-read-collaborative user-follow-read user-follow-modify user-top-read"
      );
      params.append("code_challenge_method", "S256");
      params.append("code_challenge", challenge);

      document.location = `https://accounts.spotify.com/authorize?${params.toString()}`;
    },
    async getAccessToken(clientId, code) {
      const verifier = localStorage.getItem("verifier");

      const params = new URLSearchParams();
      params.append("client_id", clientId);
      params.append("grant_type", "authorization_code");
      params.append("code", code);
      params.append(
        "redirect_uri",
        "https://statify-hyvzewj0usmbnyiu.azurewebsites.net"
      );
      params.append("code_verifier", verifier);
      params.append(
        "scope",
        "user-read-private user-read-email user-library-read playlist-read-private playlist-read-collaborative user-follow-read user-follow-modify user-top-read"
      );

      const result = await fetch("https://accounts.spotify.com/api/token", {
        method: "POST",
        headers: {
          "Content-Type": "application/x-www-form-urlencoded",
        },
        body: params,
      });

      const { access_token, refresh_token } = await result.json();
      this.UseraccessToken = access_token;
      this.UserrefreshToken = refresh_token;
      return access_token;
    },
    generateCodeVerifier(length) {
      let text = "";
      let possible =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

      for (let i = 0; i < length; i++) {
        text += possible.charAt(Math.floor(Math.random() * possible.length));
      }

      return text;
    },
    async generateCodeChallenge(verifier) {
      const hashedVerifier = await this.sha256(verifier);
      const base64Url = btoa(
        String.fromCharCode(...new Uint8Array(hashedVerifier))
      )
        .replace("+", "-")
        .replace("/", "_")
        .replace(/=+$/, "");

      return base64Url;
    },
    async sha256(plain) {
      const encoder = new TextEncoder();
      const data = encoder.encode(plain);
      const hashBuffer = await crypto.subtle.digest("SHA-256", data);
      return hashBuffer;
    },

    //---------------------------------Statify---------------------------------
  },
};
</script>
