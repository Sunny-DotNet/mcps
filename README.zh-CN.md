# MCP 模板导出（Demo）

[ English ](README.md) | [ **中文** ](README.zh-CN.md) | [ 日本語 ](README.ja-JP.md) | [ Français ](README.fr-FR.md)

---

本仓库用于存放 **MCP 模板定义示例** 以及对应的 C# 映射模型。  
主要目标是沉淀并固定 `capability + profile + parameter_schema` 这套模板结构。

## 模板清单

| 模板 Key | 文件 | 分类 | 主要运行方式 |
|---|---|---|---|
| filesystem | `filesystem.mcp.json` | filesystem | npx |
| github（legacy） | `github-legacy.mcp.json` | dev-tools | npx |
| github（official） | `github-official.mcp.json` | dev-tools | remote / docker / binary |
| brave-search | `brave-search.mcp.json` | search | npx |
| fetch | `fetch.mcp.json` | search | uvx |
| everything | `everything.mcp.json` | filesystem | npx |
| memory | `memory.mcp.json` | memory | npx |
| postgresql | `postgresql.mcp.json` | database | npx |
| puppeteer | `puppeteer.mcp.json` | browser | npx |
| sequential-thinking | `sequential-thinking.mcp.json` | general | npx |
| sqlite | `sqlite.mcp.json` | database | uvx |

## 仓库结构

- `index.json`：模板索引与文件清单入口
- `*.mcp.json`：模板定义（属性统一为 snake_case）
- `McpTemplateModels.cs`：JSON 对应的 C# DTO 映射
- `.github/copilot-instructions.md`：仓库定制的 Copilot 指南

## JSON 合同（snake_case）

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

## 关键约定

- `index.json -> templates[]` 必须与实际模板文件保持同步。
- JSON 属性统一使用 `snake_case`。
- 所有可配置项（包括密钥）统一放在 `parameter_schema`。
- 通过 `${param:<key>}` 把 profile 运行参数与配置项绑定。
- `logo` 的值统一采用 `github-official.mcp.json` 的 URL 风格。

## 构建 / 测试 / Lint

当前仓库没有提交可执行的构建、测试、Lint 工程配置，因此没有原生命令可运行。

## GitHub Pages

启用 Pages 后可访问：

- `docs/index.html`（仓库内页面）
- 或仓库对应的 GitHub Pages 发布地址
