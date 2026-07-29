namespace RotationSolver.Basic.Data;

/// <summary>
/// Where to use tinctures.
/// </summary>
public enum TinctureUseType : byte
{
    /// <summary>
    /// Do not use tinctures.
    /// </summary>
    [Description("不使用寶藥／幻藥／藥水")]
    Nowhere,

    /// <summary>
    /// Only use tinctures in high-end duties.
    /// </summary>
    [Description("只在高難度任務使用寶藥／幻藥／藥水")]
    InHighEndDuty,

    /// <summary>
    /// Use tinctures anywhere.
    /// </summary>
    [Description("在任何地方使用寶藥／幻藥／藥水")]
    Anywhere,
}
