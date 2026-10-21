# Repository bootstrap and first commits

This repository begins on `main` with a small README. The `chore/repository-foundation` PR deliberately introduces the engineering scaffold in three meaningful commits:

1. `chore: bootstrap name sorter solution` — solution, CLI shell, initial tests, samples, README and basic tooling.
2. `docs: establish engineering and versioning standards` — CONTRIBUTING, architecture, GitVersion and PR template.
3. `ci: add build test format and PR policy checks` — GitHub Actions and Conventional Commits validation.

## After approving the foundation PR

1. Merge the PR into `main` (preserve commit history if you want those three commits visible; squash into one if your repository policy favors linear, single-topic PRs).
2. Create `develop` from the resulting `main`.
3. Enable required checks and protection for both branches after checks have appeared in GitHub.
4. Write the name parser on `feature/name-parser`, then open a PR to `develop`.
5. Follow with `feature/name-sorting`, `feature/cli-output`, then `test/edge-cases`.
6. Finally merge `develop` to `main` after all acceptance criteria are met.

## Local commands

```powershell
git clone https://github.com/cyray15/NameSorter.git
cd NameSorter
git switch main
git pull --ff-only
git switch -c develop
git push -u origin develop

# Begin a focused feature:
git switch -c feature/name-parser
dotnet restore NameSorter.slnx
dotnet build NameSorter.slnx -c Release
dotnet test NameSorter.slnx -c Release
git add src tests
git commit -m "feat(parser): validate and normalize name entries"
git push -u origin feature/name-parser
```

The `git switch -c develop` command assumes `develop` has not already been created; create it only **after** the foundation is merged.

## Before submitting the assessment

- [ ] Remove the 'scaffold' note and unimplemented CLI behavior.
- [ ] Show real sorting output and expected sample file contents.
- [ ] Cover invalid names, duplicates, empty files and 1,000 names.
- [ ] Run formatter, build and tests successfully.
- [ ] Ensure the submission branch has green required status checks.
- [ ] Give the assessment reviewer access and a working run command.
