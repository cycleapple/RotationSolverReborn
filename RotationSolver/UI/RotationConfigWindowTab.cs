using ECommons.DalamudServices;
using System.ComponentModel;

namespace RotationSolver.UI;

/// <summary>
/// Attribute to mark tabs that should be skipped.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
internal class TabSkipAttribute : Attribute
{
}

/// <summary>
/// Attribute to specify an icon for a tab.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
internal class TabIconAttribute : Attribute
{
    public uint Icon { get; init; }
}

/// <summary>
/// Enum representing different tabs in the rotation config window.
/// </summary>
internal enum RotationConfigWindowTab : byte
{
    [TabSkip] About,
    [TabSkip] Rotation,

    [Description("實用資訊與巨集清單。")]
    [TabIcon(Icon = 4)] Main,

    [Description("目前循環的專用設定。")]
    [TabIcon(Icon = 4)] Job,

    [Description("設定任務循環。")]
    [TabIcon(Icon = 4)] Duty,

    [Description("設定目前職業的技能與自訂條件。")]
    [TabIcon(Icon = 4)] Actions,

    [Description("設定反應技能與狀態效果清單。")]
    [TabIcon(Icon = 21)] List,

    [Description("設定基本選項。")]
    [TabIcon(Icon = 14)] Basic,

    [Description("設定使用者介面。")]
    [TabIcon(Icon = 42)] UI,

    [Description("設定一般技能使用與控制選項。")]
    [TabIcon(Icon = 29)] Auto,

    [Description("設定目標選取。")]
    [TabIcon(Icon = 16)] Target,

    [Description("設定選用的輔助功能。")]
    [TabIcon(Icon = 51)] Extra,

    [Description("提供給開發者與循環作者的偵錯選項（不使用時請停用）。")]
    [TabIcon(Icon = 5)] Debug,

    [Description("設定 AutoDuty 選項並查看相關資訊。")]
    [TabIcon(Icon = 4)] AutoDuty,
}

/// <summary>
/// Struct representing an incompatible plugin.
/// </summary>
public readonly struct IncompatiblePlugin
{
    public string Name { get; init; }
    public string Icon { get; init; }
    public string Url { get; init; }
    public string Features { get; init; }

    /// <summary>
    /// Checks if the plugin is enabled.
    /// </summary>
    [JsonIgnore]
    public readonly bool IsEnabled
    {
        get
        {
            string name = Name;
            IEnumerable<Dalamud.Plugin.IExposedPlugin> installedPlugins = Svc.PluginInterface.InstalledPlugins;
            foreach (Dalamud.Plugin.IExposedPlugin x in installedPlugins)
            {
                if ((x.Name.Equals(name, StringComparison.OrdinalIgnoreCase) || x.InternalName.Equals(name, StringComparison.OrdinalIgnoreCase)) && x.IsLoaded)
                {
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// Checks if the plugin is installed.
    /// </summary>
    [JsonIgnore]
    public readonly bool IsInstalled
    {
        get
        {
            string name = Name;
            IEnumerable<Dalamud.Plugin.IExposedPlugin> installedPlugins = Svc.PluginInterface.InstalledPlugins;
            foreach (Dalamud.Plugin.IExposedPlugin x in installedPlugins)
            {
                if (x.Name.Equals(name, StringComparison.OrdinalIgnoreCase) || x.InternalName.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public CompatibleType Type { get; init; }
}

/// <summary>
/// Struct representing an incompatible plugin.
/// </summary>
public readonly struct AutoDutyPlugin
{
    public string Name { get; init; }
    public string Icon { get; init; }
    public string Url { get; init; }
    public string Features { get; init; }

    /// <summary>
    /// Checks if the plugin is enabled.
    /// </summary>
    [JsonIgnore]
    public readonly bool IsEnabled
    {
        get
        {
            string name = Name;
            IEnumerable<Dalamud.Plugin.IExposedPlugin> installedPlugins = Svc.PluginInterface.InstalledPlugins;
            foreach (Dalamud.Plugin.IExposedPlugin x in installedPlugins)
            {
                if ((x.Name.Equals(name, StringComparison.OrdinalIgnoreCase) || x.InternalName.Equals(name, StringComparison.OrdinalIgnoreCase)) && x.IsLoaded)
                {
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// Checks if the plugin is installed.
    /// </summary>
    [JsonIgnore]
    public readonly bool IsInstalled
    {
        get
        {
            string name = Name;
            IEnumerable<Dalamud.Plugin.IExposedPlugin> installedPlugins = Svc.PluginInterface.InstalledPlugins;
            foreach (Dalamud.Plugin.IExposedPlugin x in installedPlugins)
            {
                if (x.Name.Equals(name, StringComparison.OrdinalIgnoreCase) || x.InternalName.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

/// <summary>
/// Enum representing different types of compatibility issues.
/// </summary>
[Flags]
public enum CompatibleType : byte
{
    Skill_Usage = 1 << 0,
    Skill_Selection = 1 << 1,
    Crash = 1 << 2,
    Broken = 1 << 3,
}
