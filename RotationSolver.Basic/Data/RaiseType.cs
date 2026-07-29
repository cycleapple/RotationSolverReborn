namespace RotationSolver.Basic.Data;

/// <summary>
/// Specifies who to raise.
/// </summary>
public enum RaiseType : byte
{
    /// <summary>
    /// Raise only party members.
    /// </summary>
    [Description("只復活小隊成員")]
    PartyOnly,

    /// <summary>
    /// Raise party members and alliance supports.
    /// </summary>
    [Description("復活小隊成員與團隊中的防護／治療職業")]
    PartyAndAllianceSupports,

    /// <summary>
    /// Raise party members and alliance healers.
    /// </summary>
    [Description("復活小隊成員與團隊中的治療職業")]
    PartyAndAllianceHealers,

    /// <summary>
    /// Raise All In Duty.
    /// </summary>
    [Description("任務中復活所有玩家")]
    All,

    /// <summary>
    /// Raise all.
    /// </summary>
    [Description("復活所有玩家")]
    AllOutOfDuty,
}
