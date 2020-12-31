<template>
  <v-card class="overflow-hidden mb-4" color="primary lighten-1" dark>
    <v-toolbar class="card" flat color="primary">
      <v-icon>mdi-account</v-icon>
      <v-toolbar-title class="font-weight-light">
        {{ value.name }}
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn
        medium
        color="primary darken-3"
        fab
        @click="isEditing = !isEditing"
      >
        <v-icon medium v-if="isEditing">
          mdi-close
        </v-icon>
        <v-icon medium v-else>
          mdi-pencil
        </v-icon>
      </v-btn>
    </v-toolbar>
    <v-card-text>
      <v-form v-model="isValid" class="valid">
          <v-text-field
            :disabled="!isEditing"
            color="white"
            label="Name"
            :value="value.name"
            @input="onUpdate('name', $event)"
            :rules="[v => !!v || 'Name must not be empty']"
            required
          ></v-text-field>
          <v-text-field
            :disabled="!isEditing"
            color="white"
            label="Address"
            :value="value.address"
            @input="onUpdate('address', $event)"
            :rules="[v => !!v || 'Address must not be empty']"
            required
          ></v-text-field>
          <v-text-field
            class="valid"
            :disabled="!isEditing"
            color="white"
            label="Email"
            :value="value.email"
            @input="onUpdate('email', $event)"
            :rules="[v => !!v || 'Email must not be empty']"
            required
          ></v-text-field>
      </v-form>
    </v-card-text>
    <v-divider></v-divider>
    <v-card-actions>
      <v-row class="ma-4" justify="space-between">
        <v-btn color="secondary" class="text-left" @click="onDelete">
          Delete
        </v-btn>
        <v-btn :disabled="!isEditing || !isValid" color="success" @click="save">
          Save
        </v-btn>
      </v-row>
    </v-card-actions>
    <v-snackbar v-model="hasSaved" :timeout="2000" absolute bottom left>
      Participant added!
    </v-snackbar>
  </v-card>
</template>
<style lang="scss">
.valid .v-messages.theme--dark.error--text,
.valid .v-label.theme--dark.error--text {
  color: white !important;
  font-weight: bold;
}

.card .v-toolbar__content {
  height: 84px !important;
}
</style>
<script>
export default {
  name: "PersonCard",
  props: ['value'],
  data() {
    return {
      hasSaved: false,
      isEditing: false,
      isValid: false,
    };
  },

  methods: {
    save() {
      this.isEditing = !this.isEditing;
      this.hasSaved = true;
    },
    submit() {
      this.$refs.observer.validate();
    },
    onDelete() {
      this.$emit("delete");
    },
    onUpdate(key, value) {
      this.$emit('input', { ...this.value, [key]: value })
    }
  }
};

</script>
