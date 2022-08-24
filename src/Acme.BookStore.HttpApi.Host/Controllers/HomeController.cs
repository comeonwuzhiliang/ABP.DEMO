using Acme.BookStore.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Json;
using Volo.Abp.Json.Newtonsoft;
using Volo.Abp.Json.SystemTextJson;

namespace Acme.BookStore.Controllers
{
    public class HomeController : AbpController
    {
        private readonly IJsonSerializer _jsonSerializer;
        private readonly AbpNewtonsoftJsonSerializerProvider _abpNewtonsoftJsonSerializerProvider;
        private readonly AbpSystemTextJsonSerializerProvider _abpSystemTextJsonSerializerProvider;

        public HomeController(
            IJsonSerializer jsonSerializer,
            AbpNewtonsoftJsonSerializerProvider abpNewtonsoftJsonSerializerProvider,
            AbpSystemTextJsonSerializerProvider abpSystemTextJsonSerializerProvider
            )
        {
            _jsonSerializer = jsonSerializer;
            _abpNewtonsoftJsonSerializerProvider = abpNewtonsoftJsonSerializerProvider;
            _abpSystemTextJsonSerializerProvider = abpSystemTextJsonSerializerProvider;

            var jsonSerializerStr = _jsonSerializer.Serialize(new BookDto { Type2 = BookEnumerationType.Biography });
            var abpNewtonsoftJsonSerializerProviderStr = _abpNewtonsoftJsonSerializerProvider.Serialize(new BookDto { Type2 = BookEnumerationType.Biography });
            var abpSystemTextJsonSerializerProviderStr = _abpSystemTextJsonSerializerProvider.Serialize(new BookDto { Type2 = BookEnumerationType.Biography });
        }

        public ActionResult Index()
        {
            return Redirect("~/swagger");
        }
    }
}
