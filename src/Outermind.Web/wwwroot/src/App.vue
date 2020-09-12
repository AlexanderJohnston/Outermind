<template>
  <v-app>
    <div
      id="surface"
      :class="surfaceClass"
      :style="surfaceStyle"
      tabindex="0"
      @mousedown.prevent="mousedown"
    >
      <v-sheet
        v-if="newCard"
        id="new-card"
        :style="newCardStyle"
        :elevation="Math.min(24, newCard.elevation)"
        tile
      />

      <app-card
        v-for="card in cards"
        :key="card.id"
        :card="card"
        @selectCard="selectCard"
        @resizeCard="resizeCard"
        @dragCard="dragCard"
      />
    </div>
  </v-app>
</template>

<script>
  import Timeline from 'totem-timeline';
  import Axios from 'axios';
  import SurfaceMath from './surfaceMath';
  import AppCard from './AppCard.vue';

  export default {
    components: { AppCard },
    data() {
      return {
        grid: {
          rows: 50,
          columns: 80,
          cellWidth: 20,
          cellHeight: 20,
        },
        ctrl: false,
        top: 0,
        left: 0,
        drag: null,
        newCard: null,
        cards: [],
        cardId: 0,
      };
    },
    computed: {
      surfaceClass() {
        return {
          ctrl: this.ctrl,
          dragging: !!this.drag,
        };
      },
      surfaceStyle() {
        const { rows, columns, cellWidth, cellHeight } = this.grid;
        const { top, left } = this;

        return {
          marginTop: `${top}px`,
          marginLeft: `${left}px`,
          width: `${columns * cellWidth}px`,
          height: `${rows * cellHeight}px`,
          gridTemplateRows: `repeat(${rows}, ${cellHeight}px)`,
          gridTemplateColumns: `repeat(${columns}, ${cellWidth}px)`,
          backgroundSize: `${cellWidth}px ${cellHeight}px`
        };
      },
      newCardStyle() {
        const { row, column, rows, columns, elevation } = this.newCard;

        return {
          gridArea: `${row} / ${column} / span ${rows} / span ${columns}`,
          zIndex: elevation,
        };
      },
    },
    async mounted() {
      await Axios.get('http://localhost:8080/api/card/deck')
        .then((response) => (this.loadExistingCards(response)));
      document.addEventListener('keydown', this.keychange);
      document.addEventListener('keyup', this.keychange);
    },
    beforeDestroy() {
      document.removeEventListener('keydown', this.keychange);
      document.removeEventListener('keyup', this.keychange);
    },
    methods: {
      loadExistingCards(response) {
        const keys = Object.values(response.data.cards);
        if (keys.length)
        {
          this.cards = this.cards.concat(keys);
          const maxCardId = keys
            .map((existing) => parseInt(existing.id, 10))
            .reduce((previous, current) => ((previous > current) ? previous : current));
          this.cardId = maxCardId + 1;
        }
      },
      keychange(e) {
        this.ctrl = e.ctrlKey;
      },
      mousedown(e) {
        document.addEventListener('mousemove', this.mousemove);
        document.addEventListener('mouseup', this.mouseup);

        const originX = e.clientX - this.left;
        const originY = e.clientY - this.top;

        this.drag = { originX, originY };

        if (this.ctrl) {
          this.newCard = SurfaceMath.startNewCard(this.grid, originX, originY);
        }
      },
      mousemove(e) {
        const { grid, cards, newCard, top, left } = this;
        const { originX, originY } = this.drag;

        if (!newCard) {
          this.top = e.clientY - originY;
          this.left = e.clientX - originX;
        }
        else {
          const x = e.clientX - left;
          const y = e.clientY - top;

          this.newCard = SurfaceMath.resizeNewCard(grid, cards, newCard, originX, originY, x, y);
        }
      },
      mouseup() {
        document.removeEventListener('mousemove', this.mousemove);
        document.removeEventListener('mouseup', this.mouseup);

        this.drag = null;

        if (this.newCard) {
          const card = { ...this.newCard, id: this.cardId.toString() };
          Timeline.http.putJson('/api/card/create', { body: card });

          this.newCard = null;
          this.cards.push(card);
          this.cardId += 1;
        }
      },
      selectCard(e) {
        this.cards = SurfaceMath.selectCard(this.cards, e.card);
      },
      resizeCard(e) {
        const x = e.x - this.left;
        const y = e.y - this.top;

        this.cards = SurfaceMath.resizeCard(this.grid, this.cards, e.card, e.hover, x, y);
      },
      dragCard(e) {
        const x = e.x - this.left;
        const y = e.y - this.top;

        this.cards = SurfaceMath.dragCard(this.grid, this.cards, e.card, x, y);
      },
    },
  };
</script>

<style>
html,
body,
#app,
.application--wrap {
  height: 100%;
  margin: 0px;
  background-color: #b3e5fc;
  overflow: hidden;
}

#surface {
  display: grid;
  background: linear-gradient(to right, #b3e5fc 1px, transparent 1px),
    linear-gradient(to bottom, #b3e5fc 1px, transparent 1px);
  background-color: white;
}

#surface.dragging {
  cursor: grabbing;
}

#surface.ctrl {
  cursor: cell;
}

#new-card {
  margin: 1px 0 0 1px;
}
</style>
