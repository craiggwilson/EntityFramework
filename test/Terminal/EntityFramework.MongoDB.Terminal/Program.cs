using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Fallback;
using Microsoft.Data.Entity.MongoDB;

namespace EntityFramework.MongoDB.Terminal
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            _serviceProvider = new ServiceCollection()
                    .AddEntityFramework()
                    .AddMongoDB()
                    .ServiceCollection
                    .BuildServiceProvider();

            using (var context = CreateContext())
            {
                var simplePoco = context.Set<SimplePoco>().Add(
                    new SimplePoco
                    {
                        PocoKey = 100,
                        Name = "A. Name"
                    });
                var changes = context.SaveChanges();

                simplePoco.Name = "Updated Name";
                changes = context.SaveChanges();

                context.Set<SimplePoco>().Remove(simplePoco);
                changes = context.SaveChanges();
            }
        }

        private static DbContext CreateContext()
        {
            var options = new DbContextOptions()
                .UseModel(CreateModel())
                .UseMongoDB("mongodb://localhost:27017");

            return new DbContext(_serviceProvider, options);
        }

        private static IModel CreateModel()
        {
            var model = new Model();
            var builder = new BasicModelBuilder(model);
            builder.Entity<SimplePoco>(b =>
            {
                b.ForMongoDB().Collection("simple_poco");
                b.Key(cust => cust.PocoKey);
                b.Property(cust => cust.Name).ForMongoDB().Field("name");
            })
            .ForMongoDB().Database("ef_test");

            return model;
        }

        public class SimplePoco
        {
            public int PocoKey { get; set; }
            public string Name { get; set; }
        }
    }
}
