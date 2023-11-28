using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AV2_Biblioteca_Final.Model.Models;

namespace AV2_Biblioteca_Final.Model.Interface
{
    public interface IRepositoryBase<T> where T : class
    {
        List<T> SelecionarTodos();

        Task<T> IncluirAsync(T obj);
        Task<T> AlterarAsync(T obj);
        Task<T> SelecionarPkAsync(params object[] variavel);
        Task<List<T>> SelecionarTodosAsync();
        Task ExcluirAsync(T obj);
        Task ExcluirAsync(params object[] variavel);
    }
}
