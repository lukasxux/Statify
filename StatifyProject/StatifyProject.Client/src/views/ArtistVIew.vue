

<template>
    <div class="container">
        <div class="row">
           <div class="col-sm-6 col-md-6 col-lg-3 d-flex justify-content-center align-items-center">
              <div class="song-card ">
                 <img :src=topAritstsPic[0] alt="Song Picture">
                 <div class="song-details">
                    <h3 class="song-name">{{topArtists[0]}}</h3>
                    <p class="artist">Artist</p>
                 </div>
              </div>
           </div>
           <div class="col-sm-6 col-md-6 col-lg-3 d-flex justify-content-center align-items-center">
              <div class="song-card">
                 <img :src=topAritstsPic[1] alt="Song Picture">
                 <div class="song-details">
                    <h3 class="song-name">{{topArtists[1]}}</h3>
                    <p class="artist">Artist</p>
                 </div>
              </div>
           </div>
           <div class="col-sm-6 col-md-6 col-lg-3 d-flex justify-content-center align-items-center">
            <div class="song-card">
               <img :src=topAritstsPic[2] alt="Song Picture">
               <div class="song-details">
                  <h3 class="song-name">{{topArtists[2]}}</h3>
                  <p class="artist">Artist</p>
               </div>
            </div>
         </div>
         <div class="col-sm-6 col-md-6 col-lg-3 d-flex justify-content-center align-items-center">
            <div class="song-card">
               <img :src=topAritstsPic[3] alt="Song Picture">
               <div class="song-details">
                  <h3 class="song-name">{{topArtists[3]}}</h3>
                  <p class="artist">Artist</p>
               </div>
            </div>
         </div>
        </div>
     </div>
     <div class="container">
      <div class="row">
         <div class="col-sm-6 col-md-6 col-lg-3 d-flex justify-content-center align-items-center">
            <div class="song-card ">
               <img :src=topAritstsPic[4] alt="Song Picture">
               <div class="song-details">
                  <h3 class="song-name">{{topArtists[4]}}</h3>
                  <p class="artist">Artist</p>
               </div>
            </div>
         </div>
         <div class="ccol-sm-6 col-md-6 col-lg-3 d-flex justify-content-center align-items-center">
            <div class="song-card">
               <img :src=topAritstsPic[5] alt="Song Picture">
               <div class="song-details">
                  <h3 class="song-name">{{topArtists[5]}}</h3>
                  <p class="artist">Artist</p>
               </div>
            </div>
         </div>
         <div class="col-sm-6 col-md-6 col-lg-3 d-flex justify-content-center align-items-center">
          <div class="song-card">
             <img :src=topAritstsPic[6] alt="Song Picture">
             <div class="song-details">
                <h3 class="song-name">{{topArtists[6]}}</h3>
                <p class="artist">Artist</p>
             </div>
          </div>
       </div>
       <div class="col-sm-6 col-md-6 col-lg-3 d-flex justify-content-center align-items-center">
          <div class="song-card">
             <img :src=topAritstsPic[7] alt="Song Picture">
             <div class="song-details">
                <h3 class="song-name">{{topArtists[7]}}</h3>
                <p class="artist">Artist</p>
             </div>
          </div>
       </div>
      </div>
   </div>
   <div class="container">
    <div class="row">
       <div class="col-sm-6 col-md-6 col-lg-6 d-flex justify-content-center align-items-center">
          <div class="song-card ">
             <img :src=topAritstsPic[8] alt="Song Picture">
             <div class="song-details">
                <h3 class="song-name">{{topArtists[8]}}</h3>
                <p class="artist">Artist</p>
             </div>
          </div>
       </div>
       <div class="col-sm-6 col-md-6 col-lg-6 d-flex justify-content-center align-items-center">
          <div class="song-card">
             <img :src=topAritstsPic[9] alt="Song Picture">
             <div class="song-details">
                <h3 class="song-name">{{topArtists[9]}}</h3>
                <p class="artist">Artist</p>
             </div>
          </div>
       </div>
     </div>
    </div>


    


      
</template>

<style scoped>

img{
    height: 15rem;
    margin-top: 1rem;
}

.song-card{
border : 5px solid white;
width: 18rem;
margin-bottom: 0.5rem;
}

.artist{
    color: white;
}


</style>

<script>
export default {
  data() {
    return {
      topArtists: [], // Initialize an empty array to store the top tracks
      topAritstsPic: []
    };
  },
  mounted() {
    // Fetch the user's profile data from Spotify API and update the data properties
    this.fetchProfileData();

  },
  methods: {
    async fetchProfileData() {
      const clientId = 'afc8cff8760e496a82a85b2cf42ff99b';
      const params = new URLSearchParams(window.location.search);
      const code = params.get('code');
      if (!code) {
        this.redirectToAuthCodeFlow(clientId);
      } else {
        const accessToken = await this.getAccessToken(clientId, code);
        const profile = await this.fetchTopArtist(accessToken);
        this.populateUI(profile);
      }
    },
    async redirectToAuthCodeFlow(clientId) {
      const verifier = this.generateCodeVerifier(128);
      const challenge = await this.generateCodeChallenge(verifier);

      localStorage.setItem('verifier', verifier);

      const params = new URLSearchParams();
      params.append('client_id', clientId);
      params.append('response_type', 'code');
      params.append('redirect_uri', 'http://localhost:5173/artist');
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
      params.append('redirect_uri', 'http://localhost:5173/artist');
      params.append('code_verifier', verifier);

      const result = await fetch('https://accounts.spotify.com/api/token', {
        method: 'POST',
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        body: params,
      });

      const { access_token } = await result.json();
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
async fetchTopArtist(accessToken) {
const result = await fetch('https://api.spotify.com/v1/me/top/artists?time_range=short_term&limit=10', {
  headers: { Authorization: `Bearer ${accessToken}` },
});

const topArtists = await result.json();
console.log(topArtists)
return topArtists;
},

populateUI(topArtists) {
this.topAritstsPic = topArtists.items.map((item) => item.images[0].url);
this.topArtists = topArtists.items.map((item) => item.name);


console.log(this.topArtists)
console.log(this.topAritstsPic)
},
},
};
</script>