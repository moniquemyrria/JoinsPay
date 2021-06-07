import { ITipoContaListaView, ITipoContaListaController } from "../../contracts/Cadastro/TipoContaListaContract";
import axios from "../../plugins/axios"

import { Storage } from "../../configuration/storage"
import { Header, HeaderIcon, HeaderAction } from "../../models/header";
import { Headers } from "../../models/headers";

export class TipoContaListaController implements ITipoContaListaController {

    _view: ITipoContaListaView

    constructor(view: ITipoContaListaView) {
        this._view = view
    }

    carregarItens(): void {
        this._view.loading(true)

        axios
            .get('TipoConta')
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

        axios.delete('TipoConta/' + id).then((response: any) => {
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