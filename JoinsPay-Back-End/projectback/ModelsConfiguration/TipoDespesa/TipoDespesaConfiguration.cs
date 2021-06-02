using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using projectback.ModelsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectback.ModelsConfiguration
{
    public class TipoDespesaConfiguration
    {
        public TipoDespesaConfiguration(EntityTypeBuilder<TipoDespesaDTO> entity)
        {
            if (entity != null)
            {
                entity.ToTable("Tipo_Despesa").HasKey(t => t.id);

                entity.Property(t => t.sigla).HasMaxLength(6).IsRequired();
                
                entity.Property(t => t.descricao).HasMaxLength(30).IsRequired();
                
                entity.Property(t => t.color).HasMaxLength(10);

                entity.Property(t => t.deletado).HasMaxLength(1);

                entity.Property(t => t.dataCadastro);


            }
        }
    }
}
