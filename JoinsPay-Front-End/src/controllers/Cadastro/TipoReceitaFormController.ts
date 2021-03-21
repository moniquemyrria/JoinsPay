import {
  ITipoReceitaFormView,
  ITipoReceitaFormController,
} from "../../contracts/Cadastro/TipoReceitaFormContract";
import axios from "../../plugins/axios";

import { Header, HeaderIcon, HeaderAction } from "../../models/header";
import { Headers } from "../../models/headers";

export class TipoReceitaFormController implements ITipoReceitaFormController {
  _view: ITipoReceitaFormView;

  _lista = [
    {
      numero: "",
      produto: "",
      descricao: "",
      quantidade: "",
      dataNecessidade: "",
      um: "",
      justificativa: "",
      acao: [new HeaderAction("", "", "", "", "")],
    },
  ];

  constructor(view: ITipoReceitaFormView) {
    this._view = view;
  }

  carregarDtos(id: string): void {

    this._view.loading(true);


    if (id != undefined) {
      //CARREGA EDITAR
      axios
        .get("TipoReceita/" + id)
        .then((response: any) => {
          this._view.loading(false);
          if (response.data) {
            this._view.showTipoReceitaForEditar(response.data);
          }
        })
        .catch((e: any) => {
          console.log(e);
          this._view.loading(false);
          this._view.showMsg(e, 0);
        });
    }else{
      this._view.loading(false);
    }
  }


  salvar(funcionario: any): void {

    this._view.loading(true);

    axios
      .post("TipoReceita", funcionario)
      .then((response: any) => {
        this._view.loading(false);
        if (response.data.sucesso) {
          this._view.showMsg(response.data.message, 1);

        } else {
          this._view.showMsg("Não foi possível salvar os dados do TipoReceita", 0);
        }
      })
      .catch((e: any) => {
        this._view.loading(false);
        this._view.showMsg(e, 0);
      });

  }

  update(idFuncionario: string, funcionario: any): void {

    this._view.loading(true);

    axios
      .put("TipoReceita/" + idFuncionario, funcionario)
      .then((response: any) => {
        this._view.loading(false);
        if (response.data.sucesso) {
          this._view.showMsg(response.data.message, 1);
        } else {
          this._view.showMsg(response.data.message, 0);
        }
      })
      .catch((e: any) => {
        this._view.loading(false);
        this._view.showMsg(e, 0);
      });

  }


}
