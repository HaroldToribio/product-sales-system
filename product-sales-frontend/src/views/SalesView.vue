<template>
  <div class="sales-container">
    <h2>Sales Management</h2>
    
    <div class="form-section">
      <h3>Register New Sale</h3>
      <form @submit.prevent="createSale">
        <div class="form-row">
          <select v-model="form.clientId" required>
            <option value="" disabled>Select Client</option>
            <option v-for="client in clients" :key="client.id" :value="client.id">
              {{ client.name }}
            </option>
          </select>
          <input v-model.number="form.total" type="number" step="0.01" placeholder="Total Amount" required />
        </div>
        <button type="submit" class="btn-primary">Create Sale</button>
      </form>
    </div>

    <div class="table-section">
      <h3>Sales History</h3>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Date</th>
            <th>Client</th>
            <th>Total</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="sale in salesList" :key="sale.id">
            <td>{{ sale.id }}</td>
            <td>{{ formatDate(sale.date) }}</td>
            <td>{{ sale.client?.name || 'N/A' }}</td>
            <td>${{ sale.total.toFixed(2) }}</td>
          </tr>
          <tr v-if="salesList.length === 0">
            <td colspan="4" style="text-align: center; padding: 20px;">No sales yet</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';

const salesList = ref([]);
const clients = ref([]);
const form = ref({ clientId: '', total: 0 });

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
    clients.value = data;
  } catch (error) {
    console.error('Error loading clients:', error);
  }
};

const loadSales = async () => {
  try {
    const { data } = await axios.get('http://localhost:5119/api/sales', getHeaders());
    salesList.value = data;
  } catch (error) {
    console.error('Error loading sales:', error);
  }
};

const createSale = async () => {
  try {
    await axios.post('http://localhost:5119/api/sales', {
      clientId: parseInt(form.value.clientId),
      total: form.value.total,
      saleProducts: []
    }, getHeaders());
    
    form.value = { clientId: '', total: 0 };
    loadSales();
  } catch (error) {
    console.error('Error creating sale:', error);
    console.error('Error response:', error.response?.data); // ADD THIS LINE
    console.error('Error status:', error.response?.status); // ADD THIS LINE
  }
};

const formatDate = (dateString) => {
  const date = new Date(dateString);
  return date.toLocaleDateString() + ' ' + date.toLocaleTimeString();
};

onMounted(() => {
  loadClients();
  loadSales();
});
</script>

<style scoped>
.sales-container {
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

input, select {
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 14px;
}

input:focus, select:focus {
  outline: none;
  border-color: #42b983;
}

.btn-primary {
  padding: 10px 20px;
  background: #42b983;
  color: white;
  border: none;
  border-radius: 4px;
  font-size: 14px;
  cursor: pointer;
  transition: all 0.3s;
}

.btn-primary:hover {
  background: #359268;
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
  margin-top: 15px;
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

@media (max-width: 768px) {
  .form-row {
    grid-template-columns: 1fr;
  }
  
  .table-section {
    overflow-x: scroll;
  }
}
</style>