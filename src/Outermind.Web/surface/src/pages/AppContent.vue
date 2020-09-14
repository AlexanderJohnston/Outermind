<template>
  <div class="app-content">
    <slot></slot>
    <pre>{{app}}</pre>
  </div>
</template>

<script>
import GenericComponent from './components/GenericComponent.vue';
import Vue from 'vue';

  export default {
    props: ["cardId", "app"],
    components: {GenericComponent},
    name: "TotallyContent",
    data() {
      return {
        appString: '',
      }
    },
    created(){
      //this.appString = JSON.stringify(this.app, null, 2);
    },
    computed: {
      getComponent() {
        if (this.app) {
          console.log(this.app);
          console.log('hi hi hi hi hi hi ');
          return this.app;
        }
        else {
          return "generic-component";
        }
      }
    },
    methods: {
      syntaxHighlight(json) {
        if (typeof json != 'string') {
             json = JSON.stringify(json, undefined, 2);
        }
        json = json.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
        return json.replace(/("(\\u[a-zA-Z0-9]{4}|\\[^u]|[^\\"])*"(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)/g, function (match) {
            var cls = 'number';
            if (/^"/.test(match)) {
                if (/:$/.test(match)) {
                    cls = 'key';
                } else {
                    cls = 'string';
                }
            } else if (/true|false/.test(match)) {
                cls = 'boolean';
            } else if (/null/.test(match)) {
                cls = 'null';
            }
            return '<span class="' + cls + '">' + match + '</span>';
        });
      }
    },
  }
</script>

<style scoped>
  .app-content {
    position: absolute;
    top: 0;
    left: 0;
    bottom: 0;
    right: 0;
    margin: .5em;
    overflow: auto;
  }
</style>