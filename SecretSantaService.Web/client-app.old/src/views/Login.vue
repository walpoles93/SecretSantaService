<template>
  <v-form v-model="isFormValid">
    <v-card flat color="rgb(255, 255, 255, 0.8)">
      <v-card-title> 
        Login
      </v-card-title>

      <v-card-text>
        <v-text-field 
          outlined
          label="Party Name"
          v-model="form.partyName"
          :rules="[v => !!v || 'Party name must not be empty']"
        ></v-text-field>
        <v-text-field
          outlined
          label="Unique ID"
          v-model="form.pairingIdentifier"
          :rules="[v => !!v || 'Unique ID must not be empty']"
        ></v-text-field>
      </v-card-text>

      <v-card-actions class="px-4 pb-4">
        <v-btn depressed outlined @click="$router.push({ name: 'home' })">
          <v-icon left>mdi-home-outline</v-icon>
          Home
        </v-btn>
        <v-spacer></v-spacer>
        <v-btn
          depressed 
          color="primary" 
          :disabled="!isFormValid"
          :loading="isLoading"
          @click="onClickLogin"
        >
          Login
        </v-btn>
      </v-card-actions>
    </v-card>

    <v-snackbar v-model="hasResponseError" color="error">
      {{ responseError }}
    </v-snackbar>
  </v-form>
</template>

<script>
export default {
  data() {
    return {
      form: {
        partyName: '',
        pairingIdentifier: ''
      },
      isFormValid: false,
      isLoading: false,
      hasResponseError: false,
      responseError: ''
    }
  },
  methods: {
    async onClickLogin() {
      this.isLoading = true

      try {
        // if response is success then pairing exists
        const { partyName, pairingIdentifier } = this.form
        await this.$axios.get(`/api/pairings/${partyName}/${pairingIdentifier}`)
        this.$router.push({ 
          name: 'view', 
          query: {
            partyName,
            pairingIdentifier
          }
        })
      } catch (error) {
        this.handleResponseError(error)
      }

      this.isLoading = false
    },
    handleResponseError({ response }) {
      this.responseError = response.data.title
      this.hasResponseError = true
    }
  }
}
</script>
