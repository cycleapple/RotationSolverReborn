using ECommons.DalamudServices;
using RotationSolver.Updaters;

int checks = 0;
void Check(bool result, string name)
{
    if (!result) throw new Exception(name);
    checks++;
    Console.WriteLine($"PASS {name}");
}

var target = new IBattleChara { Distance = 14.99f };
Svc.Targets.Target = target;
DataCenter.HostileTarget = target;
var action = new TestAction(target);
var rotation = new TestRotation(action);
DataCenter.CurrentRotation = rotation;
RotationUpdater.CurrentRotationActions = [action];
var distance = new TargetCondition { TargetType = TargetType.Target, TargetConditionType = TargetConditionType.Distance, Param2 = 1, DistanceOrTime = 15 };
var combat = new TargetCondition { TargetType = TargetType.Target, TargetConditionType = TargetConditionType.InCombat };
var either = new ConditionSet { Type = LogicalType.Or, Conditions = [new Adapter(distance), new Adapter(combat)] };
Check(either.IsTrue(rotation), "14.99 < 15 triggers OR");
target.Distance = 15;
Check(!either.IsTrue(rotation), "exactly 15 is excluded");
target.Distance = 20;
Check(!either.IsTrue(rotation), "distant out-of-combat target fails");
target.Combat = true;
Check(either.IsTrue(rotation), "distant in-combat target triggers OR");
either.Type = LogicalType.And;
Check(!either.IsTrue(rotation), "AND requires both conditions");
target.Distance = 10;
Check(either.IsTrue(rotation), "AND succeeds when both match");
Svc.Targets.Target = null;
Check(!either.IsTrue(rotation), "missing target does not trigger");
Svc.Targets.Target = target;
Check(!new ConditionSet().IsTrue(rotation), "empty condition set is false");
either.Type = LogicalType.Or;

var set = DataCenter.CurrentConditionValue;
set.ConditionDict[1] = either;
set.DisableConditionDict[1] = either;
ActionSequencerUpdater.UpdateActionSequencerAction();
Check(ReferenceEquals(DataCenter.ActionSequencerAction, action), "matching condition queues forced action");
Check(DataCenter.DisabledActionSequencer.Contains(1), "matching disable condition marks action");
target.Distance = 30; target.Combat = false;
ActionSequencerUpdater.UpdateActionSequencerAction();
Check(DataCenter.ActionSequencerAction == null && DataCenter.DisabledActionSequencer.Count == 0, "false conditions clear previous frame");
target.Combat = true;
ActionSequencerUpdater.UpdateActionSequencerAction();
DataCenter.CurrentRotation = null;
ActionSequencerUpdater.UpdateActionSequencerAction();
Check(DataCenter.ActionSequencerAction == null && DataCenter.DisabledActionSequencer.Count == 0, "unloaded rotation clears previous frame");
DataCenter.CurrentRotation = rotation;
ActionSequencerUpdater.UpdateActionSequencerAction();
DataCenter.ConditionSets = null;
ActionSequencerUpdater.UpdateActionSequencerAction();
Check(DataCenter.ActionSequencerAction == null && DataCenter.DisabledActionSequencer.Count == 0, "unavailable conditions clear previous frame");
DataCenter.ConditionSets = [];
var replacement = new TestAction(target);
RotationUpdater.CurrentRotationActions = [replacement];
ActionSequencerUpdater.UpdateActionSequencerAction();
Check(ReferenceEquals(DataCenter.ActionSequencerAction, replacement), "action lookup refreshes on rotation change");
set.ConditionDict.Clear(); set.ConditionDict[999] = either;
ActionSequencerUpdater.UpdateActionSequencerAction();
Check(DataCenter.ActionSequencerAction == null, "unknown action ID is ignored");

var option = new ConditionBoolean(false, "movement") { Enable = true, Disable = true };
set.Enables["movement"] = either; set.Disables["movement"] = either;
Check(!(bool)option, "additional-condition master switch off uses stored value");
Service.Config.UseAdditionalConditions = true;
Check((bool)option, "force enable wins when both option conditions match");
option.Enable = false; option.Value = true;
Check(!(bool)option, "force disable overrides stored true");
option.Disable = false;
Check((bool)option, "disabled overrides use stored value");
option.Disable = true; target.Combat = false;
Check((bool)option, "unmatched override uses stored value");

target.Combat = true;
var saved = new TargetCondition { ID = ActionID.Move, TargetConditionType = TargetConditionType.InCombat };
DataCenter.HostileTarget = new IBattleChara();
Check(saved.IsTrue(rotation), "saved action target resolves without opening UI");
var otherRotation = new TestRotation(new TestAction(new IBattleChara()));
Check(!saved.IsTrue(otherRotation), "saved action target refreshes with rotation instance");
saved.ID = ActionID.None; saved.TargetType = TargetType.Target;
Check(saved.IsTrue(otherRotation), "switching to current target clears cached action");
Console.WriteLine($"{checks} condition regression checks passed.");

sealed class TestAction(IBattleChara target) : IBaseAction
{
    public uint ID => 1;
    public TargetInfo TargetInfo { get; } = new() { Target = target };
}
sealed class TestRotation(params IBaseAction[] actions) : ICustomRotation
{
    public IBaseAction[] AllBaseActions => actions;
}
sealed class Adapter(DelayCondition condition) : ICondition
{
    public bool IsTrue(ICustomRotation? rotation) => condition.IsTrue(rotation);
}
