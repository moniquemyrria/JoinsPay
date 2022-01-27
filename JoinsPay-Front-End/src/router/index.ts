import Vue from "vue";
import VueRouter from "vue-router";

Vue.use(VueRouter);

const routes = [
  {
    path: "/login",
    name: "Login",
    component: () => import("../views/Login.vue"),
  },
  {
    path: "/alteracaoSenha/:matricula",
    name: "AlteracaoSenha",
    component: () => import("../views/AlteracaoSenha.vue"),
  },
  {
    path: "/alteracaoSenha",
    name: "AlteracaoSenhaEdit",
    component: () => import("../views/AlteracaoSenha.vue"),
  },
  
  {
    path: "/",
    name: "Home",
    component: () => import("../views/Home.vue"),
    children: [
      {
        path: "/tipoReceitas",
        name: "TipoReceita",
        component: () => import("../views/Cadastro/TipoReceitaLista.vue"),
      },
      {
        path: "/tipoReceita/:id",
        name: "TipoReceitaEdit",
        component: () => import("../views/Cadastro/TipoReceitaForm.vue"),
      },
      {
        path: "/tipoReceita",
        name: "TipoReceita",
        component: () => import("../views/Cadastro/TipoReceitaForm.vue"),
      },

      {
        path: "/tipoDespesas",
        name: "TipoDespesa",
        component: () => import("../views/Cadastro/TipoDespesaLista.vue"),
      },
      {
        path: "/tipoDespesa/:id",
        name: "TipoDespesaEdit",
        component: () => import("../views/Cadastro/TipoDespesaForm.vue"),
      },
      {
        path: "/tipoDespesa",
        name: "TipoDespesa",
        component: () => import("../views/Cadastro/TipoDespesaForm.vue"),
      },
      {
        path: "/tipoContas",
        name: "TipoConta",
        component: () => import("../views/Cadastro/TipoContaLista.vue"),
      },
      {
        path: "/tipoConta/:id",
        name: "TipoContaEdit",
        component: () => import("../views/Cadastro/TipoContaForm.vue"),
      },
      {
        path: "/tipoConta",
        name: "TipoConta",
        component: () => import("../views/Cadastro/TipoContaForm.vue"),
      },

      {
        path: "/contas",
        name: "Conta",
        component: () => import("../views/Cadastro/ContaLista.vue"),
      },
      // {
      //   path: "/conta/:id",
      //   name: "ContaEdit",
      //   component: () => import("../views/Cadastro/ContaForm.vue"),
      // },
      // {
      //   path: "conta",
      //   name: "Conta",
      //   component: () => import("../views/Cadastro/ContaForm.vue"),
      // },

  
      {
        path: "/calendario/",
        name: "CalendarioView",
        component: () => import("../views/Calendario/CalendarioLista.vue"),
      },
     
    ],
  },
];

const router = new VueRouter({
  mode: "history",
  // base: "/smartpoint/",
  base: "/",
  routes,
});

export default router;
