<script setup>
import axios from 'axios';
</script>

<template>
  <div class="button-container">
    <div class="button-card" @click="fetchProfileData">
      <a href="#" class="spotify-button">
        <span class="spotify-icon"></span>
        <span class="spotify-text">Login using Spotify</span>
      </a>
    </div>
    <div id="or">
      <span><h5 id="or-text">or</h5></span>
    </div>
    <div class="button-card" @click="StatifyLogin">
      <router-link to="/about" class="spotify-button">
        <span class="spotify-icon"></span>
        <span class="spotify-text">Login using Statify</span>
      </router-link>
    </div>
  </div>
  </template>
  
  <style scoped>
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
  .button-container {
    display: flex;
    flex-direction: column;
    align-items: center; /* Center items horizontally */
    justify-content: center; /* Center items vertically */
    height: 80vh; /* Set the height of the container to make it center vertically */
  }
  
  .statify-icon {
    display: inline-block;
    width: 40px;
    height: 40px;
    background-image: url(../assets/img/StatifyLogo.png); /* Replace with your own Statify icon */
    background-size: cover;
    margin-right: 10px;
    vertical-align: middle;
  }
  
  .button-card {
    margin: 30px; /* Add margin-bottom to create space between the buttons */
  }
  
  .spotify-button {
    display: inline-block;
    background-color: #1db954;
    color: #fff;
    padding: 12px 24px;
    font-size: 18px;
    font-weight: bold;
    text-decoration: none;
    border-radius: 30px;
    transition: background-color 0.3s ease;
  }
  
  .spotify-button:hover {
    background-color: #1ed760;
  }
  
  .spotify-icon {
    display: inline-block;
    width: 40px;
    height: 40px;
    background-image: url('https://www.freepnglogos.com/uploads/spotify-logo-png/image-gallery-spotify-logo-21.png'); /* Replace with your own Spotify icon */
    background-size: cover;
    margin-right: 10px;
    vertical-align: middle;
  }
  
  .spotify-text {
    vertical-align: middle;
  }
  </style>
  

  <script>
    export default {
      data() {
        return {
          model:{
            username: '',
            password: ''
          },

          Userdata: [],
          topTracks: [],
          topArtists: []  
        };
      },
      mounted() {
        this.message = "";
      },
      methods: {
        sendLoginData(){
          
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
          params.append('redirect_uri', 'http://localhost:5173/login');
          params.append('scope', 'user-read-private user-read-email user-top-read');
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
          params.append('redirect_uri', 'http://localhost:5173/login');
          params.append('code_verifier', verifier);
          const result = await fetch('https://accounts.spotify.com/api/token', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/x-www-form-urlencoded'
            },
            body: params,
          });
          const {
            access_token
          } = await result.json();
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

      },
    };
</script>