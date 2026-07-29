namespace RotationSolver.Basic.Data;

/// <summary>
/// Hostile target.
/// </summary>
public enum HardCastRaiseType : byte
{
    /// <summary>
    ///
    /// </summary>
    [Description("不讀條復活")]
    NoHardCast,

    /// <summary>
    ///
    /// </summary>
    [Description("即使即刻詠唱仍在冷卻，也讀條復活")]
    HardCastNormal,

    /// <summary>
    ///
    /// </summary>
    [Description("即刻詠唱冷卻中且其他治療職業倒地時讀條復活")]
    HardCastOnlyHealer,

    /// <summary>
    ///
    /// </summary>
    [Description("即刻詠唱的剩餘冷卻時間長於復活詠唱時間時讀條復活")]
    HardCastSwiftCooldown,

    /// <summary>
    /// 
    /// </summary>
    [Description("其他治療職業倒地，且即刻詠唱的剩餘冷卻時間長於復活詠唱時間時讀條復活")]
    HardCastOnlyHealerSwiftCooldown,
}
