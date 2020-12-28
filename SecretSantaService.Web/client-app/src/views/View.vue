<template>
  <v-card flat color="rgb(255, 255, 255, 0.8)">
    <v-card-title> 
      Welcome {{ donorName }}
    </v-card-title>

    <v-card-subtitle>
      You have been invited to a Secret Santa Party ({{ partyName }}) taking place on {{ formattedPartyDate }}
    </v-card-subtitle>

    <v-card-text>
      <p>
        Your recipient is {{ recipientName }}
      </p>
      <p>
        Please send their gift to {{ recipientAddress }}
      </p>
    </v-card-text>

    <v-card-actions>
      <v-btn depressed outlined @click="$router.push({ name: 'home' })">
        <v-icon left>mdi-home-outline</v-icon>
        Home
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import { parseISO, format } from 'date-fns'

export default {
  data() {
    return {
      partyName: '',
      partyDate: '',
      donorName: '',
      recipientName: '',
      recipientAddress: ''
    }
  },
  async mounted() {
    try {
      const { partyName, pairingIdentifier } = this.$route.query
      const response = await this.$axios.get(`/api/pairings/${partyName}/${pairingIdentifier}`)
      const { data } = response

      this.partyName = data.partyName
      this.partyDate = data.partyDate
      this.donorName = data.donorName
      this.recipientName = data.recipientName
      this.recipientAddress = data.recipientAddress
    } catch {
      this.$router.push({ name: 'home' })
    }
  },
  computed: {
    formattedPartyDate() {
      const date = parseISO(this.partyDate)

      return format(date, 'dd/MM/yyyy')
    }
  }
}
</script>
