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
    public class TipoContaController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TipoContaController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/TipoConta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoContaDTO>>> GetTipoContas()
        {
            return await _context.TipoConta.Where(t => t.deletado == "N").ToListAsync();
        }

        // GET: api/TipoConta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoContaDTO>> GetTipoContaDTO(long id)
        {
            var tipoContaDTO = await _context
                                    .TipoConta
                                    .FirstOrDefaultAsync(t => t.id == id);

            if (tipoContaDTO == null)
            {
                return NotFound();
            }

            return tipoContaDTO;
        }

        // PUT: api/TipoConta/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<KRetorno> PutTipoContaDTO(long id, TipoContaDTO tipoContaDTO)
        {
            var retorno = new KRetorno();

            var tipoConta = _context
                                .TipoConta
                                .FirstOrDefault(t => t.id == id);

            if (id != tipoConta.id)
            {
                retorno.Sucesso = false;
                retorno.Message = "Tipo de Conta não localizada.";
            }
            else
            {
                try
                {
                    tipoConta.sigla       = tipoContaDTO.sigla;
                    tipoConta.descricao   = tipoContaDTO.descricao;
                    tipoConta.deletado    = tipoContaDTO.deletado;
                    _context.Entry(tipoConta).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    retorno.Sucesso = true;
                    retorno.Message = "Dados do Tipo de Conta alterada com sucesso.";
                }
                catch (Exception e)
                {
                    retorno.Sucesso = false;
                    retorno.Message = "Não foi possível alterar os dados do Tipo de receita. " + e;

                }
            }

            return retorno;//NoContent();
        }

        // POST: api/TipoConta
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<KRetorno>> PostUsuarioDTO(TipoContaDTO tipoContaDTO)
        {
            var retorno = new KRetorno();

            try
            {
                _context.TipoConta.Add(tipoContaDTO);
                await _context.SaveChangesAsync();

                retorno.Sucesso = true;
                retorno.Message = "Dados do novo Tipo de Conta cadastrada com sucesso.";

            }
            catch (Exception e)
            {
                retorno.Sucesso = false;
                retorno.Message = "Não foi possível cadastrar o novo Tipo de Receira. Error: " + e;
            }


            return retorno;

        }

        // DELETE: api/TipoConta/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<KRetorno>> DeleteTipoContaDTO(long id)
        {
            var retorno = new KRetorno();

            var tipoContaDTO = await _context.TipoConta.FindAsync(id);
            if (tipoContaDTO == null)
            {
                return NotFound();
            }

            try
            {
                var data = _context.TipoConta.FirstOrDefault(t => t.id == id);

                data.deletado = "S";

                _context.TipoConta.Update(data);
                _context.SaveChanges();

                retorno.Sucesso = true;
                retorno.Message = "Tipo de Conta deletada com sucesso.";
            }
            catch
            {
                if (!TipoContaDTOExists(id))
                {
                    //return NotFound();
                    retorno.Sucesso = false;
                    retorno.Message = "Tipo de Conta não localizada.";
                }
                else
                {
                    throw;
                }
            }

            return retorno;
        }

        private bool TipoContaDTOExists(long id)
        {
            return _context.TipoConta.Any(e => e.id == id);
        }
    }
}
