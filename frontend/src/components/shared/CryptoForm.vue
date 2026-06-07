<script setup>
import { CRYPTOS } from '../../constants/cryptos.js' //Importamos las criptomonedas para el select del formulario
import { Field, Form, ErrorMessage } from 'vee-validate'
import * as yup from 'yup'

const props = defineProps({
  title: String,
  buttonText: String,
})

const schema = yup.object({
  //Esquema de validacion para el formulario de compra
  crypto: yup.string().required('Seleccione una criptomoneda'),
  quantity: yup
    .number()
    .positive('La cantidad debe ser mayor que cero')
    .required('Ingrese la cantidad'),
  date: yup.date().typeError('Seleccione una fecha válida').required('Seleccione una fecha'),
})

const onSubmit = (values) => {
  console.log('Formulario enviado con los siguientes valores:', values)
  //agregar logica para procesar la compra
}
</script>

<template>
  <div
    class="flex h-auto min-h-screen items-center justify-center overflow-x-hidden bg-[url('https://cdn.flyonui.com/fy-assets/blocks/marketing-ui/auth/auth-background-2.png')] bg-cover bg-center bg-no-repeat py-10"
  >
    <div class="relative flex items-center justify-center px-4 sm:px-6 lg:px-8">
      <div class="absolute">
        <svg width="612" height="697" viewBox="0 0 612 697" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path d="M360.405 111.996C393.955 67.9448 456.863 59.4318 500.914 92.9818V92.9818C544.965 126.532 553.478 189.44 519.928 233.491L250.545 587.191C216.995 631.243 154.087 639.756 110.036 606.206V606.206C65.9845 572.656 57.4716 509.747 91.0216 465.696L360.405 111.996Z" fill="url(#paint0_linear)" fill-opacity="0.08"/>
          <defs>
            <linearGradient id="paint0_linear" x1="500.914" y1="92.9818" x2="110.036" y2="606.206" gradientUnits="userSpaceOnUse">
              <stop offset="0" stop-color="var(--color-primary)"/>
              <stop offset="1" stop-color="var(--color-primary)" stop-opacity="0.2"/>
            </linearGradient>
          </defs>
        </svg>
      </div>
      <div class="bg-base-100 shadow-base-300/20 z-1 w-full space-y-6 rounded-xl p-6 shadow-md sm:min-w-md lg:p-8">
        <div class="flex items-center gap-3">
          <img src="../../assets/icons/bitcoin-wallet.svg" class="size-8 invert" alt="brand-logo" />
          <h2 class="text-base-content text-xl font-bold">Crypto Wallet</h2>
        </div>
        <div>
          <h3 class="text-base-content mb-1.5 text-2xl font-semibold">{{ props.title }}</h3>
        </div>
        <div class="space-y-4">
          <Form :validation-schema="schema" @submit="onSubmit" class="mb-4 space-y-4">
            <div>
              <label class="label-text" for="selectCripto">Criptomoneda</label>
              <Field as="select" class="select" id="selectCripto" name="crypto">
                <option v-for="crypto in CRYPTOS" :key="crypto.code" :value="crypto.code">
                  {{ crypto.name }}
                </option>
              </Field>
              <ErrorMessage name="crypto" class="text-red-500 text-sm mt-1" />
            </div>
            <div>
              <label class="label-text" for="quantityCripto">Cantidad</label>
              <Field type="number" name="quantity" class="input" placeholder="Cantidad" id="quantityCripto" />
              <ErrorMessage name="quantity" class="text-red-500 text-sm mt-1" />
            </div>
            <div>
              <label class="label-text" for="dateCripto">Fecha</label>
              <Field type="date" name="date" class="input" id="dateCripto" />
              <ErrorMessage name="date" class="text-red-500 text-sm mt-1" />
            </div>
            <button type="submit" class="btn btn-lg btn-primary btn-gradient btn-block">
              {{ props.buttonText }}
            </button>
          </Form>
        </div>
      </div>
    </div>
  </div>
</template>