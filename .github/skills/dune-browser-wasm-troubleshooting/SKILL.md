---
name: dune-browser-wasm-troubleshooting
description: "Troubleshoot DuneTools browser and WASM failures. Use when seeing SkiaSharp initialization errors, missing WASM assets, 404s for native libraries, publish/runtime startup failures, or incorrect native relink behavior in src/DuneTools/DuneTools.Browser."
argument-hint: "Describe the browser error, failing URL, or publish step"
user-invocable: true
---

# Dune Browser WASM Troubleshooting

## When To Use

- Browser app fails during startup in [src/DuneTools/DuneTools.Browser](src/DuneTools/DuneTools.Browser).
- Runtime shows 404 for native libraries or framework artifacts.
- SkiaSharp or HarfBuzz initialization fails at runtime.
- A change to browser packaging, runtime config, or publish output breaks loading.

## Procedure

1. Confirm failure details first.
Collect the exact error text, failing URL, and whether the issue appears in local run, publish output, or deployed hosting.

2. Verify browser project configuration.
Check [src/DuneTools/DuneTools.Browser/DuneTools.Browser.csproj](src/DuneTools/DuneTools.Browser/DuneTools.Browser.csproj) and confirm:
- Target framework is net10.0-browser.
- WasmBuildNative is enabled.
- Project reference to [src/DuneTools/DuneTools/DuneTools.csproj](src/DuneTools/DuneTools/DuneTools.csproj) remains intact.

3. Verify dependency consistency.
Confirm Avalonia package versions remain synchronized in [src/DuneTools/Directory.Packages.props](src/DuneTools/Directory.Packages.props).

4. Verify local tooling.
If native WASM build behavior looks wrong, ensure wasm-tools workload is installed and current.

5. Reproduce with minimal commands.
Use a focused build or publish command for [src/DuneTools/DuneTools.Browser/DuneTools.Browser.csproj](src/DuneTools/DuneTools.Browser/DuneTools.Browser.csproj), then inspect output artifacts under publish directories and framework payload files.

6. Fix the smallest root cause.
Prioritize targeted changes in browser project config or package/version alignment. Avoid broad refactors.

7. Validate end-to-end.
Confirm the app starts, no missing asset requests remain, and browser console no longer reports initialization/type failures.

## Output Checklist

- Root cause summary.
- Exact files changed.
- Validation commands run.
- Remaining risk, if any.
