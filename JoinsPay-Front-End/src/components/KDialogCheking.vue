<template>
  <v-dialog v-model="dialog" :persistent="persistent" :width="width">
    <v-card>
      <v-card-title
        style="background: red; color: white"
        primary-title
        persistent
        >{{ title }}</v-card-title
      >

      <v-card-text style="margin-top: 30px">
        <slot name="text">
          <!-- <span>{{ text }}</span> -->
          <v-card-title> {{ text }}</v-card-title>
        </slot>
        <br /><br />
      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>
        <slot name="buttoes">
          <v-progress-linear
            indeterminate
            color="red"
            class="mb-0"
            @click="fecharDialog"
          ></v-progress-linear>
          <!-- <v-btn width="100" text color="#F89F43" @click="fecharDialog">OK</v-btn> -->
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
      default: true,
    },
    width: {
      type: String,
      default: "800",
    },
  },
  methods: {
    fecharDialog: function () {
      this.$emit("fecharDialog");
    },
  },
  watch: {
    dialog: function (val) {
      if (val) {
        setTimeout(() => {
          this.$emit("fecharDialog");
        }, 5000);
      }
    },
  },
});
</script>