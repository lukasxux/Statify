<script setup>
import axios from "axios";
</script>

<template>
  <div class="songView">
    <div class="container">
      <div class="center-title">
        <h1 class="title"><span style="color: #1db954">Top</span> Tracks</h1>
      </div>
      <section id="profile" class="profile-section">
        <div class="avatar-wrapper">
          <img id="avatar" class="avatar" width="200" :src="avatarSrc" />
        </div>

        <table class="song-table">
          <thead>
            <tr>
              <th>Number</th>
              <th>Cover</th>
              <th>Song</th>
              <th>Artist</th>
              <th>Release Date</th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="(track, index) in topTracks"
              :key="track.id"
              @click="navigateToTrack(track)"
            >
              <td>{{ index + 1 }}</td>
              <td><img :src="track.album.images[1].url" alt="" /></td>
              <td>{{ track.name }}</td>
              <td>{{ track.artists[0].name }}</td>
              <td>{{ formatDate(track.album.release_date) }}</td>
            </tr>
          </tbody>
        </table>
      </section>
    </div>
  </div>
</template>

<script>
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
      const access_token = localStorage.getItem("access_token");
      const result = await fetch(
        "https://api.spotify.com/v1/me/top/tracks?time_range=short_term&limit=50",
        {
          headers: {
            Authorization: `Bearer ${access_token}`,
          },
        }
      );
      const topTracks = await result.json();
      this.topTracks = topTracks.items;
      return topTracks;
    },
    navigateToTrack(track) {
      const trackLink = this.getTrackLink(track);
      if (trackLink) {
        const linkEl = document.createElement("a");
        linkEl.href = trackLink;
        linkEl.target = "_blank";
        linkEl.click();
      }
    },
    getTrackLink(track) {
      console.log(track);
      return track.external_urls.spotify;
    },
    formatDate(dateString) {
      const date = new Date(dateString);
      const options = { year: "numeric", month: "short", day: "numeric" };
      return date.toLocaleDateString("en-US", options);
    },
  },
};
</script>

<style scoped>
h1 {
  color: #fff;
}

.song-table {
  border-collapse: collapse;
  width: 100%;
}

.song-table th,
.song-table td {
  border: 1px solid #444;
  padding: 10px;
  text-align: center;
  color: #fff;
}

.song-table th {
  background-color: #222;
  color: #fff;
}

.song-table tr:nth-child(even) {
  background-color: #333;
}

.song-table tr:hover {
  background-color: #555;
}

.song-table td img {
  width: 50px;
  height: 50px;
}

.center-title {
  text-align: center;
  color: white;
}
</style>
