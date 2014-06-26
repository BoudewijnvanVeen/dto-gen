

namespace DtoGen.Sample_01.DTO
{
    using System;
    using System.Collections.Generic;
    using DtoGen.Core;
    using DtoGen.Core.Collections;
    using Models;

    public partial class UserDTO
    {
        public Int32 Id
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public String SureName
        {
            get;
            set;
        }

        public String Email
        {
            get;
            set;
        }

        public String Street
        {
            get;
            set;
        }

        public String City
        {
            get;
            set;
        }

        public String ZIP
        {
            get;
            set;
        }

        public String State
        {
            get;
            set;
        }

        public String Country
        {
            get;
            set;
        }

        public String TaxNumber
        {
            get;
            set;
        }

        public ICollection<Genre> FavoriteGenres
        {
            get;
            set;
        }

        public ICollection<Book> Books
        {
            get;
            set;
        }
    }
}

namespace Models
{
    using System;
    using System.Collections.Generic;
    using DtoGen.Core;
    using DtoGen.Core.Collections;
    using DtoGen.Sample_01.DTO;

    [DtoGen.Core.DtoGeneratedAttribute()]
    public static partial class UserDTOExtensions
    {
        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (User), typeof (UserDTO), true)]
        public static UserDTO TransformToUserDTO(this Models.User source)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            UserDTO target = new UserDTO();
            target.Id = source.Id;
            target.Name = source.Name;
            target.SureName = source.SureName;
            target.Email = source.Email;
            target.Street = source.Street;
            target.City = source.City;
            target.ZIP = source.ZIP;
            target.State = source.State;
            target.Country = source.Country;
            target.TaxNumber = source.TaxNumber;
            target.FavoriteGenres = source.FavoriteGenres;
            target.Books = source.Books;
            return target;
        }

        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (User), typeof (UserDTO), false)]
        public static void PopulateTarget(this Models.User source, UserDTO target)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            target.Id = source.Id;
            target.Name = source.Name;
            target.SureName = source.SureName;
            target.Email = source.Email;
            target.Street = source.Street;
            target.City = source.City;
            target.ZIP = source.ZIP;
            target.State = source.State;
            target.Country = source.Country;
            target.TaxNumber = source.TaxNumber;
            target.FavoriteGenres = source.FavoriteGenres;
            target.Books = source.Books;
        }
    }
}

namespace DtoGen.Sample_01.DTO
{
    using System;
    using System.Collections.Generic;
    using DtoGen.Core;
    using DtoGen.Core.Collections;
    using Models;

    [DtoGen.Core.DtoGeneratedAttribute()]
    public static class UserDTOExtensions
    {
        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (UserDTO), typeof (User), true)]
        public static Models.User TransformToSource(this UserDTO target)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            Models.User source = new Models.User();
            source.Id = target.Id;
            source.Name = target.Name;
            source.SureName = target.SureName;
            source.Email = target.Email;
            source.Street = target.Street;
            source.City = target.City;
            source.ZIP = target.ZIP;
            source.State = target.State;
            source.Country = target.Country;
            source.TaxNumber = target.TaxNumber;
            source.FavoriteGenres = target.FavoriteGenres;
            source.Books = target.Books;
            return source;
        }

        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (UserDTO), typeof (User), false)]
        public static void PopulateSource(this UserDTO target, Models.User source)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            source.Id = target.Id;
            source.Name = target.Name;
            source.SureName = target.SureName;
            source.Email = target.Email;
            source.Street = target.Street;
            source.City = target.City;
            source.ZIP = target.ZIP;
            source.State = target.State;
            source.Country = target.Country;
            source.TaxNumber = target.TaxNumber;
            source.FavoriteGenres = target.FavoriteGenres;
            source.Books = target.Books;
        }
    }
}

namespace DtoGen.Sample_01.DTO
{
    using System;
    using System.Collections.Generic;
    using DtoGen.Core;
    using DtoGen.Core.Collections;
    using Models;

    public partial class UserDTO2
    {
        public Int32 Id
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }
    }
}

namespace Models
{
    using System;
    using System.Collections.Generic;
    using DtoGen.Core;
    using DtoGen.Core.Collections;
    using DtoGen.Sample_01.DTO;

    [DtoGen.Core.DtoGeneratedAttribute()]
    public static partial class UserDTO2Extensions
    {
        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (User), typeof (UserDTO2), true)]
        public static UserDTO2 TransformToUserDTO2(this Models.User source)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            UserDTO2 target = new UserDTO2();
            target.Id = source.Id;
            target.Name = source.Name;
            return target;
        }

        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (User), typeof (UserDTO2), false)]
        public static void PopulateTarget(this Models.User source, UserDTO2 target)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            target.Id = source.Id;
            target.Name = source.Name;
        }
    }
}

namespace DtoGen.Sample_01.DTO
{
    using System;
    using System.Collections.Generic;
    using DtoGen.Core;
    using DtoGen.Core.Collections;
    using Models;

    [DtoGen.Core.DtoGeneratedAttribute()]
    public static class UserDTO2Extensions
    {
        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (UserDTO2), typeof (User), true)]
        public static Models.User TransformToSource(this UserDTO2 target)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            Models.User source = new Models.User();
            source.Id = target.Id;
            source.Name = target.Name;
            return source;
        }

        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (UserDTO2), typeof (User), false)]
        public static void PopulateSource(this UserDTO2 target, Models.User source)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            source.Id = target.Id;
            source.Name = target.Name;
        }
    }
}

