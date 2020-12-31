<template>
  <v-container>
      <Snow/>
    <v-row class="text-center align-center" justify="center">
      <v-col class="entry-text mt-8" cols="12" md="6"
        ><h1 class="text-wrap heading-entry">
          Your notes have been <span class="gradient">sent</span>!
        </h1></v-col
      >
    </v-row>

    <v-row class="text-center align-center mt-4" justify="center">
      <HappySanta/>
    </v-row>

    <v-row class="text-center align-center" justify="center">
      <v-col class="entry-text mt-8" cols="12" md="6">
        Wanna snoop?
      </v-col>
    </v-row>
    <v-row class="text-center align-center" justify="center">
      <v-btn 
        elevation="2" 
        x-large 
        rounded 
        color="primary"
        :to="{ name: 'pairings', query: { name } }"
      > View pairings </v-btn>
    </v-row>
  </v-container>
</template>
<script>
import HappySanta from "../components/HappySanta";
import Snow from "../components/Snow"

export default {
  name: "Confirmation",
  components: {
    HappySanta,
    Snow
  },
  async mounted() {
    const { name } = this.$route.query
    if(!name) {
      this.$router.push('form')
    }
  },
  computed: {
    name() {
      return this.$route.query.name
    }
  }
};
</script>
