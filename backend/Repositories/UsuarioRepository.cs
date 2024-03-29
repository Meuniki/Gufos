using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories {
    public class UsuarioRepository : IUsuario {
        public async Task<Usuario> Alterar (Usuario usuario) {
            using (GufosContext _contexto = new GufosContext ()) {
                _contexto.Entry(usuario).State = EntityState.Modified;
                 await _contexto.SaveChangesAsync();
                 return usuario;
            }
        }

        public async Task<Usuario> BurcarPorId (int id) {
            using (GufosContext _contexto = new GufosContext ()) {
                var usuario = await _contexto.Usuario.FindAsync (id);
                return usuario;
            }
        }

        public async Task<Usuario> Excluir (Usuario usuario) {
            using (GufosContext _contexto = new GufosContext ()) {

            _contexto.Usuario.Remove(usuario);
            await _contexto.SaveChangesAsync();
            
            return usuario;
            }
        }

        public async Task<List<Usuario>> Listar () {
            using (GufosContext _contexto = new GufosContext ()) {
                var usuarios = await _contexto.Usuario.ToListAsync ();
                return usuarios;
            }
        }

        public async Task<Usuario> Salvar (Usuario usuario) {
            using (GufosContext _contexto = new GufosContext ()) {
                await _contexto.AddAsync(usuario);
                await _contexto.SaveChangesAsync();

                return usuario;
            }
        }
    }
}