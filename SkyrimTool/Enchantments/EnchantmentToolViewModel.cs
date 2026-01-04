using CommunityToolkit.Mvvm.ComponentModel;
using SkyrimTool.Enchantments.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SkyrimTool.Enchantments;

public partial class EnchantmentToolViewModel : ObservableObject
{
    private readonly EnchantmentTool _tool = new();

    /// <summary>
    /// 关键字
    /// </summary>
    [ObservableProperty]
    public partial string KeyWord { get; set; } = string.Empty;

    partial void OnKeyWordChanged(string value)
    {
        Enchantments.Clear();
        foreach (EnchantmentModel enchantment in _tool.Enchantments)
        {
            if (!CanAdd(enchantment, value)) continue;
            Enchantments.Add(enchantment);
        }
    }

    /// <summary>
    /// 附魔组
    /// </summary>
    public ObservableCollection<EnchantmentModel> Enchantments { get; } = [];

    /// <summary>
    /// 物品代码
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ConsoleCode))]
    public partial string ItemCode { get; set; } = "000877A7";

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
        await _tool.InitAsync();
        foreach (EnchantmentModel enchantment in _tool.Enchantments)
        {
            Enchantments.Add(enchantment);
        }
    }

    /// <summary>
    /// 能否添加
    /// </summary>
    /// <param name="enchantment"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    private static bool CanAdd(EnchantmentModel enchantment, string key)
    {
        if (string.IsNullOrEmpty(key)) return true;
        return enchantment.EnchID.Contains(key) || enchantment.Enchantment.Contains(key) || enchantment.EnchantmentZH.Contains(key);
    }
}