﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Models
{
    public class User
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string SureName { get; set; }

        public string Email { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string ZIP { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string TaxNumber { get; set; }


        public virtual ICollection<Genre> FavoriteGenres { get; set; }

        public virtual ICollection<Book> Books { get; set; }


        public User()
        {
            FavoriteGenres = new Collection<Genre>();
            Books = new Collection<Book>();
        }
    }
}