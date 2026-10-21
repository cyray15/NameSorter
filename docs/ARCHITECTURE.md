# Architecture and decisions

## Goal

Sort a newline-delimited list of people by last name, followed by their given names in order. Print the result and overwrite `sorted-names-list.txt` with matching content. Make it easy for another engineer to locate each behavior.

## Logical components

```text
Program (composition / exit codes)
  ├─ CommandLineOptions (CLI argument validation)
  ├─ FullName (validated name value)
  ├─ FullNameParser (pure parsing)
  ├─ FullNameComparer (pure comparison)
  └─ NameSortService (line validation and sorting)
```

These components live in **one console project**; extra projects, interfaces, DI containers or cloud services would not help this sorting exercise. The comparer and parser are pure, so correctness can be checked without file I/O. The CLI owns reading and writing files and reporting errors.

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

## SOLID choices

- Each class owns one concern: name validation, parsing, comparison, sorting, or command-line validation.
- Sorting depends on the comparer supplied at construction, so ordering policy is explicit and independently testable.
- The parser is static because it has no state or dependencies; introducing an interface solely to make it injectable would add ceremony without flexibility.
- File and console operations remain at the application boundary rather than leaking into the name model or sorting service.

The tests are grouped by the class under test. CLI behavior and file output should also be verified with the documented sample run.
