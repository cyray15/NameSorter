#!/usr/bin/env bash
set -euo pipefail

conventional='^(build|chore|ci|docs|feat|fix|perf|refactor|revert|style|test)(\([a-z0-9._-]+\))?(!)?: .+'
branch='^(feature|fix|chore|docs|refactor|test|ci|release|hotfix|dev)/[a-z0-9][a-z0-9._-]*$'
failed=0

check_subject() {
  local kind="$1" subject="$2"
  if [[ "$subject" =~ $conventional ]] && [[ ${#subject} -le 120 ]]; then
    echo "PASS $kind: $subject"
  else
    echo "FAIL $kind: $subject"
    echo "Expected: feat(scope): short description (max 120 chars)."
    failed=1
  fi
}

if [[ -z "${PR_TITLE:-}" || -z "${PR_BRANCH:-}" || -z "${BASE_SHA:-}" || -z "${HEAD_SHA:-}" ]]; then
  echo "Missing required PR_TITLE, PR_BRANCH, BASE_SHA or HEAD_SHA environment variables."
  exit 2
fi

check_subject "PR title" "$PR_TITLE"

if [[ "$PR_BRANCH" == "develop" ]] || [[ "$PR_BRANCH" =~ $branch ]]; then
  echo "PASS source branch: $PR_BRANCH"
else
  echo "FAIL source branch: $PR_BRANCH"
  failed=1
fi

base=$(git merge-base "$BASE_SHA" "$HEAD_SHA")
count=0
while IFS= read -r sha; do
  [[ -n "$sha" ]] || continue
  subject=$(git log -1 --format=%s "$sha")
  check_subject "commit $sha" "$subject"
  count=$((count + 1))
done < <(git rev-list --reverse --no-merges "$base..$HEAD_SHA")

if [[ $count -eq 0 ]]; then
  echo "FAIL: no non-merge commits in this PR."
  failed=1
fi
exit "$failed"
