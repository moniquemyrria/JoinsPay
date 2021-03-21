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
