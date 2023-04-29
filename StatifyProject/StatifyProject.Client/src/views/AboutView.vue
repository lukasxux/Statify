<script setup>
import axios from 'axios';
</script>

<template>
  <div class="StatifyAccountView" >
  <div class="login-container">
    <h1>Login</h1>
    
      <div class="form-group">
        <label for="email">Email:</label>
        <input type="email" id="email">
      </div>
      <div class="form-group">
        <label for="password">Password:</label>
        <input type="password" id="password">
      </div>
      <button type="submit">Log in</button>

  </div>

  <div id="or">
    <span><h5 id="or-text">or</h5></span>
  </div>
  
  <div class="registration-container">
    <h1>Register</h1>
  
    <div class="form-group">
      <label for="username">Username:</label>
      <input type="text" id="username">
    </div>
    
    <div class="form-group">
      <label for="email">Email:</label>
      <input type="email" id="email">
    </div>
    
    <div class="form-group">
      <label for="password">Password:</label>
      <input type="password" id="password">
    </div>
    
    <button type="submit">Register</button>
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
      model() {
        return {
          email: 'username',
          password: 'password',
        };
      },
    };
  },
  mounted() {
    
  },
  methods: {
    
  },
};
</script>
