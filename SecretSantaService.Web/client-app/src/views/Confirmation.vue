<template>
  <v-container>
      <Snow/>
    <v-row class="text-center align-center" justify="center">
      <v-col class="entry-text mt-8" cols="12" md="6"
        ><h1 class="text-wrap heading-entry">
          Your names have been <span class="gradient">drawn</span>!
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
    <v-row class="text-center align-center mt-4" justify="center">
      <v-btn 
        elevation="2" 
        x-large 
        rounded 
        color="primary"
        :to="{ name: 'pairings', query: { name } }"
      > View pairings </v-btn>
    </v-row>
        <v-row class="text-center align-center mt-8" justify="center">
      <v-btn 
        elevation="2" 
        x-large 
        rounded 
        color="primary"
        @click="sendEmail"
        :loading="isSending"
      > Send emails</v-btn>
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
  },
  data() {
    return {
      isSending: false
    }
  },
  methods: {
    async sendEmail() {
      this.isSending = true

      try {
        await this.$axios.post('/api/parties/emailpartymembers', { partyName: this.name })
        this.isSending = false
      } catch {
        this.isSending = false
      }
    }
  }
};
</script>
<style lang="scss">
.heading-text {
  font-family: poppins, sans-serif;
  font-weight: 500;
  font-style: normal;
  font-size: 1.5rem;
}

.heading-entry {
  font-size: 4rem !important;
}
</style>