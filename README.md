# Name Sorter

**Dye & Durham coding assessment** · .NET 10 console application · C#

A small, testable CLI that reads a file of names, sorts by **last name first**, then by each **given name**, prints the sorted list, and writes the same result to `sorted-names-list.txt`.

The implementation emphasizes clarity for the next developer: focused responsibilities, deterministic sorting, automated tests, and a pull-request workflow with required checks.

## Quick start

**Prerequisite:** [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0).

From the repository root:

```powershell
git clone https://github.com/cyray15/NameSorter.git
cd NameSorter
dotnet run --project src/NameSorter -- ./samples/unsorted-names-list.txt
```

The application prints:

```text
Marin Alvarez
Adonis Julius Archer
Zoe Alice Archer
Janet Parsons
```

It also **overwrites** `./sorted-names-list.txt` in the **current working directory** with exactly those lines. To inspect the file in PowerShell:

```powershell
Get-Content ./sorted-names-list.txt
```

For CLI help, run `dotnet run --project src/NameSorter -- --help`. No database, external service, credentials, Docker, or deployment environment is required.

## Assessment requirements

| Requirement | Implementation |
| --- | --- |
| Input | One command-line argument: path to a text file, one person per line |
| Name structure | Exactly one last name, preceded by **1–3 given names** |
| Sort order | Ascending last name, followed by given names in sequence |
| Output | Print each sorted name and overwrite `sorted-names-list.txt` |
| Volume | Supports at least 1,000 input names |
| Invalid input | Report a useful error on stderr and exit with a nonzero code |

**Documented behavior and trade-offs:** repeated whitespace is normalized; blank lines are ignored; duplicate names are preserved. A nonblank line with fewer than two or more than four name tokens is rejected with its line number. Comparison uses `OrdinalIgnoreCase` with an ordinal tie-break for deterministic, culture-independent results—not locale-sensitive alphabetical collation. The application sorts the in-memory list using the .NET comparison APIs.

## Build, quality checks and tests

From the repository root:

```powershell
dotnet restore NameSorter.slnx
dotnet format NameSorter.slnx --verify-no-changes --no-restore
dotnet build NameSorter.slnx -c Release --no-restore
dotnet test NameSorter.slnx -c Release --no-restore
```

The test project uses **xUnit v3**. Test cases cover input parsing, whitespace, the 1–3 given-name rule, comparison order, duplicates, invalid input, and a 1,000-name sample. The GitHub Actions pipeline additionally runs the executable against `samples/unsorted-names-list.txt` and verifies that both stdout and the overwritten file match `samples/expected-sorted-names-list.txt`.

**CI checks:** [Build, test & format](https://github.com/cyray15/NameSorter/actions/workflows/ci.yml) and [PR Policy](https://github.com/cyray15/NameSorter/actions/workflows/pr-policy.yml). Both must pass for protected-branch PRs.

## Design and project structure

```text
NameSorter.slnx
src/NameSorter/
  Program.cs              # CLI validation, file/console I/O, exit codes
  CommandLineOptions.cs   # Parse required input path
  FullName.cs             # Validated name data
  FullNameParser.cs       # Parse name tokens without I/O
  FullNameComparer.cs     # Compare last name then each given name
  NameSortService.cs      # Validate lines and return sorted names
tests/NameSorter.Tests/   # Unit tests for the components above
samples/                  # Input and expected-output fixtures
.github/workflows/        # Build/test/format/smoke test and PR policy
scripts/                  # PR title, branch and commit-message checks
docs/                     # Architecture and branching decisions
GitVersion.yml            # GitFlow-based semantic version calculation
CONTRIBUTING.md           # Standards and local development workflow
```

This deliberately remains **one small application project plus a test project**. Parsing and comparison are pure and separately testable; file-system concerns stay at the CLI boundary. Additional layers, abstractions, or DI frameworks are unnecessary for the problem. For rationale and edge cases, see [architecture and decisions](docs/ARCHITECTURE.md).

## Collaboration and release standards

- **Branches:** `main` contains stable, reviewer-facing code; `develop` integrates feature work. Use short-lived branches such as `feature/name-sorting`, `fix/edge-case`, or `docs/readme`.
- **Commits and PR titles:** [Conventional Commits](https://www.conventionalcommits.org/), for example `feat(sort): order names by surname` or `test(sort): handle duplicate names`.
- **Pull requests:** use the PR template and required checks before merging. The repository ruleset protects `main` and `develop` from direct changes and force pushes.
- **Versioning:** `GitVersion.yml` calculates versions from branches, tags and explicit `+semver:` directives. GitVersion does **not** publish releases or deploy software.

See [CONTRIBUTING.md](CONTRIBUTING.md) and [branching, versioning and releases](docs/BRANCHING-AND-RELEASES.md) for detailed conventions.

## Reviewer handoff

1. Clone the repository and install the .NET 10 SDK.
2. Run the quick-start command and compare stdout with `samples/expected-sorted-names-list.txt`.
3. Confirm that `sorted-names-list.txt` is overwritten with the expected result.
4. Run the formatting, Release build and test commands above, then inspect the GitHub Actions history and the focused source/test files.

All required behavior runs locally without external dependencies. The stable submission branch is [`main`](https://github.com/cyray15/NameSorter/tree/main).

