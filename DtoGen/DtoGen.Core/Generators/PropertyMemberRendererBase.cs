using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DtoGen.Core.Generators
{
    /// <summary>
    /// A default implementation of a property renderer.
    /// </summary>
    public abstract class PropertyMemberRendererBase : IPropertyMemberRenderer
    {
        /// <summary>
        /// Gets or sets the member.
        /// </summary>
        public PropertyMember PropertyMember { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMemberRendererBase"/> class.
        /// </summary>
        public PropertyMemberRendererBase(PropertyMember propertyMember)
        {
            PropertyMember = propertyMember;
        }


        /// <summary>
        /// Generates the declaration of the property.
        /// </summary>
        public virtual IEnumerable<MemberDeclarationSyntax> GetDeclarationCode()
        {
            yield return 
                SyntaxFactory.PropertyDeclaration(SyntaxHelper.GenerateTypeSyntax(PropertyMember.TargetType), PropertyMember.TargetName)
                .WithModifiers(
                    SyntaxFactory.Token(SyntaxKind.PublicKeyword)
                )
                .WithAccessorList(
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                );
        }

        /// <summary>
        /// Generates the name of the field from the property name.
        /// </summary>
        public static string GenerateFieldName(string propertyName)
        {
            return propertyName.Substring(0, 1).ToLower() + propertyName.Substring(1);
        }

        /// <summary>
        /// Generates the code that takes the value from the source property and puts it to the target property.
        /// </summary>
        public abstract IEnumerable<StatementSyntax> GetTransformCode();

        /// <summary>
        /// Generates the code that takes the value from the target property and puts it to the source property.
        /// </summary>
        public abstract IEnumerable<StatementSyntax> GetTransformBackCode();
    }
}