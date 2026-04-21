# MCP Template Export

这一目录存放的是 **MCP 模板定义样例**，用于讨论和固定未来的 `capability + profile + parameter_schema` 结构。

## 文件说明

- `index.json`
  - 模板清单入口
- `*.mcp.json`
  - 单个 MCP 模板定义
- `McpTemplateModels.cs`
  - 对应这些 JSON 的 C# 映射实体类

## 当前模板范围

这批文件包含两类来源：

1. 当前仓库 `McpSeedService.cs` 里的 builtin 模板镜像
2. 额外补充的 `github-official.mcp.json`
   - 表达 GitHub 当前官方 MCP 的 remote / docker / binary 方案

## 顶层结构

每个 `.mcp.json` 基本结构如下：

```json
{
  "schema": "openstaff.mcp-template.v1",
  "template_id": "builtin.filesystem.legacy",
  "key": "filesystem",
  "display_name": "Filesystem",
  "description": "...",
  "category": "filesystem",
  "icon": "folder",
  "logo": "https://cdn.simpleicons.org/filesystem?viewbox=auto",
  "source": "builtin-current-seed",
  "homepage": "...",
  "match_hints": {
    "name": "Filesystem",
    "npm_package": "@modelcontextprotocol/server-filesystem",
    "pypi_package": null
  },
  "profiles": [],
  "parameter_schema": []
}
```

## 字段约定

### `icon`

偏向 UI 中的小图标语义，适合列表、卡片、按钮等轻量展示。

### `logo`

偏向品牌或资源标识，适合未来接入：

- 品牌 Logo 资源
- 更完整的服务视觉标识
- marketplace / 模板仓库展示

当前这批模板里的 `logo` 先采用字符串标识符，后续可以再映射到真正的图片资源或 Logo 组件。

### `profiles`

用于表达不同运行档案，例如：

- `remote`
- `package`
- `container`
- `binary`

也就是说，同一个 capability 可以有多个可切换的部署方式。

### `parameter_schema`

所有用户可配置项统一放在这里，包括：

- 普通字符串
- 布尔值
- 路径
- 密钥类字段

例如 `GITHUB_PERSONAL_ACCESS_TOKEN` 不再单独拆出 `secrets` 块，而是直接在 `parameter_schema` 中定义为：

```json
{
  "key": "accessToken",
  "type": "password"
}
```

## 设计目的

这套结构主要服务三个方向：

1. **模板仓库**
   - 例如 `github.mcp.json`
2. **本地实例化**
   - 用户基于模板选择 profile，再填写参数
3. **角色同步**
   - 角色文件引用 template identity
   - 必要时附带 portable profile / 参数骨架

## 注意

- 这些文件当前是 **设计导向的模板样本**
- 并不是仓库现有 importer 可以直接完整消费的最终合同
- 如果后续要正式接入应用层，建议把 `McpTemplateModels.cs` 搬到正式项目目录，并补充序列化/反序列化与校验逻辑
