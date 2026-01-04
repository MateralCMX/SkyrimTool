namespace SkyrimTool.Models;

public partial class ItemData
{
    /// <summary>
    /// 唯一标识
    /// </summary>
    public string ID { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 中文名称
    /// </summary>
    public string NameZH { get; set; } = string.Empty;

    /// <summary>
    /// 未附魔
    /// </summary>
    public bool Unenchanted { get; set; } = false;
}
