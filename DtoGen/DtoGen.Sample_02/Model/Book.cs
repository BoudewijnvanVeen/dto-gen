using System;
using System.Collections.Generic;
using System.Linq;

namespace DtoGen.Sample_02.Model
{
    public class Book
    {

        public int Id { get; set; }

        public int UserId { get; set; }
        
        public string ISBN { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime ReleasedDate { get; set; }

        public int Revision { get; set; }
        
        public virtual User User { get; set; }
    }
}