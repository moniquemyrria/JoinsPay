//TipoDespesaFormContract

export interface ITipoDespesaFormView {

    loading(visible: boolean): void
    showMsg(msg: string, modo: number): void
    carregarDtos(): void
    showTipoDespesaForEditar(item: any): void
    
}

export interface ITipoDespesaFormController {

    carregarDtos(id: string): void
    salvar(peril: any): void
    update(id: string, perfil: any): void

}