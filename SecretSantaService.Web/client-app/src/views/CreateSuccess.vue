<template>
  <v-card flat color="rgb(255, 255, 255, 0.8)">
    <v-card-title> 
      Party Details
    </v-card-title>
    <v-card-subtitle>
      Send each of the below links to each respective party member
    </v-card-subtitle>

    <v-list>
      <v-list-item two-line v-for="(member, index) in partyMembers" :key="index">
        <v-list-item-content>
          <v-list-item-title>{{ member.name }}</v-list-item-title>
          <v-list-item-subtitle>
            <a :href="getLink(member)" target="_blank">
              {{ getLink(member) }}
            </a>
          </v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>
    </v-list>

    <v-card-actions>
      <v-btn depressed outlined @click="$router.push({ name: 'home' })">
        <v-icon left>mdi-home-outline</v-icon>
        Home  
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
export default {
  data() {
    return {
      partyMembers: []
    }
  },
  async mounted() {
    try {
      const { partyName } = this.$route.query
      const response = await this.$axios.get(`/api/parties/${partyName}`)
      const { partyMembers } = response.data

      this.partyMembers = partyMembers
    } catch {
      this.$router.push({ name: 'home' })
    }
  },
  methods: {
    getLink({ partyName, pairingIdentifier }) {
      const { host } = window.location

      return `https://${host}/view?partyName=${partyName}&pairingIdentifier=${pairingIdentifier}`
    }
  }
}
</script>