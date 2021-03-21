import { KRetorno } from "../baseGenerica/KRetorno";

export class Validation {

    /**
     * Valid Schema
     */
    public ValidSchema(schema: ValidationSchema[], json: any): KRetorno {
        let retorno = new KRetorno();

        retorno.sucesso = true;
        retorno.mensagem = "";

        schema = schema.reverse()

        schema.forEach((validador: ValidationSchema) => {
            
            let value = json[validador.name];
            
            if (value) {
                
                if (typeof value !== validador.type) {
                    retorno.sucesso = false;
                    retorno.mensagem = "Tipo do campo " + validador.textName + " inváilido!";
                    return retorno;
                }

                if (validador.required && !value) {
                    retorno.sucesso = false;
                    retorno.mensagem = "Campo " + validador.textName + " é obrigatório!";
                    return retorno;
                }

                if (validador.type == "string" && validador.maxLength > 0 && (value + '').length > validador.maxLength) {
                    retorno.sucesso = false;
                    retorno.mensagem = "Campo " + validador.textName + " ultrapassou o tamanho permitido para o campo!";
                    return retorno;
                }
                if (validador.valid) {
                    let result = validador.valid(value);
                    if (!result.sucesso) {
                        retorno.sucesso = false;
                        retorno.mensagem = result.mensagem;
                        return retorno;
                    }
                }

               
            } else {

                if (validador.required) {
                    retorno.sucesso = false;
                    retorno.mensagem = "Campo " + validador.textName + " é obrigatório!";
                    return retorno;
                }
            }
        })


        return retorno;
    }
}

export class ValidationSchema {
    name: string
    textName: string
    type: string
    required: boolean
    maxLength: number
    valid?: (item: any) => (KRetorno)

    constructor(name: string, textName: string, type: string = "string", required: boolean = false, maxLength: number = 0, valid?: (item: any) => KRetorno) {
        this.name = name
        this.textName = textName
        this.type = type
        this.required = required
        this.maxLength = maxLength
        this.valid = valid
    }
}