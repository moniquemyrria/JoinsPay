//ContaFormContract

export interface IContaListaView {

    loading(visible: boolean): void
    showMsg(msg: string): void
    showItens(list: []): void

}

export interface IContaListaController {
    carregarItens(): void
    deleteitem(id: number): void
}