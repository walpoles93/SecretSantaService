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

.entry-text {
  font-family: poppins, sans-serif;
  font-weight: 700;
  font-style: normal;
  white-space: pre;
  font-size: 3.5rem;
  line-height: 1;
}

.gradient {
  background: linear-gradient(-45deg, #ff0000, #ff7878, #b2d8b2, #378b29);
  -webkit-background-clip: text;
  background-clip: text;
  -webkit-text-fill-color: transparent;
  text-fill-color: transparent;
  background-size: 200% auto;
  animation: gradient 7s ease-in-out infinite alternate;
}

@keyframes gradient {
  0% {
    background-position: 0% 50%;
  }
  50% {
    background-position: 100% 50%;
  }
  100% {
    background-position: 0% 50%;
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
};
</script>
