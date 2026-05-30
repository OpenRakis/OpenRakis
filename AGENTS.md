# AGENTS.md

Scope: This file is focused on DuneTools app development in src/DuneTools.

If a task is outside src/DuneTools, do not assume this guidance applies.

For release workflow changes under tools/cd, follow [tools-cd-release-flow.instructions.md](.github/instructions/tools-cd-release-flow.instructions.md).

## Start Here

- Read [src/DuneTools/Directory.Packages.props](src/DuneTools/Directory.Packages.props) before changing dependencies.
- Read [src/DuneTools/Directory.Build.props](src/DuneTools/Directory.Build.props) for baseline C# settings.

## DuneTools Boundaries

- Modern cross-platform app code lives in [src/DuneTools](src/DuneTools):
  - [src/DuneTools/DuneTools](src/DuneTools/DuneTools): Avalonia desktop app (net10.0).
  - [src/DuneTools/DuneTools.Browser](src/DuneTools/DuneTools.Browser): Avalonia browser host (net10.0-browser) that references the desktop project.

Default behavior: stay inside [src/DuneTools](src/DuneTools).

Only touch [tools/cd](tools/cd) when explicitly requested.

## Build, Test, Run

Use the smallest command that satisfies the task.

- Build (modern app): dotnet build src/OpenRakis.slnx
- Run desktop app: from src/DuneTools/DuneTools, run dotnet run

If using VS Code tasks, use the existing workspace tasks named build or publish.

## Code Conventions To Keep

- C# defaults come from [src/DuneTools/Directory.Build.props](src/DuneTools/Directory.Build.props): nullable enabled, latest language version.
- Keep Avalonia package versions synchronized via [src/DuneTools/Directory.Packages.props](src/DuneTools/Directory.Packages.props).
- DuneTools UI uses Avalonia with compiled bindings enabled in [src/DuneTools/DuneTools/DuneTools.csproj](src/DuneTools/DuneTools/DuneTools.csproj).
- Preserve the View/ViewModel split already used in [src/DuneTools/DuneTools/Views](src/DuneTools/DuneTools/Views) and [src/DuneTools/DuneTools/ViewModels](src/DuneTools/DuneTools/ViewModels).
- Enforce MVVM in DuneTools: keep UI behavior in Views/Behaviors and state plus presentation logic in ViewModels.
- Prefer CommunityToolkit.Mvvm for observable properties, commands, and related MVVM plumbing instead of hand-rolled notification code.
- Do not introduce fallback code paths in DuneTools. Fail explicitly and surface the issue instead of silently degrading to an alternate UI or behavior.
- Prefer amending existing code over adding parallel code paths or adjacent wrapper layers.
- Prefer short methods with clear names and short classes with a single focused responsibility.
- Prefer small focused sub-viewmodels over growing a single large viewmodel when a UI area has its own state or presentation logic.
- Only inject classes when they are genuinely shared collaboration points; otherwise construct them directly in the owning type's constructor.
- Keep objects responsible for their own behavior. Avoid anemic or helpless classes that only shuttle state to other objects.
- In C#, do not use reflection, null-forgiving operators, hacks, or shortpaths. The existing [src/DuneTools/DuneTools/ViewLocator.cs](src/DuneTools/DuneTools/ViewLocator.cs) is the explicit exception.
- ReflectionBinding may be used when appropriate, but prefer compiled bindings.

## Common Pitfalls

- Browser/WASM build depends on native relink settings in [src/DuneTools/DuneTools.Browser/DuneTools.Browser.csproj](src/DuneTools/DuneTools.Browser/DuneTools.Browser.csproj) and may require wasm-tools workload.
- Avoid unsynchronized Avalonia versions across Avalonia, Avalonia.Desktop, Avalonia.Browser, themes, and fonts.
- Resource load issues are often caused by wrong avares paths or missing AvaloniaResource entries (see [src/DuneTools/DuneTools/DuneTools.csproj](src/DuneTools/DuneTools/DuneTools.csproj)).

## Agent Behavior Expectations

- Keep changes scoped: do not refactor unrelated directories while implementing DuneTools tasks.
- Prefer targeted edits over broad rewrites.
- Validate with a relevant build before concluding, and include tests when touched code is test-covered.
- For browser and WASM failures, use the skill in [SKILL.md](.github/skills/dune-browser-wasm-troubleshooting/SKILL.md).
