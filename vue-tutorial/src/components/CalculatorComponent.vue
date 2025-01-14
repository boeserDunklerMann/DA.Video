<template>
  <h3>Calculator</h3>
  <form v-on:submit.prevent="add">
    <input type="number" v-model="num1" />
    <input type="number" v-model="num2" />
    <button type="submit">Add</button>
  </form>
  <p>Result {{ sum }}</p>
</template>

<script>
  export default {
    data() {
      return {
        num1: 0,
        num2: 0,
      };
    },
    computed: {
      sum() {
        return parseInt(this.num1, 10) + parseInt(this.num2, 10);
      }
    }
  }
</script>

<style>
</style>
