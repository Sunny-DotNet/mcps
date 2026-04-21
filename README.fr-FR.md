# Export de modèles MCP (Démo)

[ English ](README.md) | [ 中文 ](README.zh-CN.md) | [ 日本語 ](README.ja-JP.md) | [ **Français** ](README.fr-FR.md)

---

Ce dépôt contient des **exemples de modèles MCP** ainsi que les modèles C# correspondants.
L'objectif est de stabiliser la structure `capability + profile + parameter_schema`.

## Structure du dépôt

```
├── templates/             # Fichiers de définition de modèles MCP (*.mcp.json)
├── index.json             # Tableau de synthèse généré automatiquement depuis templates
├── index.html             # Interface Web de navigation des modèles
├── McpTemplateModels.cs   # Mapping C# (DTO) des documents de modèles
├── .github/
│   └── scripts/generate_index.py   # Script de génération de index.json
```

## Contrat JSON

Chaque fichier `templates/*.mcp.json` suit le schéma `openstaff.mcp-template.v1`. Tous les noms de propriétés utilisent le **snake_case**.

### Propriétés de premier niveau

| Propriété | Type | Description |
|---|---|---|
| `schema` | string | Identifiant du schéma, toujours `"openstaff.mcp-template.v1"` |
| `template_id` | string | Identifiant unique du modèle. Miroirs intégrés : `builtin.<capability>.legacy` ; officiel : `official.<name>.current` |
| `key` | string | Identifiant court utilisé pour la recherche et le matching |
| `display_name` | string | Nom affiché dans l'interface |
| `description` | string | Brève description des fonctionnalités du modèle |
| `category` | string | Catégorie : `filesystem`, `dev-tools`, `search`, `database`, `browser`, `memory`, `general` |
| `icon` | string | Identifiant d'icône (ex. `"folder"`, `"github"`) |
| `logo` | string | URL de l'icône. Format : `https://cdn.simpleicons.org/<name>?viewbox=auto` |
| `source` | string | Identifiant de la source du modèle (ex. `"builtin-current-seed"`, `"official-github-mcp"`) |
| `homepage` | string | URL du projet source ou de la documentation |
| `match_hints` | object | Indices de découverte de paquets (voir ci-dessous) |
| `profiles` | array | Configurations de déploiement exécutables (voir ci-dessous) |
| `parameter_schema` | array | Toutes les valeurs configurables par l'utilisateur, y compris les secrets (voir ci-dessous) |

### match_hints

| Propriété | Type | Description |
|---|---|---|
| `name` | string | Nom affiché pour le matching |
| `npm_package` | string\|null | Nom du paquet npm pour la découverte automatique |
| `pypi_package` | string\|null | Nom du paquet PyPI pour la découverte automatique |

### profiles

Chaque profile définit un mode de déploiement. `profile_type` supportés : `package`, `remote`, `container`, `binary`.

| Propriété | Type | Profiles concernés | Description |
|---|---|---|---|
| `id` | string | Tous | Identifiant unique du profile (ex. `"package-npm"`, `"remote"`) |
| `profile_type` | string | Tous | Parmi : `package`, `remote`, `container`, `binary` |
| `transport_type` | string | Tous | Transport de communication : `"stdio"` ou `"http"` |
| `runner_kind` | string | Tous | Catégorie du lanceur, correspond à `profile_type` |
| `runner` | string | Tous | Commande du lanceur : `"npx"`, `"uvx"`, `"remote"`, `"docker"`, `"binary"` |
| `ecosystem` | string | package | Écosystème de paquets : `"npm"` ou `"python"` |
| `package_name` | string | package | Identifiant du paquet (ex. `"@modelcontextprotocol/server-filesystem"`) |
| `package_version` | string | package | Contrainte de version (ex. `"latest"`) |
| `command` | string | package, container | Commande statique à exécuter |
| `command_template` | string | binary | Commande avec interpolation : `"${param:binaryPath}"` |
| `args_template` | string[] | Tous | Liste d'arguments avec placeholders d'interpolation |
| `env_template` | object | Tous | Variables d'environnement avec interpolation : `{"KEY": "${param:accessToken}"}` |
| `working_directory_template` | string | package, binary | Répertoire de travail avec interpolation |
| `url_template` | string | remote | URL du point de terminaison distant avec interpolation |
| `headers_template` | object | remote | En-têtes HTTP avec interpolation |
| `image` | string | container | Nom de l'image conteneur (sans tag) |
| `image_tag_template` | string | container | Tag de l'image avec interpolation |

### parameter_schema

Toutes les valeurs configurables par l'utilisateur sont ici. Pas de bloc `secrets` séparé — utiliser `type: "password"` pour les valeurs sensibles.

| Propriété | Type | Description |
|---|---|---|
| `key` | string | Identifiant du paramètre, référencé par `${param:<key>}` dans les profiles |
| `label` | string | Libellé affiché dans l'interface |
| `type` | string | Parmi : `"string"`, `"boolean"`, `"password"` |
| `required` | boolean | Si l'utilisateur doit fournir une valeur |
| `default_value` | any | Valeur par défaut. Chemin relatif au workspace : `"${project.workspace}"` |
| `description` | string | Description du rôle du paramètre |
| `applies_to_profiles` | string[]\|omis | Si défini, s'applique uniquement aux IDs de profile listés. Omis = s'applique à tous les profiles |

### Placeholders d'interpolation

- `${param:<key>}` — Référence une valeur de `parameter_schema`
- `${project.workspace}` — Chemin du workspace du projet courant

## Conventions

- `index.json` est généré via `python .github/scripts/generate_index.py` depuis `templates/*.mcp.json`, pas maintenu manuellement.
- Si les champs du schéma de modèle changent, mettre à jour `McpTemplateModels.cs` dans la même modification.
