<template>
  <div class="container">
    <br />
    <br />

    <h1 class="subtitle">
      <span class="display-name">{{ displayName }}</span>
    </h1>

    <section id="profile" class="profile-section">
      <div class="row">
        <div class="col">
          <div class="avatar-wrapper">
            <img id="avatar" class="avatar" width="350" :src="imgUrl" />
          </div>
        </div>
        <div class="col">
          <ul class="profile-list">
            <li>
              <strong>Follower: </strong>
              <span class="profile-info"> {{ followers }}</span>
            </li>
            <li>
              <strong>Country: </strong>
              <span class="profile-info"> {{ country }}</span>
            </li>
            <li>
              <strong>Product: </strong>
              <span class="profile-info"> {{ product }}</span>
            </li>
            <li>
              <strong>Spotify: </strong>
              <a :href="uri" class="profile-link"> {{ uri }}</a>
            </li>
            <!--<li><strong>Profile Image:</strong> <span class="profile-info">{{ imgUrl }}</span></li>-->
          </ul>
        </div>
      </div>
    </section>
    <br />
    <br />
    <br />
    <div class="center-hr">
      <hr />
    </div>

    <br />
    <br />
    <OtherTopFiveSongsVue></OtherTopFiveSongsVue>
    <br />
    <br />
    <OtherTopFiveArtist></OtherTopFiveArtist>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
  </div>
</template>

<style scoped>
.center-hr {
  text-align: center;
}

hr {
  display: inline-block;
  width: 50%;
  color: #1db954;
}

.subtitle {
  font-size: 30px;
  margin-bottom: 40px;
  color: #fff;
}

.display-name {
  display: flex;
  justify-content: center;
  font-weight: bold;
  color: #1db954;
  font-size: 50px;
  margin-top: 40px;
  margin-bottom: 50px;
}

.avatar-wrapper {
  display: flex;
  justify-content: center;
  align-items: center;
  margin-top: 20px;
  margin-bottom: 20px;
  margin-right: 60px;
}

.avatar {
  border-radius: 50%;
}

.profile-list {
  list-style-type: none;
  padding: 0;
  margin: 0;
  text-align: left;
  font-size: 30px;
  margin-top: 55px;
  color: #fff;
}

.profile-info {
  margin-bottom: 10px;
  text-align: left;
  margin-top: 400px;
}

.profile-link {
  color: #1db954;
  text-decoration: none;
}
li {
  margin-top: 15px;
}
</style>
<script>
import axios from "axios";
import OtherTopFiveSongsVue from "../components/OtherUserTopSong.vue";
import OtherTopFiveArtist from "../components/OtherUserTopArtist.vue";

export default {
  components: {
    OtherTopFiveSongsVue,
    OtherTopFiveArtist,
  },
  data() {
    return {
      profile: {},

      displayName: "", // Display name of the user
      avatarSrc: "", // Avatar source of the user
      followers: "", // Follower of the user
      country: "", // Country of the user
      id: "", // User ID
      email: "", // Email of the user
      product: "", // Product
      uri: "", // Spotify URI of the user
      url: "", // Link of the user's Spotify profile
      imgUrl: "", // Profile image URL of the user
    };
  },
  mounted() {
    // Fetch the user's profile data from Spotify API and update the data properties
    this.fetchProfile();
  },
  methods: {
    // Update the data properties
    async fetchProfile() {
      const token = process.env.TOKEN;
      console.log(token);
      const headers = { Authorization: `Bearer ${token}` };
      const response = await axios.get(
        "/spotify/spotifyProfile/" + sessionStorage.getItem("visit-user"),
        {
          headers,
        }
      );
      this.profile = response.data;
      console.log(this.profile);
      this.fetchUserData();
    },
    async fetchUserData() {
      this.displayName = this.profile.display_Name;
      this.imgUrl = this.profile.images[0].url;
      this.followers = this.profile.followers.total;
      this.country = this.profile.country;
      this.product = this.profile.product;
      this.uri = this.profile.external_Urls.spotify;
    },
  },
};
</script>
