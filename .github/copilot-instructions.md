# Copilot Instructions for `mcps`

## Build, test, and lint

This repository currently contains MCP template JSON samples and C# model mappings only. No build/test/lint project files are committed here (`.sln`, `.csproj`, `package.json`, `pyproject.toml`, etc.), so there are no repository-native build, test, lint, or single-test commands to run.

## High-level architecture

- `index.json` is the registry document. It declares the template index schema, provenance notes, and the canonical `templates` list that points to each `*.mcp.json` file.
- Each `*.mcp.json` file is one MCP template document (`schema: openstaff.mcp-template.v1`) containing:
  - template identity/metadata (`template_id`, `key`, `display_name`, `category`, `source`, `homepage`, `match_hints`)
  - one or more runnable `profiles`
  - a unified `parameter_schema` for all configurable inputs
- `McpTemplateModels.cs` defines the C# DTOs for both index and template documents, including nested profile/parameter objects and JSON property mapping via `System.Text.Json`.
- Template set composition:
  - `*.mcp.json` (for built-in mirrors): mirrored from the current seed definitions (legacy builtin templates)
  - `github-official.mcp.json`: separate "official GitHub MCP" template modeling remote/container/binary deployment modes

## Key conventions in this repo

- Keep `index.json -> templates[]` synchronized with actual template files whenever templates are added, removed, or renamed.
- Treat `parameter_schema` as the single source of truth for all user-configurable values, including secrets (`type: "password"`). Do not introduce a separate `secrets` block.
- Profile wiring uses interpolation placeholders:
  - `${param:<key>}` references values from `parameter_schema`
  - `${project.workspace}` is used for workspace-relative defaults
- Use `applies_to_profiles` on parameters when a value is profile-specific (for example, remote-only URL/flags or binary-only path settings).
- Follow existing `templateId`/filename conventions:
  - builtin mirrors: `template_id` like `builtin.<capability>.legacy` and filename `<capability>.mcp.json`
  - official GitHub template: `template_id` `official.github.current` and filename `github-official.mcp.json`
- If template schema fields change, update `McpTemplateModels.cs` in the same change so JSON and C# mappings stay aligned.
- Preserve Windows-style path defaults where already used (for example `C:\\...` and `${project.workspace}\\...` in parameter defaults).
