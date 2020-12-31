<template>
  <v-container>
    <v-row class="text-center align-center">
      <div>
        <router-link class="text-decoration-none brand" to="/"
          >Shh<span class="brand--dot">.</span></router-link
        >
      </div>
      <v-spacer></v-spacer>
      <v-btn elevation="2" rounded color="primary" to="form">Get started</v-btn>
    </v-row>
  </v-container>
</template>

<script>
export default {
  name: "Nav"
};
</script>
