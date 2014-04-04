DtoGen - generate DTOs automatically
====================================

DtoGen is a simple generator for Data Transfer Objects (DTOs). All you have to do is specify template class and this library will generate DTO for that class.
You can exclude some properties from the class that are not needed. You can also make simple casts (e.g. DateTime in original class to String in DTO).

## Why DTOs
Have you ever written any ASP.NET Web API or other web service code? Sometimes this occurs in the business layers also and it is boring and hard to maintain.

    return context.Addresses.Select(a => new AddressDTO() {
        Name = a.Name,
        Street = a.Street,
        City = a.City,
        ZIP = a.ZIP,
        Country = a.Country
    });
    
There are not all properties in the DTO since you don't want to give the user everything. However when someone adds a new column to the table, you need to update
the DTO also. Why bother when this can be done automatically using T4 template?

## How To Use
In the Package Manager Console in the Visual Studio, run `Install-Package DtoGen`. The DTOs folder will be added to your project and you'll find there two files:
+   DtoGenerator.tt
+   DTOHeaders.cs

There are two things you have to do. First you have to declare *empty partial classes* for each of your DTO you want to generate. The T4 template will generate 
the contents of the classes but you need those empty classes to be able to use the types in the configuration.
So open the *DTOHeaders.cs* and add this for every DTO you need:

    public partial class UserData {
    }

Then open the T4 template and generate DTOs. This says that we have a class *User* and we want to create a new class *UserData* which is our DTO.

    <#=
        Transform.Create<User, UserData>()
            .Generate()
    #>
    
This will take all properties from the class User and copies them to the UserData class. Also it creates four extension methods:
+   **TransformToTarget** on the class User creates a new instance of UserData and copies all property values to it
+   **PopulateTarget** on the class User takes an existing instance UserData and copies all property values to it
+   **TransformToSource** on the class UserData creates a new instance of User and copies all property values to it
+   **PopulateSource** on the class UserData takes an existing instance User and copies all property values to it

## Configuration
You can exclude some properties from the DTO so they won't be copied. It is good because typically there are several properties in the entities 
you don't want to have in those DTOs. Excluding properties is as simple as using Remove function:
    
    <#=
        Transform.Create<User, UserData>()
            .Remove(x => x.SecretProperty)
            .Generate()
    #>
    
Sometimes you need some properties to have different names. You can use Rename function:
    
    <#=
        Transform.Create<User, UserData>()
            .Rename(x => x.SomeName, "DifferentNameInDTO")
            .Generate()
    #>
    
If you need to convert values from one type to another, you can use:
    
    <#=
        Transform.Create<User, UserData>()
            .ConvertTo<string>(x => x.CreatedDate)
            .Generate()
    #>
    

### Collections
Let's assume you have an entity named User with collection of related entities named Books. It is not a big deal to create two transforms - one
from *User* to *UserData* and the other from *Book* to *BookData*. Then you can use "collection handler" which is a mechanism that can transform
entities in collections. If the entity has ICollection&lt;Book&gt;, in the DTO we need ICollection&lt;BookData&gt;.
You can use two functions - *SyncCollection* or *ReplaceItemsInCollection*. If you are using *TransformToTarget* or *TransformToSource*, you can 
use either of them - both of them will do the same job - they'll create a new collection and perform the operation. However when you are populating
existing instance of entity or DTO, the behavior is different.
+   **SyncCollection** required to enter one property that uniquely identifies the object - this ID property must exist in both types and items with same ID are treated as equal. If the populating collection already contains and instance with specific ID, its properties are updated. If the instance with that ID does not exist in the source collection, it is removed. If it is an instance with unknown ID, it is added.
+   **ReplaceItemsInCollection** on the other hand always clears the target collection, converts all its items and adds them in the target collection.

*NOTE: Currently those collection handlers registered in the Transform syntax do not cooperate well with Entity Framework because in case of deletion they do not remove the entity from its DbSet. However you can exclude the collection properties from the generation process and use collection handlers on your own - they have properties that allow to specify remove item, update item and insert item scenarios.*


## Roadmap &amp; Tasks
+   **IQueryable support** - the transforms should also create extensions methods on IQueryable&lt;User&gt; that will apply Select and project the entities to an IQueryable&lt;UserDTO&gt;. Currently all entities must be materialized and then the transform functions can be applied.
+   **Advanced property mapping** - it may be useful to be able to map user.Company.Name to dto.CompanyName.
+   **Collection Handler injection** - currently the collection handler instances are created in the generated code. We'll definitely need an option to create and set up the collection handler manually. 
+   Also **some events** could be added to allow handling some cases like deletion of the entity.
+   **Redesigning the fluent syntax** - it is not tested on real uses and the API could be much better.
+   **Error handling** - currently there are no checks in the code
+   **Tests**

If you have another ideas or you want to help with the development just write me a message.

**Tomáš Herceg**, Microsoft ASP.NET MVP, Chief Software Architect @ RIGANTI s.r.o. http://www.riganti.cz