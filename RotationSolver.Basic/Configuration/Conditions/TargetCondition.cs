using ECommons.DalamudServices;
using ECommons.GameFunctions;
using ECommons.GameHelpers;
using Lumina.Excel.Sheets;

namespace RotationSolver.Basic.Configuration.Conditions;

/// <summary>
/// Represents a condition based on the target.
/// </summary>
[Description("目標條件")]
internal class TargetCondition : DelayCondition
{
    internal IBaseAction? _action;
    private ICustomRotation? _resolvedRotation;
    public ActionID ID { get; set; } = ActionID.None;

    public bool FromSelf;
    internal Status? Status;
    public StatusID StatusId { get; set; }
    public TargetType TargetType;
    public TargetConditionType TargetConditionType;

    public float DistanceOrTime;
    public int GCD, Param2;

    public string CastingActionName = string.Empty;

    public string CombatRole = string.Empty;

    /// <summary>
    /// Checks if the condition is true inside the rotation.
    /// </summary>
    /// <param name="rotation">The custom rotation instance.</param>
    /// <returns><c>true</c> if the condition is met; otherwise, <c>false</c>.</returns>
    protected override bool IsTrueInside(ICustomRotation rotation)
    {
        // Resolve saved action targets during evaluation, even when the editor was never opened.
        if (ID == ActionID.None || !ReferenceEquals(_resolvedRotation, rotation))
        {
            _action = null;
        }
        _resolvedRotation = rotation;
        _ = CheckBaseAction(rotation, ID, ref _action);

        IBattleChara? tar = _action?.TargetInfo.FindTarget(true, false, false)?.Target ?? TargetType switch
        {
            TargetType.Target => Svc.Targets.Target as IBattleChara,
            TargetType.HostileTarget => DataCenter.HostileTarget,
            TargetType.Player => Player.Object,
            _ => null,
        };

        return TargetConditionType == TargetConditionType.IsNull
            ? tar == null
            : tar != null && TargetConditionType switch
            {
                TargetConditionType.HasStatus => tar.HasStatus(FromSelf, StatusId),
                TargetConditionType.IsBossFromTTK => tar.IsBossFromTTK(),
                TargetConditionType.IsBossFromIcon => tar.IsBossFromIcon(),
                TargetConditionType.IsDying => tar.IsDying(),
                TargetConditionType.InCombat => tar.InCombat(),
                TargetConditionType.Distance => CheckDistance(tar),
                TargetConditionType.StatusEnd => !tar.WillStatusEnd(DistanceOrTime, FromSelf, StatusId),
                TargetConditionType.StatusEndGCD => !tar.WillStatusEndGCD((uint)GCD, DistanceOrTime, FromSelf, StatusId),
                TargetConditionType.TimeToKill => CheckTimeToKill(tar),
                TargetConditionType.CastingAction => CheckCastingAction(tar),
                TargetConditionType.CastingActionTime => CheckCastingActionTime(tar),
                TargetConditionType.HP => CheckHP(tar),
                TargetConditionType.HPRatio => CheckHPRatio(tar),
                TargetConditionType.MP => CheckMP(tar),
                TargetConditionType.TargetName => CheckTargetName(tar),
                TargetConditionType.TargetRole => CheckTargetRole(tar),
                _ => false,
            };
    }

    private bool CheckDistance(IBattleChara tar)
    {
        return Param2 switch
        {
            0 => tar.DistanceToPlayer() > DistanceOrTime,
            1 => tar.DistanceToPlayer() < DistanceOrTime,
            2 => tar.DistanceToPlayer() == DistanceOrTime,
            _ => false,
        };
    }

    private bool CheckTimeToKill(IBattleChara tar)
    {
        return Param2 switch
        {
            0 => tar.GetTTK() > DistanceOrTime,
            1 => tar.GetTTK() < DistanceOrTime,
            2 => tar.GetTTK() == DistanceOrTime,
            _ => false,
        };
    }

    private bool CheckCastingAction(IBattleChara tar)
    {
        if (string.IsNullOrEmpty(CastingActionName) || tar.CastActionId == 0)
        {
            return false;
        }

        Lumina.Excel.Sheets.Action actionRow = Service.GetSheet<Lumina.Excel.Sheets.Action>().GetRow(tar.CastActionId);
        string castName = actionRow.Name.ExtractText();
        return CastingActionName == castName;
    }

    private bool CheckCastingActionTime(IBattleChara tar)
    {
        if (!tar.IsCasting || tar.CastActionId == 0)
        {
            return false;
        }

        float castTime = tar.TotalCastTime - tar.CurrentCastTime;
        return Param2 switch
        {
            0 => castTime > DistanceOrTime + DataCenter.DefaultGCDRemain,
            1 => castTime < DistanceOrTime + DataCenter.DefaultGCDRemain,
            2 => castTime == DistanceOrTime + DataCenter.DefaultGCDRemain,
            _ => false,
        };
    }

    private bool CheckHP(IBattleChara tar)
    {
        return Param2 switch
        {
            0 => tar.CurrentHp > GCD,
            1 => tar.CurrentHp < GCD,
            2 => tar.CurrentHp == GCD,
            _ => false,
        };
    }

    private bool CheckHPRatio(IBattleChara tar)
    {
        return Param2 switch
        {
            0 => tar.GetHealthRatio() > DistanceOrTime,
            1 => tar.GetHealthRatio() < DistanceOrTime,
            2 => tar.GetHealthRatio() == DistanceOrTime,
            _ => false,
        };
    }

    private bool CheckMP(IBattleChara tar)
    {
        return Param2 switch
        {
            0 => tar.CurrentMp > GCD,
            1 => tar.CurrentMp < GCD,
            2 => tar.CurrentMp == GCD,
            _ => false,
        };
    }

    private bool CheckTargetName(IBattleChara tar)
    {
        return !string.IsNullOrEmpty(CastingActionName) && tar.Name.TextValue == CastingActionName;
    }
    private bool CheckTargetRole(IBattleChara tar)
    {
        return !string.IsNullOrEmpty(CombatRole) && tar.GetRole().ToString() == CombatRole;
    }
}

internal enum TargetType : byte
{
    [Description("敵對目標")]
    HostileTarget,

    [Description("玩家")]
    Player,

    [Description("目前目標")]
    Target,
}

internal enum TargetConditionType : byte
{
    [Description("不存在")]
    IsNull,

    [Description("具有狀態")]
    HasStatus,

    [Description("即將死亡")]
    IsDying,

    [Description("依預估擊殺時間判定為頭目")]
    IsBossFromTTK,

    [Description("依圖示判定為頭目")]
    IsBossFromIcon,

    [Description("戰鬥中")]
    InCombat,

    [Description("距離")]
    Distance,

    [Description("狀態結束")]
    StatusEnd,

    [Description("狀態結束（GCD）")]
    StatusEndGCD,

    [Description("正在詠唱的技能")]
    CastingAction,

    [Description("技能詠唱剩餘時間")]
    CastingActionTime,

    [Description("預估擊殺時間")]
    TimeToKill,

    [Description("HP")]
    HP,

    [Description("HP%")]
    HPRatio,

    [Description("MP")]
    MP,

    [Description("目標名稱")]
    TargetName,

    [Description("目標職責")]
    TargetRole,
}
