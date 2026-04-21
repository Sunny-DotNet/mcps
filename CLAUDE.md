# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

MCP (Model Context Protocol) Template Export 仓库，包含演示用 MCP 模板文档及对应的 C# 映射模型。用于讨论和稳定 `capability + profile + parameter_schema` 模板合约。

## Build / Test / Lint

无。本仓库仅有模板 JSON 和 C# DTO，没有提交任何构建/测试/lint 项目文件（`.sln`、`.csproj`、`package.json` 等）。

## Commands

- **生成 index.json**: `python .github/scripts/generate_index.py` — 从 `templates/*.mcp.json` 提取摘要字段生成 `index.json`
- **自动生成**: GitHub Actions（`.github/workflows/generate-index.yml`）在 push 到 main 时自动触发

## Architecture

- `templates/*.mcp.json` — 每个文件一个 MCP 模板定义（`schema: openstaff.mcp-template.v1`），包含模板身份、profiles（部署方式）和 parameter_schema（可配置参数）
- `index.json` — 从 templates 自动生成的摘要数组（key, display_name, logo, description, category, homepage）
- `McpTemplateModels.cs` — 模板文档的 C# DTO 映射
- `index.html` — 模板浏览 Web 界面

### Template 结构

每个模板 JSON 包含三层：
1. **身份/元数据**: `template_id`, `key`, `display_name`, `category`, `source`, `homepage`, `match_hints`
2. **Profiles**: 可运行部署方式（package/remote/container/binary），运行器包括 npx、uvx、docker、binary
3. **parameter_schema**: 所有用户可配置值的统一来源，包括密钥（`type: "password"`）

## Conventions

- 所有 JSON 属性名使用 **snake_case**
- `index.json` 必须从模板文件生成，不要手动维护
- 所有可配置值（含密钥）统一放在 `parameter_schema`，不引入单独的 `secrets` 块
- Profile 参数绑定使用插值占位符：`${param:<key>}` 引用参数值，`${project.workspace}` 引用工作区路径
- 跨 profile 参数使用 `applies_to_profiles` 指定适用范围
- 模板 ID 命名：内置镜像用 `builtin.<capability>.legacy`，官方模板用 `official.github.current`
- 文件命名：`templates/<capability>.mcp.json`
- 模板 schema 字段变更时，同步更新 `McpTemplateModels.cs`
- 保留已有的 Windows 风格路径默认值（如 `C:\\...` 和 `${project.workspace}\\...`）
- `logo` 使用 `https://cdn.simpleicons.org/<name>?viewbox=auto` 格式
