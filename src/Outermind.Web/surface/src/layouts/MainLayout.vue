<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated>
      <q-toolbar>
        <q-btn
          flat
          dense
          round
          icon="menu"
          aria-label="Menu"
          @click="toggleDrawer"
        />

        <q-toolbar-title>
          Totem App
        </q-toolbar-title>

        <div>Quasar v{{ $q.version }}</div>
      </q-toolbar>
      <div class="q-px-lg q-pt-xl q-mb-md">
        <div class="text-h3"> Outermind </div>
        <div class="text-subtitle1">{{todaysDate}}</div>
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
import EssentialLink from 'components/EssentialLink.vue'
import { date } from 'quasar';
import Timeline from 'totem-timeline';

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
  components: { EssentialLink },
  data () {
    return {
      leftDrawerOpen: false,
      essentialLinks: linksData
    }
  },
  methods: {
    toggleDrawer() {
      Timeline.append("layoutOffsetsChanged", { left: 300, top: null});
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
</style>