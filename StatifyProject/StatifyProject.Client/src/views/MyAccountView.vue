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
    this.id = retrievedArray.id;
    this.email = retrievedArray.email;
    this.uri = retrievedArray.uri;
    this.url = retrievedArray.external_urls.spotify;
    this.imgUrl = retrievedArray.images[0].url;
 }
  }
};
</script>



