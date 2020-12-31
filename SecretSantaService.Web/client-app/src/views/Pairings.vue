<template>
  <v-container>
    <Snow/>
    <v-container>
      <v-row class="text-center align-center" justify="center">
        <v-col class="entry-text mt-8" cols="12" md="6"
          ><h1 class="text-wrap">
            Sneak <span class="gradient">peak</span>!
          </h1>
        </v-col>
      </v-row>
      <v-row class="text-center align-center" justify="center">
        <v-col cols="6" md="4">
          <p v-for="(pairing, index) in pairings" :key="index">
            {{ pairing.donorName }} is buying a gift for {{ pairing.recipientName }}
          </p>
        </v-col>
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
  }
}
</script>