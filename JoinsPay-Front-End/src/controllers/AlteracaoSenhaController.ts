import { IAlteracaoSenhaView, IAlteracaoSenhaController } from "../contracts/AlteracaoSenhaContract";
import axios from "../plugins/axios";
import { Storage } from "../configuration/storage";
export class AlteracaoSenhaController implements IAlteracaoSenhaController {
  _view: IAlteracaoSenhaView;

  constructor(view: IAlteracaoSenhaView) {
    this._view = view;

  }

  login(matricula: string, senha: string, novaSenha: string): void {
    this._view.loading(true);

    //Processamento
    let login = new Login(matricula, senha, novaSenha);

    axios.post('Login', login).then((response: any) => {
      this._view.loading(false)
      if (response.data.sucesso == true) {
        Storage.salvar("Logado", "true")
        Storage.salvar("Session", response.data.message)
        //Storage.salvar("User", response.data.tRetorno.codigo)
        //Storage.salvar("Colaborador", JSON.stringify(response.data.tRetorno))
        
        if (response.data.message == 'Primeiro acesso.') {
          this.alterarSenhaLogin(matricula, senha, novaSenha);
        }

        if (response.data.message == 'Funcionario com permissao de acesso.') {
          this.alterarSenhaLogin(matricula, senha, novaSenha);
        }

      } else {
        this._view.showMsg(response.data.message)
      }
    }).catch((e: any) => {
      this._view.loading(false)
      this._view.showMsg(e)
    })
    this._view.loading(false);
  }

  alterarSenhaLogin(matricula: string, senha: string, novaSenha: string): void {
    this._view.loading(true);

    //Processamento
    let login = new Login(matricula, senha, novaSenha);

    axios.put('Login/alteracaosenha', login).then((response: any) => {
      this._view.loading(false)
      if (response.data.sucesso == true) {
        Storage.salvar("Logado", "true")
        Storage.salvar("Session", response.data.message)
        //Storage.salvar("User", response.data.tRetorno.codigo)
        //Storage.salvar("Colaborador", JSON.stringify(response.data.tRetorno))
        this._view.showMsg(response.data.message)
        this._view.pushMain();
      } else {
        this._view.showMsg(response.data.message)
      }
    }).catch((e: any) => {
      this._view.loading(false)
      this._view.showMsg(e)
    })
    this._view.loading(false);
  }
}

class Login {
  matricula: string;
  senha: string;
  novaSenha: string;
  constructor(matricula: string, senha: string, novaSenha: string) {
    this.matricula = matricula;
    this.senha = senha;
    this.novaSenha = novaSenha;
  }

}

