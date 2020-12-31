<template>
  <v-container>
    <Snow/>
    <v-container class="grey lighten-5">
      <v-row justify="center" align="center">
        <v-col cols="12" sm="6" class="ma-7">
          <SickSanta />
        </v-col>
        <v-col cols="12" sm="5" class="ma-2">
          <Countdown date="2021-12-25" />
        </v-col>
      </v-row>
    </v-container>
    <v-container>
      <v-row class="text-center align-center" justify="center">
        <v-col class="entry-text mt-8" cols="12" md="6"
          ><h1 class="text-wrap">
            Santa is out <span class="gradient">sick</span>!
          </h1></v-col
        >
      </v-row>
      <v-row class="text-center align-center" justify="center">
        <v-col cols="6" md="4">
          <p class="text-wrap quiet-text">
            Santa's out sick, so we're in charge.
          </p>
          <p class="text-wrap quiet-text">
            Pop each name and their details in, and let's spread some festive
            cheer!
          </p></v-col
        ></v-row
      >
      <v-row class="text-center align-center" justify="center"
        ><v-col cols="6" md="4"
          ><v-btn elevation="2" x-large rounded color="primary"
            to="form">Get started</v-btn
          ></v-col
        ></v-row
      >
    </v-container>
  </v-container>
</template>

<script>
import Countdown from "../components/Countdown";
import SickSanta from "../components/SickSanta";
import Snow from "../components/Snow";

export default {
  name: "Home",

  components: {
    Countdown,
    SickSanta,
    Snow
  },

  data: () => ({
    //
  })
};
</script>

<style lang="scss" scoped>
.quiet-text {
  font-family: poppins, sans-serif;
  font-weight: 500;
  font-style: normal;
  font-size: 1rem;
  color: #666666;
}
</style>
