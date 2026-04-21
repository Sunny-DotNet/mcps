# MCP 模板导出（Demo）

[ English ](README.md) | [ **中文** ](README.zh-CN.md) | [ 日本語 ](README.ja-JP.md) | [ Français ](README.fr-FR.md)

---

本仓库用于存放 **MCP 模板定义示例** 以及对应的 C# 映射模型。
主要目标是沉淀并固定 `capability + profile + parameter_schema` 这套模板结构。

## 仓库结构

```
├── templates/             # MCP 模板定义文件 (*.mcp.json)
├── index.json             # 从 templates 自动生成的摘要数组
├── index.html             # 模板浏览 Web 界面
├── McpTemplateModels.cs   # 模板文档的 C# DTO 映射
├── .github/
│   └── scripts/generate_index.py   # 生成 index.json 的脚本
```

## JSON 合约

每个 `templates/*.mcp.json` 文件遵循 `openstaff.mcp-template.v1` schema，所有属性名使用 **snake_case**。

### 顶层属性

| 属性 | 类型 | 说明 |
|---|---|---|
| `schema` | string | Schema 标识符，固定为 `"openstaff.mcp-template.v1"` |
| `template_id` | string | 唯一模板标识。内置镜像：`builtin.<capability>.legacy`；官方模板：`official.<name>.current` |
| `key` | string | 短标识符，用于查找和匹配 |
| `display_name` | string | 在 UI 中显示的名称 |
| `description` | string | 模板功能的简要描述 |
| `category` | string | 分类：`filesystem`、`dev-tools`、`search`、`database`、`browser`、`memory`、`general` |
| `icon` | string | 图标标识（如 `"folder"`、`"github"`） |
| `logo` | string | 图标 URL，格式：`https://cdn.simpleicons.org/<name>?viewbox=auto` |
| `source` | string | 模板来源标识（如 `"builtin-current-seed"`、`"official-github-mcp"`） |
| `homepage` | string | 上游项目或文档的 URL |
| `match_hints` | object | 包发现提示（见下文） |
| `profiles` | array | 可运行的部署配置（见下文） |
| `parameter_schema` | array | 所有用户可配置值，包括密钥（见下文） |

### match_hints

| 属性 | 类型 | 说明 |
|---|---|---|
| `name` | string | 用于匹配的显示名称 |
| `npm_package` | string\|null | npm 包名，用于自动发现 |
| `pypi_package` | string\|null | PyPI 包名，用于自动发现 |

### profiles

每个 profile 定义一种部署方式。支持的 `profile_type`：`package`、`remote`、`container`、`binary`。

| 属性 | 类型 | 适用 Profile | 说明 |
|---|---|---|---|
| `id` | string | 全部 | 唯一 profile 标识（如 `"package-npm"`、`"remote"`） |
| `profile_type` | string | 全部 | 取值：`package`、`remote`、`container`、`binary` |
| `transport_type` | string | 全部 | 通信传输方式：`"stdio"` 或 `"http"` |
| `runner_kind` | string | 全部 | 运行器类别，与 `profile_type` 一致 |
| `runner` | string | 全部 | 运行器命令：`"npx"`、`"uvx"`、`"remote"`、`"docker"`、`"binary"` |
| `ecosystem` | string | package | 包生态系统：`"npm"` 或 `"python"` |
| `package_name` | string | package | 包标识符（如 `"@modelcontextprotocol/server-filesystem"`） |
| `package_version` | string | package | 版本约束（如 `"latest"`） |
| `command` | string | package, container | 静态执行命令 |
| `command_template` | string | binary | 带插值的命令：`"${param:binaryPath}"` |
| `args_template` | string[] | 全部 | 参数列表，支持插值占位符 |
| `env_template` | object | 全部 | 环境变量，支持插值：`{"KEY": "${param:accessToken}"}` |
| `working_directory_template` | string | package, binary | 工作目录，支持插值 |
| `url_template` | string | remote | 远程端点 URL，支持插值 |
| `headers_template` | object | remote | HTTP 请求头，支持插值 |
| `image` | string | container | 容器镜像名称（不含标签） |
| `image_tag_template` | string | container | 镜像标签，支持插值 |

### parameter_schema

所有用户可配置值统一放在此处。不使用单独的 `secrets` 块——密钥类型使用 `type: "password"`。

| 属性 | 类型 | 说明 |
|---|---|---|
| `key` | string | 参数标识符，在 profiles 中通过 `${param:<key>}` 引用 |
| `label` | string | 在 UI 中显示的标签 |
| `type` | string | 取值：`"string"`、`"boolean"`、`"password"` |
| `required` | boolean | 是否为必填项 |
| `default_value` | any | 默认值。工作区相对路径使用 `"${project.workspace}"` |
| `description` | string | 参数用途说明 |
| `applies_to_profiles` | string[]\|省略 | 设置时仅对列出的 profile ID 生效。省略表示对所有 profile 生效 |

### 插值占位符

- `${param:<key>}` — 引用 `parameter_schema` 中的值
- `${project.workspace}` — 当前项目工作区路径

## 约定

- `index.json` 通过 `python .github/scripts/generate_index.py` 从 `templates/*.mcp.json` 生成，不要手动维护。
- 模板 schema 字段变更时，需同步更新 `McpTemplateModels.cs`。
