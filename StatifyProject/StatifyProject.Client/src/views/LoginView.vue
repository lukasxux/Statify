

<template>
    <div class="button-card" @click="fetchProfileData">
        <a href="#" class="spotify-button">
            <span class="spotify-icon"></span>
            <span class="spotify-text">Login using Spotify</span>
          </a>
    </div>

</template>

<style scoped>

.button-card {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 80vh; /* Set the height of the container to make it center vertically */
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
      Userdata: [],
      topTracks: [],
      topArtists: []

    };
  },
  mounted() {
        
  },
  methods: {
 // Update the data properties 
 async fetchProfileData() {
         const clientId = 'afc8cff8760e496a82a85b2cf42ff99b';
         const params = new URLSearchParams(window.location.search);
         const code = params.get('code');
         if (!code) {
             this.redirectToAuthCodeFlow(clientId);
         } else {
             const accessToken = await this.getAccessToken(clientId, code);
             const profile = await this.fetchProfile(accessToken);
             const topTracks = await this.fetchTopTracks(accessToken);
             const topArist = await this.fetchTopArtist(accessToken);
             this.updateProfilInfo(profile);
             console.log("Hello");
             this.updateTopTracks(topTracks);
             this.updateTopArtist(topArist);
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

                     //---------------------------------User Data---------------------------------
                     async fetchProfile(accessToken) {
                             const result = await fetch('https://api.spotify.com/v1/me', {
                                 headers: {
                                     Authorization: `Bearer ${accessToken}`
                                 },
                             });

                             const profile = await result.json();
                             console.log(profile)
                             sessionStorage.setItem('profile', JSON.stringify(profile));
                             return profile;
                         },

                         updateProfilInfo(profile) {
                                this.profile = profile.items.map((item) => item.display_name);
                         },

                         //---------------------------------Tracks---------------------------------
                         async fetchTopTracks(accessToken) {
                                 const result = await fetch('https://api.spotify.com/v1/me/top/tracks?time_range=short_term&limit=10', {
                                     headers: {
                                         Authorization: `Bearer ${accessToken}`
                                     },
                                 });
                                 const topTracks = await result.json();

                                

                                 sessionStorage.setItem('topTracks', JSON.stringify(topTracks));

                                 return topTracks;

                             },

                             updateTopTracks(topTracks) {
                                 this.topTracks = topTracks.items.map((item) => item.name);
                                 console.log(this.topTracks)
                             },
                             //---------------------------------Artist---------------------------------
                             async fetchTopArtist(accessToken) {
                                     const result = await fetch('https://api.spotify.com/v1/me/top/artists?time_range=short_term&limit=10', {
                                         headers: {
                                             Authorization: `Bearer ${accessToken}`
                                         },
                                     });

                                     const topArtists = await result.json();
                                     console.log("------topArtists------")
                                     sessionStorage.setItem('topArtists', JSON.stringify(topArtists));
                                     return topArtists;
                                 },

                                 updateTopArtist(topArtists) {
                                     
                                     this.topArtists = topArtists.items.map((item) => item.name);
                                     console.log(this.topArtists)
                                     


                                 },




 },
 };
</script>
