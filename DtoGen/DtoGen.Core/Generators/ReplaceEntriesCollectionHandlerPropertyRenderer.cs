using System;
using System.Collections.Generic;
using System.Linq;
using DtoGen.Core.Collections;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DtoGen.Core.Generators
{
    public class ReplaceEntriesCollectionHandlerPropertyRenderer : CollectionHandlerPropertyMemberRenderer
    {
        public ReplaceEntriesCollectionHandlerPropertyRenderer(PropertyMember propertyMember) : base(propertyMember)
        {
        }


        /// <summary>
        /// Gets the transform code.
        /// </summary>
        public override IEnumerable<StatementSyntax> GetTransformCode()
        {
            // declare converter
            var converterName = GenerateCollectionHandlerName(PropertyMember.Name);
            var converterType = GetCollectionHandlerType(typeof(ReplaceEntriesCollectionHandler<,>), PropertyMember.Type, PropertyMember.TargetType);
            yield return SyntaxHelper.GenerateVariableDeclarationAndObjectCreationStatement(converterName, converterType);

            yield return GenerateCollectionNullCheck(PropertyMember.Type, "source", PropertyMember.Name);
            yield return GenerateCollectionNullCheck(PropertyMember.TargetType, "target", PropertyMember.TargetName);

            // call the converter
            yield return SyntaxHelper.GenerateStaticMethodCall("SynchronizeCollections", converterName, new[]
            {
                SyntaxFactory.Argument(SyntaxFactory.ParseName("source." + PropertyMember.Name)),
                SyntaxFactory.Argument(SyntaxFactory.ParseName("target." + PropertyMember.TargetName))
            });
        }

        /// <summary>
        /// Gets the transform back code.
        /// </summary>
        public override IEnumerable<StatementSyntax> GetTransformBackCode()
        {
            // declare converter
            var converterName = GenerateCollectionHandlerName(PropertyMember.Name);
            var converterType = GetCollectionHandlerType(typeof(ReplaceEntriesCollectionHandler<,>), PropertyMember.TargetType, PropertyMember.Type);
            yield return SyntaxHelper.GenerateVariableDeclarationAndObjectCreationStatement(converterName, converterType);

            yield return GenerateCollectionNullCheck(PropertyMember.Type, "source", PropertyMember.Name);
            yield return GenerateCollectionNullCheck(PropertyMember.TargetType, "target", PropertyMember.TargetName);

            // call the converter
            yield return SyntaxHelper.GenerateStaticMethodCall("SynchronizeCollections", converterName, new[]
            {
                SyntaxFactory.Argument(SyntaxFactory.ParseName("target." + PropertyMember.TargetName)),
                SyntaxFactory.Argument(SyntaxFactory.ParseName("source." + PropertyMember.Name))
            });
        }
    }
}