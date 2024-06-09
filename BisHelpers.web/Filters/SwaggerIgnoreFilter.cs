using System.Reflection;
using System.Text.Json.Serialization;
using BisHelpers.Domain.CustomAttributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BisHelpers.web.Filters;

public class SwaggerIgnoreFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Properties.Count == 0)
            return;

        var properties = context.Type.GetProperties();

        var excludedList = properties
            .Where(m => m.GetCustomAttribute<SwaggerIgnoreAttribute>() is not null)
            .Select(m => m.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? m.Name.ToCamelCase());

        foreach (var excludedName in excludedList)
            schema.Properties.Remove(excludedName);
    }
}
