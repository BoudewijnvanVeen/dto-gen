using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DtoGen.Sample_02.Model
{
    public class Genre
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

    }
}