# Branching, versioning and releases

## Lightweight GitFlow

```text
main   (stable and reviewer-facing)
 └── develop (integration)
      ├── feature/name-parser
      ├── feature/name-sorting
      ├── feature/cli-output
      └── test/edge-cases
```

- `main`: stable baseline; protected and merged by PR.
- `develop`: integrates reviewed feature branches.
- `feature/*`, `fix/*`, `test/*`, `docs/*`, `ci/*`, `chore/*`: focused changes, normally based on `develop`.
- `release/x.y.z` and `hotfix/*`: optional in this assessment; demonstrate understanding rather than inventing releases.
- `dev/*`: custom experimental branch supported in GitVersion, not required.

Each PR should pass the build/test/format and commit-policy checks. Prefer squash merge with a Conventional Commit PR title; validate the individual commits too.

## GitVersion

`GitVersion.yml` applies `GitFlow/v1` defaults and explicit overrides.

| Branch | Pre-release label | Default increment |
| --- | --- | --- |
| main | stable | Patch |
| develop | alpha | Minor |
| feature/* | branch name | Inherit |
| dev/* | dev | Patch |
| release/* | beta | None |
| hotfix/* | hotfix | Patch |

Version examples are **illustrative** only; actual numbers come from Git history and tags. Tags may use `1.0.0` or `v1.0.0`. GitVersion **calculates** versions; it does not build, tag, publish, or deploy your application.

```powershell
dotnet tool install --global GitVersion.Tool --version 6.8.2
dotnet-gitversion /showconfig
dotnet-gitversion /output json
```

Conventional Commits validation checks the subject format. GitVersion's explicit `+semver:` directives control commit-triggered bumps; merely writing `feat:` doesn't request a minor bump.

## Enforce repository rules in GitHub

After workflows have run, go to **Settings → Rules → Rulesets** (or branch protection):

1. Protect `main` and `develop`, require PRs.
2. Require status checks: `Build, test & format` and `Commit and branch conventions`.
3. Disallow force pushes and deletion.
4. Require another person's approval only if another reviewer is available.
5. Prefer resolved conversations and squash merges.

Committed YAML and PR templates do **not** enable branch protection by themselves. GitHub plan capabilities vary for private repositories.

## Tagging and submission

Don't create a release tag while the sorting implementation is incomplete. When tests, README and behavior are verified, merge `develop` into `main` and optionally tag that stable commit.
