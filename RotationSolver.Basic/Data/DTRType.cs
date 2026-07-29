namespace RotationSolver.Basic.Data;

/// <summary>
/// Hostile target.
/// </summary>
public enum DTRType : byte
{
    /// <summary>
    /// Cycle between first Auto, Manual, and Off
    /// </summary>
    [Description("在第一個自動模式、手動模式與停用之間循環")]
    DTRNormal,

    /// <summary>
    /// Cycle between each Auto, Manual, and Off
    /// </summary>
    [Description("在每個自動模式、手動模式與停用之間循環")]
    DTRAllAuto,

    /// <summary>
    /// Cycle between Auto and Off
    /// </summary>
    [Description("在自動模式與停用之間循環")]
    DTRAuto,

    /// <summary>
    /// Cycle between Manual and Off
    /// </summary>
    [Description("在手動模式與停用之間循環")]
    DTRManual,

    /// <summary>
    /// Cycle between Manual and Auto
    /// </summary>
    [Description("在手動模式與自動模式之間循環")]
    DTRManualAuto,
}
