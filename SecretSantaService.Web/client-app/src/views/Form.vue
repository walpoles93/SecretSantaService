<template>
  <v-container>
    <v-container>
      <v-row class="text-center align-center" justify="center">
        <v-col class="entry-text mt-8" cols="12" md="6"
          ><h1 class="text-wrap heading-entry">
            Plan your <span class="gradient">party</span>!
          </h1></v-col
        >
      </v-row>
      <v-row justify="center" align="center">
        <v-col class="mt-8" cols="12" md="6">
          <v-form ref="form" @submit.prevent="submit">
            <v-container fluid>
              <v-row>
                <v-col cols="12" sm="6">
                  <v-text-field
                    color="Primary darken-2"
                    label="Party name"
                    required
                  ></v-text-field></v-col
                ><v-col cols="12" sm="6"
                  ><v-menu
                    ref="menu"
                    v-model="menu"
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
                </v-col></v-row
              ></v-container
            ></v-form
          ></v-col
        >
      </v-row>
      <v-row justify="center" align="center">
        <v-col cols="12" md="6">
          <v-row justify="space-between">
            <v-col class="heading-text" cols="12" md="6"
              ><p class="text-wrap">Add Participants</p></v-col
            >
            <v-col class="text-right mb-4" cols="12" md="6"
              ><v-btn
                @click="onClickAdd"
                color="primary"
                elevation="2"
                fab
                x-large
                ><v-icon>mdi-plus</v-icon></v-btn
              ></v-col
            ></v-row
          >
          <PersonCard
            v-for="(person, index) in people"
            :key="index"
            v-model="people[index]"
            @delete="onDelete(index)"
          />
        </v-col>
      </v-row>
    </v-container>
  </v-container>
</template>
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
<script>
import PersonCard from "../components/PersonCard";
export default {
  name: "Form",

  data: () => ({
    date: new Date().toISOString().substr(0, 10),
    menu: false,
    modal: false,
    menu2: false,
    people: [{ name: "", address: "", email: "" }]
  }),
  methods: {
    onClickAdd() {
      this.people.push({ name: "", address: "", email: "" });
    },
    onDelete(index) {
      const people = this.people;
      this.people = [...people.slice(0, index), ...people.slice(index + 1)];
    }
  },
  components: {
    PersonCard
  }
};
</script>
