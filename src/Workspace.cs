using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class WorkspaceResponse
{
	[JsonPropertyName("Workspaces")]
	public List<Workspace> Workspaces { get; set; } = new();
}

public class Workspace
{
	[JsonPropertyName("ID")]
	public string? ID { get; set; }

	[JsonPropertyName("LayoutName")]
	public string? LayoutName { get; set; }

	[JsonPropertyName("Name")]
	public string? Name { get; set; }

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
	public JsonElement Configuration { get; set; }

	[JsonPropertyName("HiddenExpression")]
	public HiddenExpression? HiddenExpression { get; set; }

	[JsonPropertyName("isUIV3")]
	public bool IsUIV3 { get; set; }
}

public class HiddenExpression
{
	[JsonPropertyName("Description")]
	public string? Description { get; set; }

	[JsonPropertyName("FieldRefs")]
	public List<string> FieldRefs { get; set; } = new();

	[JsonPropertyName("IsFullExpression")]
	public bool IsFullExpression { get; set; }

	[JsonPropertyName("Name")]
	public string? Name { get; set; }

	[JsonPropertyName("Tree")]
	public HiddenExpressionTree? Tree { get; set; }

	[JsonPropertyName("Source")]
	public string? Source { get; set; }

	[JsonPropertyName("ValidationStatus")]
	public int ValidationStatus { get; set; }
}

public class HiddenExpressionTree
{
	[JsonPropertyName("Value")]
	public bool Value { get; set; }

	[JsonPropertyName("LongImage")]
	public string? LongImage { get; set; }

	[JsonPropertyName("IsHex")]
	public bool IsHex { get; set; }

	[JsonPropertyName("IsVerbatimText")]
	public bool IsVerbatimText { get; set; }

	[JsonPropertyName("Kind")]
	public int Kind { get; set; }
}