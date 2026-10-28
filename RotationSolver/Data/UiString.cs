using System.ComponentModel;

namespace RotationSolver.Data
{
    internal enum UiString
    {
        [Description("你所選擇的條件值。按一下即可修改。")]
        ConfigWindow_ConditionSetDesc,

        [Description("條件值")]
        ConfigWindow_ConditionSet,

        [Description("技能條件")]
        ConfigWindow_ActionSet,

        [Description("特性條件")]
        ConfigWindow_TraitSet,

        [Description("目標條件")]
        ConfigWindow_TargetSet,

        [Description("循環條件")]
        ConfigWindow_RotationSet,

        [Description("命名條件")]
        ConfigWindow_NamedSet,

        [Description("區域條件")]
        ConfigWindow_Territoryset,

        [Description("尚未載入任何循環！請檢查循環分頁。")]
        ConfigWindow_NoRotation,

        [Description("目前的任務邏輯。")]
        ConfigWindow_DutyRotationDesc,

        [Description("移除")]
        ConfigWindow_List_Remove,

        [Description("從資料夾載入")]
        ActionSequencer_Load,

        [Description("分析每一幀的 PvE 戰鬥資訊並找出最佳技能。")]
        ConfigWindow_About_Punchline,

        [Description("循環無效！\n請更新至最新版本或聯絡 {0}！")]
        ConfigWindow_Rotation_InvalidRotation,

        [Description("按一下切換循環")]
        ConfigWindow_Helper_SwitchRotation,

        [Description("搜尋結果")]
        ConfigWindow_Search_Result,

        [Description("這包含單一戰鬥幀中幾乎所有可用資訊，例如小隊成員狀態、敵對目標狀態、技能冷卻、角色 MP 與 HP、角色位置、敵對目標詠唱狀態、連擊狀態、戰鬥時間與玩家等級等。\n\n接著會在熱鍵欄醒目提示最佳技能，或協助你執行該技能。")]
        ConfigWindow_About_Description,

        [Description("本插件為一般戰鬥設計，並非專門針對零式或絕境戰內容。\n\n請謹慎使用！RSR 雖可在零式或絕境戰中運作，但不會替你處理機制；請自行注意機制並搭配巨集。")]
        ConfigWindow_About_Warning,

        [Description("RSR 已協助你執行技能 {0:N0} 次。")]
        ConfigWindow_About_ClickingCount,

        [Description("狀態巨集")]
        ConfigWindow_About_Macros,

        [Description("技能與設定巨集")]
        ConfigWindow_About_SettingMacros,

        [Description("相容性")]
        ConfigWindow_About_Compatibility,

        [Description("贊助者")]
        ConfigWindow_About_Supporters,

        [Description("連結")]
        ConfigWindow_About_Links,

        [Description("系統警告")]
        ConfigWindow_About_Warnings,

        [Description("警告訊息")]
        ConfigWindow_About_Warnings_Warning,

        [Description("警告時間")]
        ConfigWindow_About_Warnings_Time,

        [Description("Rotation Solver 會協助選擇目標並執行技能。任何會改變這些行為的插件都可能影響判斷。\n\n以下是已知不相容的插件：")]
        ConfigWindow_About_Compatibility_Description,

        [Description("無法正確執行 RSR 預期的行為。")]
        ConfigWindow_About_Compatibility_Mistake,

        [Description("與 RSR 的判斷發生衝突")]
        ConfigWindow_About_Compatibility_Mislead,

        [Description("可能造成遊戲崩潰")]
        ConfigWindow_About_Compatibility_Crash,

        [Description("衷心感謝所有贊助者。")]
        ConfigWindow_About_ThanksToSupporters,

        [Description("開啟設定資料夾")]
        ConfigWindow_About_OpenConfigFolder,

        [Description("說明")]
        ConfigWindow_Rotation_Description,

        [Description("設定")]
        ConfigWindow_Rotation_Configuration,

        [Description("任務設定")]
        ConfigWindow_DutyRotation_Configuration,

        [Description("狀態")]
        ConfigWindow_Rotation_Status,

        [Description("任務循環狀態")]
        ConfigWindow_DutyRotation_Status,

        [Description("用於自訂 RSR 自動使用特定技能的時機。請按左側清單中的技能圖示，再於下方設定該技能的使用條件。每個技能都可設定不同條件，以覆蓋預設循環行為。")]
        ConfigWindow_Actions_Description,

        [Description("顯示於冷卻視窗")]
        ConfigWindow_Actions_ShowOnCDWindow,

        [Description("允許攔截系統攔截此技能")]
        ConfigWindow_Actions_IsIntercepted,

        [Description("使用此技能所需的預估擊殺時間門檻")]
        ConfigWindow_Actions_TTK,

        [Description("使用此技能所需的目標數量")]
        ConfigWindow_Actions_AoeCount,

        [Description("此技能是否檢查自身所需狀態")]
        ConfigWindow_Actions_CheckStatus,

        [Description("此技能是否檢查目標所需狀態")]
        ConfigWindow_Actions_CheckTargetStatus,

        [Description("重新施加持續傷害／狀態前保留的 GCD 數量")]
        ConfigWindow_Actions_GcdCount,

        [Description("自動治療的 HP 比例（僅套用於治療技能）")]
        ConfigWindow_Actions_HealRatio,

        [Description("強制條件的優先度較高。符合強制條件時會忽略停用條件。")]
        ConfigWindow_Actions_ConditionDescription,

        [Description("強制啟用條件")]
        ConfigWindow_Actions_ForcedConditionSet,

        [Description("強制自動使用技能的條件")]
        ConfigWindow_Actions_ForcedConditionSet_Description,

        [Description("強制禁用條件")]
        ConfigWindow_Actions_DisabledConditionSet,

        [Description("停用自動使用技能的條件")]
        ConfigWindow_Actions_DisabledConditionSet_Description,

        [Description("你可以在此視窗設定以清單自訂的參數。")]
        ConfigWindow_List_Description,

        [Description("狀態")]
        ConfigWindow_List_Statuses,

        [Description("技能")]
        ConfigWindow_List_Actions,

        [Description("特定地圖設定")]
        ConfigWindow_List_Territories,

        [Description("狀態名稱或 ID")]
        ConfigWindow_List_StatusNameOrId,

        [Description("無敵")]
        ConfigWindow_List_Invincibility,

        [Description("優先目標")]
        ConfigWindow_List_Priority,

        [Description("可解除的減益狀態")]
        ConfigWindow_List_DangerousStatus,

        [Description("禁止施法的減益狀態")]
        ConfigWindow_List_NoCastingStatus,

        [Description("目標具有其中任一狀態時忽略該目標")]
        ConfigWindow_List_InvincibilityDesc,

        [Description("目標具有其中任一狀態時優先攻擊")]
        ConfigWindow_List_PriorityDesc,

        [Description("可解除的減益狀態清單")]
        ConfigWindow_List_DangerousStatusDesc,

        [Description("自身具有其中任一減益狀態時不使用技能")]
        ConfigWindow_List_NoCastingStatusDesc,

        [Description("複製到剪貼簿")]
        ConfigWindow_Actions_Copy,

        [Description("從剪貼簿匯入")]
        ActionSequencer_FromClipboard,

        [Description("新增狀態")]
        ConfigWindow_List_AddStatus,

        [Description("技能名稱或 ID")]
        ConfigWindow_List_ActionNameOrId,

        [Description("坦克死刑")]
        ConfigWindow_List_HostileCastingTank,

        [Description("範圍攻擊")]
        ConfigWindow_List_HostileCastingArea,

        [Description("擊退")]
        ConfigWindow_List_HostileCastingKnockback,

        [Description("凝視／停止行動")]
        ConfigWindow_List_HostileCastingStop,

        [Description("目標正在詠唱其中任一技能時，使用坦克個人減傷技能")]
        ConfigWindow_List_HostileCastingTankDesc,

        [Description("目標正在詠唱其中任一技能時，使用範圍減傷技能")]
        ConfigWindow_List_HostileCastingAreaDesc,

        [Description("目標正在詠唱其中任一技能時，使用防擊退技能")]
        ConfigWindow_List_HostileCastingKnockbackDesc,

        [Description("敵人正在詠唱此技能時停止詠唱或使用技能")]
        ConfigWindow_List_HostileCastingStopDesc,

        [Description("新增技能")]
        ConfigWindow_List_AddAction,

        [Description("不要選為目標")]
        ConfigWindow_List_NoHostile,

        [Description("不要挑釁")]
        ConfigWindow_List_NoProvoke,

        [Description("有利範圍技能位置")]
        ConfigWindow_List_BeneficialPositions,

        [Description("永遠不會選為目標的敵人")]
        ConfigWindow_List_NoHostileDesc,

        [Description("不想選為目標的敵人名稱")]
        ConfigWindow_List_NoHostilesName,

        [Description("永遠不會挑釁的敵人")]
        ConfigWindow_List_NoProvokeDesc,

        [Description("不想挑釁的敵人名稱")]
        ConfigWindow_List_NoProvokeName,

        [Description("新增有利範圍技能位置")]
        ConfigWindow_List_AddPosition,

        [Description("能力技")]
        ActionAbility,

        [Description("友方")]
        ActionFriendly,

        [Description("攻擊")]
        ActionAttack,

        [Description("一般目標")]
        NormalTargets,

        [Description("具有持續治療的目標")]
        HotTargets,

        [Description("範圍治療能力技的 HP 門檻")]
        HpAoe0Gcd,

        [Description("範圍治療魔法的 HP 門檻")]
        HpAoeGcd,

        [Description("單體治療能力技的 HP 門檻")]
        HpSingle0Gcd,

        [Description("單體治療魔法的 HP 門檻")]
        HpSingleGcd,

        [Description("停止移動")]
        InfoWindowNoMove,

        [Description("移動")]
        InfoWindowMove,

        [Description("搜尋設定")]
        ConfigWindow_Searching,

        [Description("計時器")]
        ConfigWindow_Basic_Timer,

        [Description("自動切換")]
        ConfigWindow_Basic_AutoSwitch,

        [Description("命名條件")]
        ConfigWindow_Basic_NamedConditions,

        [Description("其他")]
        ConfigWindow_Basic_Others,

        [Description("個別技能的動畫鎖定時間，例如 0.6 秒。")]
        ConfigWindow_Basic_AnimationLockTime,

        [Description("執行技能的時間範圍；RSR 會嘗試在此時機執行技能。")]
        ConfigWindow_Basic_ClickingDuration,

        [Description("理想執行時間")]
        ConfigWindow_Basic_IdealClickingTime,

        [Description("實際執行時間")]
        ConfigWindow_Basic_RealClickingTime,

        [Description("自動關閉條件")]
        ConfigWindow_Basic_SwitchCancelConditionSet,

        [Description("自動切換手動模式條件")]
        ConfigWindow_Basic_SwitchManualConditionSet,

        [Description("自動切換自動模式條件")]
        ConfigWindow_Basic_SwitchAutoConditionSet,

        [Description("條件名稱")]
        ConfigWindow_Condition_ConditionName,

        [Description("資訊")]
        ConfigWindow_UI_Information,

        [Description("覆蓋介面")]
        ConfigWindow_UI_Overlay,

        [Description("視窗")]
        ConfigWindow_UI_Windows,

        [Description("調整 RSR 自動使用技能的方式")]
        ConfigWindow_Auto_Description,

        [Description("技能使用與控制")]
        ConfigWindow_Auto_ActionUsage,

        [Description("RSR 可以使用哪些技能")]
        ConfigWindow_Auto_ActionUsage_Description,

        [Description("治療使用與控制")]
        ConfigWindow_Auto_HealingCondition,

        [Description("RSR 應如何使用治療技能")]
        ConfigWindow_Auto_HealingCondition_Description,

        [Description("自訂狀態條件")]
        ConfigWindow_Auto_StateCondition,

        [Description("範圍治療強制條件")]
        ConfigWindow_Auto_HealAreaConditionSet,

        [Description("單體治療強制條件")]
        ConfigWindow_Auto_HealSingleConditionSet,

        [Description("範圍防禦強制條件")]
        ConfigWindow_Auto_DefenseAreaConditionSet,

        [Description("單體防禦強制條件")]
        ConfigWindow_Auto_DefenseSingleConditionSet,

        [Description("解除／姿態／身位強制條件")]
        ConfigWindow_Auto_DispelStancePositionalConditionSet,

        [Description("復活／退避強制條件")]
        ConfigWindow_Auto_RaiseShirkConditionSet,

        [Description("向前移動強制條件")]
        ConfigWindow_Auto_MoveForwardConditionSet,

        [Description("向後移動強制條件")]
        ConfigWindow_Auto_MoveBackConditionSet,

        [Description("防擊退強制條件")]
        ConfigWindow_Auto_AntiKnockbackConditionSet,

        [Description("加速強制條件")]
        ConfigWindow_Auto_SpeedConditionSet,

        [Description("禁止施法條件組")]
        ConfigWindow_Auto_NoCastingConditionSet,

        [Description("這會改變 RSR 使用技能的方式")]
        ConfigWindow_Auto_ActionCondition_Description,

        [Description("設定")]
        ConfigWindow_Target_Config,

        [Description("敵對目標")]
        ConfigWindow_List_Hostile,

        [Description("敵人選取邏輯。加入多個選項後，使用 /rotation Auto 時會依序切換。\n使用 /rotation Settings TargetingTypes add <option> 新增，\n/rotation Settings TargetingTypes remove <option> 移除，\n或使用 /rotation Settings TargetingTypes removeall 移除所有選項。")]
        ConfigWindow_Param_HostileDesc,

        [Description("上移")]
        ConfigWindow_Actions_MoveUp,

        [Description("下移")]
        ConfigWindow_Actions_MoveDown,

        [Description("敵對目標選取條件")]
        ConfigWindow_Param_HostileCondition,

        [Description("RSR 主要專注於技能循環；以下是附加功能，未來可能隨時移除。")]
        ConfigWindow_Extra_Description,

        [Description("事件")]
        ConfigWindow_EventItem,
        
        [Description("內部")]
        ConfigWindow_Internal,

        [Description("其他")]
        ConfigWindow_Extra_Others,

        [Description("新增事件")]
        ConfigWindow_Events_AddEvent,

        [Description("你可以在此視窗設定使用技能後要觸發的巨集。")]
        ConfigWindow_Events_Description,

        [Description("任務開始：")]
        ConfigWindow_Events_DutyStart,

        [Description("任務結束：")]
        ConfigWindow_Events_DutyEnd,

        [Description("刪除事件")]
        ConfigWindow_Events_RemoveEvent,

        [Description("按一下反轉條件。\n目前是否反轉：{0}")]
        ActionSequencer_NotDescription,

        [Description("成員名稱")]
        ConfigWindow_Actions_MemberName,

        [Description("循環不存在。請登入遊戲或切換職業！")]
        ConfigWindow_Condition_RotationNullWarning,

        [Description("延遲切換為成立")]
        ActionSequencer_Delay_Description,

        [Description("延遲條件切換")]
        ActionSequencer_Offset_Description,

        [Description("等級足夠")]
        ActionConditionType_EnoughLevel,

        [Description("時間偏移")]
        ActionSequencer_TimeOffset,

        [Description("積蓄次數")]
        ActionSequencer_Charges,

        [Description("原始值")]
        ActionSequencer_Original,

        [Description("調整後")]
        ActionSequencer_Adjusted,

        [Description("{0} 的目標")]
        ActionSequencer_ActionTarget,

        [Description("來自所有人")]
        ActionSequencer_StatusAll,

        [Description("來自自身")]
        ActionSequencer_StatusSelf,

        [Description("不建議使用此選項，因為這個目標並非技能目標。請改從技能中選擇目標。")]
        ConfigWindow_Condition_TargetWarning,

        [Description("區域名稱")]
        ConfigWindow_Condition_TerritoryName,

        [Description("任務名稱")]
        ConfigWindow_Condition_DutyName,

        [Description("請另外綁定減傷／護盾技能，以免 RSR 在 {0} 的關鍵時刻未能使用！")]
        HighEndWarning,

        [Description("使用強制啟用條件")]
        ForcedEnableCondition,

        [Description("強制此項成立的條件")]
        ForcedEnableConditionDesc,

        [Description("使用強制停用條件")]
        ForcedDisableCondition,

        [Description("強制此項不成立的條件")]
        ForcedDisableConditionDesc,

        [Description("按一下執行指令")]
        ConfigWindow_Helper_RunCommand,

        [Description("按右鍵複製指令")]
        ConfigWindow_Helper_CopyCommand,

        [Description("巨集編號")]
        ConfigWindow_Events_MacroIndex,

        [Description("是否為共用巨集")]
        ConfigWindow_Events_ShareMacro,

        [Description("技能名稱")]
        ConfigWindow_Events_ActionName,

        [Description("將 {0} 修改為 {1}")]
        CommandsChangeSettingsValue,

        [Description("在此循環中找不到該設定，請確認輸入內容。")]
        CommandsCannotFindConfig,

        [Description("將在 {0} 秒內使用")]
        CommandsInsertAction,

        [Description("找不到該技能，請確認技能名稱。")]
        CommandsInsertActionFailure,

        [Description("無法從字串取得設定和值。請同時提供設定選項與值。")]
        CommandsMissingArgument,

        [Description("開始")]
        SpecialCommandType_Start,

        [Description("取消")]
        SpecialCommandType_Cancel,

        [Description("範圍治療")]
        SpecialCommandType_HealArea,

        [Description("單體治療")]
        SpecialCommandType_HealSingle,

        [Description("範圍防禦")]
        SpecialCommandType_DefenseArea,

        [Description("單體防禦")]
        SpecialCommandType_DefenseSingle,

        [Description("坦克姿態")]
        SpecialCommandType_TankStance,

        [Description("解除減益")]
        SpecialCommandType_Dispel,

        [Description("身位")]
        SpecialCommandType_Positional,

        [Description("退避")]
        SpecialCommandType_Shirk,

        [Description("復活")]
        SpecialCommandType_Raise,

        [Description("向前移動")]
        SpecialCommandType_MoveForward,

        [Description("向後移動")]
        SpecialCommandType_MoveBack,

        [Description("防擊退")]
        SpecialCommandType_AntiKnockback,

        [Description("爆發")]
        SpecialCommandType_Burst,

        [Description("結束特殊狀態")]
        SpecialCommandType_EndSpecial,

        [Description("加速")]
        SpecialCommandType_Speed,

        [Description("極限技")]
        SpecialCommandType_LimitBreak,

        [Description("禁止施法")]
        SpecialCommandType_NoCasting,

        [Description("自動選擇目標")]
        SpecialCommandType_Smart,

        [Description("手動選擇目標")]
        SpecialCommandType_Manual,

        [Description("關閉")]
        SpecialCommandType_Off,

        [Description("開啟設定視窗")]
        Commands_Rotation,

        [Description("啟動 RSR 戰鬥循環狀態")]
        Commands_Start,

        [Description("停用 RSR 戰鬥循環狀態")]
        Commands_Off,

        [Description("Rotation Solver Reborn 設定 v")]
        ConfigWindowHeader,

        [Description("此設定僅套用於目前職業")]
        JobConfigTip,

        [Description("目前職業無法使用此選項\n\n需要以下職能或職業：\n{0}")]
        NotInJob,

        [Description("歡迎使用 Rotation Solver Reborn！")]
        WelcomeWindow_Header,

        [Description("以下是你上次使用後錯過的更新")]
        WelcomeWindow_WelcomeBack,

        [Description("看來你是第一次使用，讓我們開始設定吧！")]
        WelcomeWindow_Welcome,

        [Description("近期變更：")]
        WelcomeWindow_Changelog,

        [Description("PvP 專用控制")]
        ConfigWindow_Auto_PvPSpecific,
    }

    public static class EnumExtensions
    {
        private static readonly Dictionary<Enum, string> _enumDescriptions = [];

        public static string GetDescription(this Enum value)
        {
            if (_enumDescriptions.TryGetValue(value, out string? description))
            {
                return description;
            }

            FieldInfo? field = value.GetType().GetField(value.ToString());
            if (field == null)
            {
                _enumDescriptions.Add(value, value.ToString());
                return value.ToString();
            }

            DescriptionAttribute? attribute = field.GetCustomAttribute<DescriptionAttribute>();

            string descString = attribute == null ? value.ToString() : attribute.Description;
            _enumDescriptions.Add(value, descString);
            return descString;
        }
    }
}
