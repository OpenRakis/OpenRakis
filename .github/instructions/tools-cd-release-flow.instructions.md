---
name: tools-cd-release-flow
description: "Use when preparing releases, changing packaging, or editing CI and build automation for tools/cd. Covers Cake targets, release assets, and safe validation steps."
applyTo: tools/cd/**
---

# tools/cd Release Flow

Apply this guidance for release-related work in [tools/cd](tools/cd).

## Source Of Truth

- Build and release pipeline: [tools/cd/build.cake](tools/cd/build.cake)
- Tool overview: [tools/cd/README.md](tools/cd/README.md)

## Required Workflow

1. Prefer Cake targets over ad-hoc commands for release paths.
Use the target chain already defined in [tools/cd/build.cake](tools/cd/build.cake): Clean, SemVer, Build, Test, Publish-All, Zip-All, GitHub-Release.

2. Keep release behavior deterministic.
Do not introduce per-machine assumptions in paths, runtime identifiers, or token handling.

3. Validate before release changes are finalized.
At minimum, run build and test for the affected solution and verify expected publish outputs.

4. Preserve artifact and naming expectations.
Do not change zip names, target runtimes, or release note generation behavior unless explicitly requested.

## Safety Checks

- If changing runtime targets, verify all existing platform outputs still work.
- If changing versioning behavior, confirm SemVer and release naming stay coherent.
- If changing release notes generation, ensure empty-note fallback still produces valid output.
