using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DtoGen.Core
{
    public static class SyntaxFactoryExtensions
    {
        public static CompilationUnitSyntax WithMembers(this CompilationUnitSyntax syntax, params MemberDeclarationSyntax[] tokens)
        {
            return syntax.WithMembers(SyntaxFactory.List(tokens));
        }


        public static NamespaceDeclarationSyntax WithMembers(this NamespaceDeclarationSyntax syntax, params MemberDeclarationSyntax[] tokens)
        {
            return syntax.WithMembers(SyntaxFactory.List(tokens));
        }

        public static NamespaceDeclarationSyntax WithUsings(this NamespaceDeclarationSyntax syntax, params UsingDirectiveSyntax[] tokens)
        {
            return syntax.WithUsings(SyntaxFactory.List(tokens));
        }


        public static ClassDeclarationSyntax WithMembers(this ClassDeclarationSyntax syntax, params MemberDeclarationSyntax[] tokens)
        {
            return syntax.WithMembers(SyntaxFactory.List(tokens));
        }

        public static ClassDeclarationSyntax WithModifiers(this ClassDeclarationSyntax syntax, params SyntaxToken[] tokens)
        {
            return syntax.WithModifiers(SyntaxFactory.TokenList(tokens));
        }


        public static PropertyDeclarationSyntax WithAccessorList(this PropertyDeclarationSyntax syntax, params AccessorDeclarationSyntax[] tokens)
        {
            return syntax.WithAccessorList(SyntaxFactory.AccessorList(SyntaxFactory.List(tokens)));
        }

        public static PropertyDeclarationSyntax WithModifiers(this PropertyDeclarationSyntax syntax, params SyntaxToken[] tokens)
        {
            return syntax.WithModifiers(SyntaxFactory.TokenList(tokens));
        }


        public static MethodDeclarationSyntax WithParameterList(this MethodDeclarationSyntax syntax, params ParameterSyntax[] tokens)
        {
            return syntax.WithParameterList(SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList(tokens)));
        }

        public static MethodDeclarationSyntax WithModifiers(this MethodDeclarationSyntax syntax, params SyntaxToken[] tokens)
        {
            return syntax.WithModifiers(SyntaxFactory.TokenList(tokens));
        }
    }
}
