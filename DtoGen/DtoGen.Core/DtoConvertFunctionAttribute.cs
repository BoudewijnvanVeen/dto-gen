using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoGen.Core
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DtoConvertFunctionAttribute : Attribute
    {

        public Type Type1 { get; private set; }

        public Type Type2 { get; private set; }

        public bool CreatesNewInstance { get; set; }

        public DtoConvertFunctionAttribute(Type type1, Type type2, bool createsNewInstance)
        {
            Type1 = type1;
            Type2 = type2;
            CreatesNewInstance = createsNewInstance;
        }
    }
}
