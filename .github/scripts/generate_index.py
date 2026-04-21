from __future__ import annotations

import json
from pathlib import Path

SUMMARY_FIELDS = ("key", "display_name", "logo", "description", "category", "homepage")


def main() -> None:
    repo_root = Path(__file__).resolve().parents[2]
    templates_dir = repo_root / "templates"
    index_file = repo_root / "index.json"

    items: list[dict[str, str]] = []
    for template_path in sorted(templates_dir.glob("*.mcp.json")):
        data = json.loads(template_path.read_text(encoding="utf-8"))
        item = {field: data.get(field, "") for field in SUMMARY_FIELDS}
        items.append(item)

    index_file.write_text(
        json.dumps(items, ensure_ascii=False, indent=2) + "\n",
        encoding="utf-8",
    )


if __name__ == "__main__":
    main()
