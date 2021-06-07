//TipoContaFormContract

export interface ITipoContaFormView {

    loading(visible: boolean): void
    showMsg(msg: string, modo: number): void
    carregarDtos(): void
    showTipoContaForEditar(item: any): void
    
}

export interface ITipoContaFormController {

    carregarDtos(id: string): void
    salvar(peril: any): void
    update(id: string, perfil: any): void

}