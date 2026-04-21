# Export de modèles MCP (Démo)

[ English ](README.md) | [ 中文 ](README.zh-CN.md) | [ 日本語 ](README.ja-JP.md) | [ **Français** ](README.fr-FR.md)

---

Ce dépôt contient des **exemples de modèles MCP** ainsi que les modèles C# correspondants.  
L’objectif est de stabiliser la structure `capability + profile + parameter_schema`.

## Catalogue des modèles

| Clé du modèle | Fichier | Catégorie | Exécuteur principal |
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

## Structure du dépôt

- `index.json` : registre des modèles et liste canonique des fichiers
- `templates/*.mcp.json` : définitions de modèles (propriétés en snake_case)
- `McpTemplateModels.cs` : mapping C# (DTO) des documents JSON
- `.github/copilot-instructions.md` : instructions Copilot spécifiques au dépôt

## Contrat JSON (snake_case)

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

## Conventions clés

- Garder `index.json -> templates[]` synchronisé avec les fichiers réels.
- Utiliser `snake_case` pour toutes les propriétés JSON.
- Placer tous les paramètres configurables (y compris les secrets) dans `parameter_schema`.
- Utiliser `${param:<key>}` pour relier les champs d’exécution des profiles aux paramètres.
- Utiliser pour `logo` le même style URL que dans `templates/github-official.mcp.json`.

## Build / Test / Lint

Ce dépôt ne contient pas de configuration de projet exécutable pour build/test/lint, donc aucune commande native n’est disponible.

## GitHub Pages

Après activation de Pages, consultez :

- `docs/index.html` (vue dépôt)
- ou l’URL GitHub Pages publiée pour ce dépôt
