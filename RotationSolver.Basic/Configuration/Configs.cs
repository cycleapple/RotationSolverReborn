using Dalamud.Configuration;
using ECommons.DalamudServices;
using ECommons.ExcelServices;
using ECommons.Logging;
using System.Collections.Concurrent;
using static RotationSolver.Basic.Configuration.ConfigTypes;

namespace RotationSolver.Basic.Configuration;

internal partial class Configs : IPluginConfiguration
{
    [JsonIgnore]
    public const string
        BasicTimer = "BasicTimer",
        BasicAutoSwitch = "BasicAutoSwitch",
        BasicParams = "BasicParams",
        UiInformation = "UiInformation",
        UiWindows = "UiWindows",
        PvPSpecificControls = "PvPSpecificControls",
        AutoActionUsage = "AutoActionUsage",
        HealingActionCondition = "HealingActionCondition",
        PhantomDutyRotationConfiguration = "PhantomDutyRotationConfiguration",
        TargetConfig = "TargetConfig",
        Extra = "Extra",
        Rotations = "Rotations",
        List = "List",
        List2 = "List2",
        List3 = "List3",
        Debug = "Debug";

    public const int CurrentVersion = 12;
    public int Version { get; set; } = CurrentVersion;

    public string LastSeenChangelog { get; set; } = "0.0.0.0";
    public bool FirstTimeSetupDone { get; set; } = false;

    public List<ActionEventInfo> Events { get; private set; } = [];
    public SortedSet<Job> DisabledJobs { get; private set; } = [];

    public string[] RotationLibs { get; set; } = [];
    public List<TargetingType> TargetingTypes { get; set; } = [];

    public MacroInfo DutyStart { get; set; } = new MacroInfo();
    public MacroInfo DutyEnd { get; set; } = new MacroInfo();

    [ConditionBool, UI("攔截玩家輸入並加入佇列，由 RSR 執行技能（僅 PvE）",
    Filter = AutoActionUsage, Section = 5)]
    private static readonly bool _interceptAction2 = false;

    [ConditionBool, UI("允許攔截魔法",
    Filter = AutoActionUsage, Section = 5, Parent = nameof(InterceptAction2))]
    private static readonly bool _interceptSpell2 = false;

    [ConditionBool, UI("允許攔截戰技",
    Filter = AutoActionUsage, Section = 5, Parent = nameof(InterceptAction2))]
    private static readonly bool _interceptWeaponskill2 = false;

    [ConditionBool, UI("允許攔截能力技",
    Filter = AutoActionUsage, Section = 5, Parent = nameof(InterceptAction2))]
    private static readonly bool _interceptAbility2 = false;

    [ConditionBool, UI("允許攔截巨集中的技能",
    Filter = AutoActionUsage, Section = 5, Parent = nameof(InterceptAction2))]
    private static readonly bool _interceptMacro = false;

    [ConditionBool, UI("允許攔截仍在冷卻中的技能",
    Filter = AutoActionUsage, Section = 5, Parent = nameof(InterceptAction2))]
    private static readonly bool _interceptCooldown = false;

    [UI("攔截技能的執行時限（攔截後 RSR 可嘗試使用技能的時間）",
    Filter = AutoActionUsage, Section = 5)]
    [Range(1, 10, ConfigUnitType.Seconds)]
    public float InterceptActionTime { get; set; } = 5;

    /// <markdown file="Auto" name="What kind of AoE moves to use" section="Action Usage and Control">
    /// - Full: Use all available AoE actions.
    /// - Cleave: Use only single-target AoE actions.
    /// - Off: Do not use any AoE actions.
    /// </markdown>
    [UI("要使用哪一類範圍技能",
    Description = "完整：使用所有可用的範圍技能。\n順劈：只使用會波及其他目標的單體技能。\n停用：不使用任何範圍技能。",
    Filter = AutoActionUsage, Section = 3)]
    public AoEType AoEType { get; set; } = AoEType.Full;

    [ConditionBool, UI("對狀態數量已達上限的敵人略過施加狀態",
    Filter = AutoActionUsage, Section = 3)]
    private static readonly bool _statuscap2 = true;

    [ConditionBool, UI("不以範圍技能攻擊新的敵人", Description = "範圍技能可能攻擊尚未敵對的敵人時，一律不使用。",
        Filter = AutoActionUsage, Section = 3)]
    private static readonly bool _noNewHostiles = false;

    [ConditionBool, UI("區域切換期間自動停用",
    Description = "在不同區域之間移動時，自動關閉戰鬥狀態。",
    Filter = BasicAutoSwitch)]
    private static readonly bool _autoOffBetweenArea = true;

    [ConditionBool, UI("過場動畫期間自動停用",
    Description = "播放過場動畫時，自動關閉戰鬥狀態。",
    Filter = BasicAutoSwitch)]
    private static readonly bool _autoOffCutScene = true;

    [ConditionBool, UI("切換職業時自動停用",
    Description = "變更職業時自動關閉戰鬥狀態。",
    Filter = BasicAutoSwitch)]
    private static readonly bool _autoOffSwitchClass = true;

    [ConditionBool, UI("在 PvE 中倒地時自動停用",
    Description = "角色倒地時自動關閉戰鬥狀態。",
    Filter = BasicAutoSwitch)]
    private static readonly bool _autoOffWhenDead = true;

    [ConditionBool, UI("任務完成時自動停用",
    Description = "任務（副本）結束時自動關閉戰鬥狀態。",
    Filter = BasicAutoSwitch)]
    private static readonly bool _autoOffWhenDutyCompleted = true;

    [ConditionBool, UI("更新後顯示變更記錄視窗",
    Description = "插件更新後以彈出視窗顯示變更記錄。",
    Filter = UiInformation)]
    private static readonly bool _changelogPopup = true;

    [ConditionBool, UI("在 DTR 資訊列顯示插件狀態",
    Description = "在伺服器資訊列顯示插件目前狀態。",
    Filter = UiInformation)]
    private static readonly bool _showInfoOnDtr = true;

    [UI("DTR 行為", Filter = UiInformation, Parent = nameof(ShowInfoOnDtr))]
    public DTRType DTRType { get; set; } = DTRType.DTRNormal;

    [ConditionBool, UI("以通知訊息顯示插件狀態",
    Description = "戰鬥狀態變更時顯示通知訊息。",
    Filter = UiInformation)]
    private static readonly bool _showInfoOnToast = false;

    [ConditionBool, UI("詠唱或執行特定技能時鎖定移動",
    Description = "詠唱或使用特定技能時，防止角色移動。",
    Filter = Extra)]
    private static readonly bool _poslockCasting = false;

    [UI("", Action = ActionID.PassageOfArmsPvE, Parent = nameof(PoslockCasting))]
    public bool PosPassageOfArms { get; set; } = false;

    [UI("", Action = ActionID.FlamethrowerPvE, Parent = nameof(PoslockCasting))]
    public bool PosFlameThrower { get; set; } = false;

    [UI("", Action = ActionID.ImprovisationPvE, Parent = nameof(PoslockCasting))]
    public bool PosImprovisation { get; set; } = false;

    /// <markdown file="Auto" name="Gemdraughts/Tinctures/Pots Usage" section="Action Usage and Control">
    /// Sets whether to use damage-boosting potions and in which duty. You also need to enable the specific
    /// potion in the `Actions` tab, under `Items`.
    /// </markdown>
    [JobConfig, UI("只有循環程式支援時才會自動使用",
    Description = "只有循環明確支援自動使用寶藥時，此設定才會生效。",
    Filter = AutoActionUsage, PvPFilter = JobFilterType.NoJob)]
    private readonly TinctureUseType _TinctureType = TinctureUseType.Nowhere;

    [ConditionBool, UI("自動使用防擊退職能技能（親疏自行、沉穩詠唱）",
    Description = "需要時依照「清單」中的防擊退技能清單，自動使用防擊退技能。",
    Filter = AutoActionUsage)]
    private static readonly bool _useKnockback = true;

    [ConditionBool, UI("自動使用 HP 藥水",
    Description = "允許插件自動使用恢復 HP 的藥水。",
    Filter = AutoActionUsage)]
    private static readonly bool _useHpPotions = false;

    [ConditionBool, UI("自動使用 MP 藥水",
    Description = "允許插件自動使用恢復 MP 的藥水。",
    Filter = AutoActionUsage)]
    private static readonly bool _useMpPotions = false;

    [ConditionBool, UI("自動使用不死鳥之尾",
    Description = "允許插件自動使用不死鳥之尾（實驗性功能）。",
    Filter = AutoActionUsage)]
    private static readonly bool _usePhoenixDown = false;

    [UI("目標距離小於此值時使用帶有傷害的突進技能",
        Filter = AutoActionUsage)]
    [Range(0, 30, ConfigUnitType.Yalms, 1f)]
    public float DistanceForMoving2 { get; set; } = 3f;

    [ConditionBool, UI("允許對優先標記目標使用範圍技能",
    Description = "允許範圍技能命中帶有優先標記的目標。",
    Parent = nameof(ChooseAttackMark))]
    private static readonly bool _canAttackMarkAOE = true;

    [ConditionBool, UI("教學模式", Filter = UiInformation)]
    private static readonly bool _teachingMode = false;

    [ConditionBool, UI("模擬按下技能的效果",
        Filter = UiInformation)]
    private static readonly bool _keyBoardNoise = true;

    [ConditionBool, UI("倒數計時開始時啟用自動模式",
        Filter = BasicAutoSwitch, Section = 1)]
    private static readonly bool _startOnCountdown = true;

    [ConditionBool, UI("倒數計時開始時啟用手動模式，而非自動模式",
               Parent = nameof(StartOnCountdown))]
    private static readonly bool _countdownStartsManualMode = false;

    [ConditionBool, UI("倒數期間提前進入戰鬥時取消自動模式",
        Filter = BasicAutoSwitch, Section = 1)]
    private static readonly bool _cancelStateOnCombatBeforeCountdown = false;

    [ConditionBool, UI("受到攻擊時自動啟用手動模式",
        Filter = BasicAutoSwitch, Section = 1)]
    private static readonly bool _startOnAttackedBySomeone = false;
    
    [ConditionBool, UI("小隊進入戰鬥時自動啟用自動模式",
         Filter = BasicAutoSwitch, Section = 1)]
    private static readonly bool _startOnPartyIsInCombat = false;
    
    [ConditionBool, UI("團隊進入戰鬥時自動啟用自動模式",
         Filter = BasicAutoSwitch, Section = 1)]
    private static readonly bool _startOnAllianceIsInCombat = false;
    
    [ConditionBool, UI("在南方博茲雅戰線／禁地優雷卡／蜃景幻界的危命任務或遭遇戰中進入戰鬥時，自動啟用自動模式",
         Filter = BasicAutoSwitch, Section = 1)]
    private static readonly bool _startOnFieldOpInCombat = false;

    /// <markdown file="Auto" name="Use healing abilities when playing a non-healer role" section="Healing Usage and Control">
    /// Allow usage of healing abilities when not playing as a healer (such as Vercure, Bloodbath, etc.)
    /// </markdown>
    [ConditionBool, UI("使用非治療職業時允許使用治療技能",
        Filter = HealingActionCondition, Section = 1,
        PvEFilter = JobFilterType.NoHealer, PvPFilter = JobFilterType.NoJob)]
    private static readonly bool _useHealWhenNotAHealer = true;

    [JobConfig, UI("可行時使用打斷技能",
        Filter = AutoActionUsage, Section = 3,
        PvEFilter = JobFilterType.Interrupt,
        PvPFilter = JobFilterType.NoJob)]
    private static readonly bool _interruptibleMoreCheck = true;

    [ConditionBool, UI("對不在禁止挑釁清單中的任何目標使用挑釁",
        Filter = AutoActionUsage, Section = 3)]
    private static readonly bool _provokeAnything = false;

    [ConditionBool, UI("目標死亡時中止詠唱", Filter = Extra)]
    private static readonly bool _useStopCasting = false;

    /// <markdown file="Auto" name="Cleanse all dispellable debuffs" section="Action Usage and Control">
    /// Enabling this setting will force the usage of Esuna on all target that are affected by a
    /// cleansable debuff.
    /// </markdown>
    [ConditionBool, UI("不考慮治療需求，解除所有可解除的減益狀態",
        Filter = AutoActionUsage, Section = 3,
        PvEFilter = JobFilterType.Dispel, PvPFilter = JobFilterType.NoJob)]
    private static readonly bool _dispelAll = false;

    [ConditionBool, UI("Debug Mode", Filter = Debug)]
    private static readonly bool _inDebug = false;

    [ConditionBool, UI("讓 /rotation Manual 成為切換指令",
        Filter = BasicParams)]
    private static readonly bool _toggleManual = false;

    [ConditionBool, UI("讓 /rotation Auto 成為切換指令（一般行為會在目標設定間循環）",
        Filter = BasicParams)]
    private static readonly bool _toggleAuto = false;

    [ConditionBool, UI("僅在附近有敵人或任務中顯示這些視窗",
        Filter = UiWindows)]
    private static readonly bool _onlyShowWithHostileOrInDuty = false;

    [ConditionBool, UI("顯示控制視窗",
        Filter = UiWindows)]
    private static readonly bool _showControlWindow = false;

    [ConditionBool, UI("鎖定控制視窗",
        Filter = UiWindows)]
    private static readonly bool _isControlWindowLock = false;

    [ConditionBool, UI("顯示下一個技能視窗", Filter = UiWindows)]
    private static readonly bool _showNextActionWindow = false;

    [ConditionBool, UI("不接受輸入", Parent = nameof(ShowNextActionWindow))]
    private static readonly bool _isInfoWindowNoInputs = false;

    [ConditionBool, UI("不可移動", Parent = nameof(ShowNextActionWindow))]
    private static readonly bool _isInfoWindowNoMove = false;

    [ConditionBool, UI("顯示物品冷卻時間",
        Parent = nameof(ShowCooldownWindow))]
    private static readonly bool _showItemsCooldown = false;

    [ConditionBool, UI("顯示 GCD 冷卻時間",
        Parent = nameof(ShowCooldownWindow))]
    private static readonly bool _showGCDCooldown = false;

    [ConditionBool, UI("顯示原始冷卻時間",
        Filter = UiInformation)]
    private static readonly bool _useOriginalCooldown = false;

    [ConditionBool, UI("永遠顯示冷卻時間", Filter = UiInformation)]
    private static readonly bool _showCooldownsAlways = false;

    [ConditionBool, UI("顯示工具提示",
        Filter = UiInformation)]
    private static readonly bool _showTooltips = true;

    [ConditionBool, UI("在熱鍵欄上標示已停用技能",
        Description = "啟用後，已在 RSR 停用的技能會在遊戲熱鍵欄上套用色調。",
        Filter = UiInformation)]
    private static readonly bool _reddenDisabledHotbarActions = false;

    [UI("已停用技能的熱鍵欄色調", Parent = nameof(ReddenDisabledHotbarActions), Filter = UiInformation)]
    public Vector4 HotbarDisabledTintColor { get; set; } = new(1f, 0f, 0f, 0.40f);

    [ConditionBool, UI("以通知訊息顯示技能執行回饋",
        Filter = UiInformation)]
    private static readonly bool _showToastsAboutDoAction = false;

    [ConditionBool, UI("允許採用此設定的循環使用循環中定義的爆發能力技", Filter = AutoActionUsage, Section = 4)]
    private static readonly bool _autoBurst = true;

    /// <markdown file="Auto" name="Disable hostile actions if something is casting an action on the Gaze/Stop list" section="Action Usage and Control">
    /// This setting is linked with the <see cref="RotationSolver.Basic.Configuration.OtherConfiguration.HostileCastingStop">Gaze/Stop list.</see>
    /// </markdown>
    [ConditionBool, UI("有目標正在詠唱凝視／停止清單中的技能時，停用敵對技能（實驗性）", Filter = AutoActionUsage, Section = 4)]
    private static readonly bool _castingStop = false;

    [UI("詠唱結束前多久讓 RSR 停止使用技能", Filter = AutoActionUsage, Section = 4, Parent = nameof(CastingStop))]
    [Range(0, 15, ConfigUnitType.Seconds)]
    public float CastingStopTime { get; set; } = 2.5f;

    [ConditionBool, UI("在整段詠唱期間停用（啟用後，整段詠唱期間都不會使用技能）", Filter = AutoActionUsage, Section = 4, Parent = nameof(CastingStop))]
    private static readonly bool _castingStopCalculate = false;

    /// <markdown file="Auto" name="Automatic Healing Thresholds" section="Healing Usage and Control" isSubsection="1">
    /// When enabled, you can customize the healing thresholds for when healing will be cast occur on target(s).
    /// </markdown>
    [ConditionBool, UI("自動治療門檻", Filter = HealingActionCondition, Section = 1, Order = 1)]
    private static readonly bool _autoHeal = true;

    /// <markdown file="Auto" name="Stop Healing Cast After Reaching Threshold" section="Healing Usage and Control" isSubsection="1">
    /// When enabled, you can customize the healing thresholds for when healing will be cast occur on target(s).
    /// </markdown>
    [ConditionBool, UI("達到門檻後中止單體 GCD 治療（高度實驗性）", Filter = HealingActionCondition, Section = 1, Order = 2, Description = "隊伍中其他治療職業可能會讓目標的 HP 超過治療門檻，造成 MP 浪費。發生此情況時會中斷詠唱。")]
    private static readonly bool _stopHealingAfterThresholdExperimental2 = false;

    /// <markdown file="Auto" name="Auto-use oGCD abilities" section="Action Usage and Control" isSubsection="1">
    /// Whether to use oGCD abilities or not at all.
    /// </markdown>
    [ConditionBool, UI("自動使用能力技", Filter = AutoActionUsage)]
    private static readonly bool _useAbility = true;

    [ConditionBool, UI("使用防禦能力技", Description = "進行大型任務時建議啟用；若能自行規劃治療與防禦能力技的使用，也可停用。",
        Parent = nameof(UseAbility))]
    private static readonly bool _useDefenseAbility = true;

    [ConditionBool, UI("自動啟用坦克姿態", Parent = nameof(UseAbility),
        PvEFilter = JobFilterType.Tank)]
    private static readonly bool _autoTankStance = true;

    [ConditionBool, UI("小隊中有另一名坦克時自動挑釁", Description = "小隊中有多名坦克，且敵人正在攻擊非坦克成員時，自動使用挑釁。",
        Parent = nameof(UseAbility), PvEFilter = JobFilterType.Tank)]
    private static readonly bool _autoProvokeForTank = true;

    /// <markdown file="Auto" name="Auto True North" section="Healing Usage and Control" subsection="RotationSolver.Basic.Configuration.Configs._useAbility">
    /// Whether to cast True North when playing as a melee DPS when you do not have the right
    /// positional on the enemy.
    /// </markdown>
    [ConditionBool, UI("自動使用真北（近戰進攻職業）",
        Parent = nameof(UseAbility),
        PvEFilter = JobFilterType.Melee)]
    private static readonly bool _autoUseTrueNorth = true;

    [ConditionBool, UI("任務中且非戰鬥狀態時使用提高移動速度的技能", Parent = nameof(UseAbility))]
    private static readonly bool _autoSpeedOutOfCombat = true;

    [ConditionBool, UI("任務外且非戰鬥狀態時使用提高移動速度的技能", Parent = nameof(UseAbility))]
    private static readonly bool _autoSpeedOutOfCombatNoDuty = false;

    [ConditionBool, UI("使用有益的地面指定技能", Description = "1. 自身目標備援：\r\n若射程為零，永遠以玩家為目標，並回傳玩家位置上所有可受影響的目標。\r\n2. 偏好位置：\r\n• 嘗試取得目前區域預先定義的有益位置。\r\n• 若找不到，且內容為討伐殲滅戰或大型任務，則使用備援點（例如 0,0 或 100,100，通常是場地中心）。\r\n• 選擇離玩家最近的位置，加入少量隨機偏移，並確認是否在效果範圍內。\r\n• 若符合，就將該處作為目標區域。\r\n3. 頭目身位備援：\r\n• 若目前目標為具有身位要求且在射程內的頭目，則使用頭目位置（或射程內的一點）作為目標區域。\r\n4. 小隊成員備援：\r\n• 收集射程加效果範圍內的小隊成員。\r\n• 嘗試找出正受到攻擊的小隊成員（坦克或焦點目標）。\r\n• 若找到，依距離與效果範圍判斷應留在玩家位置或靠近坦克。\r\n• 若找不到或不需要，預設使用玩家位置。", Filter = HealingActionCondition, Section = 3)]
    private static readonly bool _useGroundBeneficialAbility = true;

    /// <markdown file="Auto" name="Use beneficial ground-targeted actions when moving" section="Healing Usage and Control">
    /// Enable to allow the usage of ground AoE actions while moving, such as Earthly Star (AST), Sacred Soil (SCH), etc.
    /// </markdown>
    [JobConfig, UI("移動時使用有益的地面指定技能", Parent = nameof(UseGroundBeneficialAbility))]
    private static readonly bool _useGroundBeneficialAbilityWhenMoving = false;

    [JobConfig, UI("有益的地面指定技能只對自身使用，略過其他邏輯", Parent = nameof(UseGroundBeneficialAbility))]
    private static readonly bool _useGroundBeneficialAbilityOnlySelf = false;

    [ConditionBool, UI("顯示冷卻時間視窗", Filter = UiWindows)]
    private static readonly bool _showCooldownWindow = false;

    [ConditionBool, UI("顯示技能時間軸視窗", Filter = UiWindows)]
    private static readonly bool _showActionTimelineWindow = false;

    [ConditionBool, UI("僅在戰鬥中顯示時間軸", Parent = nameof(ShowActionTimelineWindow))]
    private static readonly bool _actionTimelineOnlyInCombat = true;

    [ConditionBool, UI("僅在 RSR 啟用時顯示時間軸", Parent = nameof(ShowActionTimelineWindow))]
    private static readonly bool _actionTimelineOnlyWhenActive = true;

    [ConditionBool, UI("在時間軸顯示能力技", Parent = nameof(ShowActionTimelineWindow))]
    private static readonly bool _actionTimelineShowOGCD = true;

    [ConditionBool, UI("在時間軸顯示自動攻擊", Parent = nameof(ShowActionTimelineWindow))]
    private static readonly bool _actionTimelineShowAutoAttack = false;

    [ConditionBool, UI("戰鬥後將時間軸儲存為 JSON 檔案", Parent = nameof(ShowActionTimelineWindow))]
    private static readonly bool _actionTimelineSaveToFile = false;

    [ConditionBool, UI("記錄範圍技能", Filter = List)]
    private static readonly bool _recordCastingArea = true;

    [ConditionBool, UI("戰鬥結束超過指定時間後自動關閉 RSR：",
        Filter = BasicAutoSwitch)]
    private static readonly bool _autoOffAfterCombat = true;

    [ConditionBool, UI("在主選單啟用 RSR 點擊計數器",
        Filter = Extra)]
    private static readonly bool _enableClickingCount = true;

    [ConditionBool, UI("隱藏所有警告",
        Filter = UiInformation)]
    private static readonly bool _hideWarning = false;

    /// <markdown file="Auto" name="Only heal self when not a Healer" section="Healing Usage and Control">
    /// When enabled and not a healer, skills that can target someone and heal will only target you.
    /// </markdown>
    [ConditionBool, UI("使用非治療職業時只治療自己",
        Filter = HealingActionCondition, Section = 1,
        PvPFilter = JobFilterType.NoHealer, PvEFilter = JobFilterType.NoHealer)]
    private static readonly bool _onlyHealSelfWhenNoHealer = false;

    [ConditionBool, UI("小隊中沒有存活的治療職業時，非治療職業才使用治療技能",
    Description = "啟用後，非治療職業（如進攻或防護職業）只有在小隊中沒有治療職業，或所有治療職業皆無法戰鬥（HP 為 0）時才會使用治療技能。\r\n只要仍有一名治療職業存活，非治療職業就不會使用治療技能。",
    Filter = HealingActionCondition, Section = 1,
    PvPFilter = JobFilterType.NoHealer, PvEFilter = JobFilterType.NoHealer)]
    private static readonly bool _onlyHealAsNonHealIfNoHealers = false;

    [ConditionBool, UI("在聊天視窗顯示切換的設定與新值",
        Filter = UiInformation)]
    private static readonly bool _ShowToggledSettingInChat = false;

    [ConditionBool, UI("記錄擊退技能", Filter = List2)]
    private static readonly bool _recordKnockbackies = false;

    [ConditionBool, UI("自動設定青魔法", Description = "使用青魔法師循環時，RSR 可自動將魔法書設定為該循環所需的技能。", Filter = Extra)]
    private static readonly bool _setBluActions2 = false;

    #region Float
    [UI("戰鬥結束超過指定時間後自動關閉 RSR……",
        Parent = nameof(AutoOffAfterCombat))]
    [Range(0, 600, ConfigUnitType.Seconds)]
    public float AutoOffAfterCombatTime { get; set; } = 30;

    [UI("視野錐形的角度", Parent = nameof(OnlyAttackInVisionCone))]
    [Range(0, 90, ConfigUnitType.Degree, 0.02f)]
    public float AngleOfVisionCone { get; set; } = 45;

    /// <markdown file="Auto" name="Melee Range action using offset" section="Action Usage and Control">
    /// Additional buffer in yalms where ranged attacks will be used for melee classes. For example,
    /// if you are playing as Samurai and this setting is set to `1`, Enpi will only be used starting
    /// at 4 yalms.
    ///
    /// This is because the default "overflow" for the melee range is 3 yalms, which means you can cast
    /// melee attacks 3 yalms outside the enemy's hitbox. This setting (the offset) takes max melee range and
    /// adds the value you set.
    ///
    /// This setting exists to leave you time to approach and enter in melee range of the enemy without wasting
    /// a GCD.
    /// </markdown>
    [UI("近戰範圍技能使用偏移距離",
        Filter = AutoActionUsage, Section = 3,
        PvEFilter = JobFilterType.Melee, PvPFilter = JobFilterType.Melee)]
    [Range(0, 5, ConfigUnitType.Yalms, 0.02f)]
    public float MeleeRangeOffset { get; set; } = 1;

    [UI("最低 HP 低於此值時治療", Parent = nameof(HealWhenNothingTodo))]
    [Range(0, 1, ConfigUnitType.Percent, 0.002f)]
    public float HealWhenNothingTodoBelow { get; set; } = 0.8f;

    [UI("坦克 HP 低於此值時優先治療",
        Filter = HealingActionCondition, Section = 1,
        PvEFilter = JobFilterType.Healer, PvPFilter = JobFilterType.Healer)]
    [Range(0, 1, ConfigUnitType.Percent, 0.02f)]
    public float HealthTankRatio { get; set; } = 0.45f;

    [UI("治療職業 HP 低於此值時優先治療",
        Filter = HealingActionCondition, Section = 1,
        PvEFilter = JobFilterType.Healer, PvPFilter = JobFilterType.Healer)]
    [Range(0, 1, ConfigUnitType.Percent, 0.02f)]
    public float HealthHealerRatio { get; set; } = 0.4f;

    [UI("自身 HP 低於此值時優先治療",
        Filter = HealingActionCondition, Section = 1,
        PvEFilter = JobFilterType.Healer, PvPFilter = JobFilterType.Healer)]
    [Range(0, 1, ConfigUnitType.Percent, 0.02f)]
    public float HealthSelfRatio { get; set; } = 0.4f;

    #region
    [JobConfig, UI("優先復活倒地玩家，而非治療／防禦",
        Filter = HealingActionCondition, Section = 2)]
    private static readonly bool _raisePlayerFirst = false;

    [JobConfig, UI("可用時以即刻詠唱／連續詠唱復活玩家", Description = "停用後，絕不會使用即刻詠唱／連續詠唱復活玩家。",
        Filter = HealingActionCondition, Section = 2,
        PvEFilter = JobFilterType.Raise, PvPFilter = JobFilterType.NoJob)]
    private static readonly bool _raisePlayerBySwift = true;

    [JobConfig, UI("讀條復活邏輯",
        Filter = HealingActionCondition, Section = 2)]
    private readonly HardCastRaiseType _HardCastRaiseType = HardCastRaiseType.HardCastNormal;

    [JobConfig, UI("復活對象",
        Filter = HealingActionCondition, Section = 2)]
    private readonly RaiseType _RaiseType = RaiseType.PartyOnly;

    [JobConfig, UI("復活具有瀕死狀態的玩家",
        Filter = HealingActionCondition, Section = 2)]
    private static readonly bool _raiseBrinkOfDeath = true;

    [JobConfig, UI("由小隊清單底部往上復活非治療職業（輕型小隊雙治療行為，實驗性）",
        Filter = HealingActionCondition, Section = 2)]
    private static readonly bool _h2 = false;

    [JobConfig, UI("沒有防護或治療職業倒地時，優先復活赤魔法師與召喚師（實驗性）",
        Filter = HealingActionCondition, Section = 2)]
    private static readonly bool _offRaiserRaise = false;

    #endregion

    /// <markdown file="Auto" name="How early before next GCD should RSR use swiftcast for raise" section="Healing Usage and Control">
    /// If your cast a GCD and your cooldown is of 2.5 seconds, if a teammate dies when your cooldown starts, the Swiftcast action will wait
    /// the specified amount of time before your cooldown ends to cast Swiftcast. This is to prevent using your Swiftcast too early and waste it
    /// if your co-healer manages to raise your target within your global cooldown period.
    /// </markdown>
    [JobConfig, UI("下一個 GCD 前多久讓 RSR 使用即刻詠唱復活（實驗性）",
        Filter = HealingActionCondition, Section = 2,
        PvEFilter = JobFilterType.Raise, PvPFilter = JobFilterType.NoJob)]
    [Range(0, 1.0f, ConfigUnitType.Seconds, 0.01f)]
    public float SwiftcastBuffer { get; set; } = 0.6f;

    /// <markdown file="Auto" name="Random delay range for resurrecting players" section="Healing Usage and Control">
    /// In order to not make is so obvious that you use RSR, casting a raise action will be delayed by a random amount of seconds
    /// between the two values.
    /// </markdown>
    [UI("復活玩家的隨機延遲範圍",
        Filter = HealingActionCondition, Section = 2,
        PvEFilter = JobFilterType.Raise, PvPFilter = JobFilterType.NoJob)]
    [Range(0, 10, ConfigUnitType.Seconds, 0.002f)]
    public Vector2 RaiseDelay2 { get; set; } = new(3f, 3f);

    [UI("解除狀態的隨機延遲範圍",
        Filter = HealingActionCondition, Section = 2,
        PvEFilter = JobFilterType.Dispel, PvPFilter = JobFilterType.NoJob)]
    [Range(0, 10, ConfigUnitType.Seconds, 0.002f)]
    public Vector2 EsunaDelay { get; set; } = new(0f, 0f);

    [Range(0, 10000, ConfigUnitType.None, 100)]
    [UI("MP 低於此值時絕不復活玩家",
        Filter = HealingActionCondition, Section = 2,
        PvEFilter = JobFilterType.Raise, PvPFilter = JobFilterType.NoJob)]
    public int LessMPNoRaise { get; set; } = 2400;

    /// <markdown file="Extra" name="HP standard deviation for using AoE heal">
    /// Controls how much party members' HP must differ before using AoE healing instead
    /// of single-target heals. Lower values require party members to have more similar HP
    /// for AoE healing to trigger (more selective). Higher values allow AoE healing even
    /// when HP differences are larger (less selective). Adjust only if you want to fine-tune
    /// AoE heal behavior.
    /// </markdown>
    [UI("使用範圍治療的 HP 標準差", Description = "控制小隊成員 HP 差異多大時改用範圍治療，而非單體治療。數值越低，成員 HP 必須越接近才會觸發範圍治療（較嚴格）；數值越高，即使 HP 差異較大也可使用範圍治療（較寬鬆）。只在需要微調範圍治療行為時變更。",
    Filter = Extra)]
    [Range(0, 0.5f, ConfigUnitType.Percent, 0.02f)]
    public float HealthDifference { get; set; } = 0.25f;

    [ConditionBool, UI("非戰鬥狀態時治療小隊成員",
        Filter = HealingActionCondition, Section = 3)]
    private static readonly bool _healOutOfCombat = false;

    [ConditionBool, UI("治療單人任務中的 NPC（僅於需要時啟用）", Description = "實驗性功能。",
        Filter = HealingActionCondition, Section = 3)]
    private static readonly bool _friendlyBattleNPCHeal = false;

    [ConditionBool, UI("治療及復活小隊 NPC",
        Filter = HealingActionCondition, Section = 3)]
    private static readonly bool _friendlyPartyNPCHealRaise3 = true;

    [ConditionBool, UI("治療陸行鳥／將陸行鳥設為舞伴", Description = "實驗性功能。",
        Filter = HealingActionCondition, Section = 3)]
    private static readonly bool _chocoboPartyMember = false;

    [ConditionBool, UI("在大型任務中將焦點目標玩家視為小隊成員", Description = "實驗性功能，包含滅。",
        Filter = HealingActionCondition, Section = 3)]
    private static readonly bool _focusTargetIsParty = false;

    [ConditionBool, UI("戰鬥中無事可做時，以 GCD 治療小隊成員",
        Filter = HealingActionCondition, Section = 3)]
    private static readonly bool _healWhenNothingTodo = true;

    [ConditionBool, UI("坦克死刑優先選擇低 HP 坦克",
        Filter = HealingActionCondition, Section = 3)]
    private static readonly bool _priolowtank = false;

    /// <markdown file="Basic" name="The duration of special windows opened by /rotation commands by default">
    /// The duration of special windows opened by /rotation commands by default.
    /// (Found in Main => Macros)
    /// </markdown>
    [UI("由 /rotation 指令開啟之特殊視窗的預設持續時間",
        Filter = BasicTimer, Section = 1)]
    [Range(1, 20, ConfigUnitType.Seconds, 1f)]
    public float SpecialDuration { get; set; } = 3;

    [UI("目標死亡或免疫傷害時，RSR 停止攻擊的隨機延遲範圍",
        Parent = nameof(UseStopCasting))]
    [Range(0, 3, ConfigUnitType.Seconds, 0.002f)]
    public Vector2 StopCastingDelay { get; set; } = new(0.5f, 1);

    [UI("打斷敵對目標前的隨機延遲範圍",
        Filter = AutoActionUsage, Section = 3,
        PvEFilter = JobFilterType.Interrupt, PvPFilter = JobFilterType.NoJob)]
    [Range(0, 3, ConfigUnitType.Seconds, 0.002f)]
    public Vector2 InterruptDelay { get; set; } = new(0.5f, 1);

    [UI("挑釁的隨機延遲範圍", Parent = nameof(AutoProvokeForTank))]
    [Range(0, 10, ConfigUnitType.Seconds, 0.05f)]
    public Vector2 ProvokeDelay { get; set; } = new(0.5f, 1);

    [UI("非戰鬥狀態的隨機延遲範圍",
        Filter = BasicParams)]
    [Range(0, 10, ConfigUnitType.Seconds, 0.002f)]
    public Vector2 NotInCombatDelay { get; set; } = new(2, 3);

    /// <markdown file="Basic" name="Clicking actions random delay range">
    /// Delay between showing the clicking/pressing effect on the actions
    /// in your hotbars.
    /// </markdown>
    [UI("點擊技能的隨機延遲範圍",
        Filter = BasicTimer)]
    [Range(0.00f, 0.25f, ConfigUnitType.Seconds, 0.002f)]
    public Vector2 ClickingDelay { get; set; } = new(0.1f, 0.2f);

    [UI("空檔治療的延遲範圍", Parent = nameof(HealWhenNothingTodo))]
    [Range(0, 5, ConfigUnitType.Seconds, 0.05f)]
    public Vector2 HealWhenNothingTodoDelay { get; set; } = new(0.5f, 1);

    /// <markdown file="Basic" name="How soon before countdown is finished to start casting or attacking">
    /// How soon before countdown is finished to start casting or attacking.
    /// </markdown>
    [UI("倒數結束前多久開始詠唱或攻擊",
        Filter = BasicTimer, Section = 1, PvPFilter = JobFilterType.NoJob)]
    [Range(0, 0.7f, ConfigUnitType.Seconds, 0.002f)]
    public float CountDownAhead { get; set; } = 0.4f;

    [UI("冷卻時間視窗圖示大小")]
    [Range(0, 80, ConfigUnitType.Pixels, 0.2f)]
    public float CooldownWindowIconSize { get; set; } = 30;

    [UI("下一個技能的大小比例", Parent = nameof(ShowControlWindow))]
    [Range(0, 10, ConfigUnitType.Percent, 0.02f)]
    public float ControlWindowNextSizeRatio { get; set; } = 1.5f;

    [UI("GCD 圖示大小", Parent = nameof(ShowControlWindow))]
    [Range(0, 80, ConfigUnitType.Pixels, 0.2f)]
    public float ControlWindowGCDSize { get; set; } = 40;

    [UI("能力技圖示大小", Parent = nameof(ShowControlWindow))]
    [Range(0, 80, ConfigUnitType.Pixels, 0.2f)]
    public float ControlWindow0GCDSize { get; set; } = 30;

    [UI("控制進度條高度")]
    [Range(2, 30, ConfigUnitType.Yalms)]
    public float ControlProgressHeight { get; set; } = 8;

    [UI("預估擊殺時間低於此值時停止治療：", Parent = nameof(UseHealWhenNotAHealer))]
    [Range(0, 30, ConfigUnitType.Seconds, 0.02f)]
    public float AutoHealTimeToKill { get; set; } = 8f;

    [UI("RSR 資訊更新的最短間隔（提高可改善幀率問題，但可能影響循環效能）",
    Filter = BasicTimer)]
    [JobConfig, Range(0, 0.3f, ConfigUnitType.Seconds, 0.002f)]
    public float MinUpdatingTime { get; set; } = 0.00f;

    /// <markdown file="Basic" name="Action Ahead">
    /// Percent of your GCD time remaining on a GCD cycle before RSR will try to queue the next GCD.
    ///
    /// This setting controls how many oGCDs RSR will try to fit in a single GCD window.
    /// Lower numbers mean more oGCDs, but potentially more GCD clipping.
    /// </markdown>
    [JobConfig, Range(0.05f, 0.25f, ConfigUnitType.Percent)]
    [UI("技能預先佇列（GCD 循環剩餘多少比例時，RSR 嘗試將下一個 GCD 加入佇列）", Filter = BasicTimer,
    Description = "此設定控制 RSR 會嘗試在單一 GCD 間隔內安排多少能力技。\n數值越低，能力技越多，但更可能造成 GCD 卡頓。")]
    private readonly float _action6head = 0.25f;

    /// <summary>
    /// Remove extra lag-induced animation lock delay from instant casts (read tooltip!)
    /// Do NOT use with XivAlexander or NoClippy - this should automatically disable itself if they are detected, but double check first!
    /// </summary>
    [ConditionBool, UI("移除延遲造成的瞬發技能額外動畫鎖定（請閱讀提示！）",
    Description = "請勿與 XivAlexander、BMR tweaks 或 NoClippy 同時使用。偵測到這些插件時理應自動停用，但請務必再次確認！",
    Filter = Extra)]
    private static readonly bool _removeAnimationLockDelay = false;

    /// <summary>
    /// Animation lock max. simulated delay in milliseconds
    /// Configures the maximum simulated delay in milliseconds when using animation lock removal - this is required and cannot be reduced to zero.
    /// Setting this to 20ms will enable triple-weaving when using autorotation. The minimum setting to remove triple-weaving is 26ms.
    /// The minimum of 20ms has been accepted by FFLogs and should not cause issues with your logs.
    /// </summary>
    [UI("動畫鎖定的最大模擬延遲（請閱讀提示！）",
    Description = "設定移除動畫鎖定時的最大模擬延遲（毫秒）。此值為必要設定，不能降為零。設為 20ms 會讓自動循環可三插；避免三插的最低值為 26ms。FFLogs 已接受最低 20ms，理論上不會影響戰鬥記錄。",
    Parent = nameof(RemoveAnimationLockDelay), Filter = Extra)]
    [Range(26, 50, ConfigUnitType.None, 1f)]
    public int AnimationLockDelayMax2 { get; set; } = 26;

    /// <summary>
    /// Remove extra framerate-induced cooldown delay
    /// Dynamically adjusts cooldown and animation locks to ensure queued actions resolve immediately regardless of framerate limitations
    /// </summary>
    [ConditionBool, UI("移除幀率造成的額外冷卻延遲",
    Description = "動態調整冷卻與動畫鎖定，使佇列中的技能不受幀率限制並立即生效。",
    Filter = Extra)]
    private static readonly bool _removeCooldownDelay = false;

    // Cactbot timeline integration
    [ConditionBool, UI("啟用 cactbot 時間軸整合（高度實驗性）",
        Description = "連線至 OverlayPlugin 的 WebSocket 伺服器，並回應 cactbot 廣播事件（全體攻擊、坦克死刑、擊退、空檔／無法選取）。",
        Filter = Extra)]
    private static readonly bool _enableCactbotTimeline = false;

    [ConditionBool, UI("顯示 cactbot 事件通知（偵錯）",
    Description = "收到 cactbot 廣播並對應至 RotationSolver 特殊狀態時顯示通知。",
    Filter = Extra)]
    private static readonly bool _showCactbotToasts = false;

    [JobConfig, UI("使用防禦的 HP 門檻",
        Filter = PvPSpecificControls)]
    [Range(0, 1, ConfigUnitType.Percent, 0.02f)]
    public float HealthForGuard { get; set; } = 0.15f;

    [UI("醒目提示顏色", Parent = nameof(TeachingMode))]
    public Vector4 TeachingModeColor { get; set; } = new(0f, 1f, 0f, 1f);

    [UI("目標顏色", Parent = nameof(TargetColor))]
    public Vector4 TargetColor { get; set; } = new(1f, 0.2f, 0f, 0.8f);

    [UI("控制視窗鎖定時的背景", Parent = nameof(ShowControlWindow))]
    public Vector4 ControlWindowLockBg { get; set; } = new(0, 0, 0, 0.55f);

    [UI("控制視窗未鎖定時的背景", Parent = nameof(ShowControlWindow))]
    public Vector4 ControlWindowUnlockBg { get; set; } = new(0, 0, 0, 0.75f);

    [UI("資訊視窗背景", Filter = UiWindows)]
    public Vector4 InfoWindowBg { get; set; } = new(0, 0, 0, 0.4f);
    #endregion

    #region Target
    [ConditionBool, UI("對畫面中央的物件／敵人使用移動技能",
    Description = "啟用後，移動技能會選擇畫面中央的物件或敵人；停用後，則選擇角色面向的物件或敵人。",
    Filter = TargetConfig, Section = 2)]
    private static readonly bool _moveTowardsScreenCenter = false;

    [ConditionBool, UI("優先選擇帶有攻擊標記的敵人／物件",
    Description = "使用技能時優先選擇帶有攻擊標記的目標。",
    Filter = TargetConfig)]
    private static readonly bool _chooseAttackMark = true;

    [ConditionBool, UI("優先選擇敵人部位",
    Description = "優先選擇如泰坦之心等敵人部位。",
    Filter = TargetConfig)]
    private static readonly bool _prioEnemyParts = true;

    [ConditionBool, UI("絕不攻擊帶有禁止標記的目標",
    Description = "不會攻擊帶有禁止標記的目標。",
    Filter = TargetConfig)]
    private static readonly bool _filterStopMark = true;

    [ConditionBool, UI("將 1 HP 目標視為無敵",
    Description = "只有 1 HP 的目標會視為無敵並略過，用於目標無敵卻沒有對應狀態的少數情況。",
    Filter = TargetConfig)]
    private static readonly bool _filterOneHPInvincible = true;

    [ConditionBool, UI("危命任務中忽略非危命目標，危命任務外忽略危命目標",
    Description = "進行危命任務時只考慮危命目標；未進行危命任務時忽略危命目標。",
    Filter = TargetConfig)]
    private static readonly bool _ignoreNonFateInFate = true;

    [ConditionBool, UI("範圍移動技能選擇最遠的位置",
        Filter = TargetConfig, Section = 2)]
    private static readonly bool _moveAreaActionFarthest = false;

    [ConditionBool, UI("使用敵對技能時硬選取敵人", Description = "停用後，RSR 只會為治療、護盾等技能軟選取友方目標。",
        Filter = TargetConfig, Section = 3)]
    private static readonly bool _switchTargetFriendly = false;

    [ConditionBool, UI("附近沒有有效技能目標且未選取目標時，選取最近的可選敵人",
        Filter = TargetConfig, Section = 3)]
    private static readonly bool _targetFreely = false;

    [ConditionBool, UI("只攻擊畫面內的目標",
        Filter = TargetConfig, Section = 1)]
    private static readonly bool _onlyAttackInView = false;

    [ConditionBool, UI("只攻擊視野錐形內的目標",
                Filter = TargetConfig, Section = 1)]
    private static readonly bool _onlyAttackInVisionCone = false;

    [ConditionBool, UI("優先選擇怪物狩獵／古武／理符目標（古武行為有問題）",
        Filter = TargetConfig, Section = 1)]
    private static readonly bool _targetHuntingRelicLevePriority = true;

    [ConditionBool, UI("優先選擇任務目標（覆寫交戰設定）",
        Filter = TargetConfig, Section = 1)]
    private static readonly bool _targetQuestPriority = true;

    [ConditionBool, UI("禁止選擇屬於其他玩家的任務敵人（已損壞）",
        Filter = TargetConfig, Section = 1)]
    private static readonly bool targetQuestThings = true;

    [ConditionBool, UI("出現迷失少女時忽略其他危命目標（實驗性）",
        Filter = TargetConfig, Section = 1)]
    private static readonly bool forlornPriority = true;

    [ConditionBool, UI("忽略木人",
               Filter = TargetConfig, Section = 1)]
    private static readonly bool _disableTargetDummys = false;

    [ConditionBool, UI("優先選擇危命任務目標",
        Filter = TargetConfig, Section = 1)]
    private static readonly bool _targetFatePriority = true;

    [UI("鎖定仇恨中或新目標並開始攻擊前的延遲範圍", Description = "請勿設得太低，否則可能從坦克手中搶走剛建立仇恨的副本敵人。",
        Filter = TargetConfig)]
    [Range(0, 3, ConfigUnitType.Seconds)]
    public Vector2 TargetDelay { get; set; } = new(1, 2);

    [UI("可選為移動目標的扇形角度大小",
        Description = "選取模式以角色面向為準時，角色視野內的目標可作為移動目標。\n選取模式以畫面中央為準時，角色位置朝畫面上方所形成扇形內的目標可作為移動目標。",
        Filter = TargetConfig, Section = 2)]
    [Range(0, 90, ConfigUnitType.Degree, 0.02f)]
    public float MoveTargetAngle { get; set; } = 24;

    [ConditionBool, UI("將木人視為頭目",
        Filter = TargetConfig, Section = 3)]
    private static readonly bool _dummyBoss = true;

    [UI("目標的預估擊殺時間高於此值時視為頭目",
        Filter = TargetConfig, Section = 1)]
    [Range(10, 1800, ConfigUnitType.Seconds, 0.02f)]
    public float BossTimeToKill { get; set; } = 90;

    [UI("目標的預估擊殺時間低於此值時視為即將死亡",
                Filter = TargetConfig, Section = 1)]
    [Range(0, 60, ConfigUnitType.Seconds, 0.02f)]
    public float DyingTimeToKill { get; set; } = 10;

    [UI("目標 HP 比例低於此值時視為即將死亡",
                Filter = TargetConfig, Section = 1)]
    [Range(0, 0.1f, ConfigUnitType.Percent, 0.01f)]
    public float IsDyingConfig { get; set; } = 0.02f;

    [ConditionBool, UI("使用小型目標模式且有多個小型目標時，優先選擇低 HP 而非高 HP 目標",
        Filter = TargetConfig)]
    private static readonly bool _smallHP = false;

    [ConditionBool, UI("使用大型目標模式且有多個大型目標時，優先選擇低 HP 而非高 HP 目標",
        Filter = TargetConfig)]
    private static readonly bool _bigHP = false;

    [UI("/rotation Cycle 行為", Filter = TargetConfig)]
    public CycleType CycleType { get; set; } = CycleType.CycleNormal;

    [JobConfig, UI("交戰設定", Filter = TargetConfig, PvPFilter = JobFilterType.NoJob)]
    private readonly TargetHostileType _hostileType = TargetHostileType.AllTargetsWhenSoloInDuty;
    #endregion

    #region Integer

    public int ActionSequencerIndex { get; set; }

    [UI("暫時解除移動鎖定的輔助鍵", Description = "RB 適用於控制器玩家。", Parent = nameof(PoslockCasting))]
    public ConsoleModifiers PoslockModifier { get; set; }

    [Range(0, 5, ConfigUnitType.None, 1)]
    [UI("每個技能模擬按鍵次數的隨機範圍", Parent = nameof(KeyBoardNoise))]
    public Vector2Int KeyboardNoise { get; set; } = new(2, 3);

    [Range(0, 10, ConfigUnitType.None)]
    public int TargetingIndex { get; set; }

    [UI("敵人數量", Parent = nameof(UseDefenseAbility),
        PvEFilter = JobFilterType.Tank)]
    [Range(1, 8, ConfigUnitType.None, 0.05f)]
    public int AutoDefenseNumber { get; set; } = 2;

    #endregion

    #region PvP
    [ConditionBool, UI("PvP 中忽略預估擊殺時間", Filter = PvPSpecificControls)]
    private static readonly bool _ignorePvPTTK = true;

    [ConditionBool, UI("碎冰戰中優先選擇 A 級冰之巫魂", Filter = PvPSpecificControls)]
    private static readonly bool _prioAtomelith = false;

    [ConditionBool, UI("碎冰戰中優先選擇 B 級冰之巫魂", Filter = PvPSpecificControls)]
    private static readonly bool _prioBtomelith = false;

    [JobConfig, UI("PvP 中忽略無敵判定", Filter = PvPSpecificControls)]
    private static readonly bool _ignorePvPInvincibility = false;

    [ConditionBool, UI("PvP 中倒地時自動停用",
         Filter = PvPSpecificControls)]
    private static readonly bool _autoOffWhenDeadPvP = true;

    [ConditionBool, UI("PvP 對戰結束時自動停用",
         Filter = PvPSpecificControls)]
    private static readonly bool _autoOffPvPMatchEnd = true;

    [ConditionBool, UI("PvP 對戰開始時自動啟用",
         Filter = PvPSpecificControls)]
    private static readonly bool _autoOnPvPMatchStart = true;

    #endregion

    #region Jobs

    [JobConfig, UI("使用醒夢的 MP 門檻", Filter = HealingActionCondition)]
    [Range(0, 10000, ConfigUnitType.None)]
    private readonly int _lucidDreamingMpThreshold = 6000;

    /// <markdown file="Auto" name="HP threshold for AoE healing oGCDs (Heal-over-time)" section="Healing Usage and Control" subsection="RotationSolver.Basic.Configuration.Configs._autoHeal">
    /// Relates to **oGCD** AoE healing abilities **when they have a heal-over-time already applied to them, by you or a teammate**.
    /// 
    /// This is calculated with the <see cref="RotationSolver.Basic.Configuration.Configs.HealthDifference">standard AoE healing deviation</see>.
    ///
    /// Use the button this setting to calculate your preferred values.
    /// </markdown>
    [JobConfig, Range(0, 1, ConfigUnitType.Percent)]
    private readonly float _healthAreaAbilityHot = 0.55f;

    /// <markdown file="Auto" name="HP threshold for AoE healing GCDs (Heal-over-time)" section="Healing Usage and Control" subsection="RotationSolver.Basic.Configuration.Configs._autoHeal">
    /// Relates to **GCD** AoE healing abilities **when they have a heal-over-time already applied to them, by you or a teammate**.
    /// 
    /// This is calculated with the <see cref="RotationSolver.Basic.Configuration.Configs.HealthDifference">standard AoE healing deviation</see>.
    ///
    /// Use the button this setting to calculate your preferred values.
    /// </markdown>
    [JobConfig, Range(0, 1, ConfigUnitType.Percent)]
    private readonly float _healthAreaSpellHot = 0.55f;

    /// <markdown file="Auto" name="HP threshold for AoE healing GCDs (No heal-over-time)" section="Healing Usage and Control" subsection="RotationSolver.Basic.Configuration.Configs._autoHeal">
    /// Relates to **oGCD** AoE healing abilities.
    /// 
    /// This is calculated with the <see cref="RotationSolver.Basic.Configuration.Configs.HealthDifference">standard AoE healing deviation</see>.
    ///
    /// Use the button this setting to calculate your preferred values.
    /// </markdown> 
    [JobConfig, Range(0, 1, ConfigUnitType.Percent)]
    private readonly float _healthAreaAbility = 0.75f;

    /// <markdown file="Auto" name="HP threshold for AoE healing GCDs (No heal-over-time)" section="Healing Usage and Control" subsection="RotationSolver.Basic.Configuration.Configs._autoHeal">
    /// Relates to **GCD** AoE healing abilities.
    /// 
    /// This is calculated with the <see cref="RotationSolver.Basic.Configuration.Configs.HealthDifference">standard AoE healing deviation</see>.
    ///
    /// Use the button this setting to calculate your preferred values.
    /// </markdown> 
    [JobConfig, Range(0, 1, ConfigUnitType.Percent)]
    private readonly float _healthAreaSpell = 0.65f;

    /// <markdown file="Auto" name="HP threshold for single-target healing oGCDs (Heal-over-time)" section="Healing Usage and Control" subsection="RotationSolver.Basic.Configuration.Configs._autoHeal">
    /// Relates to **oGCD** single-target healing abilities **when they have a heal-over-time already applied to them, by you or a teammate**.
    /// </markdown>
    [JobConfig, Range(0, 1, ConfigUnitType.Percent)]
    private readonly float _healthSingleAbilityHot = 0.65f;

    /// <markdown file="Auto" name="HP threshold for single-target healing GCDs (Heal-over-time)" section="Healing Usage and Control" subsection="RotationSolver.Basic.Configuration.Configs._autoHeal">
    /// Relates to **GCD** single-target healing abilities **when they have a heal-over-time already applied to them, by you or a teammate**.
    /// </markdown>
    [JobConfig, Range(0, 1, ConfigUnitType.Percent)]
    private readonly float _healthSingleSpellHot = 0.55f;

    /// <markdown file="Auto" name="HP threshold for single-target healing oGCDs (No Heal-over-time)" section="Healing Usage and Control" subsection="RotationSolver.Basic.Configuration.Configs._autoHeal">
    /// Relates to **oGCD** AoE healing abilities.
    /// </markdown>
    [JobConfig, Range(0, 1, ConfigUnitType.Percent)]
    private readonly float _healthSingleAbility = 0.7f;

    /// <markdown file="Auto" name="HP threshold for single-target healing GCDs (No Heal-over-time)" section="Healing Usage and Control" subsection="RotationSolver.Basic.Configuration.Configs._autoHeal">
    /// Relates to **GCD** single-target healing abilities.
    /// </markdown>
    [JobConfig, Range(0, 1, ConfigUnitType.Percent)]
    private readonly float _healthSingleSpell = 0.65f;

    /// <markdown file="Auto" name="The HP% for tank to use invulnerability" section="Action Usage and Control">
    /// The threshold to automatically use your tank invulnerability action when falling below the HP percentage set.
    /// </markdown>
    [JobConfig, Range(0, 1, ConfigUnitType.Percent, 0.02f)]
    [UI("坦克使用無敵技能的 HP%% 門檻",
        Filter = AutoActionUsage, Section = 3,
        PvEFilter = JobFilterType.Tank, PvPFilter = JobFilterType.NoJob)]
    private readonly float _healthForDyingTanks = 0.15f;

    [JobConfig, Range(0, 1, ConfigUnitType.Percent, 0.02f)]
    [UI("坦克使用單體／自身減傷所需的 HP%%", Parent = nameof(UseDefenseAbility),
        PvEFilter = JobFilterType.Tank)]
    private readonly float _healthForAutoDefense = 1;

    [JobConfig]
    private readonly string _PvPRotationChoice = string.Empty;

    [JobConfig]
    private readonly string _rotationChoice = string.Empty;
    #endregion

    [JobConfig]
    private readonly ConcurrentDictionary<uint, ActionConfig> _rotationActionConfig = new();

    [JobConfig]
    private readonly ConcurrentDictionary<uint, ItemConfig> _rotationItemConfig = new();

    [JobChoiceConfig]
    private readonly ConcurrentDictionary<string, string> _rotationConfigurations = new();

    public ConcurrentDictionary<uint, string> DutyRotationChoice = new();

    public void Save()
    {
#if DEBUG
        PluginLog.Information("Saved configurations.");
#endif
        File.WriteAllText(Svc.PluginInterface.ConfigFile.FullName,
            JsonConvert.SerializeObject(this, Formatting.Indented));
    }

    public static Configs Migrate(Configs oldConfigs)
    {
        // Implement migration logic if needed
        if (oldConfigs.Version != CurrentVersion)
        {
            // Reset to default if versions do not match
            return new Configs();
        }
        return oldConfigs;
    }

    public void Backup()
    {
        Save();
        File.Copy(Svc.PluginInterface.ConfigFile.FullName, Svc.PluginInterface.ConfigFile.Directory + "\\RotationSolver_Backup.json", true);
        Svc.Toasts.ShowNormal("設定已備份。");
    }

    public void Restore()
    {
        File.Copy(Svc.PluginInterface.ConfigFile.FullName, Svc.PluginInterface.ConfigFile.Directory + "\\RotationSolver_SafetySave.json", true);
        File.Copy(Svc.PluginInterface.ConfigFile.Directory + "\\RotationSolver_Backup.json", Svc.PluginInterface.ConfigFile.FullName, true);

        Configs restoredConfigs = JsonConvert.DeserializeObject<Configs>(
                                      File.ReadAllText(Svc.PluginInterface.ConfigFile.FullName))
                                  ?? new Configs();

        if (restoredConfigs.Version != CurrentVersion)
        {
            Svc.Toasts.ShowNormal("備份的設定與目前版本不相容。");
            return;
        }

        Service.Config = restoredConfigs;
        Save();
        Svc.Toasts.ShowNormal("設定已還原，將關閉視窗以套用。");
        DataCenter.HoldingRestore = true;
    }
}
