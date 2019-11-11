using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class PresencasRepository : IPresenca
    {
        public async Task<Presencas> Alterar(Presencas presenca)
        {
            using(GufosContext _contexto = new GufosContext()){

                 _contexto.Entry(presenca).State = EntityState.Modified;
                 await _contexto.SaveChangesAsync();
                 return presenca;

            }
        }

        public async Task<Presencas> BuscarPorId(int id)
        {
            using(GufosContext _contexto = new GufosContext()){
                var presenca = await _contexto.Presencas.FindAsync(id);
                return presenca;

            }
        }

        public async Task<Presencas> Excluir(Presencas presenca)
        {
            using(GufosContext _contexto = new GufosContext()){

            _contexto.Presencas.Remove(presenca);
            await _contexto.SaveChangesAsync();
            
            return presenca;
            
            }
        }

        public async Task<List<Presencas>> Listar()
        {
            using(GufosContext _contexto = new GufosContext()){
                var presencas = await _contexto.Presencas.Include("Evento").ToListAsync();
                return presencas;
            }
        }

        public async Task<Presencas> Salvar(Presencas presenca)
        {
            using(GufosContext _contexto = new GufosContext()){

                await _contexto.AddAsync(presenca);
                await _contexto.SaveChangesAsync();

                return presenca;
            }
        }
    }
}