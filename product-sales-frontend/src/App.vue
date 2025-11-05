<template>
  <div id="app">
    <nav v-if="isLoggedIn" class="navbar">
      <div class="nav-container">
        <h1>Product Sales System</h1>
        <div class="nav-links">
          <router-link to="/products">Products</router-link>
          <router-link to="/clients">Clients</router-link>
          <router-link to="/sales">Sales</router-link>
          <button @click="logout" class="btn-logout">Logout</button>
        </div>
      </div>
    </nav>
    <router-view />
  </div>
</template>

<script setup>
import { ref, watch } from 'vue';
import { useRouter, useRoute } from 'vue-router';

const router = useRouter();
const route = useRoute();
const isLoggedIn = ref(!!localStorage.getItem('token'));

watch(() => route.path, () => {
  isLoggedIn.value = !!localStorage.getItem('token');
});

const logout = () => {
  localStorage.removeItem('token');
  isLoggedIn.value = false;
  router.push('/login');
};
</script>

<style>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

body {
  font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, sans-serif;
  background: #f5f5f5;
}

#app {
  min-height: 100vh;
}

.navbar {
  background: #2c3e50;
  color: white;
  padding: 15px 0;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.nav-container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 20px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.navbar h1 {
  font-size: 22px;
  font-weight: 600;
}

.nav-links {
  display: flex;
  gap: 20px;
  align-items: center;
}

.nav-links a {
  color: white;
  text-decoration: none;
  padding: 8px 16px;
  border-radius: 4px;
  transition: background 0.3s;
}

.nav-links a:hover,
.nav-links a.router-link-active {
  background: rgba(255,255,255,0.1);
}

.btn-logout {
  background: #e74c3c;
  color: white;
  border: none;
  padding: 8px 16px;
  border-radius: 4px;
  cursor: pointer;
  font-size: 14px;
  transition: background 0.3s;
}

.btn-logout:hover {
  background: #c0392b;
}

@media (max-width: 768px) {
  .nav-container {
    flex-direction: column;
    gap: 15px;
  }
  
  .navbar h1 {
    font-size: 18px;
  }
}
</style>