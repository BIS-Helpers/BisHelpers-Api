using BisHelpers.Domain.CustomAttributes;

namespace BisHelpers.Domain;
public class BaseDto
{
    [SwaggerIgnore]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CreatedBy { get; set; } = null;

    [SwaggerIgnore]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? CreatedOn { get; set; } = null;

    [SwaggerIgnore]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LastUpdatedBy { get; set; } = null;

    [SwaggerIgnore]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? LastUpdatedOn { get; set; } = null;

    [SwaggerIgnore]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsDeleted { get; set; } = null;
}
