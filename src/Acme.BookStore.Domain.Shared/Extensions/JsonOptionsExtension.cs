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

            foreach (var typeInfo in filterTypeInfos)
            {
                var genericType = typeof(EnumerationClassSystemTextJsonConverter<>).MakeGenericType(typeInfo);
                JsonConverter genericTypeObj = (JsonConverter)Activator.CreateInstance(genericType);
                options.Converters.Add(genericTypeObj);
            }
        }
    }
}
