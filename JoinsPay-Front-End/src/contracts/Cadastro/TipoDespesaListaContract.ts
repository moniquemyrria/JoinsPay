//TipoDespesaFormContract

export interface ITipoDespesaListaView {

    loading(visible: boolean): void
    showMsg(msg: string): void
    showItens(list: []): void

}

export interface ITipoDespesaListaController {
    carregarItens(): void
    deleteitem(id: number): void
}