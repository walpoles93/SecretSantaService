<template>
  <v-container>
    <v-form v-model="isValid">
      <v-row class="text-center align-center" justify="center">
        <v-col class="entry-text mt-8" cols="12" md="6">
          <h1 class="text-wrap heading-entry">
            Plan your <span class="gradient">party</span>!
          </h1>
        </v-col>
      </v-row>
      <v-row justify="center" align="center">
        <v-col class="mt-8" cols="12" md="6">
          <v-container fluid>
            <v-row>
              <v-col cols="12" sm="6">
                <v-text-field
                  color="Primary darken-2"
                  label="Party name"
                  v-model="name"
                  :rules="[v => !!v || 'Name must not be empty']"
                  required
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6">
                <v-menu
                  ref="menu"
                  v-model="menu"
                  :rules="[v => !!v || 'Date must not be empty']"
                  :close-on-content-click="false"
                  :return-value.sync="date"
                  transition="scale-transition"
                  offset-y
                  min-width="290px"
                >
                  <template v-slot:activator="{ on, attrs }">
                    <v-text-field
                      v-model="date"
                      label="Date"
                      prepend-icon="mdi-calendar"
                      readonly
                      v-bind="attrs"
                      v-on="on"
                    ></v-text-field>
                  </template>
                  <v-date-picker v-model="date" no-title scrollable>
                    <v-spacer></v-spacer>
                    <v-btn text color="primary" @click="menu = false">
                      Cancel
                    </v-btn>
                    <v-btn
                      text
                      color="primary"
                      @click="$refs.menu.save(date)"
                    >
                      OK
                    </v-btn>
                  </v-date-picker>
                </v-menu>
              </v-col>
            </v-row>
          </v-container>
        </v-col>
      </v-row>
      <v-row justify="center" align="center">
        <v-col cols="12" md="6">
          <v-row justify="space-between">
            <v-col class="heading-text" cols="12" md="6">
              <p class="text-wrap">Add Participants</p>
            </v-col>
            <v-col class="text-right mb-4" cols="12" md="6"
              ><v-btn
                @click="onClickAdd"
                color="primary"
                elevation="2"
                fab
                x-large
                ><v-icon>mdi-plus</v-icon>
              </v-btn>
            </v-col>
          </v-row>

          <PersonCard
            v-for="(person, index) in partyMembers"
            :key="index"
            v-model="partyMembers[index]"
            @delete="onDelete(index)"
          />

          <v-btn 
            block 
            color="primary" 
            :disabled="!isValid || !allPersonCardsValid" 
            @click="onClickCreateParty"
            :loading="isSaving"
          >
            Create Party
          </v-btn>
        </v-col>
      </v-row>
    </v-form>

    <v-snackbar v-model="hasError" color="error">
      An error ocurred while creating your party. Please check your information and try again.
    </v-snackbar>
  </v-container>
</template>

<script>
import PersonCard from '../components/PersonCard';
export default {
  name: 'Form',
  data: () => ({
    name: '',
    date: new Date().toISOString().substr(0, 10),
    menu: false,
    partyMembers: [{ name: '', address: '', email: '' }],
    isValid: false,
    isSaving: false,
    hasError: false,
  }),
  methods: {
    onClickAdd() {
      this.partyMembers.push({ name: '', address: '', email: '' });
    },
    onDelete(index) {
      const partyMembers = this.partyMembers;
      this.partyMembers = [...partyMembers.slice(0, index), ...partyMembers.slice(index + 1)];
    },
    async onClickCreateParty() {
      this.isSaving = true

      try {
        await this.$axios.post('/api/parties', {
          name: this.name,
          date: this.date,
          partyMembers: this.partyMembers,
        })

        this.$router.push({
          name: 'confirmation',
          query: { name: this.name }
        })
      } catch {
        this.hasError = true
      }

      this.isSaving = false
    },
  },
  computed: {
    allPersonCardsValid() {
      return this.partyMembers
        .map(p => !!p.name && !!p.address && !!p.email)
        .every(p => p)
    },
  },
  components: {
    PersonCard
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
