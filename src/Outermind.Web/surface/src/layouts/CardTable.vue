<template>
  <div class="q-pa-md" v-if="selectedCard">
    <div class="q-gutter-md">
      <label class="text-h6 float-top">Selected</label>
      <div class="cursor-pointer" style="width: 200px">
        <div class="row">
          <label class="col"> Card: </label>
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
      <div class="cursor-pointer" style="width: 200px">
        <div class="row">
          <label class="col"> Row: </label>
          <label class="col"> {{selectedCard.row}} </label>
        </div>
        <q-popup-edit @save="saveCard" value="" v-model="selectedCard.row" :cover="false">
          <q-input color="accent" v-model="selectedCard.row" dense autofocus>
            <template v-slot:prepend>
              <q-icon name="record_voice_over" color="accent" />
            </template>
          </q-input>
        </q-popup-edit>
      </div>
      <div class="cursor-pointer" style="width: 200px">
        <div class="row">
          <label class="col"> Rows: </label>
          <label class="col"> {{selectedCard.rows}} </label>
        </div>
        <q-popup-edit @save="saveCard" value="" v-model="selectedCard.rows" :cover="false">
          <q-input color="accent" v-model="selectedCard.rows" dense autofocus>
            <template v-slot:prepend>
              <q-icon name="record_voice_over" color="accent" />
            </template>
          </q-input>
        </q-popup-edit>
      </div>
      <div class="cursor-pointer" style="width: 200px">
        <div class="row">
          <label class="col"> Column: </label>
          <label class="col"> {{selectedCard.column}} </label>
        </div>
        <q-popup-edit @save="saveCard" value="" v-model="selectedCard.column" :cover="false">
          <q-input color="accent" v-model="selectedCard.column" dense autofocus>
            <template v-slot:prepend>
              <q-icon name="record_voice_over" color="accent" />
            </template>
          </q-input>
        </q-popup-edit>
      </div>
      <div class="cursor-pointer" style="width: 200px">
        <div class="row">
          <label class="col"> Columns: </label>
          <label class="col"> {{selectedCard.columns}} </label>
        </div>
        <q-popup-edit @save="saveCard" value="" v-model="selectedCard.columns" :cover="false">
          <q-input color="accent" v-model="selectedCard.columns" dense autofocus>
            <template v-slot:prepend>
              <q-icon name="record_voice_over" color="accent" />
            </template>
          </q-input>
        </q-popup-edit>
      </div>
      <div class="cursor-pointer" style="width: 200px">
        <div class="row">
          <label class="col"> Endpoint: </label>
          <label class="col"> {{selectedCard.endpoint}} </label>
        </div>
        <q-popup-edit @save="saveCard" value="" v-model="selectedCard.endpoint" :cover="false">
          <q-input color="accent" v-model="selectedCard.roendpointw" dense autofocus>
            <template v-slot:prepend>
              <q-icon name="record_voice_over" color="accent" />
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
    }
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
