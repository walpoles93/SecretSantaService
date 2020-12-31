<template>
  <v-container>
    <Snow/>
    <v-container>
      <v-row class="text-center align-center" justify="center">
        <v-col class="entry-text mt-8" cols="12" md="6"
          ><h1 class="text-wrap heading-entry">
            Secret <span class="gradient">santa</span>
          </h1>
        </v-col>
      </v-row>
      <v-row class="text-center align-center" justify="center">
        <v-col cols="6" md="4">
          <p class="pairings-text overline ma-6" v-for="(pairing, index) in pairings" :key="index">
            {{ pairing.donorName }} is buying a gift for {{ pairing.recipientName }}
          </p>
        </v-col>
      </v-row>
              <v-row class="text-center align-center mt-4" justify="center">
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
  </v-container>
</template>

<script>
import Snow from '../components/Snow'

export default {
  components: {
    Snow,
  },
  data() {
    return {
      partyName: '',
      pairings: [],
      isSending: false
    }
  },
  async mounted() {
    const { name } = this.$route.query

    try {
      const { data } = await this.$axios.get(`/api/pairings?partyName=${name}`)
      this.partyName = data.partyName
      this.pairings = data.pairings
    } catch {
      this.$router.push('form')
    }
  },
  methods: {
    async sendEmail() {
      this.isSending = true

      try {
        await this.$axios.post('/api/parties/emailpartymembers', { partyName: this.partyName })
        this.isSending = false
      } catch {
        this.isSending = false
      }
    }
  }
}
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