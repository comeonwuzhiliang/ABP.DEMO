using Acme.BookStore.Books;
using Acme.BookStore.JsonConverters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Acme.BookStore.Extensions
{
    public static class JsonOptionsExtension
    {
        public static void AddEnumerationJsonConverters(this JsonSerializerOptions options, Type type)
        {
            Assembly assembly = type.Assembly;

            var assemblyDefinedTypeInfos = assembly.DefinedTypes;

            var filterTypeInfos = assemblyDefinedTypeInfos.Where(t => typeof(Enumeration).IsAssignableFrom(t) && !t.IsAbstract);

            // 反射版（后期会提供一个Provider）
            //foreach (var typeInfo in filterTypeInfos)
            //{
            //    var genericType = typeof(EnumerationClassSystemTextJsonConverter<>).MakeGenericType(typeInfo);
            //    JsonConverter genericTypeObj = (JsonConverter)Activator.CreateInstance(genericType);
            //    options.Converters.Add(genericTypeObj);
            //}

            // 表达式版（后期会提供一个Provider）

            IList<Expression> expressions = new List<Expression>();

            var parameterExpression = Expression.Parameter(typeof(IList<JsonConverter>), "p");

            foreach (var typeInfo in filterTypeInfos)
            {
                var genericType = typeof(EnumerationClassSystemTextJsonConverter<>).MakeGenericType(typeInfo);

                Expression newExpression = Expression.New(genericType);

                var callExpression = Expression.Call(parameterExpression, typeof(ICollection<JsonConverter>).GetMethod("Add"), newExpression);

                expressions.Add(callExpression);
            }

            var block = Expression.Block(expressions);

            Expression<Action<IList<JsonConverter>>> lambda = Expression.Lambda<Action<IList<JsonConverter>>>(block, parameterExpression);

            lambda.Compile()(options.Converters);
        }
    }
}
