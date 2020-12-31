<template>
  <!-- App.vue -->

  <v-app>
    <v-app-bar app>
      <!-- -->
      <Nav />
    </v-app-bar>

    <!-- Sizes your content based upon application components -->
    <v-main>
      <!-- Provides the application the proper gutter -->
      <v-container fluid>
        <!-- If using vue-router -->
        <router-view></router-view>
      </v-container>
    </v-main>

    <v-footer app>
      <!-- -->
    </v-footer>
  </v-app>
</template>

<style lang="scss">
@import url("https://use.typekit.net/pxo2fqf.css");
.brand {
  font-family: poppins, sans-serif;
  font-weight: 500;
  font-style: normal;
  font-size: 60px;
  letter-spacing: -8px;
  color: #313639 !important;
  .brand--dot {
    color: #c4342d;
  }

}
</style>

<script>
import Nav from "./components/Nav";

export default {
  name: "App",

  components: {
    Nav
  },

  data: () => ({
    //
  })
};
</script>
