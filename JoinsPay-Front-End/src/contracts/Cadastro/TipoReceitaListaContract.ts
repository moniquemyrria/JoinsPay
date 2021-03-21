//TipoReceitaFormContract

export interface ITipoReceitaListaView {

    loading(visible: boolean): void
    showMsg(msg: string): void
    showItens(list: []): void

}

export interface ITipoReceitaListaController {
    carregarItens(): void
    deleteitem(id: number): void
}