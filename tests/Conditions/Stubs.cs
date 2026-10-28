// Game services are substituted so the linked production condition code can run outside Dalamud.
global using System.ComponentModel;
global using RotationSolver.Basic;
global using RotationSolver.Basic.Configuration;
global using RotationSolver.Basic.Configuration.Conditions;

namespace ECommons.DalamudServices
{
    static class Svc { public static Targets Targets = new(); }
    class Targets { public IBattleChara? Target; }
}
namespace ECommons.GameHelpers
{
    static class Player { public static IBattleChara? Object; }
}
namespace ECommons.GameFunctions { }
namespace Lumina.Excel.Sheets
{
    struct Status { }
    struct Action { public TestName Name => new(); }
}
namespace RotationSolver.Basic
{
    [AttributeUsage(AttributeTargets.Property)] class JsonIgnoreAttribute : Attribute { }
    enum ActionID : uint { None, Move = 1 }
    enum StatusID { None }
    class TestName { public string TextValue = "target"; public string ExtractText() => TextValue; }
    interface IAction { uint ID { get; } }
    interface IBaseAction : IAction { TargetInfo TargetInfo { get; } }
    class TargetInfo { public IBattleChara? Target; public TargetResult? FindTarget(bool a, bool b, bool c) => Target == null ? null : new(Target); }
    record TargetResult(IBattleChara Target);
    interface ICustomRotation { IBaseAction[] AllBaseActions { get; } }
    // Minimal target service surface; assertions exercise the real TargetCondition switch/comparisons.
    class IBattleChara
    {
        public float Distance; public bool Combat;
        public uint CastActionId, CurrentHp, CurrentMp;
        public bool IsCasting; public float TotalCastTime, CurrentCastTime;
        public TestName Name = new();
        public bool HasStatus(bool self, StatusID id) => false;
        public bool IsBossFromTTK() => false;
        public bool IsBossFromIcon() => false;
        public bool IsDying() => false;
        public bool InCombat() => Combat;
        public float DistanceToPlayer() => Distance;
        public bool WillStatusEnd(float time, bool self, StatusID id) => false;
        public bool WillStatusEndGCD(uint gcd, float time, bool self, StatusID id) => false;
        public float GetTTK() => 0;
        public float GetHealthRatio() => 1;
        public string GetRole() => "Tank";
    }
    static class Service
    {
        public static Config Config = new();
        public static Sheet<T> GetSheet<T>() where T : new() => new();
    }
    class Sheet<T> where T : new() { public T GetRow(uint id) => new(); }
    class Config { public bool UseAdditionalConditions; }
    static class DataCenter
    {
        public static ICustomRotation? CurrentRotation;
        public static MajorConditionValue[]? ConditionSets = [];
        public static MajorConditionValue CurrentConditionValue = new();
        public static HashSet<uint> DisabledActionSequencer = [];
        public static IAction? ActionSequencerAction;
        public static IBattleChara? HostileTarget;
        public static float DefaultGCDRemain;
    }
}
namespace RotationSolver.Basic.Configuration.Conditions
{
    abstract class DelayCondition
    {
        public bool Not;
        public bool IsTrue(ICustomRotation? rotation) => rotation != null && (Not != IsTrueInside(rotation));
        protected abstract bool IsTrueInside(ICustomRotation rotation);
        protected static bool CheckBaseAction(ICustomRotation rotation, ActionID id, ref IBaseAction? action)
        {
            if (id != ActionID.None && (action == null || action.ID != (uint)id))
                action = rotation.AllBaseActions.FirstOrDefault(a => a.ID == (uint)id);
            return action != null;
        }
    }
    // This fixture isolates frame evaluation; it does not test filesystem persistence or Dalamud.
    class MajorConditionValue
    {
        public string Name = "Unnamed";
        public bool IsUnnamed => Name == "Unnamed";
        public Dictionary<uint, ConditionSet> ConditionDict = [], DisableConditionDict = [];
        public Dictionary<string, ConditionSet> Enables = [], Disables = [];
        public ConditionSet GetEnableCondition(string key) => Enables.GetValueOrDefault(key) ?? new();
        public ConditionSet GetDisableCondition(string key) => Disables.GetValueOrDefault(key) ?? new();
        public void Save(string folder) { }
        public static MajorConditionValue[] Read(string folder) => [];
    }
    interface ICondition { bool IsTrue(ICustomRotation? rotation); }
}
namespace RotationSolver.Updaters
{
    static class RotationUpdater { public static IAction[] CurrentRotationActions = []; }
}
