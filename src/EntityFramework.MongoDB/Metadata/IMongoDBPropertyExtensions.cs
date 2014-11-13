// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using MongoDB.Bson.Serialization;

namespace Microsoft.Data.Entity.MongoDB.Metadata
{
    public interface IMongoDBPropertyExtensions
    {
        [CanBeNull]
        string Field { get; }
    }
}
