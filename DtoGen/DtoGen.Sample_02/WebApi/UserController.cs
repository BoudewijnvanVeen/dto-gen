using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using DtoGen.Sample_02.DTO;
using DtoGen.Sample_02.Model;

namespace DtoGen.Sample_02.WebApi
{
    [RoutePrefix("WebApi")]
    public class UserController : ApiController
    {

        // This sample demonstrates generating DTOs using the T4 Template and DtoGen library.
        // The template is located in the DTO directory.


        // NOTE: This is only a sample which needs to be very simple and straightforward. 
        // In real application you should not use DbContext directly in controllers (neither Web API nor MVC)
        // - this kind of stuff should be in a business layer unless you are doing a really small app.
        


        /// <summary>
        /// Gets the user with specified ID.
        /// </summary>
        [Route("GetUser/{id}")]
        public UserData GetUser(int id)
        {
            using (var dc = CreateDbContext())
            {
                // Retrieve the entity from the database (using normal Entity Framework query)
                var user = FetchUserFromDatabase(dc, id);

                // Project the entity to DTO and pass it to the user.
                // The TransformToTarget method creates a new DTO and fills 
                // its properties with values from the entity.
                return user.TransformToTarget();
            }
        }


        /// <summary>
        /// Saves the changes in the user object.
        /// </summary>
        [Route("SaveUser")]
        public void Post(UserData modifiedUser)
        {
            using (var dc = CreateDbContext())
            {
                // Retrieve the entity from the database (using normal Entity Framework query)
                var user = FetchUserFromDatabase(dc, modifiedUser.Id);
                
                // Update the entity with the changes in the DTO.
                // We use the PopulateSource since we already have the source entity,
                // we just need to update its properties.
                // The collections will be updated automatically:
                // all user-genre relationships will be deleted and created again
                // books will be synchronized (new ones added, existing updated and missing deleted)
                modifiedUser.PopulateSource(user);

                // TODO: removing does not work because the entities must be removed also from the datacontext

                // Save the changes.
                dc.SaveChanges();
            }
        }


        /// <summary>
        /// Gets the list of genres.
        /// </summary>
        [Route("GetGenres")]
        public IEnumerable<GenreData> GetGenres()
        {
            using (var dc = CreateDbContext())
            {
                // Retrieve genres from the database (using an Entity Framework query)
                var genres = dc.Genres.ToList();

                // Project the entities to the DTO and pass them to the user.
                // Since it is a collection, we need to use Select.
                // genres.Select(g => GenreExtensions.TransformToTarget(g)) can 
                // be simplified to this:
                return genres.Select(GenreExtensions.TransformToTarget);
            }
        }


        /// <summary>
        /// Fetches the User entity from the database.
        /// </summary>
        private User FetchUserFromDatabase(MyContext dc, int id)
        {
            var user = dc.Users
                .Include(u => u.Books)
                .Include(u => u.FavoriteGenres)
                .Single(u => u.Id == id);
            return user;
        }


        /// <summary>
        /// Creates the database context.
        /// </summary>
        private MyContext CreateDbContext()
        {
            return new MyContext();
        }
    }
}