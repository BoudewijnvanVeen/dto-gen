using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DtoGen.Core.Generators
{
    /// <summary>
    /// Renders assignment from source property to target property with conversion: target.Property = PropertyConverter.Convert&lt;TSource, TTarget&gt;(source.Property);
    /// </summary>
    public class AssignmentWithConvertPropertyMemberRenderer : PropertyMemberRendererBase
    {
        public AssignmentWithConvertPropertyMemberRenderer(PropertyMember propertyMember) : base(propertyMember)
        {
        }


        /// <summary>
        /// Gets the code that assigns the source property to the target property.
        /// </summary>
        public override IEnumerable<StatementSyntax> GetTransformCode()
        {
            ExpressionSyntax leftSide = SyntaxFactory.ParseName("target." + PropertyMember.TargetName);
            ExpressionSyntax rightSide = SyntaxFactory.ParseName("source." + PropertyMember.Name);

            rightSide = SyntaxHelper.GenerateStaticMethodCall("Convert", typeof (PropertyConverter).FullName, new[]
            {
                SyntaxFactory.Argument(rightSide)
            }, typeArguments: new[] { PropertyMember.Type, PropertyMember.TargetType }).Expression;

            yield return SyntaxHelper.GenerateAssignmentStatement(leftSide, rightSide);
        }

        /// <summary>
        /// Gets the code that assigns the target property back to the source property.
        /// </summary>
        public override IEnumerable<StatementSyntax> GetTransformBackCode()
        {
            ExpressionSyntax leftSide = SyntaxFactory.ParseName("source." + PropertyMember.Name);
            ExpressionSyntax rightSide = SyntaxFactory.ParseName("target." + PropertyMember.TargetName);

            rightSide = SyntaxHelper.GenerateStaticMethodCall("Convert", typeof(PropertyConverter).FullName, new[]
            {
                SyntaxFactory.Argument(rightSide)
            }, typeArguments: new[] { PropertyMember.TargetType, PropertyMember.Type }).Expression;

            yield return SyntaxHelper.GenerateAssignmentStatement(leftSide, rightSide);
        }
    }
}