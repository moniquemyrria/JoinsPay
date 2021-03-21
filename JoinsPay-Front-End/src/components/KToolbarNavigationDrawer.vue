<template>
  <div id="app" v-if="menu">
    <v-app-bar :style="menu.appBarStyle" color="secudary" app left id="card-toolbar">
      <!-- <v-app-bar-nav-icon dark id="card-toolbar-icons" @click="drawer = !drawer" /> -->
      <!-- <v-icon dark @click="drawer = !drawer">mdi-menu</v-icon> -->
      <v-app-bar-nav-icon id="card-toolbar-icons" @click="drawer = !drawer" />
      <v-toolbar-title>
        <!-- <img
          style="width:129px; margin-top:16px; margin-left: -20px;"
          class="img-fluid"
          width="25%"
          src="../assets/logo_inventus.svg"
        /> -->
      </v-toolbar-title>

      <v-spacer></v-spacer>
      <v-menu 
        offset-y
        transition="slide-y-transition"
      >
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            title="Notificações"
            icon
            class="ma-2"
            v-bind="attrs"
            v-on="on"
            id="card-toolbar-icons"
            @click="expand = !expand"
          >
            <v-badge overlap color="error" right>
              <template v-slot:badge>
                <!-- {{produtosCotacao.length}} -->
                <!-- <span>{{itemsNotificacao.length}}</span> -->
                <span>2</span>
              </template>
              <v-icon dark>mdi-bell</v-icon>
            </v-badge>
          </v-btn>
        </template>
        <v-spacer></v-spacer>
        <v-card width="300">
          <v-list three-line>
            <template v-for="(item, index) in itemsNotificacao">
              <v-subheader v-if="item.header" :key="item.header" v-text="item.header"></v-subheader>

              <v-divider v-else-if="item.divider" :key="index" :inset="item.inset"></v-divider>

              <v-list-item v-else :key="item.title">
                <v-list-item-content>
                  <v-list-item-title s v-html="item.title"></v-list-item-title>
                  <v-list-item-subtitle v-html="item.subtitle"></v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>
            </template>
          </v-list>
        </v-card>
      </v-menu>
      <label class="colorPrimaryText--text">
        <span>Olá, {{ menu.usuario }}</span>
        <!-- <v-btn title="Redefinir senha" icon id="card-toolbar-icons" @click="changePass">
          <v-icon dark>mdi-account-key</v-icon>
        </v-btn>
        <v-btn title="Sair" icon id="card-toolbar-icons" @click="sair">
          <v-icon dark>mdi-exit-to-app</v-icon>
        </v-btn>-->

        <v-menu 
          offset-y
          transition="slide-y-transition"
        >
          <template v-slot:activator="{ on, attrs }">
            <v-btn
              title="Opções do Usuário"
              icon
              class="ma-2"
              v-bind="attrs"
              v-on="on"
              id="card-toolbar-icons"
              @click="expand = !expand"
             
            >
              <v-icon dark>mdi-chevron-down</v-icon>
            </v-btn>
          </template>
          <v-spacer></v-spacer>
          <v-card width="250">
            <v-list >
              <v-list-item @click="changeMenu('RH')">
                <v-list-item-title>Mod. Recursos Humanos</v-list-item-title>
              </v-list-item>
              <v-divider></v-divider>
              <v-list-item @click="changeMenu('PRO')">
                <v-list-item-title>Mód. Produção</v-list-item-title>
              </v-list-item>
              <v-divider></v-divider>
              <v-list-item @click>
                <v-list-item-title @click="changePass">Redefinir Senha</v-list-item-title>
              </v-list-item>
              <v-divider></v-divider>
              <v-list-item @click>
                <v-list-item-title @click="sair">Sair</v-list-item-title>
              </v-list-item>
            </v-list>
            <!-- <v-list>
              <v-list-item>
                <v-list-item-title @click>Mód. Recursos Humanos</v-list-item-title>
              </v-list-item>
              <v-divider></v-divider>
              <v-list-item>
                <v-list-item-title @click>Mód. Produção</v-list-item-title>
              </v-list-item>
              <v-divider></v-divider>
            </v-list>-->
          </v-card>
        </v-menu>

        <!-- <v-icon v-if="!isMobile" style="margin-left: 10px;" @click="changePass" dark>mdi-account-key</v-icon> -->
        <!-- <v-icon v-if="!isMobile" style="margin-left: 10px;" @click="sair" dark>mdi-exit-to-app</v-icon> -->
      </label>
    </v-app-bar>

    <v-navigation-drawer v-if="menu" id="navigation-drawer" v-model="drawer" app color="primary">
      <v-list dense>
        <v-list-item link @click="drawer = !drawer">
          <v-list-item-content class="title" id="tituloG">
            <v-list-item-title>
              <span id="titulo">J O I N S</span>
            </v-list-item-title>
            <v-list-item-subtitle>
              <span id="sub-titulo">PAY</span>
            </v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
        <div v-for="itemMenu in menu.itensMenu" :key="itemMenu.title">
          <v-list-item v-if="!itemMenu.isFather" link>
            <!-- <v-list-item-icon>
              <v-icon class="white--text">{{itemMenu.icon}}</v-icon>
            </v-list-item-icon>-->
            <v-list-item-title>
              <router-link style="text-decoration:none;" :to="itemMenu.path">
                <span class="white--text">{{itemMenu.title}}</span>
              </router-link>
            </v-list-item-title>
          </v-list-item>
          <v-list-group v-else no-action color="#F79733">
            <v-list-item link style="margin-left: -15px" slot="activator">
              <!-- <v-list-item-icon>
                <v-icon class="white--text">{{itemMenu.icon}}</v-icon>
              </v-list-item-icon>-->
              <v-list-item-title>
                <span class="white--text">{{itemMenu.title}}</span>
              </v-list-item-title>
            </v-list-item>

            <v-list-item link v-for="itemSubMenu in itemMenu.itens" :key="itemSubMenu.path">
              <v-list-item-content style="margin-left: -40px">
                <v-list-item-title>
                  <router-link :to="itemSubMenu.path">
                    <span class="white--text">{{ itemSubMenu.title }}</span>
                  </router-link>
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-list-group>
        </div>
        <template v-if="isMobile">
          <v-list-item link @click="changePass">
            <v-list-item-icon>
              <v-icon class="primary--text">mdi-account-key</v-icon>
            </v-list-item-icon>
            <v-list-item-title>
              <span class="primary--text">Alterar Senha</span>
            </v-list-item-title>
          </v-list-item>
          <v-list-item link @click="sair">
            <v-list-item-icon>
              <v-icon class="primary--text">mdi-exit-to-app</v-icon>
            </v-list-item-icon>
            <v-list-item-title>
              <span class="primary--text">Sair</span>
            </v-list-item-title>
          </v-list-item>
        </template>
      </v-list>
    </v-navigation-drawer>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { Menu, MenuItem } from "../models/menu";

import { Display } from "../configuration/display";

export default Vue.extend({
  props: {
    menu: Menu,
  },
  data: () => ({
    isMobile: Display.isMobile(),
    itemsNotificacao: [
      { header: "Notificações do Usuário" },
      {
        title: "Notificação 1",
        subtitle:
          "<span class='text--primary'>Testando</span> &mdash; Dados da Notificação teste tamanho",
      },
      { divider: true, inset: false },
      {
        title: "Notificação 2",
        subtitle:
          "<span class='text--primary'>Testando</span> &mdash; Dados da Notificação teste tamanho",
      },
    ],
    expand: false,
    /////
    drawer: null,
  }),
  methods: {
    sair() {
      this.$emit("sair");
    },
    changePass() {
      this.$emit("changePass");
    },
    changeMenu(modulo: String){
      //console.log(modulo)
      this.$emit("changeMenu", modulo);
    }
  },
});
</script>


<style >
</style>

<style scoped>
#titulo {
  margin-left: 55px;
  width: 111px;
  height: 30px;
  text-align: center;
  font: Bold 36px/40px Candara;
  letter-spacing: 0px;
  color: #ffffff;
  opacity: 1;
}

#sub-titulo {
  margin-left: 130px;
  width: 100px;
  height: 29px;
  text-align: center;
  font: bold 25px/15px Candara;
  letter-spacing: 3px;
  color: #BF360C;
  text-transform: uppercase;
  opacity: 1;
}
</style>
