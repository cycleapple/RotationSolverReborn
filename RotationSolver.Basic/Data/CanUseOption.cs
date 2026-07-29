namespace RotationSolver.Basic.Data;

/// <summary>
/// Specifies options for determining whether an action can be used.
/// </summary>
[Flags]
public enum CanUseOption : byte
{
    /// <summary>
    /// No options specified.
    /// </summary>
    None,

    /// <summary>
    /// Skip status provide check.
    /// </summary>
    [Description("略過狀態提供檢查")]
    SkipStatusProvideCheck = 1 << 0,

    /// <summary>
    /// Skip combo check.
    /// </summary>
    [Description("略過連擊檢查")]
    SkipComboCheck = 1 << 1,

    /// <summary>
    /// Skip casting and moving check.
    /// </summary>
    [Description("略過詠唱與移動檢查")]
    SkipCastingCheck = 1 << 2,

    /// <summary>
    /// Indicates that all stacks should be used up.
    /// </summary>
    [Description("用盡所有積蓄次數")]
    UsedUp = 1 << 3,

    /// <summary>
    /// Indicates that the action is the last ability.
    /// </summary>
    [Description("僅於最後一個能力技")]
    OnLastAbility = 1 << 4,

    /// <summary>
    /// Skip clipping check.
    /// </summary>
    [Description("略過技能卡頓檢查")]
    SkipClippingCheck = 1 << 5,

    /// <summary>
    /// Skip AoE check.
    /// </summary>
    [Description("略過範圍技能檢查")]
    SkipAoeCheck = 1 << 6,
}
