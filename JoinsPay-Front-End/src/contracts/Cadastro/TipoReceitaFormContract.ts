//TipoReceitaFormContract

export interface ITipoReceitaFormView {

    loading(visible: boolean): void
    showMsg(msg: string, modo: number): void
    carregarDtos(): void
    showTipoReceitaForEditar(item: any): void
    
}

export interface ITipoReceitaFormController {

    carregarDtos(id: string): void
    salvar(peril: any): void
    update(id: string, perfil: any): void

}