using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenStaff.McpTemplateExport;

public sealed class McpTemplateIndexDocument
{
    [JsonPropertyName("schema")]
    public string Schema { get; init; } = string.Empty;

    [JsonPropertyName("generated_from")]
    public string? GeneratedFrom { get; init; }

    [JsonPropertyName("notes")]
    public IReadOnlyList<string> Notes { get; init; } = [];

    [JsonPropertyName("templates")]
    public IReadOnlyList<string> Templates { get; init; } = [];
}

public sealed class McpTemplateDocument
{
    [JsonPropertyName("schema")]
    public string Schema { get; init; } = string.Empty;

    [JsonPropertyName("template_id")]
    public string TemplateId { get; init; } = string.Empty;

    [JsonPropertyName("key")]
    public string Key { get; init; } = string.Empty;

    [JsonPropertyName("display_name")]
    public string DisplayName { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("category")]
    public string? Category { get; init; }

    [JsonPropertyName("icon")]
    public string? Icon { get; init; }

    [JsonPropertyName("logo")]
    public string? Logo { get; init; }

    [JsonPropertyName("source")]
    public string? Source { get; init; }

    [JsonPropertyName("homepage")]
    public string? Homepage { get; init; }

    [JsonPropertyName("match_hints")]
    public McpTemplateMatchHints MatchHints { get; init; } = new();

    [JsonPropertyName("profiles")]
    public IReadOnlyList<McpTemplateProfileDocument> Profiles { get; init; } = [];

    [JsonPropertyName("parameter_schema")]
    public IReadOnlyList<McpTemplateParameterDocument> ParameterSchema { get; init; } = [];
}

public sealed class McpTemplateMatchHints
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("npm_package")]
    public string? NpmPackage { get; init; }

    [JsonPropertyName("pypi_package")]
    public string? PypiPackage { get; init; }
}

public sealed class McpTemplateProfileDocument
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("profile_type")]
    public string ProfileType { get; init; } = string.Empty;

    [JsonPropertyName("transport_type")]
    public string TransportType { get; init; } = string.Empty;

    [JsonPropertyName("runner_kind")]
    public string? RunnerKind { get; init; }

    [JsonPropertyName("runner")]
    public string? Runner { get; init; }

    [JsonPropertyName("ecosystem")]
    public string? Ecosystem { get; init; }

    [JsonPropertyName("package_name")]
    public string? PackageName { get; init; }

    [JsonPropertyName("package_version")]
    public string? PackageVersion { get; init; }

    [JsonPropertyName("image")]
    public string? Image { get; init; }

    [JsonPropertyName("image_tag_template")]
    public string? ImageTagTemplate { get; init; }

    [JsonPropertyName("command")]
    public string? Command { get; init; }

    [JsonPropertyName("command_template")]
    public string? CommandTemplate { get; init; }

    [JsonPropertyName("working_directory_template")]
    public string? WorkingDirectoryTemplate { get; init; }

    [JsonPropertyName("url_template")]
    public string? UrlTemplate { get; init; }

    [JsonPropertyName("args_template")]
    public IReadOnlyList<string> ArgsTemplate { get; init; } = [];

    [JsonPropertyName("env_template")]
    public IReadOnlyDictionary<string, string> EnvTemplate { get; init; } = new Dictionary<string, string>();

    [JsonPropertyName("headers_template")]
    public IReadOnlyDictionary<string, string> HeadersTemplate { get; init; } = new Dictionary<string, string>();
}

public sealed class McpTemplateParameterDocument
{
    [JsonPropertyName("key")]
    public string Key { get; init; } = string.Empty;

    [JsonPropertyName("label")]
    public string? Label { get; init; }

    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("required")]
    public bool Required { get; init; }

    [JsonPropertyName("default_value")]
    public JsonElement DefaultValue { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("applies_to_profiles")]
    public IReadOnlyList<string> AppliesToProfiles { get; init; } = [];
}
