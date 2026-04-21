# MCP Template Export (Demo)

[ **English** ](README.md) | [ 中文 ](README.zh-CN.md) | [ 日本語 ](README.ja-JP.md) | [ Français ](README.fr-FR.md)

---

This repository contains **demo MCP template documents** and the corresponding C# mapping models.  
It is used to discuss and stabilize the template contract around `capability + profile + parameter_schema`.

## Template Catalog

| Template Key | File | Category | Primary Runner |
|---|---|---|---|
| filesystem | `templates/filesystem.mcp.json` | filesystem | npx |
| github (legacy) | `templates/github-legacy.mcp.json` | dev-tools | npx |
| github (official) | `templates/github-official.mcp.json` | dev-tools | remote / docker / binary |
| brave-search | `templates/brave-search.mcp.json` | search | npx |
| fetch | `templates/fetch.mcp.json` | search | uvx |
| everything | `templates/everything.mcp.json` | filesystem | npx |
| memory | `templates/memory.mcp.json` | memory | npx |
| postgresql | `templates/postgresql.mcp.json` | database | npx |
| puppeteer | `templates/puppeteer.mcp.json` | browser | npx |
| sequential-thinking | `templates/sequential-thinking.mcp.json` | general | npx |
| sqlite | `templates/sqlite.mcp.json` | database | uvx |

## Repository Structure

- `index.json`: aggregated template summary array (generated from `templates/*.mcp.json`)
- `templates/*.mcp.json`: template definitions (snake_case properties)
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

- `index.json` should be generated from `templates/*.mcp.json` (manually via script or by GitHub Actions), not hand-maintained.
- Keep all JSON property names in `snake_case`.
- Keep all configurable values in `parameter_schema` (including secrets with `type: "password"`).
- Use `${param:<key>}` placeholders to wire profile runtime fields to parameter values.
- `logo` values follow the URL style used by `templates/github-official.mcp.json`.

## Build/Test/Lint

This repo currently has no committed build/test/lint project files, so there are no native commands to run.

## GitHub Pages

After enabling Pages, open:

- `index.html` (repository view)
- or the published Pages URL for this repo
