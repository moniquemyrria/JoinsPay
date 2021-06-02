using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace projectback.ModelsData
{
    public class ContaDTO
    {
        public long id { get; set; }

        public long idTipoConta { get; set; }

        public string codigo { get; set; }

        public string nome { get; set; }

        public string agencia { get; set; }

        public string numeroConta { get; set; }
        
        public DateTime dataCadastro { get; set; }

        public string deletado { get; set; }

        public TipoContaDTO TipoContaDTO { get; set; }

    }
}
