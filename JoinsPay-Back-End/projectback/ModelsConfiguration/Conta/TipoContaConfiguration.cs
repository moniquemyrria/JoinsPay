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
    public class TipoContaConfiguration
    {
        public TipoContaConfiguration(EntityTypeBuilder<TipoContaDTO> entity)
        {
            if (entity != null)
            {
                entity.ToTable("Tipo_Conta").HasKey(t => t.id);

                entity.Property(t => t.sigla).HasMaxLength(6).IsRequired();
                
                entity.Property(t => t.descricao).HasMaxLength(30).IsRequired();

                entity.Property(t => t.deletado).HasMaxLength(1);
                
                entity.Property(t => t.padrao).HasMaxLength(1);

            }
        }
    }
}
