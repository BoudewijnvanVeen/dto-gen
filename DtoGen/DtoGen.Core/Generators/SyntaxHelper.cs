using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DtoGen.Core.Generators
{
    /// <summary>
    /// Contains useful helper methods to generate Roslyn syntax tree parts.
    /// </summary>
    static internal class SyntaxHelper
    {
        /// <summary>
        /// Generates the static method call.
        /// </summary>
        public static ExpressionStatementSyntax GenerateStaticMethodCall(string methodName, string className, ArgumentSyntax[] arguments = null, Type[] typeArguments = null)
        {
            var methodIdentifier = GenerateMethodIdentifier(methodName, className, typeArguments);
            return SyntaxFactory.ExpressionStatement(
                SyntaxFactory.InvocationExpression(methodIdentifier,
                SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList(arguments ?? new ArgumentSyntax[] { })))
            );
        }

        /// <summary>
        /// Generates the method call.
        /// </summary>
        public static ExpressionStatementSyntax GenerateMethodCall(string methodName, string targetIdentifier, ArgumentSyntax[] arguments = null, Type[] typeArguments = null)
        {
            var methodIdentifier = GenerateMethodIdentifier(methodName, targetIdentifier, typeArguments);
            return SyntaxFactory.ExpressionStatement(
                SyntaxFactory.InvocationExpression(methodIdentifier,
                SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList(arguments ?? new ArgumentSyntax[] { })))
            );
        }

        /// <summary>
        /// Generates the method identifier.
        /// </summary>
        public static ExpressionSyntax GenerateMethodIdentifier(string methodName, string targetIdentifierOrTypeName, Type[] typeArguments = null)
        {
            ExpressionSyntax methodIdentifier = SyntaxFactory.IdentifierName(targetIdentifierOrTypeName + "." + methodName);
            if (typeArguments != null)
            {
                methodIdentifier = SyntaxFactory.GenericName(SyntaxFactory.Identifier(targetIdentifierOrTypeName + "." + methodName),
                    SyntaxFactory.TypeArgumentList(SyntaxFactory.SeparatedList(typeArguments.Select(GenerateTypeSyntax))));
            }
            return methodIdentifier;
        }

        /// <summary>
        /// Generates the variable declaration and object creation statement.
        /// </summary>
        public static LocalDeclarationStatementSyntax GenerateVariableDeclarationAndObjectCreationStatement(string variableName, string typeName)
        {
            return GenerateVariableDeclarationAndObjectCreationStatement(variableName, () => SyntaxFactory.ParseTypeName(typeName));
        }

        /// <summary>
        /// Generates the variable declaration and object creation statement.
        /// </summary>
        public static LocalDeclarationStatementSyntax GenerateVariableDeclarationAndObjectCreationStatement(string variableName, Type type)
        {
            return GenerateVariableDeclarationAndObjectCreationStatement(variableName, () => GenerateTypeSyntax(type));
        }

        /// <summary>
        /// Generates the variable declaration and object creation statement.
        /// </summary>
        private static LocalDeclarationStatementSyntax GenerateVariableDeclarationAndObjectCreationStatement(string variableName, Func<TypeSyntax> typeSyntaxFactory)
        {
            return SyntaxFactory.LocalDeclarationStatement(
                SyntaxFactory.VariableDeclaration(
                    typeSyntaxFactory(),
                    SyntaxFactory.SeparatedList(new[] {
                        SyntaxFactory.VariableDeclarator(
                            SyntaxFactory.Identifier(variableName), 
                            null, 
                            SyntaxFactory.EqualsValueClause(
                                SyntaxFactory.ObjectCreationExpression(typeSyntaxFactory(), SyntaxFactory.ArgumentList(), null)
                            )
                        )
                    })
                )
            );
        }

        /// <summary>
        /// Generates the extension method.
        /// </summary>
        public static MethodDeclarationSyntax GenerateExtensionMethod(string methodName, string returnTypeName, ParameterSyntax[] parameters, AttributeSyntax[] attributes = null)
        {
            var methodDeclaration = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName(returnTypeName ?? "void"), methodName)
                .WithModifiers(
                    SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                    SyntaxFactory.Token(SyntaxKind.StaticKeyword)
                )
                .WithParameterList(
                    parameters
                );

            if (attributes != null)
            {
                methodDeclaration = methodDeclaration.WithAttributeLists(SyntaxFactory.List(new[]
                {
                    SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(attributes)) 
                }));
            }
            return methodDeclaration;
        }

        /// <summary>
        /// Generates the method parameter.
        /// </summary>
        public static ParameterSyntax GenerateMethodParameter(string parameterName, string typeName, bool isExtensionMethodFirstParameter)
        {
            return SyntaxFactory.Parameter(
                SyntaxFactory.List<AttributeListSyntax>(),
                isExtensionMethodFirstParameter ? SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.ThisKeyword)) : SyntaxFactory.TokenList(),
                SyntaxFactory.ParseTypeName(typeName),
                SyntaxFactory.Identifier(parameterName),
                null
            );
        }

        /// <summary>
        /// Generates the class.
        /// </summary>
        public static ClassDeclarationSyntax GenerateClass(string className, SyntaxKind[] modifiers, MemberDeclarationSyntax[] methods, AttributeSyntax[] attributes = null)
        {
            var classDeclaration = SyntaxFactory.ClassDeclaration(className)
                .WithModifiers(
                    modifiers.Select(SyntaxFactory.Token).ToArray()
                )
                .WithMembers(
                    SyntaxFactory.List(methods)
                );

            if (attributes != null)
            {
                classDeclaration = classDeclaration.WithAttributeLists(SyntaxFactory.List(new[] {
                    SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(attributes))
                }));
            }
            return classDeclaration;
        }

        /// <summary>
        /// Generates the namespace.
        /// </summary>
        public static NamespaceDeclarationSyntax GenerateNamespace(string namespaceName, MemberDeclarationSyntax[] members, IEnumerable<string> usings = null)
        {
            var builtinUsings = new List<string>() { "System", "System.Collections.Generic", "DtoGen.Core", "DtoGen.Core.Collections" };
            if (usings != null)
            {
                builtinUsings.AddRange(usings);
            }

            return SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(namespaceName))
                .WithUsings(builtinUsings.Distinct().Where(u => namespaceName != u).Select(u => SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(u))).ToArray())
                .WithMembers(members);
        }

        /// <summary>
        /// Generates the type syntax.
        /// </summary>
        public static TypeSyntax GenerateTypeSyntax(Type type)
        {
            var name = GetTypeName(type);

            if (type.IsGenericType)
            {
                var genericArguments = type.GetGenericArguments();
                return GenerateGenericName(name, genericArguments);
            }

            return SyntaxFactory.ParseTypeName(name);
        }

        /// <summary>
        /// Generates the type syntax.
        /// </summary>
        public static TypeSyntax GenerateTypeSyntax(string typeName)
        {
            return SyntaxFactory.ParseTypeName(typeName);
        }

        /// <summary>
        /// Generates the name of the generic.
        /// </summary>
        public static GenericNameSyntax GenerateGenericName(string name, IEnumerable<Type> types)
        {
            return SyntaxFactory.GenericName(SyntaxFactory.Identifier(name),
                SyntaxFactory.TypeArgumentList(SyntaxFactory.SeparatedList(types.Select(GenerateTypeSyntax)))
            );
        }

        /// <summary>
        /// Gets the C# representation of System.Type with respect to generics.
        /// </summary>
        private static string GetTypeName(Type type)
        {
            var name = type.Name.Replace("+", ".");
            if (name.Contains("`"))
            {
                name = name.Substring(0, name.IndexOf("`"));
            }
            return name;
        }

        /// <summary>
        /// Generates the assignment statement.
        /// </summary>
        public static ExpressionStatementSyntax GenerateAssignmentStatement(ExpressionSyntax leftSide, ExpressionSyntax rightSide)
        {
            return SyntaxFactory.ExpressionStatement(
                SyntaxFactory.BinaryExpression(
                    SyntaxKind.SimpleAssignmentExpression,
                    leftSide,
                    rightSide
                )
            );
        }

        /// <summary>
        /// Generates the attribute on the class or method.
        /// </summary>
        public static AttributeSyntax GenerateAttribute(Type type, params ExpressionSyntax[] parameters)
        {
            return SyntaxFactory.Attribute(SyntaxFactory.ParseName(type.FullName),
                SyntaxFactory.AttributeArgumentList(SyntaxFactory.SeparatedList(parameters.Select(SyntaxFactory.AttributeArgument)))
            );
        }
    }
}