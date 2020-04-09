<template>
  <v-app>
    <div id="app-surface" :style="style" />
  </v-app>
</template>

<script>
  import UI from "totem-timeline-vue";
  import Queries from "area/queries";

  export default UI(Queries.surface, {
    data() {
      return {
        rows: 0,
        columns: 0,
        cellWidth: 0,
        cellHeight: 0
      };
    },
    computed: {
      style() {
        let { rows, columns, cellWidth, cellHeight } = this;

        return {
          gridTemplateRows: `repeat(${rows}, ${cellHeight}px)`,
          gridTemplateColumns: `repeat(${columns}, ${cellWidth}px)`,
          backgroundSize: `${cellWidth}px ${cellHeight}px`
        };
      }
    }
  });
</script>

<style>
  html, body, #app, .application--wrap {
    height: 100%;
    margin: 0px;
  }

  #app {
    background-color: #b3e5fc;
  }

  #app-surface {
    display: grid;
    background: linear-gradient(to right, #b3e5fc 1px, transparent 1px), linear-gradient(to bottom, #b3e5fc 1px, transparent 1px);
    background-color: white;
  }
</style>