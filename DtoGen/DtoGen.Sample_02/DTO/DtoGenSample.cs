

namespace DtoGen.Sample_02.DTO
{
    using System;
    using System.Collections.Generic;
    using DtoGen.Core;
    using DtoGen.Core.Collections;
    using DtoGen.Sample_02.Model;

    public partial class GenreData
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

namespace DtoGen.Sample_02.Model
{
    using System;
    using System.Collections.Generic;
    using DtoGen.Core;
    using DtoGen.Core.Collections;
    using DtoGen.Sample_02.DTO;

    [DtoGen.Core.DtoGeneratedAttribute()]
    public static class GenreExtensions
    {
        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (Genre), typeof (GenreData), true)]
        public static DtoGen.Sample_02.DTO.GenreData TransformToTarget(this DtoGen.Sample_02.Model.Genre source)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            DtoGen.Sample_02.DTO.GenreData target = new DtoGen.Sample_02.DTO.GenreData();
            target.Id = source.Id;
            target.Name = source.Name;
            return target;
        }

        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (Genre), typeof (GenreData), false)]
        public static void PopulateTarget(this DtoGen.Sample_02.Model.Genre source, DtoGen.Sample_02.DTO.GenreData target)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            target.Id = source.Id;
            target.Name = source.Name;
        }
    }
}

namespace DtoGen.Sample_02.DTO
{
    using System;
    using System.Collections.Generic;
    using DtoGen.Core;
    using DtoGen.Core.Collections;
    using DtoGen.Sample_02.Model;

    [DtoGen.Core.DtoGeneratedAttribute()]
    public static class GenreDataExtensions
    {
        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (GenreData), typeof (Genre), true)]
        public static DtoGen.Sample_02.Model.Genre TransformToSource(this DtoGen.Sample_02.DTO.GenreData target)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            DtoGen.Sample_02.Model.Genre source = new DtoGen.Sample_02.Model.Genre();
            source.Id = target.Id;
            source.Name = target.Name;
            return source;
        }

        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (GenreData), typeof (Genre), false)]
        public static void PopulateSource(this DtoGen.Sample_02.DTO.GenreData target, DtoGen.Sample_02.Model.Genre source)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            source.Id = target.Id;
            source.Name = target.Name;
        }
    }
}

namespace DtoGen.Sample_02.DTO
{
    using System;
    using System.Collections.Generic;
    using DtoGen.Core;
    using DtoGen.Core.Collections;
    using DtoGen.Sample_02.Model;

    public partial class BookData
    {
        public Int32 Id
        {
            get;
            set;
        }

        public String ISBN
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public String Description
        {
            get;
            set;
        }

        public DateTime ReleasedDate
        {
            get;
            set;
        }

        public Int32 Revision
        {
            get;
            set;
        }
    }
}

namespace DtoGen.Sample_02.Model
{
    using System;
    using System.Collections.Generic;
    using DtoGen.Core;
    using DtoGen.Core.Collections;
    using DtoGen.Sample_02.DTO;

    [DtoGen.Core.DtoGeneratedAttribute()]
    public static class BookExtensions
    {
        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (Book), typeof (BookData), true)]
        public static DtoGen.Sample_02.DTO.BookData TransformToTarget(this DtoGen.Sample_02.Model.Book source)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            DtoGen.Sample_02.DTO.BookData target = new DtoGen.Sample_02.DTO.BookData();
            target.Id = source.Id;
            target.ISBN = source.ISBN;
            target.Name = source.Name;
            target.Description = source.Description;
            target.ReleasedDate = source.ReleasedDate;
            target.Revision = source.Revision;
            return target;
        }

        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (Book), typeof (BookData), false)]
        public static void PopulateTarget(this DtoGen.Sample_02.Model.Book source, DtoGen.Sample_02.DTO.BookData target)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            target.Id = source.Id;
            target.ISBN = source.ISBN;
            target.Name = source.Name;
            target.Description = source.Description;
            target.ReleasedDate = source.ReleasedDate;
            target.Revision = source.Revision;
        }
    }
}

namespace DtoGen.Sample_02.DTO
{
    using System;
    using System.Collections.Generic;
    using DtoGen.Core;
    using DtoGen.Core.Collections;
    using DtoGen.Sample_02.Model;

    [DtoGen.Core.DtoGeneratedAttribute()]
    public static class BookDataExtensions
    {
        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (BookData), typeof (Book), true)]
        public static DtoGen.Sample_02.Model.Book TransformToSource(this DtoGen.Sample_02.DTO.BookData target)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            DtoGen.Sample_02.Model.Book source = new DtoGen.Sample_02.Model.Book();
            source.Id = target.Id;
            source.ISBN = target.ISBN;
            source.Name = target.Name;
            source.Description = target.Description;
            source.ReleasedDate = target.ReleasedDate;
            source.Revision = target.Revision;
            return source;
        }

        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (BookData), typeof (Book), false)]
        public static void PopulateSource(this DtoGen.Sample_02.DTO.BookData target, DtoGen.Sample_02.Model.Book source)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            source.Id = target.Id;
            source.ISBN = target.ISBN;
            source.Name = target.Name;
            source.Description = target.Description;
            source.ReleasedDate = target.ReleasedDate;
            source.Revision = target.Revision;
        }
    }
}

namespace DtoGen.Sample_02.DTO
{
    using System;
    using System.Collections.Generic;
    using DtoGen.Core;
    using DtoGen.Core.Collections;
    using DtoGen.Sample_02.Model;

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

        public ICollection<GenreData> FavoriteGenres
        {
            get;
            set;
        }

        public ICollection<BookData> Books
        {
            get;
            set;
        }
    }
}

namespace DtoGen.Sample_02.Model
{
    using System;
    using System.Collections.Generic;
    using DtoGen.Core;
    using DtoGen.Core.Collections;
    using DtoGen.Sample_02.DTO;

    [DtoGen.Core.DtoGeneratedAttribute()]
    public static class UserExtensions
    {
        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (User), typeof (UserData), true)]
        public static DtoGen.Sample_02.DTO.UserData TransformToTarget(this DtoGen.Sample_02.Model.User source)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            DtoGen.Sample_02.DTO.UserData target = new DtoGen.Sample_02.DTO.UserData();
            target.Id = source.Id;
            target.Name = source.Name;
            target.Email = source.Email;
            target.Street = source.Street;
            target.City = source.City;
            target.ZIP = source.ZIP;
            target.State = source.State;
            target.Country = source.Country;
            target.TaxNumber = source.TaxNumber;
            ReplaceEntriesCollectionHandler<Genre, GenreData> ___favoriteGenresCollectionHandler = new ReplaceEntriesCollectionHandler<Genre, GenreData>();
            if (source.FavoriteGenres == null)
                source.FavoriteGenres = new List<Genre>();
            if (target.FavoriteGenres == null)
                target.FavoriteGenres = new List<GenreData>();
            ___favoriteGenresCollectionHandler.SynchronizeCollections(source.FavoriteGenres, target.FavoriteGenres);
            SyncCollectionHandler<Book, BookData> ___booksCollectionHandler = new SyncCollectionHandler<Book, BookData>();
            ___booksCollectionHandler.KeySelector1 = __x => __x.Id;
            ___booksCollectionHandler.KeySelector2 = __x => __x.Id;
            ___booksCollectionHandler.NewItemFactory = DtoGen.Core.PropertyConverter.Convert<Book, BookData>;
            ___booksCollectionHandler.UpdateItemAction = DtoGen.Core.PropertyConverter.Populate<Book, BookData>;
            if (source.Books == null)
                source.Books = new List<Book>();
            if (target.Books == null)
                target.Books = new List<BookData>();
            ___booksCollectionHandler.SynchronizeCollections(source.Books, target.Books);
            return target;
        }

        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (User), typeof (UserData), false)]
        public static void PopulateTarget(this DtoGen.Sample_02.Model.User source, DtoGen.Sample_02.DTO.UserData target)
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
            ReplaceEntriesCollectionHandler<Genre, GenreData> ___favoriteGenresCollectionHandler = new ReplaceEntriesCollectionHandler<Genre, GenreData>();
            if (source.FavoriteGenres == null)
                source.FavoriteGenres = new List<Genre>();
            if (target.FavoriteGenres == null)
                target.FavoriteGenres = new List<GenreData>();
            ___favoriteGenresCollectionHandler.SynchronizeCollections(source.FavoriteGenres, target.FavoriteGenres);
            SyncCollectionHandler<Book, BookData> ___booksCollectionHandler = new SyncCollectionHandler<Book, BookData>();
            ___booksCollectionHandler.KeySelector1 = __x => __x.Id;
            ___booksCollectionHandler.KeySelector2 = __x => __x.Id;
            ___booksCollectionHandler.NewItemFactory = DtoGen.Core.PropertyConverter.Convert<Book, BookData>;
            ___booksCollectionHandler.UpdateItemAction = DtoGen.Core.PropertyConverter.Populate<Book, BookData>;
            if (source.Books == null)
                source.Books = new List<Book>();
            if (target.Books == null)
                target.Books = new List<BookData>();
            ___booksCollectionHandler.SynchronizeCollections(source.Books, target.Books);
        }
    }
}

namespace DtoGen.Sample_02.DTO
{
    using System;
    using System.Collections.Generic;
    using DtoGen.Core;
    using DtoGen.Core.Collections;
    using DtoGen.Sample_02.Model;

    [DtoGen.Core.DtoGeneratedAttribute()]
    public static class UserDataExtensions
    {
        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (UserData), typeof (User), true)]
        public static DtoGen.Sample_02.Model.User TransformToSource(this DtoGen.Sample_02.DTO.UserData target)
        {
            DtoGen.Core.PropertyConverter.EnsureInitialized();
            DtoGen.Sample_02.Model.User source = new DtoGen.Sample_02.Model.User();
            source.Id = target.Id;
            source.Name = target.Name;
            source.Email = target.Email;
            source.Street = target.Street;
            source.City = target.City;
            source.ZIP = target.ZIP;
            source.State = target.State;
            source.Country = target.Country;
            source.TaxNumber = target.TaxNumber;
            ReplaceEntriesCollectionHandler<GenreData, Genre> ___favoriteGenresCollectionHandler = new ReplaceEntriesCollectionHandler<GenreData, Genre>();
            if (source.FavoriteGenres == null)
                source.FavoriteGenres = new List<Genre>();
            if (target.FavoriteGenres == null)
                target.FavoriteGenres = new List<GenreData>();
            ___favoriteGenresCollectionHandler.SynchronizeCollections(target.FavoriteGenres, source.FavoriteGenres);
            SyncCollectionHandler<BookData, Book> ___booksCollectionHandler = new SyncCollectionHandler<BookData, Book>();
            ___booksCollectionHandler.KeySelector1 = __x => __x.Id;
            ___booksCollectionHandler.KeySelector2 = __x => __x.Id;
            ___booksCollectionHandler.NewItemFactory = DtoGen.Core.PropertyConverter.Convert<BookData, Book>;
            ___booksCollectionHandler.UpdateItemAction = DtoGen.Core.PropertyConverter.Populate<BookData, Book>;
            if (source.Books == null)
                source.Books = new List<Book>();
            if (target.Books == null)
                target.Books = new List<BookData>();
            ___booksCollectionHandler.SynchronizeCollections(target.Books, source.Books);
            return source;
        }

        [DtoGen.Core.DtoConvertFunctionAttribute(typeof (UserData), typeof (User), false)]
        public static void PopulateSource(this DtoGen.Sample_02.DTO.UserData target, DtoGen.Sample_02.Model.User source)
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
            ReplaceEntriesCollectionHandler<GenreData, Genre> ___favoriteGenresCollectionHandler = new ReplaceEntriesCollectionHandler<GenreData, Genre>();
            if (source.FavoriteGenres == null)
                source.FavoriteGenres = new List<Genre>();
            if (target.FavoriteGenres == null)
                target.FavoriteGenres = new List<GenreData>();
            ___favoriteGenresCollectionHandler.SynchronizeCollections(target.FavoriteGenres, source.FavoriteGenres);
            SyncCollectionHandler<BookData, Book> ___booksCollectionHandler = new SyncCollectionHandler<BookData, Book>();
            ___booksCollectionHandler.KeySelector1 = __x => __x.Id;
            ___booksCollectionHandler.KeySelector2 = __x => __x.Id;
            ___booksCollectionHandler.NewItemFactory = DtoGen.Core.PropertyConverter.Convert<BookData, Book>;
            ___booksCollectionHandler.UpdateItemAction = DtoGen.Core.PropertyConverter.Populate<BookData, Book>;
            if (source.Books == null)
                source.Books = new List<Book>();
            if (target.Books == null)
                target.Books = new List<BookData>();
            ___booksCollectionHandler.SynchronizeCollections(target.Books, source.Books);
        }
    }
}