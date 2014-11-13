﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Microsoft.Data.Entity.MongoDB.Metadata
{
    public interface IMongoDBModelExtensions
    {
        [CanBeNull]
        string Database { get; }
    }
}
