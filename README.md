# MCP Template Export (Demo)

[ **English** ](README.md) | [ 中文 ](README.zh-CN.md) | [ 日本語 ](README.ja-JP.md) | [ Français ](README.fr-FR.md)

---

This repository contains **demo MCP template documents** and the corresponding C# mapping models.  
It is used to discuss and stabilize the template contract around `capability + profile + parameter_schema`.

## Template Catalog

| Template Key | File | Category | Primary Runner |
|---|---|---|---|
| filesystem | `filesystem.mcp.json` | filesystem | npx |
| github (legacy) | `github-legacy.mcp.json` | dev-tools | npx |
| github (official) | `github-official.mcp.json` | dev-tools | remote / docker / binary |
| brave-search | `brave-search.mcp.json` | search | npx |
| fetch | `fetch.mcp.json` | search | uvx |
| everything | `everything.mcp.json` | filesystem | npx |
| memory | `memory.mcp.json` | memory | npx |
| postgresql | `postgresql.mcp.json` | database | npx |
| puppeteer | `puppeteer.mcp.json` | browser | npx |
| sequential-thinking | `sequential-thinking.mcp.json` | general | npx |
| sqlite | `sqlite.mcp.json` | database | uvx |

## Repository Structure

- `index.json`: template registry and canonical file list
- `*.mcp.json`: template definitions (snake_case properties)
- `McpTemplateModels.cs`: C# DTO mapping for index/template JSON
- `.github/copilot-instructions.md`: repository-specific Copilot guidance

## JSON Contract (snake_case)

```json
{
  "schema": "openstaff.mcp-template.v1",
  "template_id": "builtin.filesystem.legacy",
  "key": "filesystem",
  "display_name": "Filesystem",
  "match_hints": {
    "name": "Filesystem",
    "npm_package": "@modelcontextprotocol/server-filesystem",
    "pypi_package": null
  },
  "profiles": [],
  "parameter_schema": []
}
```

## Conventions

- Keep `index.json -> templates[]` synchronized with actual files.
- Keep all JSON property names in `snake_case`.
- Keep all configurable values in `parameter_schema` (including secrets with `type: "password"`).
- Use `${param:<key>}` placeholders to wire profile runtime fields to parameter values.
- `logo` values follow the URL style used by `github-official.mcp.json`.

## Build/Test/Lint

This repo currently has no committed build/test/lint project files, so there are no native commands to run.

## GitHub Pages

After enabling Pages, open:

- `docs/index.html` (repository view)
- or the published Pages URL for this repo
