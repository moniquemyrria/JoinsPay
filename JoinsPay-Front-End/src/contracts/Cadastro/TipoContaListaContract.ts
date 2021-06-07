//TipoContaFormContract

export interface ITipoContaListaView {

    loading(visible: boolean): void
    showMsg(msg: string): void
    showItens(list: []): void

}

export interface ITipoContaListaController {
    carregarItens(): void
    deleteitem(id: number): void
}