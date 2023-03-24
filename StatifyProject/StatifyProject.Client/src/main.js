import { createApp } from 'vue'
import axios from "axios";             // npm install axios

import App from './App.vue'
import router from './router'
import store from './store.js'
import process from 'node:process'
import './assets/main.css'
import 'bootstrap/dist/css/bootstrap.min.css'

axios.defaults.baseURL = process.env.NODE_ENV == 'production' ? "/api" : "https://localhost:5002/api";

const app = createApp(App)
app.use(router)
app.use(store)
app.mount('#app')
