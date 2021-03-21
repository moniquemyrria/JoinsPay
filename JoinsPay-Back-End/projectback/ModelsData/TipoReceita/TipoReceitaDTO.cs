using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace projectback.ModelsData
{
    public class TipoReceitaDTO
    {
        public long id { get; set; }

        public string sigla { get; set; }

        public string descricao { get; set; }

        public string color { get; set; }

        public DateTime dataCadastro { get; set; }

        public string deletado { get; set; }

    }
}
