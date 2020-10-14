<template>
  <div class="q-pa-md" v-if="selectedCard">
    <div class="q-gutter-md">
      <label class="q-pa-sm text-h6 float-top">Selected</label>
      <div class="cursor-pointer" style="width: 300px">
        <div class="row">
          <label class="col q-pa-sm"> Card: </label>
          <label class="col"> {{selectedCard.id}} </label>
        </div>
        <q-popup-edit @save="saveCard" value="" :v-model="selectedCard.id" content-class="bg-accent text-white">
          <q-input dark color="white" v-model="selectedCard.id" dense autofocus>
            <template v-slot:append>
              <q-icon name="edit" />
            </template>
          </q-input>
        </q-popup-edit>
      </div>
      <div class="cursor-pointer" style="width: 300px">
        <div class="row">
          <label class="q-pa-sm"> Type: </label>
          <q-btn-dropdown color="primary" label="Card Type">
          <q-list>
            <q-item clickable v-close-popup @click="onItemClick">
              <q-item-section>
                <q-item-label>Event</q-item-label>
              </q-item-section>
            </q-item>

            <q-item clickable v-close-popup @click="onItemClick">
              <q-item-section>
                <q-item-label>Topic</q-item-label>
              </q-item-section>
            </q-item>

            <q-item clickable v-close-popup @click="onItemClick">
              <q-item-section>
                <q-item-label>Query</q-item-label>
              </q-item-section>
            </q-item>
          </q-list>
        </q-btn-dropdown>
        </div>
      </div>
      <div class="cursor-pointer" style="width: 300px">
        <div class="row">
          <label class="col q-pa-sm"> Endpoint: </label>
          <label class="col"> {{selectedCard.endpoint}} </label>
        </div>
        <q-popup-edit @save="saveCard" value="" v-model="selectedCard.endpoint" :cover="false">
          <q-input color="accent" v-model="selectedCard.endpoint" dense autofocus>
            <template v-slot:prepend>
              <q-icon name="edit" color="accent" />
            </template>
          </q-input>
        </q-popup-edit>
      </div>
    </div>
  </div>
</template>

<script>
import QueryData from "totem-timeline-vue";
import Queries from "../area/queries.js";
import Timeline from 'totem-timeline';

const columns = [
  { name: 'id', align: 'left', label: 'Card ID', field: 'id' },
  { name: 'row', align: 'center', label: 'Row (y)', field: 'row' },
  { name: 'rows', label: 'Rows (length)', field: 'rows' },
  { name: 'column', label: 'Column (x)', field: 'column' },
  { name: 'columns', label: 'Columns (length)', field: 'columns' },
  { name: 'endpoint', label: 'Endpoint', field: 'endpoint' },
]


export default{
  name: "CardTable",
  mixins: [QueryData(Queries.selectedCard, "selectedCard")],
  methods:{
  },
  methods: {
    saveCard() {
      Timeline.append("updateCard", {card: this.selectedCard});
    },
    onItemClick() {

    },
  },
  data () {
    return {
      selectedCard: null,
      columns,
    }
  }
}
</script>

<style scoped>

</style>
