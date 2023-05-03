

<template>
  <div class="container">
    <br>
    <br>

    <h1 class="subtitle">Logged in as <span class="display-name">{{ displayName }}</span></h1>
  
    <section id="profile" class="profile-section">
      <div class="row">
        <div class="col">
          <div class="avatar-wrapper">
            <img id="avatar" class="avatar" width="350" :src="avatarSrc" />
          </div>
        </div>
        <div class="col">
          <ul class="profile-list">
            <li><strong>Follower:</strong>  <span class="profile-info">{{ followers }}</span></li> 
            <li><strong>Country:</strong>  <span class="profile-info">{{ country }}</span></li>
            <li><strong>User ID:</strong> <span class="profile-info">{{ id }}</span></li>
            <li><strong>Email:</strong> <span class="profile-info">{{ email }}</span></li>
            <li><strong>Product:</strong>  <span class="profile-info">{{ product }}</span></li>
            <li><strong>Spotify:</strong> <a :href="uri" class="profile-link">{{ uri }}</a></li>
            <!--<li><strong>Profile Image:</strong> <span class="profile-info">{{ imgUrl }}</span></li>-->
          </ul>
        </div>
      </div>
    </section>
  </div>
</template>

<style scoped>
.container {
  width: 1000px;
}

.subtitle {
  font-size: 30px;
  margin-bottom: 40px;
  color: #fff;
}

.display-name {
  font-weight: bold;
  color:  #1db954;
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
  text-align: left ;
  font-size: 20px;
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
li{
  margin-top: 15px;
}

</style>
<script>
export default {
  data() {
    return {
      displayName: '', // Display name of the user
      avatarSrc: '', // Avatar source of the user
      followers: '', // Follower of the user
      country: '', // Country of the user
      id: '', // User ID
      email: '', // Email of the user
      product: '', // Product
      uri: '', // Spotify URI of the user
      url: '', // Link of the user's Spotify profile
      imgUrl: '', // Profile image URL of the user


    };
  },
  mounted() {
    // Fetch the user's profile data from Spotify API and update the data properties
    this.fetchUserData();
  },
  methods: {
 // Update the data properties 
 fetchUserData(){
  const storedArrayString = sessionStorage.getItem('profile');
    const retrievedArray = JSON.parse(storedArrayString);
    console.log(retrievedArray);
    this.displayName = retrievedArray.display_name;
    this.avatarSrc = retrievedArray.images[0].url;
    this.followers = retrievedArray.followers.total;
    this.country = retrievedArray.country;
    this.id = retrievedArray.id;
    this.email = retrievedArray.email;
    this.product = retrievedArray.product;
    this.uri = retrievedArray.uri;
    this.url = retrievedArray.external_urls.spotify;
    this.imgUrl = retrievedArray.images[0].url;
 }
  }
};
</script>



