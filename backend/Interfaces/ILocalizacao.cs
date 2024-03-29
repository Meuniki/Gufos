using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;

namespace backend.Interfaces
{
    public interface ILocalizacao
    {
         Task<List<Localizacao>> Listar();
         Task<Localizacao> BuscarPorId(int id);
         Task<Localizacao> Salvar(Localizacao localizacao);
         Task<Localizacao> Alterar(Localizacao localizacao);
         Task<Localizacao> Excluir(Localizacao localizacaoS);
    }
}