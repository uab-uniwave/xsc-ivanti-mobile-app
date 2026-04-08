using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.RoleWorkspaces;

public sealed class Workspace
{
    [JsonPropertyName("ID")]
    public string Id { get; set; } = default!;

    [JsonPropertyName("LayoutName")]
    public string? LayoutName { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("Behavior")]
    public string? Behavior { get; set; }

    [JsonPropertyName("Profile")]
    public string? Profile { get; set; }

    [JsonPropertyName("Link")]
    public string? Link { get; set; }

    [JsonPropertyName("Module")]
    public string? Module { get; set; }

    [JsonPropertyName("RecentList")]
    public bool RecentList { get; set; }

    [JsonPropertyName("Default")]
    public bool Default { get; set; }

    [JsonPropertyName("Visible")]
    public bool Visible { get; set; }

    [JsonPropertyName("VisibleInMainMenu")]
    public bool VisibleInMainMenu { get; set; }

    [JsonPropertyName("Closable")]
    public bool Closable { get; set; }

    [JsonPropertyName("Searchable")]
    public bool Searchable { get; set; }

    [JsonPropertyName("AdminRoleRequired")]
    public bool AdminRoleRequired { get; set; }

    [JsonPropertyName("Configuration")]
    public RoleWorkspaceConfiguration? Configuration { get; set; }

    [JsonPropertyName("HiddenExpression")]
    public string? HiddenExpression { get; set; }

    [JsonPropertyName("isUIV3")]
    public bool IsUiV3 { get; set; }
}
