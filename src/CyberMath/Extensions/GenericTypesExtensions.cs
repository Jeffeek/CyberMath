#region Using namespaces

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#endregion

namespace CyberMath.Extensions
{
    /// <summary>
    ///     Just a class to make our life and programming faster and more productive
    /// </summary>
    public static class GenericTypesExtensions
    {
        /// <summary>
        /// Fast copy for arrays of unmanaged types using Span
        /// </summary>
        public static T[]? SpanDeepCopy<T>(this T[]? source) where T : unmanaged
        {
            if (source == null) return null;

            var destination = new T[source.Length];
            var sourceSpan = new ReadOnlySpan<T>(source);
            var destSpan = new Span<T>(destination);
            sourceSpan.CopyTo(destSpan);

            return destination;
        }

        /// <summary>
        /// Fast copy for 2D arrays of unmanaged types
        /// </summary>
        public static T[,]? SpanDeepCopy2D<T>(this T[,]? source) where T : unmanaged
        {
            if (source == null) return null;

            var rows = source.GetLength(0);
            var cols = source.GetLength(1);
            var destination = new T[rows, cols];

            Buffer.BlockCopy(source, 0, destination, 0,
                rows * cols * System.Runtime.InteropServices.Marshal.SizeOf<T>());

            return destination;
        }

        /// <summary>
        /// Creates a deep copy using reflection
        /// Pros: Handles circular references, works with any type
        /// Cons: Slower than compiled approaches, complex implementation
        /// </summary>
        public static T ReflectionDeepCopy<T>(this T source)
        {
            var result = ReflectionDeepCopyInternal(source, new Dictionary<object, object>());

            if (result is T typedResult)
                return typedResult;

            return default!;
        }

        private static object? ReflectionDeepCopyInternal(object? source, Dictionary<object, object> visited)
        {
            if (source == null) return null;

            var type = source.GetType();

            if (type.IsPrimitive || type == typeof(string) || type == typeof(decimal))
                return source;

            if (visited.TryGetValue(source, out var @internal))
                return @internal;

            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                var array = source as Array;
                var copied = Array.CreateInstance(elementType!, array!.Length);
                visited[source] = copied;

                for (var i = 0; i < array.Length; i++)
                    copied.SetValue(ReflectionDeepCopyInternal(array.GetValue(i), visited), i);

                return copied;
            }

            if (type.IsGenericType)
            {
                var genericType = type.GetGenericTypeDefinition();

                if (genericType == typeof(List<>))
                {
                    var list = Activator.CreateInstance(type);
                    visited[source] = list;

                    var addMethod = type.GetMethod("Add");
                    foreach (var item in (IEnumerable)source)
                        addMethod!.Invoke(list, new[] { ReflectionDeepCopyInternal(item, visited) });

                    return list;
                }

                if (genericType == typeof(Dictionary<,>))
                {
                    var dict = Activator.CreateInstance(type);
                    visited[source] = dict;

                    var addMethod = type.GetMethod("Add");
                    var sourceDict = source as IDictionary;

                    foreach (DictionaryEntry entry in sourceDict!)
                    {
                        var key = ReflectionDeepCopyInternal(entry.Key, visited);
                        var value = ReflectionDeepCopyInternal(entry.Value, visited);

                        addMethod!.Invoke(dict, new[] { key, value });
                    }

                    return dict;
                }
            }

            var result = Activator.CreateInstance(type);
            visited[source] = result;

            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                var fieldValue = field.GetValue(source);
                var copiedValue = ReflectionDeepCopyInternal(fieldValue, visited);
                field.SetValue(result, copiedValue);
            }

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!(property is { CanRead: true, CanWrite: true })) continue;

                var propertyValue = property.GetValue(source);
                var copiedValue = ReflectionDeepCopyInternal(propertyValue, visited);
                property.SetValue(result, copiedValue);
            }

            return result;
        }
    }

    public static class ExpressionDeepCopy<T>
    {
        private static readonly Func<T, T> Cloner = CreateCloner();

        public static T Clone(T source)
        {
            if (source == null) return default!;
            return Cloner(source);
        }

        private static Func<T, T> CreateCloner()
        {
            var type = typeof(T);

            if (type.IsPrimitive || type == typeof(string) || type == typeof(decimal))
                return x => x;

            var parameter = Expression.Parameter(type, "source");

            var bindings = (from field in type.GetFields(BindingFlags.Public | BindingFlags.Instance) let fieldAccess = Expression.Field(parameter, field) select Expression.Bind(field, fieldAccess)).Cast<MemberBinding>().ToList();

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!(property is { CanRead: true, CanWrite: true })) continue;

                var propertyAccess = Expression.Property(parameter, property);
                var binding = Expression.Bind(property, propertyAccess);
                bindings.Add(binding);
            }

            var constructor = type.GetConstructor(Type.EmptyTypes);
            if (constructor == null)
                throw new InvalidOperationException($"Type {type.Name} must have a parameterless constructor");

            var newExpression = Expression.New(constructor);
            var memberInit = Expression.MemberInit(newExpression, bindings);

            var lambda = Expression.Lambda<Func<T, T>>(memberInit, parameter);
            return lambda.Compile();
        }
    }
}