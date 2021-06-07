using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace projectback.ModelsData
{
    public class TipoContaDTO
    {
        public long id { get; set; }

        public string sigla { get; set; }

        public string descricao { get; set; }

        public string deletado { get; set; }
        
        public string padrao { get; set; }

        [JsonIgnore]
        public List<ContaDTO> Conta { get; set; }

    }
}
