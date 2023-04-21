<template>
  <div class="songView">

      <h1 class="title">Top 10 Songs</h1>
  
      <section id="profile" class="profile-section">
        <h2 class="subtitle">Logged in as <span class="display-name">{{ displayName }}</span></h2>
        <div class="avatar-wrapper">
          <img id="avatar" class="avatar" width="200" :src="avatarSrc" />
        </div>
        <ul class="profile-list">
          <li class="profile-card">
            <img class="profile-card-img" :src="topAritstsPic[0]" alt="Song Picture">
            <div class="profile-card-info">
              <strong class="profile-card-title"> Artist 1:</strong>
              <span class="profile-card-subtitle">{{ topArtistsName[0] }}</span>
            </div>
          </li>
          <li class="profile-card">
            <img class="profile-card-img" :src="topAritstsPic[1]" alt="Song Picture">
            <div class="profile-card-info">
              <strong class="profile-card-title">Artist 2:</strong>
              <span class="profile-card-subtitle">{{ topArtistsName[1] }}</span>
            </div>
          </li>
          <li class="profile-card">
            <img class="profile-card-img" :src="topAritstsPic[2]" alt="Song Picture">
            <div class="profile-card-info">
              <strong class="profile-card-title">Artist 3:</strong>
              <span class="profile-card-subtitle">{{ topArtistsName[2] }}</span>
            </div>
          </li>
          <li class="profile-card">
            <img class="profile-card-img" :src="topAritstsPic[3]" alt="Song Picture">
            <div class="profile-card-info">
              <strong class="profile-card-title">Artist 4:</strong>
              <span class="profile-card-subtitle">{{ topArtistsName[3] }}</span>
            </div>
          </li>
          <li class="profile-card">
            <img class="profile-card-img" :src="topAritstsPic[4]" alt="Song Picture">
            <div class="profile-card-info">
              <strong class="profile-card-title">Artist 5:</strong>
              <span class="profile-card-subtitle">{{ topArtistsName[4] }}</span>
            </div>
          </li>
          <li class="profile-card">
            <img class="profile-card-img" :src="topAritstsPic[5]" alt="Song Picture">
            <div class="profile-card-info">
              <strong class="profile-card-title">Artist 6:</strong>
              <span class="profile-card-subtitle">{{ topArtistsName[5] }}</span>
            </div>
          </li>
          <li class="profile-card">
            <img class="profile-card-img" :src="topAritstsPic[6]" alt="Song Picture">
            <div class="profile-card-info">
              <strong class="profile-card-title">Artist 7:</strong>
              <span class="profile-card-subtitle">{{ topArtistsName[6] }}</span>
            </div>
          </li>
          <li class="profile-card">
            <img class="profile-card-img" :src="topAritstsPic[7]" alt="Song Picture">
            <div class="profile-card-info">
              <strong class="profile-card-title">Artist 8:</strong>
              <span class="profile-card-subtitle">{{ topArtistsName[7] }}</span>
            </div>
          </li>
          <li class="profile-card">
            <img class="profile-card-img" :src="topAritstsPic[8]" alt="Song Picture">
            <div class="profile-card-info">
              <strong class="profile-card-title">Artist 9:</strong>
              <span class="profile-card-subtitle">{{ topArtistsName[8] }}</span>
            </div>
          </li>
          <li class="profile-card">
            <img class="profile-card-img" :src="topAritstsPic[9]" alt="Song Picture">
            <div class="profile-card-info">
              <strong class="profile-card-title">Artist 10:</strong>
              <span class="profile-card-subtitle">{{ topArtistsName[9] }}</span>
            </div>
          </li>

        </ul>
      </section>
    </div>

  
  



    


      
</template>

<style scoped>

.songView .container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 20px;
}

.title {
  font-size: 24px;
  font-weight: bold;
}

.profile-section {
  margin-top: 20px;
}

.subtitle {
  font-size: 18px;
  font-weight: bold;
}

.avatar-wrapper {
  margin-top: 10px;
}

.avatar {
  border-radius: 50%;
}

.profile-list {
  list-style-type: none;
  padding: 0;
  margin-top: 20px;
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 10px;
}

.profile-card {
  background-color: #f8f8f8;
  padding: 10px;
  display: flex;
  align-items: center;
}

.profile-card-img {
  width: 100px;
  height: 100px;
  object-fit: cover;
  border-radius: 5px;
}

.profile-card-info {
  margin-left: 10px;
}

.profile-card-title {
  font-weight: bold;
  color: #666;
}

.profile-card-subtitle {
  display: block;
  margin-top: 5px;
  color: #666;
}


</style>

<script>
export default {
  data() {
    return {
      topArtistsName: [], // Initialize an empty array to store the top tracks
      topAritstsPic: []
    };
  },
  mounted() {
    // Fetch the user's profile data from Spotify API and update the data properties
    this.logTopArtist();

  },
  methods: {
   logTopArtist(){
      
    const storedArrayString = sessionStorage.getItem('topArtists');
    const retrievedArray = JSON.parse(storedArrayString);
    console.log("retrievedArray");
    console.log(retrievedArray);
    this.topArtistsName = retrievedArray.items.map((item) => item.name);
    this.topAritstsPic = retrievedArray.items.map((item) => item.images[1].url);
    console.log("Artist Name");
    console.log(this.topAritstsPic);

   }
   
},
};
</script>