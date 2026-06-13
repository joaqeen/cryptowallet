<script setup>
import api from '@/services/api.js'
import { ref, onMounted } from 'vue'
import * as yup from 'yup'
import { Field, Form, ErrorMessage } from 'vee-validate'
import { useRouter, useRoute } from 'vue-router'
import {CRYPTOS} from '@/constants/cryptos.js'

const USERS = ref([])
const router = useRouter()
const route = useRoute()
const transactionId = route.params.id
const TRANSACTION = ref({})
const formUser = ref('')
const formCrypto = ref("")
const formQuantity = ref(0)
const formDate = ref("")

onMounted(async () => {
  const response = await api.get('/client')
  USERS.value = response.data

  const response2 = await api.get('/transaction/' + transactionId)
  TRANSACTION.value = response2.data

  formUser.value = TRANSACTION.value.idCliente;
  formCrypto.value = TRANSACTION.value.codCrypto;
  formQuantity.value = TRANSACTION.value.cantidadCrypto;
  formDate.value = TRANSACTION.value.fecha;
})

async function onSubmit() {
  const body = {
    id: transactionId,
    accion: TRANSACTION.value.accion,
    codCrypto: formCrypto.value,
    idCliente: formUser.value,
    cantidadCrypto: formQuantity.value,
    fecha: formDate.value,
  }

  await api.patch('/transaction/' + transactionId, body)
  alert('Transacción editada con éxito. Redirigiendo a la página principal...');

  router.push('/')

}


const schema = yup.object({
  //Esquema de validacion para el formulario de compra
  crypto: yup.string().required('Seleccione una criptomoneda'),
  quantity: yup
    .number()
    .positive('La cantidad debe ser mayor que cero')
    .required('Ingrese la cantidad'),
  date: yup.date().typeError('Seleccione una fecha válida').required('Seleccione una fecha'),
  user: yup.string().required('Seleccione un usuario'),
})


</script>

<template>
 <div
    class="flex h-auto min-h-screen items-center justify-center overflow-x-hidden bg-[url('https://cdn.flyonui.com/fy-assets/blocks/marketing-ui/auth/auth-background-2.png')] bg-cover bg-center bg-no-repeat py-10"
  >
    <div class="relative flex items-center justify-center px-4 sm:px-6 lg:px-8">
      <div class="absolute">
        <svg
          width="2000"
          height="2000"
          viewBox="0 0 612 697"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            d="M360.405 111.996C393.955 67.9448 456.863 59.4318 500.914 92.9818V92.9818C544.965 126.532 553.478 189.44 519.928 233.491L250.545 587.191C216.995 631.243 154.087 639.756 110.036 606.206V606.206C65.9845 572.656 57.4716 509.747 91.0216 465.696L360.405 111.996Z"
            fill="url(#paint0_linear)"
            fill-opacity="0.08"
          />
          <path
            d="M360.405 111.996C393.955 67.9448 456.863 59.4318 500.914 92.9818V92.9818C544.965 126.532 553.478 189.44 519.928 233.491L250.545 587.191C216.995 631.243 154.087 639.756 110.036 606.206V606.206C65.9845 572.656 57.4716 509.747 91.0216 465.696L360.405 111.996Z"
            fill="url(#paint0_linear_13715_136336)"
            fill-opacity="0.08"
          />
          <path
            d="M519.53 233.188L250.147 586.888C216.765 630.72 154.17 639.19 110.339 605.808C66.5071 572.425 58.0367 509.831 91.4194 465.999L360.802 112.299C394.185 68.4674 456.78 59.9969 500.611 93.3796C544.443 126.762 552.913 189.357 519.53 233.188Z"
            stroke="var(--color-primary)"
            stroke-opacity="0.2"
          />
          <defs>
            <linearGradient
              id="paint0_linear"
              x1="500.914"
              y1="92.9818"
              x2="110.036"
              y2="606.206"
              gradientUnits="userSpaceOnUse"
            >
              <stop offset="0" stop-color="var(--color-primary)" />
              <stop offset="1" stop-color="var(--color-primary)" stop-opacity="0.2" />
            </linearGradient>
          </defs>
        </svg>
      </div>
      <div
        class="bg-base-100 shadow-base-300/20 z-1 w-full space-y-6 rounded-xl p-6 shadow-md sm:min-w-md lg:p-8"
      >
        <div class="flex items-center gap-3">
          <img src="@/assets/icons/bitcoin-wallet.svg" class="size-8 invert" alt="brand-logo" />
          <h2 class="text-base-content text-xl font-bold">Crypto Wallet</h2>
        </div>
        <div>
          <h3 class="text-base-content mb-1.5 text-2xl font-semibold">Editar Transacción</h3>
        </div>
        <div class="space-y-4">
          
          <Form :validation-schema="schema" @submit="onSubmit" class="mb-4 space-y-4">
            <div>
              <label class="label-text" for="selectUser">Usuario</label>
              <Field as="select" class="select" id="selectUser" name="user" v-model="formUser">
                <option v-for="user in USERS" :key="user.id" :value="user.id">  
                  {{ user.nombre }}
                </option>
              </Field>
              <ErrorMessage name="user" class="text-red-500 text-sm mt-1" />
            </div>
            <div>
              <label class="label-text" for="selectCripto">Criptomoneda</label>
              <Field
                as="select"
                class="select"
                id="selectCripto"
                name="crypto"
                v-model="formCrypto"
              >
                <option v-for="crypto in CRYPTOS" :key="crypto.code" :value="crypto.code">
                  {{ crypto.name }}
                </option>
              </Field>
              <ErrorMessage name="crypto" class="text-red-500 text-sm mt-1" />
            </div>
            <div>
              <label class="label-text" for="quantityCripto">Cantidad</label>
              <Field
                type="number"
                name="quantity"
                class="input"
                placeholder="Cantidad"
                id="quantityCripto"
                v-model="formQuantity"
              />
              <ErrorMessage name="quantity" class="text-red-500 text-sm mt-1" />
            </div>

            <div>
              <label class="label-text" for="dateCripto">Fecha</label>
              <Field type="datetime-local" name="date" class="input" id="dateCripto" v-model="formDate" />
              <ErrorMessage name="date" class="text-red-500 text-sm mt-1" />
            </div>
            <button type="submit" class="btn btn-lg btn-primary btn-gradient btn-block">
              Editar Transacción
            </button>
          </Form>

        </div>
      </div>
    </div>
  </div>
</template>
