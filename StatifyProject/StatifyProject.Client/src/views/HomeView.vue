<template>
  <div class="container">
    <div class="row">
      <div class="col">
        <img
          class="img-statifyLogo"
          src="../assets/img/StatifyLogo.png"
          alt=""
        />
      </div>
      <div id="welcome" class="col d-flex align-items-center">
        <div>
          <h1>Welcome to <span style="color: #1db954">Stat</span>ify</h1>
          <h5>
            Discover the beat of your musical journey with our powerful
            integration of the Spotify API. Whether you're curious about your
            own top songs and favorite artists or eager to explore the musical
            tastes of other users, we've got you covered!
          </h5>
        </div>
      </div>
    </div>
  </div>
  <div class="hr-container">
    <hr>
  </div>
  <div class="container">
    <div class="row">
      <div class="col">
        <h1 class="col-title">
          <span style="color: #1db954">New</span> Releases
        </h1>
        <div>
          <ul v-for="release in newReleases" :key="release.name">
            <li style="list-style-type: none;">
              <div class="container">
                <div class="row">
                  <div class="col-auto"> <!-- Set the column width to auto -->
                    <span>
                      <img class="releases-img" :src="release.images[0].url" alt="" />
                    </span>
                  </div>
                  <div class="col">
                    <span class="release-info">
                      <span>
                        {{ release.name }}
                        <br />
                        <span style="color: #a3a3a3">{{ release.artists[0].name }}</span>
                      </span>
                    </span>
                  </div>
                </div>
              </div>
            </li>
            
          </ul>
        </div>
      </div>
      <div class="col">
        <h1 class="col-title"><span style="color: #1db954">Popular</span> Singles:</h1>
        <div>
          <ul v-for="single in popularSingles" :key="single.name">

            <li style="list-style-type: none;">
              <div class="container">
                <div class="row">
                  <div class="col-auto"> <!-- Set the column width to auto -->
                    <span>
                      <img class="releases-img" :src="single.track.album.images[0].url" alt="" />
                    </span>
                  </div>
                  <div class="col">
                    <span class="release-info">
                      <span>
                        {{ single.track.name }}
                        <br />
                        <span style="color: #a3a3a3">{{ single.track.artists[0].name }}</span>
                      </span>
                    </span>
                  </div>
                </div>
              </div>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </div>
  <br />
  <br />
  <br />
  <br />
</template>

<style scoped>
.hr-container {
  display: flex;
  justify-content: center;
}

.col-title {
  margin-bottom: 20px;
  text-align: center;
}
li {
  text-align: left;
  margin-bottom: 10px;

}

.releases-title {
  display: flex;
  justify-content: left;
}
.releases-img {
  width: 40px;
  margin-right: 20px;
  border-radius: 100%;
  transition: transform 0.3s ease;
}

.releases-img:hover {
  transform: scale(1.2);
}

hr {
  display: inline-block;
  border-width: 2px;
  width: 80%;
  color: #1db954;
  margin-bottom: 40px;
}
.img-statifyLogo {
  width: 400px;
}
button {
  width: 150px;
  height: 50px;
  border-radius: 10px;
  background-color: #1db954;
  font-size: 15px;
  font-weight: bold;
}
.container {
  position: relative;
  text-align: left;
  color: white;
}
#welcome {
  text-align: left;
}
</style>

<script>
export default {
  data() {
    return {
      accessToken: "",
      newReleases: {},
      popularSingles: {},
    };
  },
  mounted() {
    this.accessToken = localStorage.getItem("access_token");
    this.fetchNewReleases();
    this.fetchPopularSingles();
  },
  methods: {
    async fetchNewReleases() {
      const result = await fetch(
        "https://api.spotify.com/v1/browse/new-releases?country=AT&limit=6",
        {
          headers: {
            Authorization: `Bearer ${this.accessToken}`,
          },
        }
      );
      const newReleases = await result.json();
      this.newReleases = newReleases.albums.items;
      return this.newReleases;
    },

    async fetchPopularSingles() {
      const result = await fetch(
        "https://api.spotify.com/v1/playlists/37i9dQZEVXbNG2KDcFcKOF",
        {
          headers: {
            Authorization: `Bearer ${this.accessToken}`,
          },
        }
      );
      const popularSingles = await result.json();
      this.popularSingles = popularSingles.tracks.items.slice(0, 6);
      console.log(this.popularSingles);
      return this.popularSingles;
    },
  },
};
</script>
