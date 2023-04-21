<template>
  <div class="container">
    <h1 class="title">Display your Spotify Profile Data</h1>
  
    <section id="profile" class="profile-section">
      <h2 class="subtitle">Logged in as <span class="display-name">{{ displayName }}</span></h2>
      <div class="avatar-wrapper">
        <img id="avatar" class="avatar" width="200" :src="avatarSrc" />
      </div>
      <ul class="profile-list">
        <li><strong>User ID:</strong> <span class="profile-info">{{ id }}</span></li>
        <li><strong>Email:</strong> <span class="profile-info">{{ email }}</span></li>
        <li><strong>Spotify URI:</strong> <a :href="uri" class="profile-link">{{ uri }}</a></li>
        <li><strong>Link:</strong> <a :href="url" class="profile-link">{{ url }}</a></li>
        <li><strong>Profile Image:</strong> <span class="profile-info">{{ imgUrl }}</span></li>
      </ul>
    </section>
  </div>
  
  
  
</template>

<style scoped>
.container {
  max-width: 600px;
  margin: 0 auto;
  padding: 20px;
  background-color: #333;
  color: #fff;
}

.title {
  font-size: 24px;
  text-align: center;
  margin-bottom: 30px;
}

.profile-section {
  border: 1px solid #ddd;
  padding: 20px;
  background-color: #222;
}

.subtitle {
  font-size: 18px;
  margin-bottom: 20px;
  color: #fff;
}

.display-name {
  font-weight: bold;
  color: #00bfff;
}

.avatar-wrapper {
  display: flex;
  justify-content: center;
  align-items: center;
  margin-bottom: 20px;
}

.avatar {
  border-radius: 50%;
  box-shadow: 0 2px 6px rgba(0,0,0,0.1);
}

.profile-list {
  list-style-type: none;
  padding: 0;
  margin: 0;
}

.profile-info {
  margin-bottom: 10px;
}

.profile-link {
  color: #00bfff;
  text-decoration: none;
}

</style>

<script>

export default {
  data() {
    return {
      displayName: '', // Display name of the user
      avatarSrc: '', // Avatar source of the user
      id: '', // User ID
      email: '', // Email of the user
      uri: '', // Spotify URI of the user
      url: '', // Link of the user's Spotify profile
      imgUrl: '', // Profile image URL of the user
      topTracks: []

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
        const profile = await this.fetchProfile(accessToken);
        const topTracks = await this.fetchTopTracks(accessToken);
        this.updateProfilInfo(profile);
        this.updateTopTracks(topTracks);
      }
    },
    async redirectToAuthCodeFlow(clientId) {
      const verifier = this.generateCodeVerifier(128);
      const challenge = await this.generateCodeChallenge(verifier);

      localStorage.setItem('verifier', verifier);

      const params = new URLSearchParams();
      params.append('client_id', clientId);
      params.append('response_type', 'code');
      params.append('redirect_uri', 'http://localhost:5173/my-account');
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
      params.append('redirect_uri', 'http://localhost:5173/my-account');
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
async fetchProfile(accessToken) {
const result = await fetch('https://api.spotify.com/v1/me', {
  headers: { Authorization: `Bearer ${accessToken}` },
});

const profile = await result.json();
console.log(profile)
return profile;
},
async fetchTopTracks(accessToken) {
const result = await fetch('https://api.spotify.com/v1/me/top/tracks?time_range=short_term&limit=10', {
  headers: { Authorization: `Bearer ${accessToken}` },
});
const topTracks = await result.json();

console.log(topTracks)

sessionStorage.setItem('topTracks', JSON.stringify(topTracks));

return topTracks;


},
updateProfilInfo(profile) {
this.displayName = profile.display_name;
this.avatarSrc = profile.images[0].url;
this.id = profile.id;
this.email = profile.email;
this.uri = profile.uri;
this.url = profile.external_urls.spotify;
this.imgUrl = profile.images[0].url;
},

updateTopTracks(topTracks) {
  this.topTracks = topTracks.items.map((item) => item.name);
console.log(this.topTracks)
},
},
};
</script>



