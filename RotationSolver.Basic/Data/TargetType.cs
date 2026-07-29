namespace RotationSolver.Basic.Data;

/// <summary>
/// The type of targeting.
/// </summary>
public enum TargetingType
{
    /// <summary>
    /// Find the target whose hit box is biggest.
    /// </summary>
    [Description("體型最大")]
    Big,

    /// <summary>
    /// Find the target whose hit box is smallest.
    /// </summary>
    [Description("體型最小")]
    Small,

    /// <summary>
    /// Find the target whose HP is highest.
    /// </summary>
    [Description("目前 HP 最高")]
    HighHP,

    /// <summary>
    /// Find the target whose HP is lowest.
    /// </summary>
    [Description("目前 HP 最低")]
    LowHP,

    /// <summary>
    /// Find the target whose HP percentage is highest.
    /// </summary>
    [Description("HP 比例最高")]
    HighHPPercent,

    /// <summary>
    /// Find the target whose HP percentage is lowest.
    /// </summary>
    [Description("HP 比例最低")]
    LowHPPercent,

    /// <summary>
    /// Find the target whose max HP is highest.
    /// </summary>
    [Description("最大 HP 最高")]
    HighMaxHP,

    /// <summary>
    /// Find the target whose max HP is lowest.
    /// </summary>
    [Description("最大 HP 最低")]
    LowMaxHP,

    /// <summary>
    /// Find the target that is nearest.
    /// </summary>
    [Description("距離最近")]
    Nearest,

    /// <summary>
    /// Find the target that is farthest.
    /// </summary>
    [Description("距離最遠")]
    Farthest,

    /// <summary>
    /// PVP: Find the nearest Healer.
    /// </summary>
    [Description("PvP 優先治療職業")]
    PvPHealers,

    /// <summary>
    /// PVP: Find the nearest Tank.
    /// </summary>
    [Description("PvP 優先防護職業")]
    PvPTanks,

    /// <summary>
    /// PVP: Find the nearest DPS.
    /// </summary>
    [Description("PvP 優先進攻職業")]
    PvPDPS
}
