<template>
  <!-- <div class="loginFluid"> -->
  <v-app id="inspire">
    <v-main class="Fundo">
      <v-row align="center" justify="left">
        <k-loading title="Carregando" :dialog="dialogLoading"></k-loading>
        <k-dialog
          @fecharDialog="fecharDialogMessage"
          :dialog="dialogMessage"
          title="Error"
          :text="textError"
        ></k-dialog>
        <v-snackbar v-model="snackbar" :color="colorBorder" timeout="6000" :vertical="vertical">
          <b>{{ text }}</b>
          <template v-slot:action="{ attrs }">
            <v-btn :color="color" text v-bind="attrs" @click="snackbar = false">Ok</v-btn>
          </template>
        </v-snackbar>

        <v-container style="margin: auto;  margin-top: 5vh; margin-left: 15%;">
          <div style="text-align:center;">
            <!-- <v-row>
            <img src="../assets/logo_inventus-light.svg" id="logo" />
            </v-row>-->
            <v-row>
              <img src="../assets/Inventus_Power_logo.png" id="logo" />
            </v-row>

            <v-card style=" margin-top: -13vh;" flat id="modalPrimeiroAcesso" width="500">
              <v-card-text>
                <v-row>
                  <v-col sm="12">
                    <span id="modalPrimeiroAcessoTituloJoins">JOINS</span>
                  </v-col>
                </v-row>
                <v-spacer></v-spacer>
                <v-row style="margin-top: -2vh">
                  <v-col sm="12">
                    <span id="modalPrimeiroAcessoTituloPay">PAY</span>
                  </v-col>
                </v-row>

                <v-row>
                  <div id="modalPrimeiroAcessoLogo">
                    <v-icon id="modalPrimeiroAcessoIcon">mdi-lock</v-icon>
                  </div>
                </v-row>
                <v-row>
                  <!-- <div id="modalPrimeiroAcessoTexto"> -->
                  <v-col>
                    <span id="modalPrimeiroAcessoTexto">
                      Nova Senha de Acesso
                    </span>
                    <!-- </div> -->
                  </v-col>
                </v-row>
                <!-- <v-row>
                <div id="modalPrimeiroAcessoTexto1">
                  <span>Alteração de Senha de Acesso</span>
                </div>
                </v-row>-->
                <v-row id="modalPrimeiroTextField">
                  <v-col cols="12" sm="6" md="10">
                    <v-text-field
                      filled
                      label="Matrícula"
                      placeholder="Ex.: 123456"
                      autocomplete="current-password"
                      v-model="usuario.matricula"
                      maxlength="6"
                      counter="6"
                    ></v-text-field>
                  </v-col>
                </v-row>
                <v-row id="modalPrimeiroTextField">
                  <v-col cols="12" sm="6" md="10">
                    <v-text-field
                      filled
                      label="Senha Atual"
                      placeholder="Senha atual (6 caracteres)"
                      autocomplete="current-password"
                      v-model="usuario.senhaAtual"
                      type="password"
                      maxlength="6"
                      counter="6"
                    ></v-text-field>
                  </v-col>
                </v-row>
                <v-row id="modalPrimeiroTextField">
                  <v-col cols="12" sm="6" md="10">
                    <v-text-field
                      filled
                      label="Nova senha"
                      autocomplete="current-password"
                      placeholder="Nova Senha (6 caracteres)"
                      v-model="usuario.novaSenha"
                      type="password"
                      maxlength="6"
                      counter="6"
                    ></v-text-field>
                  </v-col>
                </v-row>
                <v-row id="modalPrimeiroTextField">
                  <v-col cols="12" sm="6" md="10">
                    <v-text-field
                      filled
                      label="Confirmar nova Senha"
                      placeholder="Confirmar Nova Senha (6 caracteres)"
                      autocomplete="current-password"
                      v-model="usuario.confimarNovaSenha"
                      maxlength="6"
                      type="password"
                      counter="6"
                    ></v-text-field>
                  </v-col>
                </v-row>
              </v-card-text>
              <v-card-actions>
                <v-col cols="12" sm="6" md="10">
                  <v-btn
                    type="submit"
                    id="modalPrimeiroAcessoBotao"
                    width="85%"
                    @click.prevent.stop="funcLogin"
                    dark
                  >Alterar</v-btn>
                </v-col>
              </v-card-actions>
            </v-card>
            <v-btn style="margin-top: 48%" absolute dark fab top right color="primery">
              <v-icon @click="sair">mdi-login</v-icon>
            </v-btn>
          </div>
        </v-container>
        <!-- <v-footer color="#313C42" dark>
        <v-spacer></v-spacer>
        <v-spacer></v-spacer>
        </v-footer>-->
      </v-row>
    </v-main>
  </v-app>
  <!-- </div> -->
</template>

<script lang="ts">
import Vue from "vue";

import KLoading from "../components/KLoading.vue";
import KDialog from "../components/KDialog.vue";
import KFooter from "../components/KFooter.vue";

import Component from "vue-class-component";

import {
  IAlteracaoSenhaView,
  IAlteracaoSenhaController,
} from "../contracts/AlteracaoSenhaContract";
import { AlteracaoSenhaController } from "../controllers/AlteracaoSenhaController";

@Component({
  components: {
    KLoading,
    KDialog,
    KFooter,
  },
})
export default class AlteracaoSenha extends Vue implements IAlteracaoSenhaView {
  _controller?: IAlteracaoSenhaController;

  dialogLoading = false;
  dialogMessage = false;
  textError = "";

  usuario = {
    matricula: "",
    senhaAtual: "",
    novaSenha: "",
    confimarNovaSenha: "",
  };

  //SNACKBAR
  snackbar = false;
  text = "";
  vertical = true;
  color = "";
  colorBorder = "";

  dialog = true;

  validate() {
    if (this.usuario.matricula == "" || this.usuario.matricula == undefined) {
      this.colorBorder = "error";
      this.color = "white";
      this.text = "Informe a sua Matrícula.";
      this.snackbar = true;
      return true;
    }

    if (this.usuario.senhaAtual == "") {
      this.colorBorder = "error";
      this.color = "white";
      this.text = "Informe a Senha atual.";
      this.snackbar = true;
      return true;
    }

    if (this.usuario.novaSenha == "") {
      this.colorBorder = "error";
      this.color = "white";
      this.text = "Informe a nova Senha para realizar a alteração.";
      this.snackbar = true;
      return true;
    }

    if (this.usuario.confimarNovaSenha == "") {
      this.colorBorder = "error";
      this.color = "white";
      this.text = "Confirme a nova Senha para realizar a alteração.";
      this.snackbar = true;
      return true;
    }

    if (this.usuario.novaSenha == this.usuario.senhaAtual) {
      this.colorBorder = "error";
      this.color = "white";
      this.text = "A nova senha não pode ser igual a senha atual.";
      this.snackbar = true;
      return true;
    }

    if (this.usuario.novaSenha != this.usuario.confimarNovaSenha) {
      this.colorBorder = "error";
      this.color = "white";
      this.text =
        "A senha de confirmação e nova senha informada não coincidem.";
      this.snackbar = true;
      return true;
    }

    if (this.usuario.novaSenha.length < 6) {
      this.colorBorder = "error";
      this.color = "white";
      this.text = "A nova senha informada deve conter no mínimo 6 caracteres.";
      this.snackbar = true;
      return true;
    }
  }

  sair() {
    localStorage.clear();
    this.$router.push("/Login");
  }

  loading(visible: boolean): void {
    this.dialogLoading = visible;
  }

  fecharDialogMessage() {
    this.dialogMessage = false;
  }

  showMsg(msg: string | Error): void {
    this.textError = msg.toString();
    this.dialogMessage = true;
  }

  pushMain(): void {
    this.$router.push("/");
  }

  funcLogin() {
    if (this.validate()) {
    } else {
      this._controller!.login(
        this.usuario.matricula,
        this.usuario.senhaAtual,
        this.usuario.novaSenha
      );
    }
  }

  created() {
    this.usuario.matricula = this.$route.params.matricula;
    if (this.usuario.matricula != undefined) {
      this.colorBorder = "blue";
      this.color = "white";
      this.text =
        "Bem-vindo(a) ao Joins Pay. Este é seu primeiro acesso. Por favor, altere a sua senha de acesso ao sistema";
      this.snackbar = true;
    }
    this._controller = new AlteracaoSenhaController(this);
  }
}
</script>

<style scoped>
.loginFluid {
  background-image: url("../assets/background-inventus6.png");
  background-size: cover;
  height: 100%;
}

#inspire {
  background: none;
  height: 100%;
}

#titulo {
  margin-top: 171px;
  width: 296px;
  height: 97px;
  text-align: left;
  font: Bold 75px/96px Montserrat;
  letter-spacing: 0px;
  color: #ffffff;
  opacity: 1;
}

#titulo2 {
  margin-top: 246px;
  margin-left: 100px;
  width: 186px;
  height: 62px;
  text-align: center;
  font: Bold 45px/15px Montserrat;
  letter-spacing: 6.24px;
  color: #313c42;
  text-transform: uppercase;
  opacity: 1;
}



#logo {
  margin-left: 110vh;
  width: 40vh;
  background: transparent 0% 0% no-repeat padding-box;
  opacity: 1;
}

#modalPrimeiroAcesso {
  margin-left: -10vh;
  /* width: 75vh; */
  background: var(--unnamed-color-313c42) 0% 0% no-repeat padding-box;
  background: #ffffff 0% 0% no-repeat padding-box;
  box-shadow: 0px 3px 6px #00000029;
  border-radius: 7px;
  opacity: 1;
  margin-top: 5vh;
}

#modalPrimeiroAcessoBotao {
  background: var(--unnamed-color-f79733) 0% 0% no-repeat padding-box;
  background: #f79733 0% 0% no-repeat padding-box;
  box-shadow: 0px 3px 6px #00000029;
  opacity: 1;
  height: 50px;
  margin-left: 21%;
}

#modalPrimeiroTextField {
  margin-bottom: -30px;
  margin-left: 15%;
}

#modalPrimeiroAcessoLogo {
  width: 120px;
  height: 120px;
  border-radius: 50%;
  display: inline-block;
  background: var(--unnamed-color-ffffff) 0% 0% no-repeat padding-box;
  background: #f79733 0% 0% no-repeat padding-box;
  border: 1px solid #f79733;
  opacity: 1;
  margin-left: 40%;
  margin-top: -10px;
}

#modalPrimeiroAcessoIcon {
  font-size: 80px;
  color: #ffffff;
  opacity: 1;
  margin-left: 2%;
  margin-top: 13%;
}

#modalPrimeiroAcessoTituloJoins {
  margin-left: 31vh;
  color: var(--unnamed-color-313c42);
  text-align: center;
  font-style: normal normal bold Montserrat;
  letter-spacing: 0px;
  color: #313c42;
  opacity: 1;
  font-size: 4.5vh;
  font-weight: bold;
}

#modalPrimeiroAcessoTituloPay {
  margin-left: 38vh;
  color: var(--unnamed-color-313c42);
  text-align: center;
  font-style: normal normal bold Montserrat;
  letter-spacing: 0px;
  color: #f79733;
  opacity: 1;
  font-size: 3vh;
  font-weight: bold;
}

#modalPrimeiroAcessoTexto {
  margin-bottom: 3vh;
  color: var(--unnamed-color-000000);
  text-align: left;
  font-style: normal normal medium 25px/33px Roboto;
  letter-spacing: 0px;
  color: #000000;
  opacity: 1;
  font-weight: bold;
  font-size: 2.5vh;
}

#modalPrimeiroAcessoTexto1 {
  margin-left: 30%;
  margin-top: 1%;
  color: var(--unnamed-color-f7f7f7);
  text-align: left;
  font-style: normal normal bold 10px/11px Arial;
  letter-spacing: 0px;
  color: #000000;
  opacity: 1;
  font-size: 1.5vh;
  font-weight: bold;
}

.Fundo {
  background-image: url(../assets/background-inventus6.png);
  background-size: cover;
  height: 100%;
}
.center {
  margin-top: 15vh;
}

.hidden-sm-and-down .v-icon {
  color: white !important;
}
</style>
