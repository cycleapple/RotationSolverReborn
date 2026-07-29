namespace RotationSolver.Basic.Data;

/// <summary>
/// The type of description.
/// </summary>
public enum DescType : byte
{
    /// <summary>
    /// No description.
    /// </summary>
    None,

    /// <summary>
    /// Burst actions.
    /// </summary>
    [Description("爆發技能")]
    BurstActions,

    /// <summary>
    /// Area heal GCDs.
    /// </summary>
    [Description("範圍治療 GCD")]
    HealAreaGCD,

    /// <summary>
    /// Area heal oGCDs.
    /// </summary>
    [Description("範圍治療能力技")]
    HealAreaAbility,

    /// <summary>
    /// Single target heal GCDs.
    /// </summary>
    [Description("單體治療 GCD")]
    HealSingleGCD,

    /// <summary>
    /// Single target heal oGCDs.
    /// </summary>
    [Description("單體治療能力技")]
    HealSingleAbility,

    /// <summary>
    /// Area defensive GCDs (shields, mitigation, etc).
    /// </summary>
    [Description("範圍防禦 GCD")]
    DefenseAreaGCD,

    /// <summary>
    /// Area defensive oGCDs (shields, mitigation, etc).
    /// </summary>
    [Description("範圍防禦能力技")]
    DefenseAreaAbility,

    /// <summary>
    /// Single target defensive GCDs (shields, mitigation, etc).
    /// </summary>
    [Description("單體防禦 GCD")]
    DefenseSingleGCD,

    /// <summary>
    /// Single target defensive oGCDs (shields, mitigation, etc).
    /// </summary>
    [Description("單體防禦能力技")]
    DefenseSingleAbility,

    /// <summary>
    /// Move forward GCD.
    /// </summary>
    [Description("向前移動 GCD")]
    MoveForwardGCD,

    /// <summary>
    /// Move forward ability.
    /// </summary>
    [Description("向前移動能力技")]
    MoveForwardAbility,

    /// <summary>
    /// Move back ability.
    /// </summary>
    [Description("向後移動能力技")]
    MoveBackAbility,

    /// <summary>
    /// Speed ability.
    /// </summary>
    [Description("加速能力技")]
    SpeedAbility,
}
