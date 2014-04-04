using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DtoGen.Core.Collections;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DtoGen.Core.Generators
{
    /// <summary>
    /// A base class that is handy for future implementations of a property collection handler.
    /// </summary>
    public abstract class CollectionHandlerPropertyMemberRenderer : PropertyMemberRendererBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionHandlerPropertyMemberRenderer"/> class.
        /// </summary>
        protected CollectionHandlerPropertyMemberRenderer(PropertyMember propertyMember) : base(propertyMember)
        {
        }


        /// <summary>
        /// Generates the name of the collection handler local variable.
        /// </summary>
        protected string GenerateCollectionHandlerName(string propertyName)
        {
            return "___" + GenerateFieldName(propertyName) + "CollectionHandler";
        }

        /// <summary>
        /// Gets the type of the collection handler.
        /// </summary>
        protected Type GetCollectionHandlerType(Type collectionHandlerType, Type sourceType, Type targetType)
        {
            var elementType1 = GetICollectionInterfaceType(sourceType).GetGenericArguments().First();
            var elementType2 = GetICollectionInterfaceType(targetType).GetGenericArguments().First();

            return collectionHandlerType.MakeGenericType(elementType1, elementType2);
        }

        /// <summary>
        /// Gets the ICollection&lt;T&gt; interface type from which we can get the element type.
        /// </summary>
        private static Type GetICollectionInterfaceType(Type sourceType)
        {
            var type = new[] { sourceType }.Concat(sourceType.GetInterfaces()).FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof (ICollection<>));
            if (type == null)
            {
                throw new NotSupportedException(string.Format("The collection handler behavior cannot be applied to a property of type {0} which does not implement ICollection<T>.", sourceType));
            }
            return type;
        }


        /// <summary>
        /// Generates the collection null check.
        /// </summary>
        protected StatementSyntax GenerateCollectionNullCheck(Type collectionType, string identifier, string propertyName)
        {
            if (collectionType.IsInterface)
            {
                collectionType = typeof (List<>).MakeGenericType(collectionType.GetGenericArguments().First());
            }

            var value = SyntaxFactory.ObjectCreationExpression(SyntaxHelper.GenerateTypeSyntax(collectionType), SyntaxFactory.ArgumentList(), null);

            return SyntaxFactory.IfStatement(
                SyntaxFactory.BinaryExpression(SyntaxKind.EqualsExpression, SyntaxFactory.ParseName(identifier + "." + propertyName), SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression)),
                SyntaxHelper.GenerateAssignmentStatement(SyntaxFactory.ParseName(identifier + "." + propertyName), value)
            );
        }
    }
}