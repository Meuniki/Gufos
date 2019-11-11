using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;

namespace backend.Interfaces
{
    public interface ITipoUsuario
    {
        Task<List<TipoUsuario>> Listar();
        Task<TipoUsuario> BurcarPorId(int id);
        Task<TipoUsuario> Salvar(TipoUsuario usuario);
        Task<TipoUsuario> Alterar(TipoUsuario usuario);
        Task<TipoUsuario> Excluir(TipoUsuario usuario);
    }
}