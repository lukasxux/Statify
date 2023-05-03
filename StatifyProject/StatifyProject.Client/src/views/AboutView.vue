<script setup>
import axios from 'axios';
import store from '../store.js';
</script>

<template>
  <div class="StatifyAccountView">
    <div v-if="!loggedIn" class="login-container">
      <h1>Login</h1>
      <form @submit.prevent="loginUser">
        <div class="form-group">
          <label for="email">Email:</label>
          <input type="email" id="email" v-model="model.email" />
        </div>
        <div class="form-group">
          <label for="password">Password:</label>
          <input type="password" id="password" v-model="model.password" />
        </div>
        <button type="submit">Log in</button>
      </form>
    </div>

    <div v-else class="user-info">
      <p>Welcome, {{ user.username }}!</p>
      <p>Username: {{ user.username }}</p>
      <p>Email: {{ user.email }}</p>
      <p>Favorite Song: {{ user.favoriteSong }}</p>
      <p>Favorite Artist: {{ user.favoriteArtist }}</p>
      <button @click="logoutUser">Log out</button>
    </div>

    <div v-if="!loggedIn" id="or">
      <span><h5 id="or-text">or</h5></span>
    </div>

    <div v-if="!loggedIn" class="registration-container">
      <h1>Register</h1>
      <form @submit.prevent="registerUser">
        <div class="form-group">
          <label for="username">Username:</label>
          <input type="text" id="username" v-model="regmodel.regUsername" />
        </div>
        <div class="form-group">
          <label for="email">Email:</label>
          <input type="email" id="email" v-model="regmodel.regEmail" />
        </div>
        <div class="form-group">
          <label for="password">Password:</label>
          <input type="password" id="password" v-model="regmodel.regPassword" />
        </div>
        <button type="submit">Register</button>
      </form>
    </div>
  </div>
</template>

<style>


/*OR THING CSS----------------------------*/
#or-text{
  font-weight: bold;
  font-size: 18px;
  color: #ffffff94;
  text-transform: uppercase;
}
#or {
  display: flex;
  align-items: center;
  text-align: center;
  width: 100%;
  margin: 20px 0;
}

#or::before,
#or::after {
  content: "";
  flex: 1;
  border-bottom: 1px solid #ffffff94;
  margin: 0 10px;
}

#or span {
  font-weight: bold;
  font-size: 18px;
  color: #666;
  text-transform: uppercase;
  margin: 0 10px;
}

/*Login CSS----------------------------*/
.login-container {
  max-width: 400px;
  margin: auto;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.3);
  display: flex;
  flex-direction: column;
  align-items: center;
}

h1 {
  text-align: center;
  margin-bottom: 20px;
}

.form-group {
  margin-bottom: 20px;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}

label {
  margin-bottom: 5px;
  font-weight: bold;
}

input[type="email"],
input[type="password"] {
  padding: 10px;
  font-size: 16px;
  border-radius: 5px;
  border: 1px solid #ccc;
  width: 100%;
  box-sizing: border-box;
}

button[type="submit"] {
  padding: 10px;
  font-size: 16px;
  background-color: #1ed760;
  color: #fff;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}
/*Register CSS----------------------------*/
.registration-container {
  max-width: 400px;
  margin: auto;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.3);
  display: flex;
  flex-direction: column;
  align-items: center;
}

h1 {
  text-align: center;
  margin-bottom: 20px;
}

.form-group {
  margin-bottom: 20px;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}

label {
  margin-bottom: 5px;
  font-weight: bold;
}

input[type="text"],
input[type="email"],
input[type="password"] {
  padding: 10px;
  font-size: 16px;
  border-radius: 5px;
  border: 1px solid #ccc;
  width: 100%;
  box-sizing: border-box;
}

button[type="submit"] {
  padding: 10px;
  font-size: 16px;
  background-color: #1ed760;
  color: #fff;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}


</style>


<script>
export default {
  data() {
    return {
      regmodel: {
        regusername: '',
        regemail: '',
        regpassword: '',
      },
      model: {
        email: '',
        password: '',
      },
      loggedIn: false,
      userGuid: {},
      user: {},

    };
  },
  methods: {
    async registerUser() {
      try {
        const response = await axios.post('https://localhost:5001/api/users', {
          username: this.regmodel.regUsername,
          email: this.regmodel.regEmail,
          password: this.regmodel.regPassword,
          bio: 'default bio',
        });
        alert('Registration successful!');
      } catch (error) {
        console.error(error);
        alert('Registration failed.');
      }
    },
    async UpdateUserData(){
      const response3 = await axios.get("https://localhost:5001/api/users/"+this.userGuid);

      this.user = response3.data; 

    },
    async loginUser() {
      try {
        const response2 = await axios.post('https://localhost:5001/api/users/login', {
          email: this.model.email,
          password: this.model.password,
        });
        this.userGuid = response2.data.userGuid;
        this.$store.commit('authenticate', response2.data);
        console.log(this.$store.state.user);
        await this.UpdateUserData();
        this.loggedIn = true;
        alert('Login successful!');
      } catch (error) {
        console.error(error);
        alert('Login failed.');
      }
    },
    logoutUser() {
      this.loggedIn = false;
      this.username = "";
    }
    
  },
};
</script>
