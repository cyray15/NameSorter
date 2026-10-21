# Name Sorter — Dye & Durham Coding Assessment

A .NET 10 console application that sorts newline-delimited person names by **last name, then by given names**. Beyond the algorithm, this repository shows a maintainable delivery process: readable architecture, focused tests, documented engineering decisions, Conventional Commits, pull request review, and automated CI.

> **Status: starter scaffold.** The CLI shell and its tests are present; the parser, sorting logic and output writer are intentionally not implemented yet. The commands describing sorting below are the **target behavior**, not a claim that the unfinished scaffold works end-to-end.

## Requirements

- Accept an input text-file path as a command-line argument.
- Every name contains **1–3 given names and exactly one last name**.
- Sort ascending by surname, then first given name, then any additional given names.
- Display sorted names on stdout and overwrite `sorted-names-list.txt` in the current working directory.
- Handle at least 1,000 names.

## Stack

- .NET 10 LTS, C#, nullable reference types, built-in analyzers
- xUnit v3 (Microsoft Testing Platform)
- GitHub Actions for compilation, tests, formatting and PR-policy checks
- GitVersion for semantic versions, plus Conventional Commits

## Repository layout

```text
src/NameSorter/             Console entry point, parsing and sorting (planned)
tests/NameSorter.Tests/     Unit and integration tests
samples/                    Example input and expected output
.github/workflows/          CI build, test, formatting and PR validation
.github/pull_request_template.md
scripts/                    Git / PR policy validation
docs/                       Architecture, versioning and bootstrap guide
GitVersion.yml              Semantic version calculation
CONTRIBUTING.md             Branch, commit and review conventions
```

## Build and run

Prerequisite: [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0).

```powershell
dotnet restore NameSorter.slnx
dotnet format NameSorter.slnx --verify-no-changes --no-restore
dotnet build NameSorter.slnx -c Release --no-restore
dotnet test NameSorter.slnx -c Release --no-restore
dotnet run --project src/NameSorter -- --help
```

Target command after the sorting implementation is complete:

```powershell
dotnet run --project src/NameSorter -- ./samples/unsorted-names-list.txt
# Prints sorted names and overwrites ./sorted-names-list.txt
```

### Target example

Input:

```text
Janet Parsons
Adonis Julius Archer
Marin Alvarez
Zoe Alice Archer
```

Output (both console and file):

```text
Marin Alvarez
Adonis Julius Archer
Zoe Alice Archer
Janet Parsons
```

## Behavior decisions to test

| Scenario | Intended behavior |
| --- | --- |
| Ordering | OrdinalIgnoreCase comparisons for platform-independent results, followed by ordinal tie-breaking |
| Repeated whitespace | Normalize to one separator, trim both ends |
| Duplicate names | Preserve them |
| Name length | Reject nonblank lines that do not have 2–4 tokens, with a line-numbered message |
| Blank lines | Ignore them |
| Empty input | Produce empty output; overwrite an empty result file |
| Input/output errors | Explain on stderr and exit nonzero |
| Capacity | Verify on at least 1,000 names |

These decisions are implementation targets, not yet tested behavior. The project favors a pure parser and comparer, with I/O confined to the CLI workflow. Avoid abstractions not required by the small problem domain.

## Collaboration and versioning

Use `main` as the stable submission branch and `develop` for integration. Make changes in `feature/name-parser`, `feature/name-sorting`, `fix/...`, `docs/...` branches and open focused PRs. Follow Conventional Commits: `feat(parser): validate name parts`, `test(sort): cover duplicate surnames`, `ci: add quality gates`.

Read [CONTRIBUTING.md](CONTRIBUTING.md), [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md), and [docs/BRANCHING-AND-RELEASES.md](docs/BRANCHING-AND-RELEASES.md) for details. GitVersion computes versions; it does not perform deployment.

## Assessment handoff

The code is not ready for evaluation until parsing/sorting/output and their tests are implemented, the sample run is verified, and CI is green. Provide the reviewer this repo URL and straightforward run commands. Do not commit credentials or local/generated build files.
