<template>
  <div class="clients-container">
    <h2>Clients Management</h2>
    
    <div class="form-section">
      <h3>{{ editId ? 'Edit Client' : 'Add New Client' }}</h3>
      <form @submit.prevent="saveClient">
        <div class="form-row">
          <input v-model="form.name" placeholder="Client Name" required />
          <input v-model="form.email" type="email" placeholder="Email" required />
        </div>
        <div class="form-row">
          <input v-model="form.phone" placeholder="Phone" required />
        </div>
        <div class="form-actions">
          <button type="submit" class="btn-primary">
            {{ editId ? 'Update' : 'Create' }}
          </button>
          <button v-if="editId" type="button" @click="cancelEdit" class="btn-secondary">
            Cancel
          </button>
        </div>
      </form>
    </div>

    <div class="table-section">
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Email</th>
            <th>Phone</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="client in clientsList" :key="client.id">
            <td>{{ client.id }}</td>
            <td>{{ client.name }}</td>
            <td>{{ client.email }}</td>
            <td>{{ client.phone }}</td>
            <td class="actions">
              <button @click="editClient(client)" class="btn-edit">Edit</button>
              <button @click="deleteClient(client.id)" class="btn-delete">Delete</button>
            </td>
          </tr>
          <tr v-if="clientsList.length === 0">
            <td colspan="5" style="text-align: center; padding: 20px;">No clients yet</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';

const clientsList = ref([]);
const form = ref({ name: '', email: '', phone: '' });
const editId = ref(null);

const getHeaders = () => {
  const token = localStorage.getItem('token');
  return {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  };
};

const loadClients = async () => {
  try {
    const { data } = await axios.get('http://localhost:5119/api/clients', getHeaders());
    clientsList.value = data;
  } catch (error) {
    console.error('Error loading clients:', error);
  }
};

const saveClient = async () => {
  try {
    if (editId.value) {
      await axios.put(`http://localhost:5119/api/clients/${editId.value}`, form.value, getHeaders());
      editId.value = null;
    } else {
      await axios.post('http://localhost:5119/api/clients', form.value, getHeaders());
    }
    form.value = { name: '', email: '', phone: '' };
    loadClients();
  } catch (error) {
    console.error('Error saving client:', error);
  }
};

const editClient = (client) => {
  form.value = { ...client };
  editId.value = client.id;
};

const cancelEdit = () => {
  form.value = { name: '', email: '', phone: '' };
  editId.value = null;
};

const deleteClient = async (id) => {
  if (confirm('Are you sure you want to delete this client?')) {
    try {
      await axios.delete(`http://localhost:5119/api/clients/${id}`, getHeaders());
      loadClients();
    } catch (error) {
      console.error('Error deleting client:', error);
    }
  }
};

onMounted(loadClients);
</script>

<style scoped>
.clients-container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 20px;
}

h2 {
  color: #333;
  margin-bottom: 30px;
}

h3 {
  color: #555;
  margin-bottom: 15px;
  font-size: 18px;
}

.form-section {
  background: white;
  padding: 25px;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.1);
  margin-bottom: 30px;
}

.form-row {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 15px;
  margin-bottom: 15px;
}

input {
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 14px;
}

input:focus {
  outline: none;
  border-color: #42b983;
}

.form-actions {
  display: flex;
  gap: 10px;
}

.btn-primary, .btn-secondary {
  padding: 10px 20px;
  border: none;
  border-radius: 4px;
  font-size: 14px;
  cursor: pointer;
  transition: all 0.3s;
}

.btn-primary {
  background: #42b983;
  color: white;
}

.btn-primary:hover {
  background: #359268;
}

.btn-secondary {
  background: #95a5a6;
  color: white;
}

.btn-secondary:hover {
  background: #7f8c8d;
}

.table-section {
  background: white;
  padding: 25px;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.1);
  overflow-x: auto;
}

table {
  width: 100%;
  border-collapse: collapse;
}

th {
  background: #f8f9fa;
  padding: 12px;
  text-align: left;
  font-weight: 600;
  color: #555;
  border-bottom: 2px solid #dee2e6;
}

td {
  padding: 12px;
  border-bottom: 1px solid #dee2e6;
}

tr:hover {
  background: #f8f9fa;
}

.actions {
  display: flex;
  gap: 8px;
}

.btn-edit, .btn-delete {
  padding: 6px 12px;
  border: none;
  border-radius: 4px;
  font-size: 13px;
  cursor: pointer;
  transition: all 0.3s;
}

.btn-edit {
  background: #3498db;
  color: white;
}

.btn-edit:hover {
  background: #2980b9;
}

.btn-delete {
  background: #e74c3c;
  color: white;
}

.btn-delete:hover {
  background: #c0392b;
}

@media (max-width: 768px) {
  .form-row {
    grid-template-columns: 1fr;
  }
  
  .table-section {
    overflow-x: scroll;
  }
}
</style>