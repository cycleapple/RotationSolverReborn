namespace RotationSolver.Basic.Data;

/// <summary>
/// Hostile target.
/// </summary>
public enum TargetHostileType : byte
{
    /// <summary>
    /// All targets that are in range for any abilities (Tanks/Autoduty).
    /// </summary>
    [Description("攻擊範圍內的所有目標（坦克／AutoDuty）")]
    AllTargetsCanAttack,

    /// <summary>
    /// Previously engaged targets (Non-Tanks).
    /// </summary>
    [Description("僅先前已進入交戰的目標（非坦克）")]
    TargetsHaveTarget,

    /// <summary>
    /// All targets when solo in duty, or previously engaged.
    /// </summary>
    [Description("單人進入任務（包含蜃景幻界）時攻擊所有目標，否則僅攻擊已交戰目標。")]
    AllTargetsWhenSoloInDuty,

    /// <summary>
    /// All targets when solo, or previously engaged.
    /// </summary>
    [Description("單人時攻擊所有目標，否則僅攻擊已交戰目標。")]
    AllTargetsWhenSolo,

    //[Description("Only attack targets in your parties enemy list")]
    //TargetIsInEnemiesList,

    //[Description("All targets when solo, or only attack targets in your parties enemy list")]
    //AllTargetsWhenSoloTargetIsInEnemiesList,

    //[Description("All targets when solo in duty, or only attack targets in your parties enemy list")]
    //AllTargetsWhenSoloInDutyTargetIsInEnemiesList,
}
