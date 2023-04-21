<script setup>
import axios from 'axios';
</script>

<template>
    <div class="songView">
      <div class="container">
        <h1 class="title">Top 10 Songs</h1>
      
        <section id="profile" class="profile-section">
          <h2 class="subtitle">Logged in as <span class="display-name">{{ displayName }}</span></h2>
          <div class="avatar-wrapper">
            <img id="avatar" class="avatar" width="200" :src="avatarSrc" />
          </div>
          <ul class="profile-list">
            <img :src=topTrackImg[0] alt="Song Picture">
            <li><strong>Track 1:</strong> <span class="profile-info">{{ topTrackTitle[0] }} </span></li>
            <img :src=topTrackImg[1] alt="Song Picture">
            <li><strong>Track 2:</strong> <span class="profile-info">{{ topTrackTitle[1] }}  </span></li>
            <img :src=topTrackImg[2] alt="Song Picture">
            <li><strong>Track 3:</strong> <span class="profile-info">{{ topTrackTitle[2] }}  </span></li>
            <img :src=topTrackImg[3] alt="Song Picture">
            <li><strong>Track 4:</strong> <span class="profile-info">{{ topTrackTitle[3] }}  </span></li>
            <img :src=topTrackImg[4] alt="Song Picture">
            <li><strong>Track 5:</strong> <span class="profile-info">{{ topTrackTitle[4] }}  </span></li>
            <img :src=topTrackImg[5] alt="Song Picture">
            <li><strong>Track 6:</strong> <span class="profile-info">{{ topTrackTitle[5] }}  </span></li>
            <img :src=topTrackImg[6] alt="Song Picture">
            <li><strong>Track 7:</strong> <span class="profile-info">{{ topTrackTitle[6] }}  </span></li>
            <img :src=topTrackImg[7] alt="Song Picture">
            <li><strong>Track 8:</strong> <span class="profile-info">{{ topTrackTitle[7] }}  </span></li>
            <img :src=topTrackImg[8] alt="Song Picture">
            <li><strong>Track 9:</strong> <span class="profile-info">{{ topTrackTitle[8] }} </span></li>
            <img :src=topTrackImg[9] alt="Song Picture">
            <li><strong>Track 10:</strong> <span class="profile-info">{{ topTrackTitle[9] }}  </span></li>
          </ul>
        </section>
      </div>
      </div>

</template>

<style scoped>
h1 {
  color: #336699;
}

.top-10-list {
  list-style-type: none;
  padding: 0;
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
}

.top-10-list li {
  margin: 20px;
  width: 200px;
}

.artist-name {
  font-size: 18px;
  margin-top: 10px;
}

</style>

<script>


export default {
  data() {
    return {
      topTrackTitle: [],
      topTrackImg: []
    };
  },
  mounted() {
    this.logToptracks();
  },
  methods: {
   logToptracks(){

    const storedArrayString = sessionStorage.getItem('topTracks');
    const retrievedArray = JSON.parse(storedArrayString);
    console.log("retrievedArray");
    console.log(retrievedArray);
    this.topTrackTitle = retrievedArray.items.map((item) => item.name);
    this.topTrackImg = retrievedArray.items.map((item) => item.album.images[1].url);
    console.log("img");
    console.log(this.topTrackImg);
   },
   }
};
</script>






