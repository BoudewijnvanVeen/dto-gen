using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DtoGen.Core.Generators
{
    /// <summary>
    /// Generates the TransformToSource and PopulateSource extension methods that transform target object to the source object.
    /// </summary>
    public class GenerateTransformBackExtensionMethodTask : TransformTask
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateTransformBackExtensionMethodTask"/> class.
        /// </summary>
        public GenerateTransformBackExtensionMethodTask(Transform transform) : base(transform)
        {
        }

        /// <summary>
        /// Renders the code.
        /// </summary>
        public override IEnumerable<MemberDeclarationSyntax> Render()
        {
            // TransformToSource method
            var method = SyntaxHelper.GenerateExtensionMethod("TransformToSource", Transform.SourceType.FullName, new []
                {
                    SyntaxHelper.GenerateMethodParameter("target", Transform.TargetType.FullName, true)
                },
                new[] { 
                    SyntaxHelper.GenerateAttribute(
                        typeof(DtoConvertFunctionAttribute),
                        SyntaxFactory.TypeOfExpression(SyntaxHelper.GenerateTypeSyntax(Transform.TargetType)), 
                        SyntaxFactory.TypeOfExpression(SyntaxHelper.GenerateTypeSyntax(Transform.SourceType)),
                        SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression)
                    )
                })
                .WithBody(SyntaxFactory.Block(
                    new StatementSyntax[] {
                        SyntaxHelper.GenerateStaticMethodCall("EnsureInitialized", typeof (PropertyConverter).FullName),
                        SyntaxHelper.GenerateVariableDeclarationAndObjectCreationStatement("source", Transform.SourceType.FullName)
                    }.Concat(
                        Transform.Members.SelectMany(m => m.PropertyMemberRenderer.GetTransformBackCode()).ToArray()
                    ).Concat(new[] {
                        SyntaxFactory.ReturnStatement(SyntaxFactory.ParseName("source"))
                    })
                ));

            // PopulateSource method
            var method2 = SyntaxHelper.GenerateExtensionMethod("PopulateSource", null, new[]
                {
                    SyntaxHelper.GenerateMethodParameter("target", Transform.TargetType.FullName, true),
                    SyntaxHelper.GenerateMethodParameter("source", Transform.SourceType.FullName, false)
                },
                new[] { 
                    SyntaxHelper.GenerateAttribute(
                        typeof(DtoConvertFunctionAttribute),
                        SyntaxFactory.TypeOfExpression(SyntaxHelper.GenerateTypeSyntax(Transform.TargetType)), 
                        SyntaxFactory.TypeOfExpression(SyntaxHelper.GenerateTypeSyntax(Transform.SourceType)),
                        SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression)
                    )
                })
                .WithBody(SyntaxFactory.Block(
                    new StatementSyntax[]
                    {
                        SyntaxHelper.GenerateStaticMethodCall("EnsureInitialized", typeof (PropertyConverter).FullName),
                    }.Concat(
                        Transform.Members.SelectMany(m => m.PropertyMemberRenderer.GetTransformBackCode()).ToArray()
                    )
                ));

            // generate the static class
            var className = Transform.TargetType.Name + "Extensions";
            yield return SyntaxHelper.GenerateNamespace(Transform.TargetType.Namespace, new MemberDeclarationSyntax[]
            {
                SyntaxHelper.GenerateClass(
                    className, 
                    new[] { SyntaxKind.PublicKeyword, SyntaxKind.StaticKeyword }, 
                    new MemberDeclarationSyntax[] { method, method2 },
                    new[] { SyntaxHelper.GenerateAttribute(typeof(DtoGeneratedAttribute)) }
                ) 
            }, 
            GetUsingsForNamespace());
        }
    }
}