namespace RotationSolver.Basic.Data;

/// <summary>
/// Special State.
/// </summary>
public enum SpecialCommandType : byte
{
    /// <summary>
    /// To end this special duration before the set time.
    /// </summary>
    [Description("在設定時間結束前終止目前的特殊狀態。")]
    EndSpecial,

    /// <summary>
    /// Open a window to use AoE heal.
    /// </summary>
    [Description("啟用範圍治療技能。")]
    HealArea,

    /// <summary>
    /// Open a window to use single heal.
    /// </summary>
    [Description("啟用單體治療技能。")]
    HealSingle,

    /// <summary>
    /// Open a window to use AoE defense.
    /// </summary>
    [Description("啟用範圍減傷技能。")]
    DefenseArea,

    /// <summary>
    /// Open a window to use single defense.
    /// </summary>
    [Description("啟用單體減傷技能。")]
    DefenseSingle,

    /// <summary>
    /// Open a window to use Esuna, tank stance actions or True North.
    /// </summary>
    [Description("啟用康復、坦克姿態或真北等技能。")]
    DispelStancePositional,

    /// <summary>
    /// Open a window to use Raise or Shirk.
    /// </summary>
    [Description("啟用復活或退避等技能。")]
    RaiseShirk,

    /// <summary>
    /// Open a window to move forward.
    /// </summary>
    [Description("啟用向前移動技能。")]
    MoveForward,

    /// <summary>
    /// Open a window to move back.
    /// </summary>
    [Description("啟用向後移動技能。")]
    MoveBack,

    /// <summary>
    /// Open a window to use knockback immunity actions.
    /// </summary>
    [Description("啟用防擊退技能。")]
    AntiKnockback,

    /// <summary>
    /// Open a window to burst.
    /// </summary>
    [Description("啟用爆發技能。")]
    Burst,

    /// <summary>
    /// Open a window to speed up.
    /// </summary>
    [Description("啟用加速技能。")]
    Speed,

    /// <summary>
    /// Open a window to use limit break.
    /// </summary>
    [Description("啟用極限技。")]
    LimitBreak,

    /// <summary>
    /// Open a window to do not use the casting action.
    /// </summary>
    [Description("暫停使用需要詠唱的技能。")]
    NoCasting,

    /// <summary>
    /// Intercepting action.
    /// </summary>
    [Description("顯示 RSR 正在攔截技能輸入。")]
    Intercepting,
}

/// <summary>
/// The state of the plugin.
/// </summary>
public enum StateCommandType : byte
{
    /// <summary>
    /// Stop the addon. Always remember to turn it off when it is not in use!
    /// </summary>
    [Description("停止插件。不使用時請記得將其關閉。")]
    Off,

    /// <summary>
    /// Start the addon in Auto mode. When out of combat or when combat starts, switches the target according to the set condition.
    /// </summary>
    [Description("以自動模式啟動插件。離開戰鬥或開始戰鬥時，會依照設定條件切換目標。" +
        "\r\n你也可以在命令結尾指定目標類型，例如：/rotation Auto Big")]
    Auto,

    /// <summary>
    /// Start the addon in Manual mode. You need to choose the target manually. This will bypass any engage settings that you have set up and will start attacking immediately once something is targeted.
    /// </summary>
    [Description("以手動模式啟動插件。你必須自行選擇目標；此模式會略過交戰條件，選中目標後立即開始攻擊。")]
    Manual,

    /// <summary>
    /// 
    /// </summary>
    [Description("此模式由 AutoDuty 插件管理。")]
    AutoDuty,
}

/// <summary>
/// Some Other Commands.
/// </summary>
public enum OtherCommandType : byte
{
    /// <summary>
    /// Open the settings.
    /// </summary>
    [Description("開啟設定。")]
    Settings,

    /// <summary>
    /// Open the rotations.
    /// </summary>
    [Description("開啟循環設定。")]
    Rotations,

    /// <summary>
    /// Open the rotations.
    /// </summary>
    [Description("開啟任務循環設定。")]
    DutyRotations,

    /// <summary>
    /// Perform the actions.
    /// </summary>
    [Description("執行指定技能。")]
    DoActions,

    /// <summary>
    /// Toggle the actions.
    /// </summary>
    [Description("切換指定技能的啟用狀態。")]
    ToggleActions,

    /// <summary>
    /// Do the next action.
    /// </summary>
    [Description("執行下一個技能。")]
    NextAction,

    /// <summary>
    /// Cycles between states following settings in Target > Configuration.
    /// </summary>
    [Description("依照「目標 > 設定」中的選項循環切換狀態。")]
    Cycle,
}
