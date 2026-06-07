<script setup>
</script>

<template>
  <nav class="navbar rounded-box shadow-sm bg-base-300">
  <div class="w-full md:flex md:items-center md:gap-2">
    <div class="flex items-center justify-between">
      <div class="navbar-start items-center justify-between max-md:w-full">
        <RouterLink to ="/wallet" class="flex items-center gap-5">
          <img src="/src/assets/icons/bitcoin-wallet.svg" alt="Logo" class="w-20 h-10 invert" />
          <span class="font-bold text-base">CryptoWallet</span>
        </RouterLink>
        <div class="md:hidden">
          <button type="button" class="collapse-toggle btn btn-outline btn-secondary btn-sm btn-square" data-collapse="#logo-navbar-collapse" aria-controls="logo-navbar-collapse" aria-label="Toggle navigation" >
            <span class="icon-[tabler--menu-2] collapse-open:hidden size-4"></span>
            <span class="icon-[tabler--x] collapse-open:block hidden size-4"></span>
          </button>
        </div>
      </div>
    </div>
    <div id="logo-navbar-collapse" class="md:navbar-end collapse hidden grow basis-full overflow-hidden transition-[height] duration-300 max-md:w-full" >
      <ul class="menu md:menu-horizontal gap-2 p-0 text-base max-md:mt-2">
        <li><RouterLink to ="/wallet">Billetera</RouterLink></li>
        <li><RouterLink to="/buy">Comprar</RouterLink></li>
        <li><RouterLink to="/sell">Vender</RouterLink></li>
        <li><RouterLink to="/userRegister">Registrar Usuario</RouterLink></li>
      </ul>
    </div>
  </div>
</nav>
</template>