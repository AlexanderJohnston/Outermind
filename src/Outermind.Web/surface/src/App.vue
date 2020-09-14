<template>
  <div id="q-app">
    <router-view :routerStack="stackEndpoint" />
  </div>
</template>
<script>
import Timeline from 'totem-timeline';
import QueryHub from 'totem-timeline-signalr';
import Topics from '@area/topics.js';
import Web from '@area/web';
import Queries from '@area/queries';
import QueryData from "totem-timeline-vue";
import { HttpTransportType, LogLevel } from "@aspnet/signalr";

export default {
  name: 'App',
  mixins:[
      QueryData(Web.cardStack), 
  ],
  data() {
    return {
      stackEndpoint: '/api/card/stack',
    };
  },
  mounted() {
    Timeline.console.enable();
    QueryHub.enable('/hubs/query');
  }
}
</script>
