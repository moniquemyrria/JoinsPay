export interface IAlteracaoSenhaView {

    loading(visible: boolean): void
    showMsg(msg: string): void
    pushMain(): void

}

export interface IAlteracaoSenhaController {

    login(matricula: string, senha: string, novaSenha: string): void
   
}