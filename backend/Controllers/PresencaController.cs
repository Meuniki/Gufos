using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Para adicionar a árvore de objetos adicionamos uma nova biblioteca JSON
// dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson

namespace backend.Controllers
{
    // Definimos nossa rota do controller e dizemos que é um controller de API
    [Route("api/[controller]")]
    [ApiController]
    public class PresencaController: ControllerBase
    {
        PresencasRepository _repositorio = new PresencasRepository();

        //GET: api/Presenca
        [HttpGet]
        public async Task<ActionResult<List<Presencas>>> Get()
        {
            var presencas = await _repositorio.Listar();
            
            if(presencas == null){
                return NotFound();
            }
            return presencas;
        }
        //GET: api/Presenca/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Presencas>> Get(int id)
        {
            var presenca = await _repositorio.BuscarPorId(id);
            
            if(presenca == null){
                return NotFound();
            }
            return presenca;
        }

        [HttpPost]
        public async Task<ActionResult<Presencas>> Post(Presencas presenca){
            try{
                await _repositorio.Salvar(presenca);
            }catch(DbUpdateConcurrencyException){
                throw;
            }
            return presenca;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Presencas presenca){
            // Se o ID do objeto não existir, ele retorna o erro 400
            if(id != presenca.PresecaId){
                return BadRequest();
            }
            try{
                await _repositorio.Alterar(presenca);
            }catch(DbUpdateConcurrencyException){

                var presenca_valido = await _repositorio.BuscarPorId(id);

                if(presenca_valido == null){
                    return NotFound();
                }else{
                    throw;
                }
            }
            // NoContent = Retorna 204, sem nada
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Presencas>> Delete(int id){
            var presenca = await _repositorio.BuscarPorId(id);
            if(presenca == null){
                return NotFound();
            }

            await _repositorio.Excluir(presenca);
            return presenca;
        }
    }
}