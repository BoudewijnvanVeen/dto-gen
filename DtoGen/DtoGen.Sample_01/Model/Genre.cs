using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DtoGen.Sample_01.Model
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