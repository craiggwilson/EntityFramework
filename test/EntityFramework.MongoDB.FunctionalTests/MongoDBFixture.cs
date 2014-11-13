// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Fallback;

namespace Microsoft.Data.Entity.MongoDB.FunctionalTests
{
    public class MongoDBFixture
    {
        public readonly IServiceProvider ServiceProvider;

        public MongoDBFixture()
        {
            ServiceProvider
                = new ServiceCollection()
                    .AddEntityFramework()
                    .AddMongoDB()
                    .ServiceCollection
                    .BuildServiceProvider();
        }
    }
}
