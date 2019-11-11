using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Domains;
using backend.Repositories;
using backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    // Definimos nossa rota do controller e dizemos que é um controller de API
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController: ControllerBase
    {
        
        CategoriaRepository _repositorio = new CategoriaRepository();

        //GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Get()
        {
            var categorias = await _repositorio.Listar();
            
            if(categorias == null){
                return NotFound();
            }
            return categorias;
        }
        //GET: api/Categoria/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> Get(int id)
        {
            var categoria = await _repositorio.BuscarPorId(id);
            
            if(categoria == null){
                return NotFound();
            }
            return categoria;
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> Post(Categoria categoria){
            try{
                categoria = await _repositorio.Salvar(categoria);
            }catch(DbUpdateConcurrencyException){
                throw;
            }
            return categoria;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Categoria categoria){
            // Se o ID do objeto não existir, ele retorna o erro 400
            if(id != categoria.CategoriaId){
                return BadRequest();
            }
            
            try{
                
                await _repositorio.Alterar(categoria);

            }catch(DbUpdateConcurrencyException){
                // Verificamos se o objeto inserido realmente existe no banco
                var categoria_valido = await _repositorio.BuscarPorId(id);

                if(categoria_valido == null){
                    return NotFound();
                }else{
                    throw;
                }
            }
            // NoContent = Retorna 204, sem nada
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> Delete(int id){
            var categoria = await _repositorio.BuscarPorId(id);
            if(categoria == null){
                return NotFound();
            }

            categoria = await _repositorio.Excluir(categoria);

            return categoria;
        }
        
        [HttpGet("FiltrarPorNome")]
        public ActionResult<List<Categoria>> GetFiltrar(FiltroViewModel filtro){
            using(GufosContext _contexto = new GufosContext()){

                //Lista Categorias que comecem com o filtrar inserido
                //List<Categoria> categorias = _contexto.Categoria.Where(c => c.Titulo.Contains(filtro.Palavra)).ToList();

                //Lista categorias que contenham o filtro em qualquer lugar do titulo
                List<Categoria> categorias = _contexto.Categoria.Where(c => c.Titulo.Contains(filtro.Palavra)).ToList();

                return categorias;
            }
        }
    }
}