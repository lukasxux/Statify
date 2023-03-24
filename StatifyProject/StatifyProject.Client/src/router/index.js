import { createRouter, createWebHistory } from 'vue-router'

import HomeView from '../views/HomeView.vue'
import LoginView from '../views/LoginView.vue'
import ArtistView from '../views/ArtistView2.vue'
import SongView from '../views/SongView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    
    {
      path: '/login',
      name: 'login',
      component: LoginView,
    },

    {
      path: '/artist',
      name: 'artist',
      component: ArtistView,
    },

    {
      path: '/song',
      name: 'song',
      component: SongView,
    },
    
  ]
});



export default router;
