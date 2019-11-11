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
    public class TipoUsuarioController: ControllerBase
    {
        TipoUsuarioRepository _repositorio = new TipoUsuarioRepository();

        //GET: api/TipoUsuario
        [HttpGet]
        public async Task<ActionResult<List<TipoUsuario>>> Get()
        {
            var usuarios = await _repositorio.Listar();
            
            if(usuarios == null){
                return NotFound();
            }
            return usuarios;
        }
        //GET: api/TipoUsuario/2
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoUsuario>> Get(int id)
        {
            var usuario = await _repositorio.BurcarPorId(id);
            
            if(usuario == null){
                return NotFound();
            }
            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult<TipoUsuario>> Post(TipoUsuario usuario){
            try{

                await _repositorio.Salvar(usuario);
            }catch(DbUpdateConcurrencyException){
                throw;
            }
            return usuario;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, TipoUsuario usuario){
            // Se o ID do objeto não existir, ele retorna o erro 400
            if(id != usuario.TipoUsuarioId){
                return BadRequest();
            }
            
            try{
                await _repositorio.Alterar(usuario);
            }catch(DbUpdateConcurrencyException){

                var usuario_valido = await _repositorio.BurcarPorId(id);
                if(usuario_valido == null){
                    return NotFound();
                }else{
                    throw;
                }
            }
            // NoContent = Retorna 204, sem nada
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoUsuario>> Delete(int id){
            
            var usuario = await _repositorio.BurcarPorId(id);
            if(usuario == null){
                return NotFound();
            }

            await _repositorio.Excluir(usuario);

            return usuario;
        }
    }
}