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
    public class ContaConfiguration
    {
        public ContaConfiguration(EntityTypeBuilder<ContaDTO> entity)
        {
            if (entity != null)
            {
                entity.ToTable("Conta").HasKey(t => t.id);

                entity.Property(t => t.codigo).HasMaxLength(10).IsRequired();
                
                entity.Property(t => t.nome).HasMaxLength(50).IsRequired();
                
                entity.Property(t => t.agencia).HasMaxLength(10).IsRequired();
                
                entity.Property(t => t.numeroConta).HasMaxLength(10).IsRequired();

                entity.Property(t => t.dataCadastro);

                entity.Property(t => t.dataCadastro).HasMaxLength(1).IsRequired();

                //FK - TipoConta
                entity.HasOne(t => t.TipoContaDTO).WithMany(t => t.Conta).HasForeignKey(t => t.idTipoConta).HasPrincipalKey(t => t.id);




            }
        }
    }
}
