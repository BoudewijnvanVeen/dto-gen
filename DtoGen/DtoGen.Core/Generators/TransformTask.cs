using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DtoGen.Core.Generators
{
    /// <summary>
    /// Represents a task that generates some part of the code.
    /// </summary>
    public abstract class TransformTask
    {
        /// <summary>
        /// Gets the transform that will be rendered.
        /// </summary>
        public Transform Transform { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformTask"/> class.
        /// </summary>
        protected TransformTask(Transform transform)
        {
            Transform = transform;
        }

        /// <summary>
        /// Renders the code.
        /// </summary>
        public abstract IEnumerable<MemberDeclarationSyntax> Render();



        /// <summary>
        /// Gets the usings for namespace.
        /// </summary>
        protected List<string> GetUsingsForNamespace()
        {
            return new List<string>() { Transform.SourceType.Namespace, Transform.TargetNameSpace };
        }
    }
}