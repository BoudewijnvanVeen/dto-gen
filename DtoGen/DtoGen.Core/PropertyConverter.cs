using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace DtoGen.Core
{
    public class PropertyConverter
    {
        private static bool isInitialized = false;
        private static object locker = new object();
        private static Dictionary<TypePair, Func<object, object>> convertFunctions = new Dictionary<TypePair, Func<object, object>>();
        private static Dictionary<TypePair, Action<object, object>> populateFunctions = new Dictionary<TypePair, Action<object, object>>();

        /// <summary>
        /// Registers the value converters.
        /// </summary>
        public static void RegisterConverter<T1, T2>(Func<T1, T2> function)
        {
            RegisterConverter(typeof(T1), typeof(T2), i => function((T1)i));
        }
        
        /// <summary>
        /// Registers the value converters.
        /// </summary>
        public static void RegisterConverter(Type type1, Type type2, Func<object, object> function)
        {
            lock (locker)
            {
                var key = new TypePair(type1, type2);
                convertFunctions[key] = function;
            }
        }

        /// <summary>
        /// Registers the value populator.
        /// </summary>
        public static void RegisterPopulator<T1, T2>(Action<T1, T2> function)
        {
            RegisterPopulator(typeof(T1), typeof(T2), (i, j) => function((T1)i, (T2)j));
        }

        /// <summary>
        /// Registers the value converters.
        /// </summary>
        public static void RegisterPopulator(Type type1, Type type2, Action<object, object> function)
        {
            lock (locker)
            {
                var key = new TypePair(type1, type2);
                populateFunctions[key] = function;
            }
        }

        /// <summary>
        /// Looks up for all attributes and registers them in the dictionary.
        /// </summary>
        public static void EnsureInitialized(bool force = false)
        {
            if (force)
            {
                FindAllConverterFunctions();
            }
            else if (!isInitialized)
            {
                lock (locker)
                {
                    if (!isInitialized)
                    {
                        FindAllConverterFunctions();
                        isInitialized = true;
                    }
                }
            }
        }

        /// <summary>
        /// Finds all converter functions and registers them.
        /// </summary>
        private static void FindAllConverterFunctions()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).Where(t => t.GetCustomAttribute<DtoGeneratedAttribute>() != null);
            foreach (var method in types.SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public)))
            {
                var attr = method.GetCustomAttribute<DtoConvertFunctionAttribute>();
                if (attr != null)
                {
                    if (attr.CreatesNewInstance)
                    {
                        RegisterConverter(attr.Type1, attr.Type2, i => method.Invoke(null, new[] { i }));
                    }
                    else
                    {
                        RegisterPopulator(attr.Type1, attr.Type2, (i, j) => method.Invoke(null, new[] { i, j }));
                    }
                }
            }
        }


        /// <summary>
        /// Converts the type to another type.
        /// </summary>
        public static T2 Convert<T1, T2>(T1 instance)
        {
            // try to find the registered converter function
            var key = new TypePair(typeof(T1), typeof(T2));
            Func<object, object> result = null;
            lock (locker)
            {
                convertFunctions.TryGetValue(key, out result);
            }
            if (result != null)
            {
                return (T2)result(instance);
            }

            // otherwise perform standard conversions
            return (T2)ConvertValue(instance, typeof (T2));
        }

        /// <summary>
        /// Converts the type to another type.
        /// </summary>
        public static void Populate<T1, T2>(T1 instance, T2 target)
        {
            // try to find the registered converter function
            var key = new TypePair(typeof(T1), typeof(T2));
            Action<object, object> result = null;
            lock (locker)
            {
                populateFunctions.TryGetValue(key, out result);
            }
            if (result != null)
            {
                result(instance, target);
            }
            else
            {
                throw new NotSupportedException(string.Format("There is no transform from type {0} to type {1}!", typeof(T1), typeof(T2)));
            }
        }

        /// <summary>
        /// Converts a value to a specified type
        /// </summary>
        private static object ConvertValue(object value, Type type)
        {
            // handle null values
            if ((value == null) && (type.IsValueType))
                return Activator.CreateInstance(type);

            // handle nullable types
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if ((value is string) && ((string)value == string.Empty))
                {
                    // value is an empty string, return null
                    return null;
                }
                else
                {
                    // value is not null
                    var nullableConverter = new NullableConverter(type);
                    type = nullableConverter.UnderlyingType;
                }
            }

            // handle exceptions
            if ((value is string) && (type == typeof(Guid)))
                return new Guid((string)value);
            if (type == typeof(object)) return value;

            // convert
            return System.Convert.ChangeType(value, type);
        }


    }

    public struct TypePair
    {
        public TypePair(Type type1, Type type2) : this()
        {
            Type1 = type1;
            Type2 = type2;
        }

        public Type Type1 { get; private set; }

        public Type Type2 { get; private set; }
    }
}