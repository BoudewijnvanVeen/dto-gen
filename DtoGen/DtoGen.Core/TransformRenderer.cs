using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DtoGen.Core.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DtoGen.Core
{
    public class TransformRenderer
    {

        /// <summary>
        /// Renders the C# code for a specified transform.
        /// </summary>
        public string Render(Transform transform)
        {
            // add transform methods to generate
            transform.Tasks.Add(new GenerateTargetClassTask(transform));
            transform.Tasks.Add(new GenerateTransformExtensionMethodTask(transform));
            transform.Tasks.Add(new GenerateTransformBackExtensionMethodTask(transform));

            // run generation
            var declarations = new List<MemberDeclarationSyntax>();
            foreach (var task in transform.Tasks)
            {
                declarations.AddRange(task.Render());
            }
            
            // generate source code
            return SyntaxFactory.CompilationUnit()
                .WithMembers(SyntaxFactory.List(declarations))
                .NormalizeWhitespace()
                .ToFullString();
        }

    }
}
