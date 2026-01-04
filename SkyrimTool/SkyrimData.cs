using Materal.Extensions;
using SkyrimTool.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SkyrimTool;

public static class SkyrimData
{
    #region 数据
    /// <summary>
    /// 附魔
    /// </summary>
    public static IReadOnlyList<EnchantmentData> Enchantments { get; private set; } = [];

    /// <summary>
    /// 法杖附魔
    /// </summary>
    public static IReadOnlyList<EnchantmentData> StaffEnchantments { get; private set; } = [];

    /// <summary>
    /// 护身符
    /// </summary>
    public static IReadOnlyList<ItemData> Amulets { get; private set; } = [];

    /// <summary>
    /// 头环
    /// </summary>
    public static IReadOnlyList<ItemData> Circlets { get; private set; } = [];

    /// <summary>
    /// 戒指
    /// </summary>
    public static IReadOnlyList<ItemData> Rings { get; private set; } = [];

    /// <summary>
    /// 服装
    /// </summary>
    public static IReadOnlyList<ItemData> Clothings { get; private set; } = [];

    /// <summary>
    /// 轻型护甲
    /// </summary>
    public static IReadOnlyList<ItemData> LightArmors { get; private set; } = [];

    /// <summary>
    /// 重型护甲
    /// </summary>
    public static IReadOnlyList<ItemData> HeavyArmors { get; private set; } = [];

    /// <summary>
    /// 技能
    /// </summary>
    public static IReadOnlyList<SkillData> Skills { get; private set; } = [];
    #endregion
    #region 私有字段
    /// <summary>
    /// 数据路径
    /// </summary>
    private static readonly string _dataPath = Path.Combine(typeof(SkyrimData).Assembly.GetDirectoryPath(), "Data");

    /// <summary>
    /// 初始化标记
    /// </summary>
    private static bool _isInit = false;

    /// <summary>
    /// 初始化信号量
    /// </summary>
    private static readonly SemaphoreSlim _initSemaphoreSlim = new(1, 1);
    #endregion

    /// <summary>
    /// 初始化
    /// </summary>
    /// <returns></returns>
    public static async Task InitAsync()
    {
        if (_isInit) return;
        _initSemaphoreSlim.Wait();
        try
        {
            if (_isInit) return;
            _isInit = true;
            Enchantments = await ReadEnchantmentDataAsync("Armor", "Weapon", "Others");
            StaffEnchantments = await ReadEnchantmentDataAsync("Staff");
            Amulets = await ReadItemDataAsync(nameof(Amulets));
            Circlets = await ReadItemDataAsync(nameof(Circlets));
            Rings = await ReadItemDataAsync(nameof(Rings));
            Clothings = await ReadItemDataAsync(nameof(Clothings));
            LightArmors = await ReadItemDataAsync(nameof(LightArmors));
            HeavyArmors = await ReadItemDataAsync(nameof(HeavyArmors));
            Skills = await ReadSkillDataAsync(nameof(Skills));
        }
        finally
        {
            _initSemaphoreSlim.Release();
        }
    }

    /// <summary>
    /// 读取技能数据
    /// </summary>
    /// <param name="fileNames"></param>
    /// <returns></returns>
    private static async Task<List<SkillData>> ReadSkillDataAsync(params string[] fileNames)
    {
        List<SkillData> result = await ReadDataAsync<SkillData>(_dataPath, fileNames);
        return [.. result.OrderBy(m => m.ID)];
    }

    /// <summary>
    /// 读取物品数据
    /// </summary>
    /// <param name="fileNames"></param>
    /// <returns></returns>
    private static async Task<List<ItemData>> ReadItemDataAsync(params string[] fileNames)
    {
        string dataPath = Path.Combine(_dataPath, "Items");
        List<ItemData> result = await ReadDataAsync<ItemData>(dataPath, fileNames);
        return [.. result.OrderBy(m => m.NameZH)];
    }

    /// <summary>
    /// 读取附魔数据
    /// </summary>
    /// <param name="fileNames"></param>
    /// <returns></returns>
    private static async Task<List<EnchantmentData>> ReadEnchantmentDataAsync(params string[] fileNames)
    {
        string dataPath = Path.Combine(_dataPath, "Enchantments");
        List<EnchantmentData> result = await ReadDataAsync<EnchantmentData>(dataPath, fileNames);
        return [.. result.Where(m => !string.IsNullOrWhiteSpace(m.Enchantment)).OrderBy(m => m.EnchantmentZH)];
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="fileNames"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    private static async Task<List<T>> ReadDataAsync<T>(string path, params string[] fileNames)
    {
        List<T> result = [];
        foreach (string fileName in fileNames)
        {
            string filePath = Path.Combine(path, $"{fileName}.json");
            if (!File.Exists(filePath)) throw new FileNotFoundException($"文件 {filePath} 不存在");
            string jsonContent = await File.ReadAllTextAsync(filePath);
            result.AddRange(jsonContent.JsonToObject<List<T>>());
        }
        return result;
    }
}
