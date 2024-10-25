﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IBooksRepository Books {  get; }
        ICategoryRepository Category { get; }

        void Save();
    }
}
