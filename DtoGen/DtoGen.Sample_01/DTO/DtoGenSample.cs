

namespace DtoGen.Sample_01.DTO
{
    using System;
    using System.Collections.Generic;
    using DtoGen.Core;
    using DtoGen.Core.Collections;
    using DtoGen.Sample_01.Model;

    public partial class UserData
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
    }
}

namespace DtoGen.Sample_01.Model
{
    using System;
    using System.Collections.Generic;
    using DtoGen.Core;
    using DtoGen.Core.Collections;
    using DtoGen.Sample_01.DTO;

    [DtoGen.Core.DtoGeneratedAttribute()]
    public static class UserExtensions
    {
        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (User), typeof (UserData), true)]
        public static DtoGen.Sample_01.DTO.UserData TransformToTarget(this DtoGen.Sample_01.Model.User source)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            DtoGen.Sample_01.DTO.UserData target = new DtoGen.Sample_01.DTO.UserData();
            target.Id = source.Id;
            target.Name = source.Name;
            target.Email = source.Email;
            target.Street = source.Street;
            target.City = source.City;
            target.ZIP = source.ZIP;
            target.State = source.State;
            target.Country = source.Country;
            target.TaxNumber = source.TaxNumber;
            return target;
        }

        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (User), typeof (UserData), false)]
        public static void PopulateTarget(this DtoGen.Sample_01.Model.User source, DtoGen.Sample_01.DTO.UserData target)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            target.Id = source.Id;
            target.Name = source.Name;
            target.Email = source.Email;
            target.Street = source.Street;
            target.City = source.City;
            target.ZIP = source.ZIP;
            target.State = source.State;
            target.Country = source.Country;
            target.TaxNumber = source.TaxNumber;
        }
    }
}

namespace DtoGen.Sample_01.DTO
{
    using System;
    using System.Collections.Generic;
    using DtoGen.Core;
    using DtoGen.Core.Collections;
    using DtoGen.Sample_01.Model;

    [DtoGen.Core.DtoGeneratedAttribute()]
    public static class UserDataExtensions
    {
        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (UserData), typeof (User), true)]
        public static DtoGen.Sample_01.Model.User TransformToSource(this DtoGen.Sample_01.DTO.UserData target)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            DtoGen.Sample_01.Model.User source = new DtoGen.Sample_01.Model.User();
            source.Id = target.Id;
            source.Name = target.Name;
            source.Email = target.Email;
            source.Street = target.Street;
            source.City = target.City;
            source.ZIP = target.ZIP;
            source.State = target.State;
            source.Country = target.Country;
            source.TaxNumber = target.TaxNumber;
            return source;
        }

        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (UserData), typeof (User), false)]
        public static void PopulateSource(this DtoGen.Sample_01.DTO.UserData target, DtoGen.Sample_01.Model.User source)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            source.Id = target.Id;
            source.Name = target.Name;
            source.Email = target.Email;
            source.Street = target.Street;
            source.City = target.City;
            source.ZIP = target.ZIP;
            source.State = target.State;
            source.Country = target.Country;
            source.TaxNumber = target.TaxNumber;
        }
    }
}

