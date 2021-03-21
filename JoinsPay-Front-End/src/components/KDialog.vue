<template>
  <v-dialog v-model="dialog" :persistent="persistent" :width="width">
    <v-card>
      <v-card-title id="colorDialog" primary-title persistent>{{title}}</v-card-title>

      <v-card-text style="margin-top: 30px">
        <slot name="text">
          <span>{{text}}</span>
        </slot>
        <br><br>
      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>
        <slot name="buttoes">
          <v-btn width="100" text color="primary" @click="fecharDialog">OK</v-btn>
        </slot>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import Vue from "vue";
export default Vue.extend({
  props: {
    title: String,
    text: String,
    dialog: Boolean,
    persistent: {
      type: Boolean,
      default: true
    },
    width: {
      type: String,
      default: "500"
    }
  },
  methods: {
    fecharDialog: function() {
      this.$emit("fecharDialog");
    }
  }
});
</script>

<style scoped>
#colorDialog{
  background: #BF360C; 
  color: white
}
</style>