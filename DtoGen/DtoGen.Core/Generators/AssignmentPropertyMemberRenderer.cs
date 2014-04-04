using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DtoGen.Core.Generators
{
    /// <summary>
    /// Renders simple assignment from source property to target property: target.Property = source.Property;
    /// </summary>
    public class AssignmentPropertyMemberRenderer : PropertyMemberRendererBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AssignmentPropertyMemberRenderer"/> class.
        /// </summary>
        public AssignmentPropertyMemberRenderer(PropertyMember propertyMember) : base(propertyMember)
        {
        }


        /// <summary>
        /// Gets the code that assigns the source property to the target property.
        /// </summary>
        public override IEnumerable<StatementSyntax> GetTransformCode()
        {
            var leftSide = SyntaxFactory.ParseName("target." + PropertyMember.TargetName);
            var rightSide = SyntaxFactory.ParseName("source." + PropertyMember.Name);

            yield return SyntaxHelper.GenerateAssignmentStatement(leftSide, rightSide);
        }


        /// <summary>
        /// Gets the code that assigns the target property back to the source property.
        /// </summary>
        public override IEnumerable<StatementSyntax> GetTransformBackCode()
        {
            var leftSide = SyntaxFactory.ParseName("source." + PropertyMember.Name);
            var rightSide = SyntaxFactory.ParseName("target." + PropertyMember.TargetName);

            yield return SyntaxHelper.GenerateAssignmentStatement(leftSide, rightSide);
        }

    }
}