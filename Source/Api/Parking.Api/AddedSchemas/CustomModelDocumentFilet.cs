using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Parking.Api.Swasbuckle
{
    public class CustomModelDocumentFilter<T> : IDocumentFilter where T : class
    {
        public void Apply(OpenApiDocument openapiDoc, DocumentFilterContext context)
        {
            context.SchemaGenerator.GenerateSchema(typeof(T), context.SchemaRepository);
        }
    }
}
