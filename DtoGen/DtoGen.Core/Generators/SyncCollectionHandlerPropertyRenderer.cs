using System;
using System.Collections.Generic;
using System.Linq;
using DtoGen.Core.Collections;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DtoGen.Core.Generators
{
    /// <summary>
    /// Synchronizes the collections using specified key properties.
    /// </summary>
    public class SyncCollectionHandlerPropertyRenderer : CollectionHandlerPropertyMemberRenderer
    {
        /// <summary>
        /// Gets or sets the name of the key property in the source class.
        /// </summary>
        public string KeyPropertyName { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="SyncCollectionHandlerPropertyRenderer"/> class.
        /// </summary>
        public SyncCollectionHandlerPropertyRenderer(PropertyMember propertyMember, string key1PropertyName) : base(propertyMember)
        {
            KeyPropertyName = key1PropertyName;
        }

        /// <summary>
        /// Gets the transform code.
        /// </summary>
        public override IEnumerable<StatementSyntax> GetTransformCode()
        {
            // declare converter
            var converterName = GenerateCollectionHandlerName(PropertyMember.Name);
            var converterType = GetCollectionHandlerType(typeof(SyncCollectionHandler<,>), PropertyMember.Type, PropertyMember.TargetType);
            var elementTypes = converterType.GetGenericArguments();
            yield return SyntaxHelper.GenerateVariableDeclarationAndObjectCreationStatement(converterName, converterType);

            // set key accessors
            yield return SyntaxHelper.GenerateAssignmentStatement(
                SyntaxFactory.ParseName(converterName + ".KeySelector1"),
                SyntaxFactory.ParseExpression("__x => __x." + KeyPropertyName)
            );
            yield return SyntaxHelper.GenerateAssignmentStatement(
                SyntaxFactory.ParseName(converterName + ".KeySelector2"),
                SyntaxFactory.ParseExpression("__x => __x." + KeyPropertyName)
            );
            yield return SyntaxHelper.GenerateAssignmentStatement(
                SyntaxFactory.ParseName(converterName + ".NewItemFactory"),
                SyntaxHelper.GenerateGenericName(typeof(PropertyConverter).FullName + ".Convert", elementTypes)
            );
            yield return SyntaxHelper.GenerateAssignmentStatement(
                SyntaxFactory.ParseName(converterName + ".UpdateItemAction"),
                SyntaxHelper.GenerateGenericName(typeof(PropertyConverter).FullName + ".Populate", elementTypes)
            );

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
            var converterType = GetCollectionHandlerType(typeof(SyncCollectionHandler<,>), PropertyMember.TargetType, PropertyMember.Type);
            var elementTypes = converterType.GetGenericArguments();
            yield return SyntaxHelper.GenerateVariableDeclarationAndObjectCreationStatement(converterName, converterType);

            // set key accessors
            yield return SyntaxHelper.GenerateAssignmentStatement(
                SyntaxFactory.ParseName(converterName + ".KeySelector1"),
                SyntaxFactory.ParseExpression("__x => __x." + KeyPropertyName)
            );
            yield return SyntaxHelper.GenerateAssignmentStatement(
                SyntaxFactory.ParseName(converterName + ".KeySelector2"),
                SyntaxFactory.ParseExpression("__x => __x." + KeyPropertyName)
            );
            yield return SyntaxHelper.GenerateAssignmentStatement(
                SyntaxFactory.ParseName(converterName + ".NewItemFactory"),
                SyntaxHelper.GenerateGenericName(typeof(PropertyConverter).FullName + ".Convert", elementTypes)
            );
            yield return SyntaxHelper.GenerateAssignmentStatement(
                SyntaxFactory.ParseName(converterName + ".UpdateItemAction"),
                SyntaxHelper.GenerateGenericName(typeof(PropertyConverter).FullName + ".Populate", elementTypes)
            );

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
