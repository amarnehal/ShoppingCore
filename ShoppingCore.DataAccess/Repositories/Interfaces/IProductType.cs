﻿using ShoppingCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCore.DataAccess.Repositories.Interfaces
{
    public interface IProductType:BaseRepository<ProductType>
    {
        void Update(ProductType productType);
    }
}
