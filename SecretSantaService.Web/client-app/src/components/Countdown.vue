<template id="countdown-template">
  <v-card elevation="2" color="primary" class="countdown pa-7 text-center">
    <div class="block">
      <p class="digit countdown-text">
        {{ days | two_digits }} <span class="text countdown-text">Days</span>
      </p>
      <p class="countdown-text event">'til Christmas!</p>
    </div>
  </v-card>
</template>

<style lang="scss">
.countdown-text {
  font-family: poppins, sans-serif;
  font-weight: 500;
  text-transform: uppercase;
  font-size: 3rem;
  color: white;

  &.event {
    font-size: 1rem;
  }
}
</style>

<script>
import Vue from "vue";

export default {
  mounted() {
    window.setInterval(() => {
      this.now = Math.trunc(new Date().getTime() / 1000);
    }, 1000);
  },
  props: {
    date: {
      type: String
    }
  },
  data() {
    return {
      now: Math.trunc(new Date().getTime() / 1000)
    };
  },
  computed: {
    dateInMilliseconds() {
      return Math.trunc(Date.parse(this.date) / 1000);
    },
    seconds() {
      return (this.dateInMilliseconds - this.now) % 60;
    },
    minutes() {
      return Math.trunc((this.dateInMilliseconds - this.now) / 60) % 60;
    },
    hours() {
      return Math.trunc((this.dateInMilliseconds - this.now) / 60 / 60) % 24;
    },
    days() {
      return Math.trunc((this.dateInMilliseconds - this.now) / 60 / 60 / 24);
    }
  }
};

Vue.filter("two_digits", (value) => {
  if (value < 0) {
    return "00";
  }
  if (value.toString().length <= 1) {
    return `0${value}`;
  }
  return value;
});
</script>
