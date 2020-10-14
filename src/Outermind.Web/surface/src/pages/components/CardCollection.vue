<template>
  <div>
    <app-card
      v-for="card in cards"
      :endpoint="card.endpoint"
      :key="card.id"
      :card="card"
      :surfaceKey="keyHandler"
      :selected="isCardSelected(card.id)"
      :topOffset="topOffset"
      :leftOffset="leftOffset"
      @selectCard="selectCard"
      @resizeCard="resizeCard"
      @dragCard="dragCard"
      @removeCard="removeCard"
    />
  </div>
</template>

<script>
import AppCard from './AppCard.vue';
import Timeline from 'totem-timeline';
import QueryData from "totem-timeline-vue";
import Web from "../../area/web.js";
import SurfaceMath from '../../surfaceMath';
import Queries from "../../area/queries.js";

export default{
  components: { AppCard },
  props: ['grid', 'stack', 'surfaceKey', 'topOffset', 'leftOffset'],
  name: "CardCollection",
  mixins: [
    QueryData(Queries.selectedCard, "selectedCard"),
  ],
  created() {
      this.cards = this.stack;
  },
  mounted() {
      this.cards = this.stack;
    },
  computed: {
    keyHandler: {
      get() {
        return this.surfaceKey;
      }
    },
    cards: {
      get() {
        return this.stack;
      },
      set(stack) {
        return stack;
      }
    },
  },
  data(){
    return{
      cardStack: [],
    }
  },
  methods:{
    isCardSelected(id) {
      return this.selectedCard.id == id
    },
    selectCard(e) {
      this.$emit("selectCard", {e});
      // this.cards = SurfaceMath.selectCard(this.cards, e.card);
      // Timeline.append("openCard", {card: e.card});
    },
    resizeCard(e) {
        this.$emit("resizeCard", {e});
      // const x = e.x - this.left - this.leftOffset;
      // const y = e.y - this.top - this.topOffset;

      // this.cards = SurfaceMath.resizeCard(this.grid, this.cards, e.card, e.hover, x, y);
    },
    dragCard(e) {
      this.$emit("dragCard", e);
      // const x = e.x - this.left;
      // const y = e.y - this.top;

      // this.cards = SurfaceMath.dragCard(this.grid, this.cards, e.card, x, y);
    },
    removeCard(e) {
      this.$emit("removeCard", e);
      // Timeline.http.deleteJson('/api/card/remove', { body: e.card });
    },
  },
}
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