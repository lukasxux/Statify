<template>
  <div>
    <div class="container">
      <div class="row">
        <div class="col">
          <div class="img-center">
            <h2 id="top-songs-title"></h2>
            <div v-if="topArtists.length">
              <div class="row">
                <div
                  class="col-md-6"
                  v-for="artist in topArtists.slice(0, 2)"
                  :key="artist.id"
                >
                  <img
                    :src="artist.images[1].url"
                    style="margin-bottom: -150px"
                  />
                </div>
              </div>
              <div class="row">
                <div
                  class="col-md-4"
                  v-for="artist in topArtists.slice(2, 5)"
                  :key="artist.id"
                >
                  <img
                    :src="artist.images[1].url"
                    style="margin-bottom: -150px"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="col">
          <h2 id="top-songs-title">Top 5 Artists</h2>
          <ul class="aritst-name">
            <div v-for="artist in topArtists" :key="artist.id">
              <li style="text-align: right">{{ artist.name }}&nbsp;</li>
            </div>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.col {
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-end;
}

ul.aritst-name {
  margin: 0;
  padding: 0;
  list-style: none;
}

#top-songs-title {
  display: flex;
  justify-content: right;
  color: #fff;
  font-size: 30px;
  margin-bottom: 40px;
  margin-top: 40px;
}

li {
  display: flex;
  justify-content: left;
  align-items: center;
  text-align: right;
  color: #fff;
  font-size: 20px;
  margin-bottom: 10px;
  margin-top: 10px;
}

span {
  color: darkgrey;
}
img {
  height: 200px;
}

.col img {
  margin-bottom: 0;
  transition: transform 0.3s ease-in-out, z-index 0.3s ease-in-out;
}

.col img:hover {
  transform: scale(1.1);
  z-index: 1;
}

.col img:not(:hover) {
  z-index: 0;
}
</style>

<script>
import axios from "axios";

export default {
  data() {
    return {
      topArtists: [],
    };
  },
  mounted() {
    this.fetchTopArtist();
  },
  methods: {
    async fetchTopArtist() {
      const token = process.env.TOKEN;
      const headers = { Authorization: `Bearer ${token}` };
      const response = await axios.get(
        "https://localhost:5001/api/spotify/topArtists/" +
          sessionStorage.getItem("visit-user"),
        {
          headers,
        }
      );
      this.topArtists = response.data.items.slice(0, 5);
    },
  },
};
</script>
