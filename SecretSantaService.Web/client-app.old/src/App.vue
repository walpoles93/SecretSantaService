<template>
  <v-app app>
    <span class="bg"></span>
    <v-container fluid fill-height>
      <v-row justify="center">
        <v-col cols="12" sm="6" md="4">
          <router-view/>
        </v-col>
      </v-row>
    </v-container>
  </v-app>
</template>

<style scoped>
.bg {
    width: 100%;
    height: 100%;
    position: absolute;
    top: 0;
    left: 0;
    background: url( '~@/assets/bg.jpg') no-repeat center center;
    background-size: cover;
    background-color: white;
  }
</style>