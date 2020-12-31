<template>
  <v-card class="overflow-hidden mb-4" color="primary lighten-1" dark>
    <v-toolbar class="card" flat color="primary">
      <v-icon>mdi-account</v-icon>
      <v-toolbar-title class="font-weight-light">
        {{ name }}
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
      <form class="valid" @submit.prevent="submit">
        <validation-provider
          v-slot="{ errors }"
          name="Name"
          rules="required|max:24"
        >
          <v-text-field
            :disabled="!isEditing"
            color="white"
            label="Name"
            :error-messages="errors"
            v-model="name"
            required
          ></v-text-field
        ></validation-provider>
        <validation-provider
          v-slot="{ errors }"
          name="email"
          rules="required|email"
        >
          <v-text-field
            :disabled="!isEditing"
            color="white"
            label="Address"
            :error-messages="errors"
            v-model="address"
            required
          ></v-text-field>
        </validation-provider>
        <validation-provider
          v-slot="{ errors }"
          name="email"
          rules="required|email"
        >
          <v-text-field
            class="valid"
            :disabled="!isEditing"
            color="white"
            label="Email"
            v-model="email"
            :error-messages="errors"
            required
          ></v-text-field>
        </validation-provider>
      </form>
    </v-card-text>
    <v-divider></v-divider>
    <v-card-actions>
      <v-row class="ma-4" justify="space-between">
        <v-btn color="secondary" class="text-left" @click="onDelete">
          Delete
        </v-btn>
        <v-btn :disabled="!isEditing" color="success" @click="save">
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
import { required, digits, email, max, regex } from "vee-validate/dist/rules";
import { extend, ValidationProvider, setInteractionMode } from "vee-validate";

export default {
  name: "PersonCard",
  components: {
    ValidationProvider
  },
  props: {
    name: String,
    address: String,
    email: String
  },
  data() {
    return {
      hasSaved: false,
      isEditing: null,
      model: null
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
    clear() {
      this.name = "";
      this.email = "";
      this.address = "";
      this.$refs.observer.reset();
    }
  }
};

setInteractionMode("eager");

extend("digits", {
  ...digits,
  message: "{_field_} needs to be {length} digits. ({_value_})"
});

extend("required", {
  ...required,
  message: "{_field_} cannot be empty"
});

extend("max", {
  ...max,
  message: "{_field_} may not be greater than {length} characters"
});

extend("regex", {
  ...regex,
  message: "{_field_} {_value_} does not match {regex}"
});

extend("email", {
  ...email,
  message: "Email must be valid"
});
</script>
