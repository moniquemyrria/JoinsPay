<template>
  <div id="app">
    <v-content style="margin: 10px">
      <v-container>
        <v-snackbar
          v-model="snackbar"
          :color="colorBorder"
          shaped
          timeout="6000"
          :vertical="vertical"
        >
          <b>{{ text }}</b>
          <template v-slot:action="{ attrs }">
            <v-btn v-bind="attrs" @click="snackbar = false" icon dark small>
              <v-icon>mdi-close</v-icon>
            </v-btn>
          </template>
        </v-snackbar>

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

        <v-row ref="form">
          <v-col cols="12">
            <h3>
              <b>Calendário</b>
            </h3>
            <span>
              <b>Calendário férias, dias ponte e etc.</b>
            </span>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="12" sm="2" v-for="item in itemsTipos" :key="item.id">
            <v-icon small :color="item.cor">mdi-circle</v-icon>
            <span>{{
              " " +
              item.descricao.charAt(0).toUpperCase() +
              item.descricao.slice(1).toLowerCase()
            }}</span>
          </v-col>
        </v-row>

        <!-- DELETE EVENTO -->
        <v-dialog v-model="dialogDelete" width="500">
          <v-card>
            <v-card-title primary-title persistent
              >Remover Evento?</v-card-title
            >

            <v-card-text>
              <slot name="text">
                <span
                  >O Evento selecionado será removido. Deseja Continuar?</span
                >
              </slot>
              <br />
              <br />
            </v-card-text>

            <v-divider></v-divider>

            <v-card-actions>
              <v-spacer></v-spacer>
              <slot name="buttoes">
                <v-btn
                  @click="modalCancelaDeleteItem"
                  width="100"
                  text
                  dark
                  color="red darken-2"
                  >Cancelar</v-btn
                >
                <v-btn
                  @click="deleteItemConfirmado"
                  width="100"
                  text
                  dark
                  color="green darken-2"
                  >Sim</v-btn
                >
              </slot>
            </v-card-actions>
          </v-card>
        </v-dialog>

        <!-- ADD EVENTO  / ALTERAR-->
        <v-dialog
          ok-title="Save"
          v-model="show"
          persistent
          max-width="600px"
          @ok="saveEvent"
        >
          <v-card>
            <v-card-title style="background-color: #1b023a">
              <span style="color: white" class="headline">{{
                currentId != null ? "Editar Evento" : "Inserir Evento"
              }}</span>
            </v-card-title>
            <v-card-text>
              <v-container>
                <v-row>
                  <v-col cols="12" sm="12">
                    <span>
                      <b>Dados Gerais do Evento</b>
                    </span>
                  </v-col>
                </v-row>
                <form class="form-horizontal">
                  <input
                    type="hidden"
                    id="event-id"
                    v-model="currentId"
                    class="form-control"
                  />

                  <v-row>
                    <v-col cols="12" sm="12">
                      <v-text-field
                        id="event-name"
                        class="form-control"
                        v-model="currentName"
                        outlined
                        label="Descrição *"
                        placeholder="Descrição do Evento"
                        dense
                        required
                        maxlength="30"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-row>
                    <v-col cols="12" sm="12">
                      <v-select
                        id="event-tipo"
                        :items="itemsTipos"
                        return-object
                        label="Tipo de Evento *"
                        autocomplete="off"
                        v-model="currentTipo"
                        item-text="descricao"
                        placeholder="Tipo de Evento"
                        outlined
                        dense
                      ></v-select>
                    </v-col>
                  </v-row>

                  <v-row>
                    <v-col cols="12" sm="4">
                      <v-menu
                        ref="menu"
                        v-model="menu"
                        :close-on-content-click="false"
                        :return-value.sync="currentStartDate"
                        transition="scale-transition"
                        offset-y
                        min-width="290px"
                      >
                        <template v-slot:activator="{ on, attrs }">
                          <v-text-field
                            v-model="currentStartDate"
                            label="Período Inicial"
                            outlined
                            readonly
                            v-bind="attrs"
                            v-on="on"
                            id="min-date"
                            type="date"
                            class="form-control"
                            dense
                          ></v-text-field>
                        </template>
                        <v-date-picker
                          v-model="currentStartDate"
                          no-title
                          scrollable
                        >
                          <v-spacer></v-spacer>
                          <v-btn text color="error" @click="menu = false"
                            >CANCELAR</v-btn
                          >
                          <v-btn
                            text
                            color="green"
                            @click="$refs.menu.save(currentStartDate)"
                            >SALVAR</v-btn
                          >
                        </v-date-picker>
                      </v-menu>
                    </v-col>

                    <v-col cols="12" sm="4">
                      <v-menu
                        ref="menu2"
                        v-model="menu2"
                        :close-on-content-click="false"
                        :return-value.sync="currentEndDate"
                        transition="scale-transition"
                        offset-y
                        min-width="290px"
                      >
                        <template v-slot:activator="{ on, attrs }">
                          <v-text-field
                            v-model="currentEndDate"
                            label="Período Final"
                            outlined
                            readonly
                            v-bind="attrs"
                            v-on="on"
                            type="date"
                            class="form-control"
                            dense
                          ></v-text-field>
                        </template>
                        <v-date-picker
                          v-model="currentEndDate"
                          no-title
                          scrollable
                        >
                          <v-spacer></v-spacer>
                          <v-btn text color="error" @click="menu2 = false"
                            >CANCELAR</v-btn
                          >
                          <v-btn
                            text
                            color="green"
                            @click="$refs.menu2.save(currentEndDate)"
                            >SALVAR</v-btn
                          >
                        </v-date-picker>
                      </v-menu>
                    </v-col>
                  </v-row>
                </form>
              </v-container>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn
                color="orange lighten-1"
                width="150"
                dark
                @click="cancelarModal"
                >Cancelar</v-btn
              >
              <v-btn color="orange darken-1" width="150" dark @click="saveEvent"
                >Salvar</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-dialog>

        <Calendar
          id="calendar"
          @mouse-on-day="mouseOnDay"
          render-style="border"
          language="pt"
          :display-week-number="true"
          :enable-range-selection="true"
          :data-source="dataSource"
          :enable-context-menu="true"
          :context-menu-items="contextMenuItems"
          display-week-first="1"
          @select-range="selectRange"
        ></Calendar>
      </v-container>
    </v-content>
  </div>
</template>

<script lang="js">
import Vue from "vue";

import Component from "vue-class-component";
import Calendar from "../../models/v-year-calendar";//'v-year-calendar';
import "../../models/v-year-calendar.pt";//'v-year-calendar/locales/v-year-calendar.pt';
import tippy from 'tippy.js';
import 'tippy.js/dist/tippy.css';
import axios from "../../plugins/axios";

var currentYear = new Date().getFullYear();

export default  {
  name: 'Calendario',
  components: {
    Calendar,
  },


  data() {
    return {
        itemsTipos: [],
        menu: false,
        menu2: false,
        show: false,
        tooltip: null,
        caminho: [
          {
            text: "Home",
            disabled: false,
            href: "/",
          },
        ],

        //SNACKBAR
        snackbar: false,
        text: "",
        vertical: true,
        color: "",
        colorBorder: "",
        show: false,
        dialogDelete: false,

        currentId: null,
        currentStartDate: null,
        currentEndDate: null,
        currentName: null,
        currentColor: null,
        currentTipo: null,
        currentWeek: null,
        dataSource: [],
        contextMenuItems: [
          {
            text: "  ALTERAR  ",
            click: evt => {
              this.currentId = evt.id;
              this.currentStartDate = evt.startDate.toISOString().substring(0, 10);
              this.currentEndDate = evt.endDate.toISOString().substring(0, 10);
              this.currentName = evt.name;
              this.currentColor = evt.color,
              this.currentTipo = evt.tipo,
              this.week = evt.week,
              this.show = true;

            }
          },
          {
            text: " EXCLUÍR ",
            click: evt => {
              this.currentId = evt.id;
              this.dialogDelete = true;
            }
          }
        ],
      
    };
  },

  methods: {

    modalCancelaDeleteItem(){
      this.dialogDelete = false;
      this.currentId = null;
    },

    deleteItemConfirmado() {
      axios
        .delete("Calendario/" + this.currentId)
        .then(response => {
          if (response.data.sucesso == true) {
            this.dataSource = [];
            this.dialogDelete = false;
            this.carregaCalendario();
            this.colorBorder = "green";
            this.color = "green";
            this.text = response.data.message;
            this.snackbar = true;

          }
        })
        .catch((e) => {
            console.log(e);
            
        });
      
    },

    carregaCalendario(){

      axios
        .get("Calendario" )
        .then(response => {
          let eventosCalendario = [];
          if (response.data.length > 0) {
            eventosCalendario = response.data;
            this.dataSource = [];

            let arrayEventosAgrupados = [];

            eventosCalendario.forEach((element) => {
                let existe = false;
                arrayEventosAgrupados.forEach((element2) => {
                    if(element2.idEvento == element.idEvento){
                        element2.itens.push(element)
                        existe = true;
                    }
                });
                
                if(!existe){
                    arrayEventosAgrupados.push({
                      idEvento: element.idEvento, 
                      itens: [element]
                    });
                }
            });


            for(let i = 0; i < arrayEventosAgrupados.length; i++){
            
              this.dataSource.push(
                {
                  id: arrayEventosAgrupados[i].idEvento,
                  name: arrayEventosAgrupados[i].itens[0].descricao,
                  startDate: new Date(arrayEventosAgrupados[i].itens[0].data),
                  endDate:new Date(arrayEventosAgrupados[i].itens[arrayEventosAgrupados[i].itens.length - 1].data),
                  tipo: arrayEventosAgrupados[i].itens[0].calendarioTipoDTO,
                  color: arrayEventosAgrupados[i].itens[0].calendarioTipoDTO.cor,
                  week: arrayEventosAgrupados[i].itens[0].week
                }
              );
            
            };
            
          }
        })
        .catch((e) => {
            console.log(e);
            
        });
    },

    carregarTiposFeriado() {
      axios
        .get("CalendarioTipo" )
        .then(response => {
        
            if (response.data) {
            this.itemsTipos = response.data 
            }
        })
        .catch((e) => {
            console.log(e);
            
        });
    },

    cancelarModal(){
        this.show = false;
    },
    selectRange: function(e) {

      this.currentId = null;
      this.currentName = null;
      this.currentStartDate = e.startDate.toISOString().substring(0, 10);
      this.currentEndDate = e.endDate.toISOString().substring(0, 10);
      this.currentTipo = e.tipo,
      this.currentColor = null,      
      this.currentWeek = null,      
      this.Calendario = [];
      this.Calendario = e.calendar; 
      this.show = true;
    },


    saveEvent: function(e) {


      if (!this.validateEvento()){

        var dataEvt = new Date(this.currentStartDate+"T00:00:00");

        var calendarioEventos = [];
        calendarioEventos.push({
          descricao: this.currentName,
          data: new Date(dataEvt),
          week: this.Calendario.getWeekNumber(new Date(this.currentStartDate+"T00:00:00")).toString()
        });

        while(dataEvt <= new Date(this.currentEndDate) ){
            
          var dataEvento = new Date(dataEvt.setDate(dataEvt.getDate() + 1));
          var weekEvento = this.Calendario.getWeekNumber(new Date(dataEvt));
          calendarioEventos.push({
            descricao: this.currentName,
            data: dataEvento,
            week: weekEvento.toString(),
          });
            
        }

       

        if (this.currentId !== null){

          // ALTERAR
          axios
            .put("Calendario/" + this.currentId, {
              idCalendarioTipo: this.currentTipo.id,
              Eventos: calendarioEventos
            }).then(response => {
                
                if (response.data.sucesso == true) {
                  this.dataSource = [];
                  this.show = false;
                  this.colorBorder = "green";
                  this.color = "green";
                  this.text = response.data.message;
                  this.snackbar = true;
                  
                  this.carregaCalendario();
                }
                
            })
            .catch((e) => {
                console.log(e);
            });
          
        }else{

          // NOVO
          axios
            .post("Calendario", {
              idCalendarioTipo: this.currentTipo.id,
              Eventos: calendarioEventos
            }).then(response => {
                
                if (response.data.sucesso == true) {
                  this.colorBorder = "green";
                  this.color = "green";
                  this.text = response.data.message;
                  this.snackbar = true;
                  this.show = false;
                  this.carregaCalendario();
                }
                
            })
            .catch((e) => {
                console.log(e);
            });
        }
      }
    },


    validateEvento(){
      if (this.currentName == "" || this.currentName == null){
        this.colorBorder = "warning";
        this.color = "warning";
        this.text = "Informe a descrição do Evento.";
        this.snackbar = true;
        return true;
      }

      if (this.currentTipo == "" || this.currentTipo == null || this.currentTipo.length == 0){
        this.colorBorder = "warning";
        this.color = "warning";
        this.text = "Selecione o Tipo do Evento.";
        this.snackbar = true;
        return true;
      }

      if (this.currentEndDate < this.currentStartDate){
        this.colorBorder = "warning";
        this.color = "warning";
        this.text = "A data Inicial não pdoe ser superior a data Final do Evento.";
        this.snackbar = true;
        return true;
      }
    },


    mouseOnDay: function(e) {
      this.Calendario = [];
      this.Calendario = e.calendar; 
      if (e.events.length > 0) {
        var content = '';

        for (var i in e.events) {
       
          content += e.events[i].name;
        }

        if (this.tooltip != null) {
          this.tooltip.destroy();
          this.tooltip = null;
        }

        this.tooltip = tippy(e.element, {
            placement: 'right',
            content: content,
            animateFill: false,
            animation: 'shift-away',
            arrow: true
        });
        this.tooltip.show();
      }
    },
    mouseOutDay: function() {
      if (this.tooltip !== null) {
        this.tooltip.destroy();
        this.tooltip = null;
      }
    },
  },

  created: function(e) {
  
    this.carregarTiposFeriado();
    this.carregaCalendario();

  }


}
</script>

<style>
body {
  font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
}

.calendar .calendar-header .year-neighbor {
  color: inherit;
  opacity: 0.7;
}

body {
  font-family: "Roboto", "Helvetica Neue", Helvetica, Arial, sans-serif;
}

.calendar .calendar-header {
  box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.2);
  border: 0;
}

.calendar .calendar-header .year-title {
  font-size: 18px;
}

.calendar
  .calendar-header
  .year-title:not(.year-neighbor):not(.year-neighbor2) {
  border-bottom: 2px solid #BF360C;
}

.calendar .months-container .month-container {
  height: 260px;
  margin-bottom: 25px;
}

.calendar table.month {
  height: 100%;
}

.calendar table.month td.day .day-content {
  padding: 8px;
}
</style>