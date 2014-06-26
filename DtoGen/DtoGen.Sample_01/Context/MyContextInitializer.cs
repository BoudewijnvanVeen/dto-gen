using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Models;

namespace DtoGen.Sample_01.Model
{
    public class MyContextInitializer : CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            context.Genres.AddOrUpdate(g => g.Id,
                new Genre() { Id = 1, Name = "Biography" },
                new Genre() { Id = 2, Name = "Fantasy" },
                new Genre() { Id = 3, Name = "Sci-Fi" }
            );

            context.Users.AddOrUpdate(u => u.Id,
                new User()
                {
                    Id = 1,
                    Name = "Tomas",
                    Email = "tomas@mail.com",
                    Street = "13 Green Lane",
                    City = "Little Den",
                    ZIP = "12345",
                    Country = "USA",
                    State = "WA"
                });
        }
    }
}