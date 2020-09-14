<template>
  <div
    id="surface"
    :class="surfaceClass"
    :style="surfaceStyle"
    tabindex="0"
    @mousedown.prevent="mousedown"
  >
    <q-item
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
      :surfaceKey="surfaceKey"
      :leftOffset="leftOffset"
      :topOffset="topOffset"
      @selectCard="selectCard"
      @resizeCard="resizeCard"
      @dragCard="dragCard"
      @removeCard="removeCard"
    />
  </div>
</template>
<script>
  import Timeline from 'totem-timeline';
  import SurfaceMath from '../surfaceMath';
  import AppCard from './AppCard.vue';
  import QueryHub from 'totem-timeline-signalr';
  import QueryData from "totem-timeline-vue";
  import Web from "../area/web.js";
  import Queries from "../area/queries.js";

  export default {
    components: { AppCard },
    mixins: [
      QueryData(Web.cardStack), 
      QueryData(Queries.layoutOffsets, "leftOffset", "topOffset"),
      QueryData(Queries.updatedCard, "updatedCard"),
    ],
    props: ['notes', 'cards'],
    data() {
      return {
        grid: {
          rows: 20,
          columns: 20,
          cellWidth: 20,
          cellHeight: 20,
        },
        ctrl: false,
        top: 0,
        left: 0,
        drag: null,
        newCard: null,
        cardId: 0,
        surfaceKey: null,
        stackEndpoint: '/api/card/stack',
        topOffset: 270,
        leftOffset: 300,
        updatedCard: null,
        renderKey: 0,
      };
    },
    watch: {
      // This watch maps the web query onto this.cards to avoid refactoring this.cards
      "data.stack"(args) {
        // We need to track the current max card ID since the client is currently
        // responsible for not sending bad or duplicate IDs.
        console.log('in the data watcher');
        const keys = Object.values(args);
        if (!keys.length)
        {
          console.log('excuse me sir');
          return;
        }
        const maxCardId = keys
            .map((existing) => parseInt(existing.id, 10))
            .reduce((previous, current) => ((previous > current) ? previous : current));
          this.cardId = maxCardId + 1;
        //this.cards = keys;
      },
      // The width/height watch is responsible for properly sizing the grid to avoid scrolling.
      "$q.screen.width"() {
        let columns = Math.floor((this.$q.screen.width - this.leftOffset) / 20);
        this.grid.columns = columns;
      },
      "$q.screen.height"() {
        let rows = Math.floor((this.$q.screen.height - this.topOffset) / 20);
        this.grid.rows = rows;
      },
      leftOffset() {
        let columns = Math.floor((this.$q.screen.width - this.leftOffset) / 20);
        this.grid.columns = columns;
      },
      updatedCard() {
        this.updateCard(this.updatedCard);
      }
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
    created() {
      // resize the grid because it hasn't been observed yet.
      // the -192 and -300 are offsets for the header and drawer.
      let rows = Math.floor((this.$q.screen.height - this.topOffset) / 20) - 1;
      this.grid.rows = rows;
      let columns = Math.floor((this.$q.screen.width - this.leftOffset) / 20) - 1;
      this.grid.columns = columns;
    },
    async mounted() {
      // the automatic way of loading cards in.
      Timeline.console.enable();
      QueryHub.enable('/hubs/query');

      // the manual way of loading cards in, to be removed when I'm not so lazy
      // await Axios.get('http://localhost:8080/api/card/deck')
      //   .then((response) => (this.loadExistingCards(response)));

      document.addEventListener('keydown', this.keychange);
      document.addEventListener('keyup', this.keychange);
    },
    beforeDestroy() {
      document.removeEventListener('keydown', this.keychange);
      document.removeEventListener('keyup', this.keychange);
    },
    methods: {
      loadExistingCards(response) {
        const keys = Object.values(response.data.stack);
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
        if(e.type === 'keydown')
          this.surfaceKey = e.key;
        else
          this.surfaceKey = null;
        this.ctrl = e.ctrlKey;
      },
      mousedown(e) {
        document.addEventListener('mousemove', this.mousemove);
        document.addEventListener('mouseup', this.mouseup);

        const originX = e.clientX - this.left - this.leftOffset;
        const originY = e.clientY - this.top - this.topOffset;

        this.drag = { originX, originY };

        if (this.ctrl) {
          this.newCard = SurfaceMath.startNewCard(this.grid, originX, originY);
        }
      },
      mousemove(e) {
        const { grid, cards, newCard, top, left } = this;
        const { originX, originY } = this.drag;

        if (!newCard) {
          this.top = e.clientY - originY - this.topOffset;
          this.left = e.clientX - originX - this.leftOffset;
        }
        else {
          const x = e.clientX - left - this.leftOffset;
          const y = e.clientY - top - this.topOffset;

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
        let newCards = SurfaceMath.selectCard(this.cards, e.card);
        // Timeline.append("updateCards", {cards: newCards});
        Timeline.append("openCard", {card: e.card});
      },
      resizeCard(e) {
        const x = e.x - this.left - this.leftOffset;
        const y = e.y - this.top - this.topOffset;

        this.cards = SurfaceMath.resizeCard(this.grid, this.cards, e.card, e.hover, x, y);
      },
      dragCard(e) {
        const x = e.x - this.left;
        const y = e.y - this.top;

        this.cards = SurfaceMath.dragCard(this.grid, this.cards, e.card, x, y);
      },
      removeCard(e) {
        Timeline.http.deleteJson('/api/card/remove', { body: e.card });
      },
      updateCard(e) {
        Timeline.http.postJson('/api/card/update', {body: e});
      }
    },
  };
</script>

<style>
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
  background: white;
}
</style>
