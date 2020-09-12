import Vue from "vue";
import Vuetify from "vuetify";
import App from "./App";
import "vuetify/dist/vuetify.css";

Vue.use(Vuetify);
Vue.config.productionTip = false;

/* eslint-disable no-new */
new Vue({
  el: "#app",
  components: { App },
  template: "<App/>"
});