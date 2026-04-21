# MCP テンプレートエクスポート（デモ）

[ English ](README.md) | [ 中文 ](README.zh-CN.md) | [ **日本語** ](README.ja-JP.md) | [ Français ](README.fr-FR.md)

---

このリポジトリには、**MCP テンプレート定義のサンプル** と対応する C# マッピングモデルを格納しています。  
目的は `capability + profile + parameter_schema` の構造を整理・固定することです。

## テンプレート一覧

| テンプレート Key | ファイル | カテゴリ | 主なランナー |
|---|---|---|---|
| filesystem | `templates/filesystem.mcp.json` | filesystem | npx |
| github（legacy） | `templates/github-legacy.mcp.json` | dev-tools | npx |
| github（official） | `templates/github-official.mcp.json` | dev-tools | remote / docker / binary |
| brave-search | `templates/brave-search.mcp.json` | search | npx |
| fetch | `templates/fetch.mcp.json` | search | uvx |
| everything | `templates/everything.mcp.json` | filesystem | npx |
| memory | `templates/memory.mcp.json` | memory | npx |
| postgresql | `templates/postgresql.mcp.json` | database | npx |
| puppeteer | `templates/puppeteer.mcp.json` | browser | npx |
| sequential-thinking | `templates/sequential-thinking.mcp.json` | general | npx |
| sqlite | `templates/sqlite.mcp.json` | database | uvx |

## リポジトリ構成

- `index.json`: `templates/*.mcp.json` から生成されるテンプレート要約配列
- `templates/*.mcp.json`: テンプレート定義（プロパティ名は snake_case）
- `McpTemplateModels.cs`: index/template JSON の C# DTO マッピング
- `.github/copilot-instructions.md`: このリポジトリ向け Copilot ガイド

## JSON 契約（snake_case）

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

## 主要な規約

- `index.json` は `templates/*.mcp.json` から生成すること（スクリプトまたは GitHub Actions）。手動管理はしないこと。
- JSON のプロパティ名はすべて `snake_case` を使用すること。
- 設定可能な値（シークレットを含む）は `parameter_schema` に集約すること。
- profile と設定値の紐づけは `${param:<key>}` を使うこと。
- `logo` の値は `templates/github-official.mcp.json` と同じ URL スタイルを使うこと。

## Build / Test / Lint

このリポジトリには build/test/lint 用の実行可能なプロジェクト設定がないため、専用コマンドはありません。

## GitHub Pages

Pages を有効化した後、以下を参照できます。

- `index.html`（リポジトリ内ページ）
- またはこのリポジトリの GitHub Pages 公開 URL
