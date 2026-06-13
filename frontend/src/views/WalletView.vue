<script setup>
import { ref, onMounted } from 'vue'
import api from '@/services/api.js'
import { useRouter } from 'vue-router'
//import Chart from 'chart.js/auto'

const USERS = ref([])
const TRANSACTIONS = ref([])
const selectedUserId = ref(null)
const showTransactions = ref(false)
const router = useRouter()
//const chartRef = ref(null)

onMounted(async () => {
  const response = await api.get('/client')
  const response2 = await api.get('/transaction')
  USERS.value = response.data
  TRANSACTIONS.value = response2.data

  /*new Chart(chartRef.value, { 
    type: 'doughnut',
    data: {
      labels: ['Red', 'Blue', 'Yellow'],
      datasets: [
        {
          label: 'My First Dataset',
          data: [300, 50, 100],
          backgroundColor: [
            'rgb(255, 99, 132)',
            'rgb(54, 162, 235)',
            'rgb(255, 205, 86)'
          ],
          hoverOffset: 4
        }
      ]
    }
  })*/
})

async function deleteTransaction(id) {
  const confirmacion = confirm('Esta seguro que desea Eliminar esta Transaccion?')
  if (confirmacion) {
    await api.delete('/transaction/' + id)
    TRANSACTIONS.value = TRANSACTIONS.value.filter((trans) => trans.id !== id)
  }
}
</script>

<template>
  <div class="w-96">
    <label class="label-text" for="client">Seleccione un cliente</label>
    <select class="select" id="client" v-model="selectedUserId">
      <!-- guardo el ID del cliente seleccionado en selectedUserId -->
      <option v-for="user in USERS" :key="user.id" :value="user.id">
        {{ user.nombre }}
      </option>
    </select>
  </div>

  <!--<div class="w-[300px] h-[300px]">
    <canvas ref="chartRef"></canvas>
  </div>-->

  <div class="w-full overflow-x-auto">
    <table class="table">
      <thead>
        <tr>
          <th>N°</th>
          <th>Accion</th>
          <th>Fecha</th>
          <th>Crypto</th>
          <th>Cantidad</th>
        </tr>
      </thead>

      <tbody>
        <tr
          class="row-hover"
          v-for="(trans, index) in TRANSACTIONS.filter(
            (trans) => trans.idCliente === selectedUserId,
          ).slice(0, showTransactions ? undefined : 5)"
          :key="trans.idCliente"
        >
          <!--recorro con for las transacciones filtrando por el selectedUserId-->
          <td>{{ index + 1 }}</td>
          <td class="badge badge-soft text-xs">{{ trans.accion }}</td>
          <td>{{ trans.fecha }}</td>
          <td>{{ trans.codCrypto }}</td>
          <td>{{ trans.cantidadCrypto }}</td>
          <td>
            <button
              class="btn btn-circle btn-text btn-sm"
              @click="router.push('/editTransactions/' + trans.id)"
            >
              <span class="icon-[tabler--pencil] size-5"></span>
            </button>
            <button class="btn btn-circle btn-text btn-sm" @click="deleteTransaction(trans.id)">
              <span class="icon-[tabler--trash] size-5"></span>
            </button>
          </td>
        </tr>
      </tbody>
    </table>
    <div v-if="TRANSACTIONS.filter((trans) => trans.idCliente === selectedUserId).length > 5">
      <button @click="showTransactions = true" class="btn w-full btn-circle btn-sm">
        Ver Historial de Transacciones completo
      </button>
    </div>
  </div>
</template>
