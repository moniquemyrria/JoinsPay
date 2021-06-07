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
    public class TipoReceitaController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TipoReceitaController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/TipoReceita
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoReceitaDTO>>> GetTipoReceitas()
        {
            return await _context.TipoReceita.Where(t => t.deletado == "N").ToListAsync();
        }

        // GET: api/TipoReceita/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoReceitaDTO>> GetTipoReceitaDTO(long id)
        {
            var tipoReceitaDTO = await _context
                                    .TipoReceita
                                    .FirstOrDefaultAsync(t => t.id == id);

            if (tipoReceitaDTO == null)
            {
                return NotFound();
            }

            return tipoReceitaDTO;
        }

        // PUT: api/TipoReceita/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<KRetorno> PutTipoReceitaDTO(long id, TipoReceitaDTO tipoReceitaDTO)
        {
            var retorno = new KRetorno();

            var tipoReceita = _context
                                .TipoReceita
                                .FirstOrDefault(t => t.id == id);

            if (id != tipoReceita.id)
            {
                retorno.Sucesso = false;
                retorno.Message = "Tipo de Receita não localizada.";
            }
            else
            {
                try
                {
                    tipoReceita.sigla       = tipoReceitaDTO.sigla;
                    tipoReceita.descricao   = tipoReceitaDTO.descricao;
                    tipoReceita.color       = tipoReceitaDTO.color;
                    _context.Entry(tipoReceita).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    retorno.Sucesso = true;
                    retorno.Message = "Dados do Tipo de Receita alterada com sucesso.";
                }
                catch (Exception e)
                {
                    retorno.Sucesso = false;
                    retorno.Message = "Não foi possível alterar os dados do Tipo de receita. " + e;

                }
            }

            return retorno;//NoContent();
        }

        // POST: api/TipoReceita
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<KRetorno>> PostUsuarioDTO(TipoReceitaDTO tipoReceitaDTO)
        {
            var retorno = new KRetorno();

            try
            {
                _context.TipoReceita.Add(tipoReceitaDTO);
                await _context.SaveChangesAsync();

                retorno.Sucesso = true;
                retorno.Message = "Dados do novo Tipo de Receita cadastrada com sucesso.";

            }
            catch (Exception e)
            {
                retorno.Sucesso = false;
                retorno.Message = "Não foi possível cadastrar o novo Tipo de Receira. Error: " + e;
            }


            return retorno;

        }

        // DELETE: api/TipoReceita/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<KRetorno>> DeleteTipoReceitaDTO(long id)
        {
            var retorno = new KRetorno();

            var tipoReceitaDTO = await _context.TipoReceita.FindAsync(id);
            if (tipoReceitaDTO == null)
            {
                return NotFound();
            }

            try
            {
                var data = _context.TipoReceita.FirstOrDefault(t => t.id == id);

                data.deletado = "S";

                _context.TipoReceita.Update(data);
                _context.SaveChanges();

                retorno.Sucesso = true;
                retorno.Message = "Tipo de Receita deletada com sucesso.";
            }
            catch
            {
                if (!TipoReceitaDTOExists(id))
                {
                    //return NotFound();
                    retorno.Sucesso = false;
                    retorno.Message = "Tipo de Receita não localizada.";
                }
                else
                {
                    throw;
                }
            }

            return retorno;
        }

        private bool TipoReceitaDTOExists(long id)
        {
            return _context.TipoReceita.Any(e => e.id == id);
        }
    }
}
