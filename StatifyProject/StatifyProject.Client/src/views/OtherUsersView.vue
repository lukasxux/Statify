<script setup>
import axios from 'axios';
</script>

<template>
  <div>
    <h1>Search for other users</h1>
    <div class="search-container">
      <img
        src="../assets/img/StatifyLogo.png"
        alt="Spotify logo"
        class="logo"
      />
      <form>
        <input type="text" placeholder="Search..." />
        <button type="submit"><i class="fas fa-search"></i></button>
      </form>
    </div>
  </div>
  <br>
  <br>
  <hr>
  <br>
  <br>
  <br>



  <div v-for="(user) in users" :key="user.id" class="user-card">
    <img src="../assets/img/StatifyLogo.png" alt="">
    <p>{{user.username}}</p>

  </div>
</template>


<style scoped>
hr{
    display: inline-block;
    width: 50%;
  }

.user-card img {
    width: 80px;
    height: 80px;
    margin-right: 10px;
    border-radius: 50%;
  }
.user-card {
    display: flex;
    align-items: center;
    border-radius: 10px;
    border: 1px solid white;
    border-radius: 10px;
    width: 400px;
    height: 100px;
  }
  

.search-container {
  margin-top: 30px;
  margin-bottom: 10px;
  background-color: #1db954;
  display: flex;
  align-items: center;
  padding: 10px;
  border-radius: 5px;
}

.logo {
  height: 60px;
  margin-right: 10px;
}

form {
  display: flex;
  align-items: center;
  background-color: #222;
  border-radius: 10px;
  flex: 1;
  padding: 5px;
}

input[type="text"] {
  border: none;
  outline: none;
  padding: 10px;
  flex: 1;
}

button[type="submit"] {
  border: none;
  background-color: transparent;
  cursor: pointer;
  margin-right: 10px;
}

.fa-search {
  background-color: #222;
  color: #222;
  font-size: 18px;
}

input[type="text"] {
  border: none;
  outline: none;
  padding: 10px;
  flex: 1;
  background-color: #222;
  color: #fff;
}
</style>

<script>
export default {
  data() {
    return {
        users: []
    };
  },
  mounted() {
    this.loadUsers();
  },
  methods: {
    async loadUsers() {
        const token = "4UO5FmFW6wqj6PtWTXWRRiCvfdKq2dx+gsCM6d+eWR6++LrfKCP6jRvpMkw95KbYr9u1/VS1/fPWRg/XBmYjeQrR6knknq3w0TaDNOaU2QC8qP/CpTgdB5u3aHdIfpI1Tzn/5dx2fWYp0JCqYNhxzMDLGRifddA7JKUlhxVrx3E=";
        const headers = { Authorization: `Bearer ${token}` };
        const response = await axios.get("https://localhost:5001/api/users", { headers });
        this.users = response.data;
        console.log(this.users);

    },
  },
};
</script>

