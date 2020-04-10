<template>
  <v-app>
    <div
      id="app-surface"
      :class="surfaceClass"
      :style="surfaceStyle"
      @mousedown.prevent="mousedown"
    />
  </v-app>
</template>

<script>
  import UI from "totem-timeline-vue";
  import Queries from "area/queries";

  export default UI(Queries.surface, {
    data() {
      return {
        grid: null,
        left: 0,
        top: 0,
        moving: false,
        moveOriginLeft: 0,
        moveOriginTop: 0
      };
    },
    computed: {
      surfaceClass() {
        return this.moving ? "moving" : null;
      },
      surfaceStyle() {
        let { rows, columns, cellWidth, cellHeight } = this.grid;
        let { left, top } = this;

        return {
          marginLeft: `${left}px`,
          marginTop: `${top}px`,
          width: `${columns * cellWidth}px`,
          height: `${rows * cellHeight}px`,
          gridTemplateRows: `repeat(${rows}, ${cellHeight}px)`,
          gridTemplateColumns: `repeat(${columns}, ${cellWidth}px)`,
          backgroundSize: `${cellWidth}px ${cellHeight}px`
        };
      }
    },
    methods: {
      mousedown(e) {
        e.target.addEventListener("mousemove", this.mousemove);
        e.target.addEventListener("mouseup", this.mouseup);
        window.addEventListener("mouseup", this.mouseup);

        this.moving = true;
        this.moveOriginLeft = e.clientX - this.left;
        this.moveOriginTop = e.clientY - this.top;
      },

      mousemove(e) {
        if(!this.moving) return;

        this.left = e.clientX - this.moveOriginLeft;
        this.top = e.clientY - this.moveOriginTop;
      },

      mouseup(e) {
        e.target.removeEventListener("mousemove", this.mousemove);
        e.target.removeEventListener("mouseup", this.mouseup);
        window.removeEventListener("mouseup", this.mouseup);

        this.moving = false;
      }
    }
  });
</script>

<style>
  html, body, #app, .application--wrap {
    height: 100%;
    margin: 0px;
    background-color: #b3e5fc;
    overflow: hidden;
  }

  #app-surface {
    display: grid;
    background: linear-gradient(to right, #b3e5fc 1px, transparent 1px), linear-gradient(to bottom, #b3e5fc 1px, transparent 1px);
    background-color: white;
  }

  #app-surface.moving {
    cursor: grabbing;
  }
</style>