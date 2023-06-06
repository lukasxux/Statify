<template>
  <div>
    <div class="container">
      <div class="row">
        <div class="col">
          <h2 id="top-songs-title">Top 5 Songs</h2>
          <ul>
            <div v-for="track in topTracks" :key="track.id">
              <li>
                {{ track.name }}&nbsp;<span
                  >by {{ track.artists[0].name }}</span
                >
              </li>
            </div>
          </ul>
        </div>
        <div class="col">
          <div class="img-center">
            <h2 id="top-songs-title"></h2>
            <div v-if="topTracks.length">
              <div class="row">
                <div
                  class="col-md-6"
                  v-for="track in topTracks.slice(0, 2)"
                  :key="track.id"
                >
                  <img
                    :src="track.album.images[1].url"
                    style="margin-bottom: -150px"
                  />
                </div>
              </div>
              <div class="row">
                <div
                  class="col-md-4"
                  v-for="track in topTracks.slice(2, 5)"
                  :key="track.id"
                >
                  <img
                    :src="track.album.images[1].url"
                    style="margin-bottom: -150px"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
#top-songs-title {
  display: flex;
  justify-content: left;
  color: #fff;
  font-size: 30px;
  margin-bottom: 40px;
  margin-top: 40px;
}

li {
  display: flex;
  justify-content: left;
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
      topTracks: [],
    };
  },
  mounted() {
    this.fetchTopTracks();
  },
  methods: {
    async fetchTopTracks() {
      const token = process.env.TOKEN;
      const headers = { Authorization: `Bearer ${token}` };
      const response = await axios.get(
        "https://localhost:5001/api/spotify/topTracks/" +
          sessionStorage.getItem("visit-user"),
        {
          headers,
        }
      );
      this.topTracks = response.data.items.slice(0, 5);
    },
  },
};
</script>
