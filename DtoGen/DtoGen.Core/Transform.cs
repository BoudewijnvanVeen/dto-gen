using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DtoGen.Core.Generators;

namespace DtoGen.Core
{
    public abstract class Transform
    {
        public static readonly System.Reflection.MethodInfo convertMethodInfo = typeof(PropertyConverter).GetMethod("Convert");

        /// <summary>
        /// Gets a list of generation tasks that needs to be done.
        /// </summary>
        public List<TransformTask> Tasks { get; private set; }

        /// <summary>
        /// Gets the list of members that represents the mapping.
        /// </summary>
        public List<PropertyMember> Members { get; protected set; }

        /// <summary>
        /// Hidden list of members that represents the mapping us
        /// </summary>
        protected List<PropertyMember> HiddenMembers { get; set; }

        /// <summary>
        /// Gets or sets the type of the source class.
        /// </summary>
        public Type SourceType { get; set; }

        /// <summary>
        /// Gets or sets the typeNAME of the target class.
        /// </summary>
        public String TargetTypeName { get; set; }

        /// <summary>
        /// Gets or sets the Namespace of the target class.
        /// </summary>
        public String TargetNameSpace { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transform"/> class.
        /// </summary>
        protected Transform()
        {
            Tasks = new List<TransformTask>();
            HiddenMembers = new List<PropertyMember>();
        }

        /// <summary>
        /// Creates the transform between the two specified types.
        /// </summary>
        public static Transform<TSource> Create<TSource>()
        {
            var transform = new Transform<TSource>();
            transform.Members = transform.LoadMembers<TSource>();
            transform.HiddenMembers = transform.LoadMembers<TSource>();
            return transform;
        }

        /// <summary>
        /// Loads the members.
        /// </summary>
        private List<PropertyMember> LoadMembers<T>()
        {
            var members = new List<PropertyMember>();
            foreach (var prop in typeof(T).GetProperties())
            {
                if (!prop.CanRead || !prop.CanWrite)
                {
                    // we can only transform properties with both get and set
                    continue;
                }

                if (prop.CanWrite)
                {
                    // standard property
                    members.Add(new PropertyMember()
                    {
                        Type = prop.PropertyType,
                        Name = prop.Name,
                        TargetType = prop.PropertyType,
                        TargetName = prop.Name
                    });
                }
            }
            return members;
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        protected PropertyMember GetProperty(string propertyName, IEnumerable<PropertyMember> list)
        {
            var prop = list.FirstOrDefault(p => p.Name == propertyName);
            if (prop == null)
            {
                throw new Exception(string.Format("Cannot exclude property '{0}' because it does not exist on the target type!", propertyName));
            }
            return prop;
        }


        /// <summary>
        /// Returns the generated code.
        /// </summary>
        public string Generate(string targetTypeName, string targetNameSpace)
        {
            TargetTypeName = targetTypeName;
            TargetNameSpace = targetNameSpace;

            var transformRenderer = new TransformRenderer();
            return transformRenderer.Render(this);
        }
    }

    public class Transform<TSource> : Transform
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Transform{TSource}"/> class.
        /// </summary>
        public Transform()
        {
            SourceType = typeof(TSource);
        }

        /// <summary>
        /// Use this to start with an empty member list
        /// </summary>
        public Transform<TSource> None()
        {
            Members.Clear();
            return this;
        }

        /// <summary>
        /// Removes the specified property on the first object from the mapping.
        /// </summary>
        public Transform<TSource> Add(Expression<Func<TSource, object>> propertySelector)
        {
            var propertyName = Helpers.ExtractNameFromGetPropertyExpression(propertySelector);
            var member = GetProperty(propertyName, HiddenMembers);
            Members.Add(member);
            return this;
        }

        /// <summary>
        /// Removes the specified property on the first object from the mapping.
        /// </summary>
        public Transform<TSource> Remove(Expression<Func<TSource, object>> propertySelector)
        {
            var propertyName = Helpers.ExtractNameFromGetPropertyExpression(propertySelector);
            var member = GetProperty(propertyName, HiddenMembers);
            Members.Remove(member);
            return this;
        }

        /// <summary>
        /// Renames the specified property on the first object to another name in the second object.
        /// </summary>
        public Transform<TSource> Rename(Expression<Func<TSource, object>> propertySelector, string newPropertyName)
        {
            var propertyName = Helpers.ExtractNameFromGetPropertyExpression(propertySelector);
            var member = GetProperty(propertyName, HiddenMembers);
            member.TargetName = newPropertyName;
            return this;
        }

        /// <summary>
        /// Convert the specified property on the first object to another type in the second object.
        /// </summary>
        public Transform<TSource> ConvertTo<TTargetType>(Expression<Func<TSource, object>> propertySelector)
        {
            var propertyName = Helpers.ExtractNameFromGetPropertyExpression(propertySelector);
            var member = GetProperty(propertyName, HiddenMembers);
            member.TargetType = typeof(TTargetType);
            member.PropertyMemberRenderer = new AssignmentWithConvertPropertyMemberRenderer(member);
            return this;
        }

        /// <summary>
        /// Deletes all items in the specified collection and replaces the contents with the items from a new collection.
        /// </summary>
        public Transform<TSource> ReplaceItemsInCollection<T1, T2>(Expression<Func<TSource, ICollection<T1>>> propertySelector)
            where T1 : class
            where T2 : class
        {
            var propertyName = Helpers.ExtractNameFromGetPropertyExpression(propertySelector);
            var member = GetProperty(propertyName, HiddenMembers);
            member.TargetType = typeof(ICollection<T2>);
            member.PropertyMemberRenderer = new ReplaceEntriesCollectionHandlerPropertyRenderer(member);
            return this;
        }

        /// <summary>
        /// Synchronizes the collections using defined key properties.
        /// </summary>
        public Transform<TSource> SyncCollection<T1, T2>(
            Expression<Func<TSource, ICollection<T1>>> propertySelector,
            Expression<Func<T1, object>> keySelector)
            where T1 : class
            where T2 : class
        {
            var propertyName = Helpers.ExtractNameFromGetPropertyExpression(propertySelector);
            var member = GetProperty(propertyName, HiddenMembers);
            member.TargetType = typeof(ICollection<T2>);

            var keyPropertyName = Helpers.ExtractNameFromGetPropertyExpression(keySelector);

            member.PropertyMemberRenderer = new SyncCollectionHandlerPropertyRenderer(member, keyPropertyName);
            return this;
        }
    }
}
