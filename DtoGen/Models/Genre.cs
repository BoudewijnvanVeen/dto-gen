using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Models
{
    public class Genre
    {

        public int Id { get; set; }

        public string Name { get; set; }
        
        public virtual ICollection<User> Users { get; set; }


        public Genre()
        {
            Users = new Collection<User>();
        }

    }
}