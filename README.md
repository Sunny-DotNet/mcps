# MCP Template Export (Demo)

[ **English** ](README.md) | [ 中文 ](README.zh-CN.md) | [ 日本語 ](README.ja-JP.md) | [ Français ](README.fr-FR.md)

---

This repository contains **demo MCP template documents** and the corresponding C# mapping models.
It is used to discuss and stabilize the template contract around `capability + profile + parameter_schema`.

## Repository Structure

```
├── templates/             # MCP 模板定义文件 (*.mcp.json)
├── index.json             # 从 templates 自动生成的摘要数组
├── index.html             # 模板浏览 Web 界面
├── McpTemplateModels.cs   # 模板文档的 C# DTO 映射
├── .github/
│   └── scripts/generate_index.py   # 生成 index.json 的脚本
```

## JSON Contract

Each `templates/*.mcp.json` file follows the `openstaff.mcp-template.v1` schema. All property names use **snake_case**.

### Top-level Properties

| Property | Type | Description |
|---|---|---|
| `schema` | string | Schema identifier, always `"openstaff.mcp-template.v1"` |
| `template_id` | string | Unique template identifier. Builtin mirrors: `builtin.<capability>.legacy`; official: `official.<name>.current` |
| `key` | string | Short identifier used for lookup and matching |
| `display_name` | string | Human-readable name shown in UI |
| `description` | string | Brief description of what the template provides |
| `category` | string | One of: `filesystem`, `dev-tools`, `search`, `database`, `browser`, `memory`, `general` |
| `icon` | string | Icon identifier (e.g. `"folder"`, `"github"`) |
| `logo` | string | Icon URL, format: `https://cdn.simpleicons.org/<name>?viewbox=auto` |
| `source` | string | Template origin identifier (e.g. `"builtin-current-seed"`, `"official-github-mcp"`) |
| `homepage` | string | URL to the upstream project or documentation |
| `match_hints` | object | Package discovery hints (see below) |
| `profiles` | array | Runnable deployment configurations (see below) |
| `parameter_schema` | array | All user-configurable values including secrets (see below) |

### match_hints

| Property | Type | Description |
|---|---|---|
| `name` | string | Display name for matching |
| `npm_package` | string\|null | npm package name for auto-discovery |
| `pypi_package` | string\|null | PyPI package name for auto-discovery |

### profiles

Each profile defines a deployment mode. Supported `profile_type` values: `package`, `remote`, `container`, `binary`.

| Property | Type | Applicable Profiles | Description |
|---|---|---|---|
| `id` | string | all | Unique profile identifier (e.g. `"package-npm"`, `"remote"`) |
| `profile_type` | string | all | One of: `package`, `remote`, `container`, `binary` |
| `transport_type` | string | all | Communication transport: `"stdio"` or `"http"` |
| `runner_kind` | string | all | Runner category, matches `profile_type` |
| `runner` | string | all | Runner command: `"npx"`, `"uvx"`, `"remote"`, `"docker"`, `"binary"` |
| `ecosystem` | string | package | Package ecosystem: `"npm"` or `"python"` |
| `package_name` | string | package | Package identifier (e.g. `"@modelcontextprotocol/server-filesystem"`) |
| `package_version` | string | package | Version constraint (e.g. `"latest"`) |
| `command` | string | package, container | Static command to execute |
| `command_template` | string | binary | Command with interpolation: `"${param:binaryPath}"` |
| `args_template` | string[] | all | Argument list with interpolation placeholders |
| `env_template` | object | all | Environment variables with interpolation: `{"KEY": "${param:accessToken}"}` |
| `working_directory_template` | string | package, binary | Working directory with interpolation |
| `url_template` | string | remote | Remote endpoint URL with interpolation |
| `headers_template` | object | remote | HTTP headers with interpolation |
| `image` | string | container | Container image name (without tag) |
| `image_tag_template` | string | container | Image tag with interpolation |

### parameter_schema

All user-configurable values live here. No separate `secrets` block — use `type: "password"` for sensitive values.

| Property | Type | Description |
|---|---|
| `key` | string | Parameter identifier, referenced as `${param:<key>}` in profiles |
| `label` | string | Human-readable label shown in UI |
| `type` | string | One of: `"string"`, `"boolean"`, `"password"` |
| `required` | boolean | Whether the user must provide a value |
| `default_value` | any | Default value. Use `"${project.workspace}"` for workspace-relative defaults |
| `description` | string | Explanation of what this parameter controls |
| `applies_to_profiles` | string[]\|omitted | If set, only applies to the listed profile IDs. Omitted = applies to all profiles |

### Interpolation Placeholders

- `${param:<key>}` — References a value from `parameter_schema`
- `${project.workspace}` — Current project workspace path

## Conventions

- `index.json` is generated from `templates/*.mcp.json` via `python .github/scripts/generate_index.py`, not hand-maintained.
- If template schema fields change, update `McpTemplateModels.cs` in the same change.
