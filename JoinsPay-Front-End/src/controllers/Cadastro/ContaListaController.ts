import { IContaListaView, IContaListaController } from "../../contracts/Cadastro/ContaListaContract";
import axios from "../../plugins/axios"

import { Storage } from "../../configuration/storage"
import { Header, HeaderIcon, HeaderAction } from "../../models/header";
import { Headers } from "../../models/headers";

export class ContaListaController implements IContaListaController {

    _view: IContaListaView

    constructor(view: IContaListaView) {
        this._view = view
    }

    carregarItens(): void {
        this._view.loading(true)

        axios
            .get('Conta')
            .then((response: any) => {

                response.data.forEach((element: any) => {

                    if (element.padrao == "N") {
                        element.acoes = [
                            new HeaderAction("mdi-pencil", "primary", "Teste", "editarItem", "200px"),
                            new HeaderAction("mdi-delete", "primary", "Teste", "deleteItem", "200px")
                        ]
                    }
                });

                this._view.loading(false)
                this._view.showItens(response.data)
            }).catch((e: any) => {
                this._view.loading(false)
                this._view.showMsg(e)
            })
    }


    deleteitem(id: number): void {
        this._view.loading(true)

        axios.delete('Conta/' + id).then((response: any) => {
            this._view.loading(false)
            if (response.data.sucesso) {
                // this._view.showMsg("Salvo com sucesso.")
                this._view.showMsg(response.data.message)
                this.carregarItens()
            } else {
                this._view.showMsg(response.data.message)
            }
        }).catch((e: any) => {
            this._view.loading(false)
            this._view.showMsg(e)
        })
    }


}