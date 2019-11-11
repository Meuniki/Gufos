using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;

namespace backend.Interfaces
{
    public interface IPresenca
    {
        Task<List<Presencas>> Listar();
        Task<Presencas> BuscarPorId(int id);
        Task<Presencas> Salvar(Presencas presenca);
        Task<Presencas> Alterar(Presencas presenca);
        Task<Presencas> Excluir(Presencas presenca);
    }
}