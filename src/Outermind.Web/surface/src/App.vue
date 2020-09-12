<template>
  <div id="q-app">
    <router-view />
  </div>
</template>
<script>
import Timeline from 'totem-timeline';
import QueryHub from 'totem-timeline-signalr';
import Topics from '@area/topics.js';
import Web from '@area/web';
import Queries from '@area/queries';
import { HttpTransportType, LogLevel } from "@aspnet/signalr";

export default {
  name: 'App',
  mounted() {
    Timeline.console.enable();
    QueryHub.enable('/hubs/query');
  }
}
</script>
