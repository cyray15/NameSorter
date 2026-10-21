# Contributing to Name Sorter

The assessment values empathy for the next engineer, readable code, useful tests and pragmatic SOLID principles. Keep changes understandable and reviewable.

## Development flow

1. Read README and docs/ARCHITECTURE.md before adding implementation.
2. Branch from `develop`: `feature/name-parser`, `feature/name-sorting`, `feature/cli-output`, `fix/edge-case`, `docs/readme`, etc.
3. Implement one coherent behavior at a time with tests.
4. Run the local quality gate below.
5. Open a pull request against `develop`, complete the template, and address CI failures. Only after verification should `develop` be merged into `main`.

## Commit messages and PR titles

Use **Conventional Commits**:

```text
<type>(optional-scope)!: subject

feat(parser): validate names with three given names
fix(sort): compare remaining given names in order
test(cli): cover overwriting the output file
refactor(cli): isolate filesystem handling
docs: document ordering rules
ci: add commit and format checks
```

Allowed types: `build`, `chore`, `ci`, `docs`, `feat`, `fix`, `perf`, `refactor`, `revert`, `style`, `test`. Subject maximum: 120 characters. Avoid vague subjects such as `updated files`.

Version increments are **not inferred from Conventional Commit type** in the current GitVersion configuration. If explicit override is required, use `+semver: major`, `+semver: minor`, `+semver: patch` or `+semver: none` in the commit message.

## Pull request expectations

- Explain *what* and *why*, with the relevant acceptance criteria.
- For behavior changes, include automated tests and any manual verification.
- Update README or architecture notes when contracts or edge-case decisions change.
- Keep unrelated formatting, generated code, secrets and machine-specific files out of PRs.
- Prefer one focused change with a descriptive PR title.
- If the PR is documentation-only, explain why application tests are not applicable.
- Squash merging is recommended; the **PR title** becomes the final commit message. The PR policy also validates individual commits in the PR.

## Local verification

```powershell
dotnet restore NameSorter.slnx
dotnet format NameSorter.slnx --verify-no-changes --no-restore
dotnet build NameSorter.slnx -c Release --no-restore
dotnet test NameSorter.slnx -c Release --no-restore
```

## What to test

- Sorting by surname, then each given name; 1–3 given names
- Duplicate full names, mixed case, whitespace, ties
- Blank and empty files, invalid line structure, non-existent input
- Overwritten `sorted-names-list.txt` matching console output
- 1,000+ names, exit codes and readable error messages

Do not mark the sorting behavior complete until these checks and the example run pass.
