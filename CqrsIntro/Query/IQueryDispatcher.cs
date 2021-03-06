﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsIntro.Query
{
    public interface IQueryDispatcher
    {
        TResult Query<TQuery, TResult>(TQuery query) where TQuery : IQuery;
    }
}
