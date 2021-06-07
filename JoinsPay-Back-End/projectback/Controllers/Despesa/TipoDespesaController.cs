using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryMR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectback.Context;
using projectback.ModelsData;
using projectback.ModelView;

namespace projectback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDespesaController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TipoDespesaController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/TipoDespesa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoDespesaDTO>>> GetTipoDespesas()
        {
            return await _context.TipoDespesa.Where(t => t.deletado == "N").ToListAsync();
        }

        // GET: api/TipoDespesa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoDespesaDTO>> GetTipoDespesaDTO(long id)
        {
            var tipoDespesaDTO = await _context
                                    .TipoDespesa
                                    .FirstOrDefaultAsync(t => t.id == id);

            if (tipoDespesaDTO == null)
            {
                return NotFound();
            }

            return tipoDespesaDTO;
        }

        // PUT: api/TipoDespesa/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<KRetorno> PutTipoDespesaDTO(long id, TipoDespesaDTO tipoDespesaDTO)
        {
            var retorno = new KRetorno();

            var tipoDespesa = _context
                                .TipoDespesa
                                .FirstOrDefault(t => t.id == id);

            if (id != tipoDespesa.id)
            {
                retorno.Sucesso = false;
                retorno.Message = "Tipo de Despesa não localizada.";
            }
            else
            {
                try
                {
                    tipoDespesa.sigla       = tipoDespesaDTO.sigla;
                    tipoDespesa.descricao   = tipoDespesaDTO.descricao;
                    tipoDespesa.color       = tipoDespesaDTO.color;
                    _context.Entry(tipoDespesa).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    retorno.Sucesso = true;
                    retorno.Message = "Dados do Tipo de Despesa alterada com sucesso.";
                }
                catch (Exception e)
                {
                    retorno.Sucesso = false;
                    retorno.Message = "Não foi possível alterar os dados do Tipo de despesa. " + e;

                }
            }

            return retorno;//NoContent();
        }

        // POST: api/TipoDespesa
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<KRetorno>> PostUsuarioDTO(TipoDespesaDTO tipoDespesaDTO)
        {
            var retorno = new KRetorno();

            try
            {
                _context.TipoDespesa.Add(tipoDespesaDTO);
                await _context.SaveChangesAsync();

                retorno.Sucesso = true;
                retorno.Message = "Dados do novo Tipo de Despesa cadastrada com sucesso.";

            }
            catch (Exception e)
            {
                retorno.Sucesso = false;
                retorno.Message = "Não foi possível cadastrar o novo Tipo de Receira. Error: " + e;
            }


            return retorno;

        }

        // DELETE: api/TipoDespesa/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<KRetorno>> DeleteTipoDespesaDTO(long id)
        {
            var retorno = new KRetorno();

            var tipoDespesaDTO = await _context.TipoDespesa.FindAsync(id);
            if (tipoDespesaDTO == null)
            {
                return NotFound();
            }

            try
            {
                var data = _context.TipoDespesa.FirstOrDefault(t => t.id == id);

                data.deletado = "S";

                _context.TipoDespesa.Update(data);
                _context.SaveChanges();

                retorno.Sucesso = true;
                retorno.Message = "Tipo de Despesa deletada com sucesso.";
            }
            catch
            {
                if (!TipoDespesaDTOExists(id))
                {
                    //return NotFound();
                    retorno.Sucesso = false;
                    retorno.Message = "Tipo de Despesa não localizada.";
                }
                else
                {
                    throw;
                }
            }

            return retorno;
        }

        private bool TipoDespesaDTOExists(long id)
        {
            return _context.TipoDespesa.Any(e => e.id == id);
        }
    }
}
