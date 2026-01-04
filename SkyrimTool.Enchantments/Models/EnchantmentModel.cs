using System.Text.Json.Serialization;

namespace SkyrimTool.Enchantments.Models;

/// <summary>
/// 表示一个附魔数据条目，对应 JSON 文件中的一行记录。
/// </summary>
public class EnchantmentModel
{
    /// <summary>
    /// 附魔 ID
    /// </summary>
    [JsonPropertyName("ench_id")]
    public string EnchID { get; set; } = string.Empty;
    /// <summary>
    /// 附魔原文名称
    /// </summary>
    [JsonPropertyName("enchantment")]
    public string Enchantment { get; set; } = string.Empty;
    /// <summary>
    /// 附魔中文名称
    /// </summary>
    [JsonPropertyName("enchantment_zh")]
    public string EnchantmentZH { get; set; } = string.Empty;
    /// <summary>
    /// 附魔参数（如倍率、数值）
    /// </summary>
    [JsonPropertyName("variable")]
    public string Variable { get; set; } = string.Empty;
    /// <summary>
    /// 附魔参数的中文说明
    /// </summary>
    [JsonPropertyName("variable_zh")]
    public string VariableZH { get; set; } = string.Empty;
    /// <summary>
    /// 标记该附魔是否经过实机验证
    /// </summary>
    [JsonPropertyName("tested")]
    public string Tested { get; set; } = string.Empty;
    /// <summary>
    /// 标记此附魔是否仅能存在于附魔槽位中
    /// </summary>
    [JsonPropertyName("only_enchantment")]
    public string OnlyEnchantment { get; set; } = string.Empty;
    /// <summary>
    /// 附魔效果描述。
    /// </summary>
    [JsonPropertyName("effect")]
    public string Effect { get; set; } = string.Empty;
    /// <summary>
    /// 附魔效果的中文描述。
    /// </summary>
    [JsonPropertyName("effect_zh")]
    public string EffectZH { get; set; } = string.Empty;
}
