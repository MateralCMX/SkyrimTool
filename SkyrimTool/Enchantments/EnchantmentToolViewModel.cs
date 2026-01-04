using CommunityToolkit.Mvvm.ComponentModel;
using SkyrimTool.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SkyrimTool.Enchantments;

public partial class EnchantmentToolViewModel : ObservableObject
{
    /// <summary>
    /// 物品关键字
    /// </summary>
    [ObservableProperty]
    public partial string ItemKeyWord { get; set; } = string.Empty;

    partial void OnItemKeyWordChanged(string value) => QueryItems();

    /// <summary>
    /// 关键字
    /// </summary>
    [ObservableProperty]
    public partial string EnchantmentKeyWord { get; set; } = string.Empty;

    partial void OnEnchantmentKeyWordChanged(string value) => QueryEnchantments();

    /// <summary>
    /// 附魔组
    /// </summary>
    public ObservableCollection<EnchantmentData> Enchantments { get; } = [];

    /// <summary>
    /// 物品组
    /// </summary>
    public ObservableCollection<ItemData> Items { get; } = [];

    /// <summary>
    /// 物品类型
    /// </summary>
    public ObservableCollection<ItemTypeViewModel> ItemTypes { get; } = [
        new() { Name = "戒指", Value = nameof(SkyrimData.Rings) },
        new() { Name = "项链", Value = nameof(SkyrimData.Amulets) },
        new() { Name = "头环", Value = nameof(SkyrimData.Circlets) },
        new() { Name = "服装", Value = nameof(SkyrimData.Clothings) },
        new() { Name = "轻甲", Value = nameof(SkyrimData.LightArmors) },
        new() { Name = "重甲", Value = nameof(SkyrimData.HeavyArmors) }
    ];

    /// <summary>
    /// 选中的物品类型值
    /// </summary>
    [ObservableProperty]
    public partial string SelectedItemTypeValue { get; set; } = nameof(SkyrimData.Rings);

    partial void OnSelectedItemTypeValueChanged(string value) => QueryItems();

    /// <summary>
    /// 物品代码
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ConsoleCode))]
    public partial string ItemCode { get; set; } = "000877A7";

    /// <summary>
    /// 物品代码
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ConsoleCode))]
    public partial string ItemDescription { get; set; } = "紫水晶银戒指";

    /// <summary>
    /// 效果代码
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ConsoleCode))]
    public partial string EffectCode1 { get; set; } = string.Empty;

    /// <summary>
    /// 效果说明1
    /// </summary>
    [ObservableProperty]
    public partial string EffectDescription1 { get; set; } = string.Empty;

    /// <summary>
    /// 效果代码2
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ConsoleCode))]
    public partial string EffectCode2 { get; set; } = string.Empty;

    /// <summary>
    /// 效果说明2
    /// </summary>
    [ObservableProperty]
    public partial string EffectDescription2 { get; set; } = string.Empty;

    /// <summary>
    /// 控制台代码
    /// </summary>
    public string ConsoleCode => $"playerenchantobject {ItemCode} {EffectCode1} {EffectCode2}";

    /// <summary>
    /// 初始化
    /// </summary>
    /// <returns></returns>
    public async Task InitAsync()
    {
        await SkyrimData.InitAsync();
        QueryItems();
        QueryEnchantments();
    }

    /// <summary>
    /// 查询附魔
    /// </summary>
    private void QueryEnchantments()
    {
        Enchantments.Clear();
        foreach (EnchantmentData enchantment in SkyrimData.Enchantments)
        {
            if (!CanAddEnchantment(enchantment, EnchantmentKeyWord)) continue;
            Enchantments.Add(enchantment);
        }
    }

    /// <summary>
    /// 查询物品
    /// </summary>
    private void QueryItems()
    {
        Items.Clear();
        IReadOnlyList<ItemData> items = SelectedItemTypeValue switch
        {
            nameof(SkyrimData.Amulets) => SkyrimData.Amulets,
            nameof(SkyrimData.Circlets) => SkyrimData.Circlets,
            nameof(SkyrimData.Clothings) => SkyrimData.Clothings,
            nameof(SkyrimData.LightArmors) => SkyrimData.LightArmors,
            nameof(SkyrimData.HeavyArmors) => SkyrimData.HeavyArmors,
            _ => SkyrimData.Rings,
        };
        foreach (ItemData item in items)
        {
            if (!CanAddItem(item, ItemKeyWord)) continue;
            Items.Add(item);
        }
    }

    /// <summary>
    /// 能否添加附魔
    /// </summary>
    /// <param name="enchantment"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    private static bool CanAddEnchantment(EnchantmentData enchantment, string key)
    {
        if (string.IsNullOrEmpty(key)) return true;
        return enchantment.EnchID.Contains(key) || enchantment.Enchantment.Contains(key) || enchantment.EnchantmentZH.Contains(key);
    }

    /// <summary>
    /// 能否添加物品
    /// </summary>
    /// <param name="item"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    private static bool CanAddItem(ItemData item, string key)
    {
        if (!item.Unenchanted) return false;
        if (string.IsNullOrEmpty(key)) return true;
        return item.ID.Contains(key) || item.NameZH.Contains(key) || item.Name.Contains(key);
    }
}
