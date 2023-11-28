using AV2_Biblioteca_Final.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AV2_Biblioteca_Final.Model.Repositories
{
    public class RepositoryLivros : RepositoryBase<Livros>
    {
        public RepositoryLivros(bool saveChanges = true) : base(saveChanges)
        {

        }
    }
}