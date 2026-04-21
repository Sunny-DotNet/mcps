# MCP テンプレートエクスポート（デモ）

[ English ](README.md) | [ 中文 ](README.zh-CN.md) | [ **日本語** ](README.ja-JP.md) | [ Français ](README.fr-FR.md)

---

このリポジトリには、**MCP テンプレート定義のサンプル** と対応する C# マッピングモデルを格納しています。
目的は `capability + profile + parameter_schema` の構造を整理・固定することです。

## リポジトリ構成

```
├── templates/             # MCP テンプレート定義ファイル (*.mcp.json)
├── index.json             # templates から自動生成される要約配列
├── index.html             # テンプレート閲覧 Web インターフェース
├── McpTemplateModels.cs   # テンプレート文書の C# DTO マッピング
├── .github/
│   └── scripts/generate_index.py   # index.json 生成スクリプト
```

## JSON 契約

各 `templates/*.mcp.json` ファイルは `openstaff.mcp-template.v1` スキーマに従います。すべてのプロパティ名は **snake_case** です。

### トップレベルプロパティ

| プロパティ | 型 | 説明 |
|---|---|---|
| `schema` | string | スキーマ識別子。固定値 `"openstaff.mcp-template.v1"` |
| `template_id` | string | 一意のテンプレート識別子。組み込みミラー：`builtin.<capability>.legacy`、公式：`official.<name>.current` |
| `key` | string | 検索・マッチングに使用する短い識別子 |
| `display_name` | string | UI に表示される名前 |
| `description` | string | テンプレート機能の簡潔な説明 |
| `category` | string | カテゴリ：`filesystem`、`dev-tools`、`search`、`database`、`browser`、`memory`、`general` |
| `icon` | string | アイコン識別子（例：`"folder"`、`"github"`） |
| `logo` | string | アイコン URL。形式：`https://cdn.simpleicons.org/<name>?viewbox=auto` |
| `source` | string | テンプレートの提供元識別子（例：`"builtin-current-seed"`、`"official-github-mcp"`） |
| `homepage` | string | 上流プロジェクトまたはドキュメントの URL |
| `match_hints` | object | パッケージ自動検出のヒント（下記参照） |
| `profiles` | array | 実行可能なデプロイメント設定（下記参照） |
| `parameter_schema` | array | ユーザー設定可能な全値。シークレットを含む（下記参照） |

### match_hints

| プロパティ | 型 | 説明 |
|---|---|---|
| `name` | string | マッチングに使用する表示名 |
| `npm_package` | string\|null | npm パッケージ名（自動検出用） |
| `pypi_package` | string\|null | PyPI パッケージ名（自動検出用） |

### profiles

各 profile はデプロイメントモードを定義します。対応する `profile_type`：`package`、`remote`、`container`、`binary`。

| プロパティ | 型 | 対象 Profile | 説明 |
|---|---|---|---|
| `id` | string | 全て | 一意の profile 識別子（例：`"package-npm"`、`"remote"`） |
| `profile_type` | string | 全て | `package`、`remote`、`container`、`binary` のいずれか |
| `transport_type` | string | 全て | 通信トランスポート：`"stdio"` または `"http"` |
| `runner_kind` | string | 全て | ランナーの分類。`profile_type` と一致 |
| `runner` | string | 全て | ランナーコマンド：`"npx"`、`"uvx"`、`"remote"`、`"docker"`、`"binary"` |
| `ecosystem` | string | package | パッケージエコシステム：`"npm"` または `"python"` |
| `package_name` | string | package | パッケージ識別子（例：`"@modelcontextprotocol/server-filesystem"`） |
| `package_version` | string | package | バージョン制約（例：`"latest"`） |
| `command` | string | package, container | 静的な実行コマンド |
| `command_template` | string | binary | 補間付きコマンド：`"${param:binaryPath}"` |
| `args_template` | string[] | 全て | 引数リスト。補間プレースホルダーに対応 |
| `env_template` | object | 全て | 環境変数。補間対応：`{"KEY": "${param:accessToken}"}` |
| `working_directory_template` | string | package, binary | 作業ディレクトリ。補間対応 |
| `url_template` | string | remote | リモートエンドポイント URL。補間対応 |
| `headers_template` | object | remote | HTTP リクエストヘッダー。補間対応 |
| `image` | string | container | コンテナイメージ名（タグなし） |
| `image_tag_template` | string | container | イメージタグ。補間対応 |

### parameter_schema

すべてのユーザー設定可能な値はここに集約します。シークレット用の別ブロックは作成せず、`type: "password"` を使用します。

| プロパティ | 型 | 説明 |
|---|---|---|
| `key` | string | パラメータ識別子。profiles 内で `${param:<key>}` として参照 |
| `label` | string | UI に表示されるラベル |
| `type` | string | `"string"`、`"boolean"`、`"password"` のいずれか |
| `required` | boolean | ユーザー入力が必須かどうか |
| `default_value` | any | デフォルト値。ワークスペース相対パスには `"${project.workspace}"` を使用 |
| `description` | string | パラメータの用途説明 |
| `applies_to_profiles` | string[]\|省略 | 指定時はリストされた profile ID にのみ適用。省略時は全 profile に適用 |

### 補間プレースホルダー

- `${param:<key>}` — `parameter_schema` の値を参照
- `${project.workspace}` — 現在のプロジェクトワークスペースパス

## 規約

- `index.json` は `python .github/scripts/generate_index.py` で `templates/*.mcp.json` から生成します。手動管理はしないでください。
- テンプレートのスキーマフィールドを変更する際は、同じ変更で `McpTemplateModels.cs` も更新してください。
