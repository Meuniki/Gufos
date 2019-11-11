using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories {
    public class TipoUsuarioRepository : ITipoUsuario {
        public async Task<TipoUsuario> Alterar (TipoUsuario usuario) {
            using (GufosContext _contexto = new GufosContext ()) {
                _contexto.Entry(usuario).State = EntityState.Modified;
                 await _contexto.SaveChangesAsync();
                 return usuario;
            }
        }

        public async Task<TipoUsuario> BurcarPorId (int id) {
            using (GufosContext _contexto = new GufosContext ()) {
                var usuario = await _contexto.TipoUsuario.FindAsync (id);
                return usuario;
            }
        }

        public async Task<TipoUsuario> Excluir (TipoUsuario usuario) {
            using (GufosContext _contexto = new GufosContext ()) {

            _contexto.TipoUsuario.Remove(usuario);
            await _contexto.SaveChangesAsync();
            
            return usuario;
            }
        }

        public async Task<List<TipoUsuario>> Listar () {
            using (GufosContext _contexto = new GufosContext ()) {
                var usuarios = await _contexto.TipoUsuario.ToListAsync ();
                return usuarios;
            }
        }

        public async Task<TipoUsuario> Salvar (TipoUsuario usuario) {
            using (GufosContext _contexto = new GufosContext ()) {
                await _contexto.AddAsync(usuario);
                await _contexto.SaveChangesAsync();

                return usuario;
            }
        }
    }
}