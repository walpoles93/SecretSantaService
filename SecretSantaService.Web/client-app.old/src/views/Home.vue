<template>
  <v-card flat color="rgb(255, 255, 255, 0.8)">
    <v-card-title class="justify-center"> 
      <h1 class="display-1"> Secret Santa Service </h1>
    </v-card-title>

    <v-card-text class="text-center">
      Easily create a Secret Santa party! Just enter your party details and we'll anonymously choose a recipient for each participant. 
    </v-card-text>

    <v-card-actions>
      <v-flex>
        <v-btn block depressed color="primary" class="mb-1" :to="{ name: 'create' }">
          Create Party
        </v-btn>
        <v-btn block outlined color="primary" :to="{ name: 'login' }">
          Party Login
        </v-btn>
      </v-flex>
    </v-card-actions>
  </v-card>
</template>
