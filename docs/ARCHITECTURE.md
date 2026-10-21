# Architecture and decisions

## Goal

Sort a newline-delimited list of people by last name, followed by their given names in order. Print the result and overwrite `sorted-names-list.txt` with matching content. Make it easy for another engineer to locate each behavior.

## Proposed logical components

```text
Program (composition / exit codes)
  ├─ CLI argument validation
  ├─ File reader
  ├─ Name parser / value type
  ├─ Pure comparer / sorter
  └─ Output formatter / writer
```

These components should start in **one console project**; adding extra projects, interfaces, DI containers or cloud services would not help a sorting exercise. The comparer should be pure so correctness can be checked without file I/O; use integration tests for the application boundary.

## Behavior contracts and reasoning

| Concern | Decision / rationale |
| --- | --- |
| Name input | Last token is surname; preceding 1–3 tokens are given names |
| Tokenization | Trim lines, collapse repeated whitespace; ignore blank lines |
| Invalid lines | Fail with a line-numbered error, not a partial output |
| Comparison | OrdinalIgnoreCase for predictable sorting regardless of culture, ordinal tie-break for determinism |
| Duplicates | Preserve every input record |
| Result | Console output equals written file lines; overwrite on every success |
| File output | Put `sorted-names-list.txt` in the process working directory |
| Errors | Useful stderr diagnostics and nonzero exit code |
| Performance | 1,000 names fit in memory; normal `OrderBy` / comparer is sufficient |

Ordinal ordering is predictable, **not locale-aware alphabetical collation**. If requirements change to linguistic ordering, make that an explicit decision supported by tests.

## Status

The first scaffold intentionally implements CLI validation only. Parsing, sorting, output writing, and behavior-focused tests are remaining work and should be delivered in dedicated PRs. Remove this status section or mark it complete only after verification.
