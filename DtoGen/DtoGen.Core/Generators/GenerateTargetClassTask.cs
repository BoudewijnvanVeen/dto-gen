using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DtoGen.Core.Generators
{
    /// <summary>
    /// Generates the declaration of the class for the second type in the transform.
    /// </summary>
    public class GenerateTargetClassTask : TransformTask
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateTargetClassTask"/> class.
        /// </summary>
        public GenerateTargetClassTask(Transform transform) : base(transform)
        {
        }


        /// <summary>
        /// Renders the class declaration.
        /// </summary>
        public override IEnumerable<MemberDeclarationSyntax> Render()
        {
            // get property declarations
            var declarations = Transform.Members.SelectMany(m => m.PropertyMemberRenderer.GetDeclarationCode()).ToArray();

            // generate class
            yield return SyntaxHelper.GenerateNamespace(Transform.TargetType.Namespace, new MemberDeclarationSyntax[]
            {
                SyntaxHelper.GenerateClass(Transform.TargetType.Name, new [] { SyntaxKind.PublicKeyword, SyntaxKind.PartialKeyword }, declarations)
            }, 
            GetUsingsForNamespace());
        }
    }
}