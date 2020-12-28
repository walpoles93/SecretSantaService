<template>
  <v-form v-model="isFormValid">
    <v-card flat color="rgb(255, 255, 255, 0.8)">
      <v-card-title> Create a Party </v-card-title>

      <v-card-text>
        <v-text-field
          outlined
          label="Party Name"
          v-model="form.name"
          :rules="[v => !!v || 'Party name must not be empty']"
        ></v-text-field>

        <v-menu
          ref="datepicker"
          v-model="isDatePickerOpen"
          :close-on-content-click="false"
          :return-value.sync="form.date"
          transition="scale-transition"
          offset-y
          min-width="290px"
        >
          <template v-slot:activator="{ on, attrs }">
            <v-text-field
              v-model="form.date"
              label="Party Date"
              readonly
              v-bind="attrs"
              v-on="on"
              outlined
              :rules="[v => !!v || 'Party date must not be empty']"
            ></v-text-field>
          </template>
          <v-date-picker
            v-model="form.date"
            no-title
            scrollable
          >
            <v-spacer></v-spacer>
            <v-btn
              text
              color="primary"
              @click="isDatePickerOpen = false"
            >
              Cancel
            </v-btn>
            <v-btn
              text
              color="primary"
              @click="$refs.datepicker.save(form.date)"
            >
              OK
            </v-btn>
          </v-date-picker>
        </v-menu>

        <div v-for="(member, i) in form.partyMembers" :key="i" class="mb-3">
          <div>
            Person {{ i + 1 }}
            <v-btn icon color="error" @click="onClickDelete(member)">
              <v-icon small>mdi-delete-outline</v-icon>
            </v-btn>
          </div>
          <v-text-field
            label="Name"
            v-model="member.name"
            hide-details="auto"
            :rules="[v => !!v || 'Name must not be empty']"
            class="pt-0 mt-0"
          ></v-text-field>
          <v-text-field
            label="Email"
            v-model="member.email"
            hide-details="auto"
            :rules="[v => (!!v) || 'Email must not be empty']"
          ></v-text-field>
          <v-text-field
            label="Address"
            v-model="member.address"
            hide-details="auto"
            :rules="[v => (!!v) || 'Address must not be empty']"
          ></v-text-field>
        </div>
        <v-btn depressed block color="primary" @click="onClickAdd">
          Add another member
        </v-btn>
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
          :loading="isSaving"
          @click="onClickSave"
        >
          Save
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
        name: '',
        date: '',
        partyMembers: [
          {
            name: '',
            email: '',
            address: ''
          }
        ]
      },
      isSaving: false,
      isFormValid: false,
      isDatePickerOpen: false,
      hasResponseError: false,
      responseError: ''
    }
  },
  methods: {
    onClickAdd() {
      this.form.partyMembers.push({
        name: '',
        email: '',
        address: ''
      })
    },
    onClickDelete(member) {
      const { partyMembers }  = this.form
      const index = partyMembers.indexOf(member)
      this.form.partyMembers = [...partyMembers.slice(0, index), ...partyMembers.slice(index + 1)]
    },
    async onClickSave() {
      this.isSaving = true

      try {
        await this.$axios.post('/api/parties', this.form)
        this.$router.push({
          name: 'create-success',
          query: {
            partyName: this.form.name
          }
        })
      } catch (error) {
        this.handleResponseError(error)
      }

      this.isSaving = false
    },
    handleResponseError({ response }) {
      this.responseError = response.data.title
      this.hasResponseError = true
    }
  }
}
</script>
