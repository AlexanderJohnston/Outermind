<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated>
      <div class="row justify-between">
        <div class="row-2">
          <q-toolbar>
            <q-btn
              flat
              dense
              round
              icon="menu"
              aria-label="Menu"
              @click="toggleDrawer"
            />

            <q-toolbar-title class="title-bar">
              Totem App
            </q-toolbar-title>      
          </q-toolbar>
          <div class="q-px-lg q-pt-xl q-mb-md app-info">
            <div class="text-h3"> Outermind </div>
            <div class="text-subtitle1">{{todaysDate}}</div>
          </div>
        </div>
        <div class="row-10">
          <div class="">
            <card-table :selectedCard="selectedCard"></card-table>
          </div>
        </div>
      </div>
      <q-img 
      src="../statics/totem-unsplash.jpg"
      class="header-image absolute-top"/>
    </q-header>

    <q-drawer
      v-model="leftDrawerOpen"
      show-if-above
      bordered
      content-class="bg-grey-1"
    >
      <q-list>
        <q-item-label
          header
          class="text-grey-8"
        >
          Essential Links
        </q-item-label>
        <EssentialLink
          v-for="link in essentialLinks"
          :key="link.title"
          v-bind="link"
        />
      </q-list>
    </q-drawer>

    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script>
import CardTable from './CardTable.vue';
import EssentialLink from 'components/EssentialLink.vue'
import { date } from 'quasar';
import Timeline from 'totem-timeline';
import QueryData from "totem-timeline-vue";
import Queries from "../area/queries.js";

const linksData = [
  {
    title: 'Surface',
    caption: 'outermind.surface',
    icon: 'school',
    link: '/surface'
  },
  {
    title: 'Github',
    caption: 'outermind.source',
    icon: 'code',
    link: 'https://github.com/bwatts/Outermind'
  },
  {
    title: 'Slack Chat Channel',
    caption: 'ddd.cqrs.es',
    icon: 'chat',
    link: 'https://ddd-cqrs-es.slack.com'
  },
];

export default {
  name: 'MainLayout',
  mixins: [QueryData(Queries.selectedCard, "selectedCard")],
  components: {CardTable, EssentialLink },
  data () {
    return {
      selectedCard: null,
      leftDrawerOpen: false,
      essentialLinks: linksData
    }
  },
  methods: {
    toggleDrawer() {
      if (this.leftDrawerOpen) {
        Timeline.append("layoutOffsetsChanged", { left: 0, top: null});
      }
      else {
        Timeline.append("layoutOffsetsChanged", { left: 300, top: null});
      }
      this.leftDrawerOpen = !this.leftDrawerOpen;
    }
  },
  computed: {
    todaysDate() {
      let timeStamp = Date.now();
      return date.formatDate(timeStamp, 'dddd, MMMM D');
    }
  }
}
</script>

<style lang="scss">
.header-image {
  height: 100%;
  transform: scaleX(-1);
  z-index: -1;
  opacity: 0.4;
  filter: grayscale(100%);
}

.title-bar {
  float: left;
  max-width: 200px;
}

.card-table {
  float: top;
  height: 100%;
  flex-direction: column;
  display: flex;
}

.float-right {
  float: right;
}

.app-info {
  float: bottom;
}
</style>