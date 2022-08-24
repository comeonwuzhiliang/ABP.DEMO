using Acme.BookStore.Books;
using Acme.BookStore.JsonConverters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Volo.Abp.Collections;

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

        public static void AddEnumerationClassJsonConverters(this IServiceCollection serviceCollection, Type type)
        {
            Assembly assembly = type.Assembly;

            var assemblyDefinedTypeInfos = assembly.DefinedTypes;

            var filterTypeInfos = assemblyDefinedTypeInfos.Where(t => typeof(Enumeration).IsAssignableFrom(t) && !t.IsAbstract);

            IList<Expression> addSingletonServiceExpressions = new List<Expression>();


            foreach (var typeInfo in filterTypeInfos)
            {
                var genericType = typeof(EnumerationClassNewtonsoftJsonConverter<>).MakeGenericType(typeInfo);

                MethodInfo methodInfoAddSingletonService = typeof(ServiceCollectionServiceExtensions).GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance)
    .FirstOrDefault(
    m => m.Name == "AddSingleton" && m.IsGenericMethod == true && m.GetGenericArguments().Length == 1 && m.GetParameters().Length == 1);

                var callAddSingletonServiceExpression = Expression.Call(null, methodInfoAddSingletonService.MakeGenericMethod(genericType), Expression.Constant(serviceCollection));

                addSingletonServiceExpressions.Add(callAddSingletonServiceExpression);
            }

            var addSingletonServiceBlock = Expression.Block(addSingletonServiceExpressions);

            Expression<Action> addSingletonServicelambda =
                Expression.Lambda<Action>(addSingletonServiceBlock);

            addSingletonServicelambda.Compile()();
        }

        public static void AddEnumerationJsonConverters(this ITypeList<Newtonsoft.Json.JsonConverter> converters, Type type)
        {
            Assembly assembly = type.Assembly;

            var assemblyDefinedTypeInfos = assembly.DefinedTypes;

            var filterTypeInfos = assemblyDefinedTypeInfos.Where(t => typeof(Enumeration).IsAssignableFrom(t) && !t.IsAbstract);

            // 表达式版（后期会提供一个Provider）
            IList<Expression> expressions = new List<Expression>();

            var parameterExpression = Expression.Parameter(typeof(ITypeList<Newtonsoft.Json.JsonConverter>), "p");

            foreach (var typeInfo in filterTypeInfos)
            {
                var genericType = typeof(EnumerationClassNewtonsoftJsonConverter<>).MakeGenericType(typeInfo);

                MethodInfo methodInfo = typeof(ITypeList<Newtonsoft.Json.JsonConverter>).GetMethods().FirstOrDefault(
                    m => m.Name == "Add" && m.IsGenericMethod == true && m.GetGenericArguments().Length == 1
                );

                // ITypeList 底层利用了泛型接口的逆变思想
                var callExpression = Expression.Call(parameterExpression, methodInfo.MakeGenericMethod(genericType));

                expressions.Add(callExpression);
            }

            var block = Expression.Block(expressions);

            Expression<Action<ITypeList<Newtonsoft.Json.JsonConverter>>> lambda = Expression.Lambda<Action<ITypeList<Newtonsoft.Json.JsonConverter>>>(block, parameterExpression);

            lambda.Compile()(converters);
        }
    }
}
