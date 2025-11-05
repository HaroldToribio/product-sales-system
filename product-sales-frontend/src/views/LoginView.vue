<template>
  <div class="login-container">
    <div class="login-box">
      <h2>Login</h2>
      <form @submit.prevent="handleLogin">
        <div class="form-group">
          <input 
            v-model="username" 
            type="text"
            placeholder="Username" 
            required 
          />
        </div>
        <div class="form-group">
          <input 
            v-model="password" 
            type="password" 
            placeholder="Password" 
            required 
          />
        </div>
        <button type="submit" class="btn-primary">Login</button>
        <p v-if="error" class="error">{{ error }}</p>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { auth } from '@/services/api';

const router = useRouter();
const username = ref('');
const password = ref('');
const error = ref('');

const handleLogin = async () => {
  try {
    error.value = '';
    const { data } = await auth.login(username.value, password.value);
    localStorage.setItem('token', data.token);
    router.push('/products');
  } catch (err) {
    error.value = 'Invalid credentials';
  }
};
</script>

<style scoped>
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh;
  background: #f5f5f5;
}

.login-box {
  background: white;
  padding: 40px;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0,0,0,0.1);
  width: 100%;
  max-width: 400px;
}

h2 {
  margin-bottom: 20px;
  text-align: center;
  color: #333;
}

.form-group {
  margin-bottom: 15px;
}

input {
  width: 100%;
  padding: 12px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 14px;
  box-sizing: border-box;
}

input:focus {
  outline: none;
  border-color: #42b983;
}

.btn-primary {
  width: 100%;
  padding: 12px;
  background: #42b983;
  color: white;
  border: none;
  border-radius: 4px;
  font-size: 16px;
  cursor: pointer;
  transition: background 0.3s;
}

.btn-primary:hover {
  background: #359268;
}

.error {
  color: #e74c3c;
  margin-top: 10px;
  text-align: center;
  font-size: 14px;
}
</style>