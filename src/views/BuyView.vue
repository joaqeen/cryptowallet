<script setup>
import { CRYPTOS } from '@/constants/cryptos' //Importamos las criptomonedas para el select del formulario
import { Field, Form, ErrorMessage } from 'vee-validate'
import * as yup from 'yup'

const schema = yup.object({
  //Esquema de validacion para el formulario de compra
  crypto: yup.string().required('Seleccione una criptomoneda'),
  quantity: yup
    .number()
    .positive('La cantidad debe ser mayor que cero')
    .required('Ingrese la cantidad a comprar'),
  date: yup
    .date()
    .typeError('Seleccione una fecha válida')
    .required('Seleccione la fecha de compra'),
})

const onSubmit = (values) => {
  console.log('Formulario enviado con los siguientes valores:', values)
  //agregar logica para procesar la compra
}
</script>

<template>
  <h1 class="text-3xl font-bold m-6 ml-8 underline">Compra de Cryptomoneda</h1>

  <Form :validation-schema="schema" @submit="onSubmit">
    <div class="w-80 ml-8">
      <label class="label-text" for="selectCripto">Cryptomoneda:</label>
      <Field
        as="select"
        class="select select-sm"
        aria-label="select"
        id="selectCripto"
        name="crypto"
      >
        <option v-for="crypto in CRYPTOS" :key="crypto.code" :value="crypto.code">
          {{ crypto.name }}
        </option>
      </Field>
      <ErrorMessage name="crypto" class="text-red-500 text-sm mt-1" />
    </div>

    <div class="w-80 ml-8">
      <label class="label-text" for="quantityCripto">Cantidad:</label>
      
      <Field
        type="number"
        name="quantity"
        class="input input-sm"
        placeholder="Cantidad a comprar"
        id="quantityCripto"
      />
      <ErrorMessage name="quantity" class="text-red-500 text-sm mt-1" />
    </div>

    <div class="w-80 ml-8">
      <label class="label-text" for="dateCripto">Seleccione la fecha de compra:</label>

      <Field
        type="date"
        name="date"
        class="input input-sm"
        placeholder="YYYY-MM-DD"
        id="dateCripto"
      />
        <ErrorMessage name="date" class="text-red-500 text-sm mt-1" />
    </div>

    <button type="submit" class="btn m-8 w-80 btn-sm btn-gradient btn-primary rounded-full">
      Comprar
    </button>
  </Form>
</template>
