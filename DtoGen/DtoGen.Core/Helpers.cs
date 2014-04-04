using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DtoGen.Core
{
    public class Helpers
    {

        /// <summary>
        /// Extracts the name from the lambda expression in the 'a => a.Property' format.
        /// </summary>
        public static string ExtractNameFromGetPropertyExpression<TSource, TField>(Expression<Func<TSource, TField>> Field)
        {
            return (Field.Body as MemberExpression ?? ((UnaryExpression)Field.Body).Operand as MemberExpression).Member.Name;
        }

    }
}
