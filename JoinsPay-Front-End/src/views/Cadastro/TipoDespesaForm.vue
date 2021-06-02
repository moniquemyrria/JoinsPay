<template>
  <v-content style="margin: 10px">
    <v-snackbar
      v-model="snackbar"
      :color="colorBorder"
      shaped
      timeout="3000"
      :vertical="vertical"
    >
      <b>{{ text }}</b>
      <template v-slot:action="{ attrs }">
        <v-btn v-bind="attrs" @click="snackbar = false" icon dark small>
          <v-icon>mdi-close</v-icon>
        </v-btn>
      </template>
    </v-snackbar>

    <k-loading title="Carregando" :dialog="dialogLoading"></k-loading>

    <k-dialog
      @fecharDialog="fecharDialogMessage"
      :dialog="dialogMessage"
      title="Tipo de Rereitas"
      :text="textError"
    ></k-dialog>

    <v-container>
      <v-row>
        <v-breadcrumbs style="margin-left: -10px" :items="caminho">
          <template v-slot:divider>
            <!-- <v-icon>mdi-home</v-icon> -->
          </template>
          <template v-slot:item="{ item }">
            <router-link :to="item.href">
              {{ item.text }}
            </router-link>
          </template>
        </v-breadcrumbs>
      </v-row>
      <v-card>
        <v-col cols="12" v-if="!idTipoDespesa">
          <v-card-text>
            <p class="display-1 text--primary">Novo Tipo de Despesa</p>
            <div>Cadastro de Categorias de Despesas</div>
          </v-card-text>
        </v-col>

        <v-col cols="12" v-if="idTipoDespesa">
          <v-card-text>
            <p class="display-1 text--primary">Editar Tipo de Despesa</p>
            <div>Edção de dados de Categorias de Despesas</div>
          </v-card-text>
        </v-col>

        <v-col cols="12" sm="12">
          <v-row>
            <v-col cols="12" sm="3">
              <v-text-field
                outlined
                v-model="tipoDespesa.sigla"
                label="Sigla"
                placeholder="Sigla"
                maxl
                dense
                counter="4"
                maxlength="4"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="4">
              <v-color-picker style="margin-top: -20px" title="Selecione a cor para o Tipo da Despesa" hide-canvas hide-inputs v-model="picker" flat
                >Cor</v-color-picker
              >
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" sm="12">
              <v-text-field
                outlined
                v-model="tipoDespesa.descricao"
                label="Descrição *"
                placeholder="Descrição da categoria"
                counter="30"
                dense
                required
                maxlength="30"
              ></v-text-field>
            </v-col>
          </v-row>
        </v-col>

        <v-col cols="12" sm="12">
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn
              color="secondary lighten-1"
              @click="retornaListaTipoDespesas()"
              large
              dark
              width="150"
            >
              <v-icon left> mdi-close </v-icon>Cancelar</v-btn
            >

            <v-btn
              v-if="!isMobile && idTipoDespesa"
              color="secondary darken-1"
              large
              dark
              width="150"
              @click="updateTipoDespesa()"
              ><v-icon left> mdi-content-save </v-icon>Salvar</v-btn
            >

            <v-btn
              v-if="!isMobile && !idTipoDespesa"
              color="secondary darken-1"
              large
              dark
              width="150"
              @click="saveTipoDespesa()"
              ><v-icon left> mdi-content-save </v-icon>Salvar</v-btn
            >

            <v-btn
              v-else-if="isMobile && !idTipoDespesa"
              color="secondary darken-1"
              large
              dark
              width="150"
              @click="saveTipoDespesa()"
              ><v-icon left> mdi-content-save </v-icon>Salvar</v-btn
            >
          </v-card-actions>
        </v-col>
      </v-card>
    </v-container>
  </v-content>
</template>

<script lang="ts">
var validaNumeros = require("../../baseGenerica/validaNumeros.js");

import Vue from "vue";

import KLoading from "../../components/KLoading.vue";
import KDialog from "../../components/KDialog.vue";
import KDataList from "../../components/KDataList.vue";

import Component from "vue-class-component";
import { Watch } from "vue-property-decorator";

import { Display } from "../../configuration/display";
import { Storage } from "../../configuration/storage";

import { Header, HeaderIcon, HeaderAction } from "../../models/header";
import { Headers } from "../../models/headers";

import {
  ITipoDespesaFormView,
  ITipoDespesaFormController,
} from "../../contracts/Cadastro/TipoDespesaFormContract";
import { TipoDespesaFormController } from "../../controllers/Cadastro/TipoDespesaFormController";

@Component({
  components: {
    KLoading,
    KDialog,
    KDataList,
  },
})
export default class TipoDespesaForm
  extends Vue
  implements ITipoDespesaFormView {
  _controller?: ITipoDespesaFormController;

  caminho = [
    {
      text: "Home",
      disabled: false,
      href: "/",
    },
    {
      text: "Tipos de Despesas",
      disabled: false,
      href: "/tipoDespesas",
    },
  ];

  //validate form
  valid = true;
  lazy = false;

  isMobile = false;

  isSucesso = false;

  dialogLoading = false;
  dialogMessage = false;
  textError = "";

  picker: any = {};

  idTipoDespesa = "";

  tipoDespesa = {
    id: 0,
    sigla: "",
    descricao: "",
    deletado: "N",
    color: "",
    dataCadastro: new Date(),
  };

  //SNACKBAR
  snackbar = false;
  text = "";
  vertical = true;
  color = "";
  colorBorder = "";

  apenasNumeros(event: any) {
    validaNumeros(event);
  }

  loading(visible: boolean): void {
    this.dialogLoading = visible;
  }

  fecharDialogMessage() {
    this.dialogMessage = false;
    if (this.isSucesso) {
      this.$router.push("/tipoDespesas");
    }
  }

  retornaListaTipoDespesas() {
    this.$router.push("/tipoDespesas");
  }

  showMsg(msg: string, modo: number): void {
    this.textError = msg;
    if (modo == 1) {
      this.isSucesso = true;
    }
    this.dialogMessage = true;
  }

  validate() {
    if (
      this.tipoDespesa.descricao == "" ||
      this.tipoDespesa.descricao.length > 30
    ) {
      this.colorBorder = "error";
      this.color = "error";
      this.text =
        "O campo Descrição do Tipo de Despesa está vazio ou inválido.";
      this.snackbar = true;
      return true;
    }

    return false;
  }

  saveTipoDespesa() {
    
    if (this.validate()) {
    } else {
       this.tipoDespesa.color = this.picker.hex;
      //this.solicitacao.user = parseInt(Storage.get("User")!);
      this._controller!.salvar(this.tipoDespesa);
    }
  }

  updateTipoDespesa() {
    if (this.validate()) {
    } else {
       this.tipoDespesa.color = this.picker.hex;
      //this.solicitacao.user = parseInt(Storage.get("User")!);
      this._controller!.update(this.idTipoDespesa, this.tipoDespesa);
    }
  }

  carregarDtos(): void {
    this.idTipoDespesa = this.$route.params.id;
  }

  showTipoDespesaForEditar(item: any) {
    this.picker = item.color
    this.tipoDespesa = item;
  }

  created() {
    this._controller = new TipoDespesaFormController(this);
    this._controller.carregarDtos(this.$route.params.id);
    this.carregarDtos();
    this.isMobile = Display.isMobile();
  }
}
</script>
