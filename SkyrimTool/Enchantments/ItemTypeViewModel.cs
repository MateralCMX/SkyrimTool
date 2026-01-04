using CommunityToolkit.Mvvm.ComponentModel;

namespace SkyrimTool.Enchantments;

/// <summary>
/// 物品类型视图模型
/// </summary>
public partial class ItemTypeViewModel : ObservableObject
{
    /// <summary>
    /// 名字
    /// </summary>
    [ObservableProperty]
    public partial string Name { get; set; }

    /// <summary>
    /// 值
    /// </summary>
    [ObservableProperty]
    public partial string Value { get; set; }
}