# Condition restoration regression checks

This executable links the production `TargetCondition`, `ConditionSet`,
`ConditionBoolean`, and `ActionSequencerUpdater` source. Game services, delays,
and persistence are substituted so the checks can run without FFXIV.

Build with a .NET SDK whose Roslyn version supports the repository's source
generators (the local .NET 10 SDK works); run the test DLL with a .NET 9 runtime:

```powershell
dotnet build tests/Conditions/Conditions.csproj -c Release
dotnet tests/Conditions/bin/Release/net9.0-windows/Conditions.dll
```

Coverage: distance strictly below 15, combat status, OR/AND, missing targets,
empty conditions, forced action selection, disable lists, clearing stale frame
state, replaced rotations/actions, unknown action IDs, boolean override priority,
and resolving saved action targets without opening the editor.

These checks do not verify ImGui rendering, real target acquisition, skill
execution, condition delay timing, or file persistence in a running game.

## In-game verification

1. Load the 7.3.5.4 build and select a job's movement ability in the skill settings.
2. Leave the ability's ordinary enable checkbox checked. Open 強制啟用條件.
3. Add two 目標條件 entries with target 目前目標: 距離 `< 15` and 戰鬥中.
4. Select `||` for the enclosing group. Verify the live condition indicators at
   distances below, equal to, and above 15; also test a distant target in combat.
5. Repeat with `&&` to require both conditions. A missing target should not match.
6. Add a matching 強制禁用條件. With no forced match, the skill must be excluded;
   with both matching, the forced action retains the original higher priority.
7. Close the window, reload the plugin, and verify the saved conditions still
   apply without opening the editor. Switch jobs and check no stale action remains.
8. For general checkbox overrides, enable 基本 → 其他 → 使用額外條件, then test
   the checkbox's 強制啟用／禁用 entries. Action conditions do not require this toggle.

Forced conditions still use the existing action usability checks (including
cooldown, level, and valid targets). They do not guarantee an otherwise unusable
skill will be executed. Distance is the existing game's yalm distance metric.
