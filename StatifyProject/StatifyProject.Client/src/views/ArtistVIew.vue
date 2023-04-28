<template>
  <div class="songView">
      <h1 class="title"><span style="color: #1db954;">Top</span> Artists</h1>
      <section id="profile" class="profile-section">
        <div class="avatar-wrapper">
          <img id="avatar" class="avatar" width="200" :src="avatarSrc" />
        </div>
        <ul class="profile-list">
          <tr v-for="(Artist, index) in topArtists" :key="Artist.name">
            <div class="card" :id="'card'+index+1">
              <img :src="Artist.images[0].url" alt="Famous artist">
              <div class="card-text">
                <p class="Number">{{index+1}}</p>
                <h2>{{ Artist.name }}</h2>
              </div>
            </div>
          </tr>         
        </ul>
      </section>
    </div>
      
</template>

<style scoped>

.Number{
  font-size: 20px;
  font-weight: bold;
  color: #fff;
  margin: 0 0 8px;
}
.songView .container {
  max-width: 1200px;
  margin: 0 auto;

  display: flex;
  justify-content: center;
}

.profile-list {
  list-style-type: none;
  padding: 0;
  margin-top: 20px;
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  grid-gap: 10px;
}

.profile-list > div:nth-child(3n) {
  grid-column: span 2;
}

.card {
  width: 300px;
  height: 500px; /* add this line */
  border: 1px solid #1db954;;
  border-radius: 8px;
  overflow: hidden;
  background-color: #1db9542a;
  color: #ffffff;
}

.card img {
  width: 100%;
  height: 100%;
  display: block;
  object-fit: cover;
}


.card-text {
  padding: 16px;
}

.card-text p {
  margin: 0 0 8px;
}

.card-text h2 {
  margin: 0;
}






</style>

<script>
export default {
  data() {
    return {
      topArtists: [],
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
      this.topArtists = retrievedArray.items;
  },

},
};
</script>