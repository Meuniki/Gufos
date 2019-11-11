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
    public class UsuarioController: ControllerBase
    {
        UsuarioRepository _repositorio = new UsuarioRepository();

        //GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            var usuarios = await _repositorio.Listar();
            
            if(usuarios == null){
                return NotFound();
            }
            return usuarios;
        }
        //GET: api/Usuario/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var usuario = await _repositorio.BurcarPorId(id);
            
            if(usuario == null){
                return NotFound();
            }
            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(Usuario usuario){
            try{

                await _repositorio.Salvar(usuario);
            }catch(DbUpdateConcurrencyException){
                throw;
            }
            return usuario;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Usuario usuario){
            // Se o ID do objeto não existir, ele retorna o erro 400
            if(id != usuario.UsuarioId){
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
        public async Task<ActionResult<Usuario>> Delete(int id){
            
            var usuario = await _repositorio.BurcarPorId(id);
            if(usuario == null){
                return NotFound();
            }

            await _repositorio.Excluir(usuario);

            return usuario;
        }
    }
}