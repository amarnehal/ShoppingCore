using ShoppingCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCore.DataAccess.Repositories.Interfaces
{
    public interface ICatageoryRepository:BaseRepository<Category>
    {
        void Update(Category obj);
       
    }
}
