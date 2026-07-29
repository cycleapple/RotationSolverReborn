namespace RotationSolver.Basic.Attributes;

/// <summary>
/// The range of the configs.
/// </summary>
/// <param name="minValue">min value</param>
/// <param name="maxValue">max value</param>
/// <param name="unitType">the unit type</param>
/// <param name="speed">the speed.</param>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class RangeAttribute(float minValue, float maxValue, ConfigUnitType unitType, float speed = 0.005f) : Attribute
{
    /// <summary>
    /// Min Value
    /// </summary>
    public float MinValue => minValue;

    /// <summary>
    /// Max Value
    /// </summary>
    public float MaxValue => maxValue;

    /// <summary>
    /// Speed
    /// </summary>
    public float Speed => speed;

    /// <summary>
    /// The unit type.
    /// </summary>
    public ConfigUnitType UnitType => unitType;
}

/// <summary>
/// The config unit type.
/// </summary>
public enum ConfigUnitType : byte
{
    /// <summary>
    /// None unit type.
    /// </summary>
    None,

    /// <summary>
    /// 
    /// </summary>
    [Description("時間單位：秒")]
    Seconds,

    /// <summary>
    /// 
    /// </summary>
    [Description("角度單位：度")]
    Degree,

    /// <summary>
    /// 
    /// </summary>
    [Description("距離單位：公尺")]
    Yalms,

    /// <summary>
    /// 
    /// </summary>
    [Description("比例單位：百分比")]
    Percent,

    /// <summary>
    /// 
    /// </summary>
    [Description("顯示單位：像素")]
    Pixels,
}
