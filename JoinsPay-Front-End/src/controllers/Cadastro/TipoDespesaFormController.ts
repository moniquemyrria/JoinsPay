import {
  ITipoDespesaFormView,
  ITipoDespesaFormController,
} from "../../contracts/Cadastro/TipoDespesaFormContract";
import axios from "../../plugins/axios";

import { Header, HeaderIcon, HeaderAction } from "../../models/header";
import { Headers } from "../../models/headers";

export class TipoDespesaFormController implements ITipoDespesaFormController {
  _view: ITipoDespesaFormView;

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

  constructor(view: ITipoDespesaFormView) {
    this._view = view;
  }

  carregarDtos(id: string): void {

    this._view.loading(true);


    if (id != undefined) {
      //CARREGA EDITAR
      axios
        .get("TipoDespesa/" + id)
        .then((response: any) => {
          this._view.loading(false);
          if (response.data) {
            this._view.showTipoDespesaForEditar(response.data);
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
      .post("TipoDespesa", funcionario)
      .then((response: any) => {
        this._view.loading(false);
        if (response.data.sucesso) {
          this._view.showMsg(response.data.message, 1);

        } else {
          this._view.showMsg("Não foi possível salvar os dados do TipoDespesa", 0);
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
      .put("TipoDespesa/" + idFuncionario, funcionario)
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
