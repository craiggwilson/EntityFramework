using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Core.Clusters;
using MongoDB.Driver.Core.Configuration;

namespace Microsoft.Data.Entity.MongoDB
{
    public static class ClusterCache
    {
        public static ICluster GetOrCreate(ConnectionString connectionString)
        {
            // TODO: actually cache this...
            return new ClusterBuilder()
                .ConfigureWithConnectionString(connectionString)
                .BuildCluster();
        }
    }
}
