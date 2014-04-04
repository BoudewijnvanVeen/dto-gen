using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DtoGen.Core.Generators
{
    /// <summary>
    /// Defines the requirements for property renderer implementations.
    /// </summary>
    public interface IPropertyMemberRenderer
    {
        /// <summary>
        /// Generates the declaration of the property.
        /// </summary>
        IEnumerable<MemberDeclarationSyntax> GetDeclarationCode();

        /// <summary>
        /// Generates the code that takes the value from the source property and puts it to the target property.
        /// </summary>
        IEnumerable<StatementSyntax> GetTransformCode();

        /// <summary>
        /// Generates the code that takes the value from the target property and puts it to the source property.
        /// </summary>
        IEnumerable<StatementSyntax> GetTransformBackCode();
    }
}