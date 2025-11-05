<template>
  <div class="products-container">
    <h2>Products Management</h2>
    
    <div class="form-section">
      <h3>{{ editId ? 'Edit Product' : 'Add New Product' }}</h3>
      <form @submit.prevent="saveProduct">
        <div class="form-row">
          <input v-model="form.name" placeholder="Product Name" required />
          <input v-model="form.description" placeholder="Description" />
        </div>
        <div class="form-row">
          <input v-model.number="form.price" type="number" step="0.01" placeholder="Price" required />
          <input v-model.number="form.stock" type="number" placeholder="Stock" required />
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
            <th>Description</th>
            <th>Price</th>
            <th>Stock</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="product in productsList" :key="product.id">
            <td>{{ product.id }}</td>
            <td>{{ product.name }}</td>
            <td>{{ product.description }}</td>
            <td>${{ product.price.toFixed(2) }}</td>
            <td>{{ product.stock }}</td>
            <td class="actions">
              <button @click="editProduct(product)" class="btn-edit">Edit</button>
              <button @click="deleteProduct(product.id)" class="btn-delete">Delete</button>
            </td>
          </tr>
          <tr v-if="productsList.length === 0">
            <td colspan="6" style="text-align: center; padding: 20px;">No products yet</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { products as api } from '@/services/api';
import axios from 'axios';

const productsList = ref([]);
const form = ref({ name: '', description: '', price: 0, stock: 0 });
const editId = ref(null);

const getHeaders = () => {
  const token = localStorage.getItem('token');
  return {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  };
};

const loadProducts = async () => {
  try {
    const { data } = await axios.get('http://localhost:5119/api/products', getHeaders());
    productsList.value = data;
  } catch (error) {
    console.error('Error loading products:', error);
  }
};

const saveProduct = async () => {
  try {
    if (editId.value) {
      await axios.put(`http://localhost:5119/api/products/${editId.value}`, form.value, getHeaders());
      editId.value = null;
    } else {
      await axios.post('http://localhost:5119/api/products', form.value, getHeaders());
    }
    form.value = { name: '', description: '', price: 0, stock: 0 };
    loadProducts();
  } catch (error) {
    console.error('Error saving product:', error);
  }
};

const editProduct = (product) => {
  form.value = { ...product };
  editId.value = product.id;
};

const cancelEdit = () => {
  form.value = { name: '', description: '', price: 0, stock: 0 };
  editId.value = null;
};

const deleteProduct = async (id) => {
  if (confirm('Are you sure you want to delete this product?')) {
    try {
      await axios.delete(`http://localhost:5119/api/products/${id}`, getHeaders());
      loadProducts();
    } catch (error) {
      console.error('Error deleting product:', error);
    }
  }
};

onMounted(loadProducts);
</script>

<style scoped>
.products-container {
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