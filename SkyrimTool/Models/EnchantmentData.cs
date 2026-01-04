namespace SkyrimTool.Models;

/// <summary>
/// 附魔数据条目
/// </summary>
public partial class EnchantmentData
{
    /// <summary>
    /// 附魔ID
    /// </summary>
    public string EnchID { get; set; } = string.Empty;

    /// <summary>
    /// 附魔原文名称
    /// </summary>
    public string Enchantment { get; set; } = string.Empty;

    /// <summary>
    /// 附魔中文名称
    /// </summary>
    public string EnchantmentZH { get; set; } = string.Empty;

    /// <summary>
    /// 附魔参数（如倍率、数值）
    /// </summary>
    public string Variable { get; set; } = string.Empty;

    /// <summary>
    /// 附魔参数的中文说明
    /// </summary>
    public string VariableZH { get; set; } = string.Empty;

    /// <summary>
    /// 标记该附魔是否经过实机验证
    /// </summary>
    public string Tested { get; set; } = string.Empty;

    /// <summary>
    /// 标记此附魔是否仅能存在于附魔槽位中
    /// </summary>
    public string OnlyEnchantment { get; set; } = string.Empty;

    /// <summary>
    /// 附魔效果描述。
    /// </summary>
    public string Effect { get; set; } = string.Empty;

    /// <summary>
    /// 附魔效果的中文描述。
    /// </summary>
    public string EffectZH { get; set; } = string.Empty;
}
