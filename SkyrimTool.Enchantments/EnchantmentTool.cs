using Materal.Extensions;
using SkyrimTool.Enchantments.Models;

namespace SkyrimTool.Enchantments;

/// <summary>
/// 附魔工具
/// </summary>
public static partial class EnchantmentTool
{
    public static IReadOnlyList<EnchantmentModel> Enchantments { get; private set; } = [];

    public static IReadOnlyList<EnchantmentModel> Staffs { get; private set; } = [];

    private static readonly string _dataPath = Path.Combine(typeof(EnchantmentTool).Assembly.GetDirectoryPath(), "Data");

    private static bool _isInit = false;

    private static readonly SemaphoreSlim _initSemaphoreSlim = new(1, 1);

    public static async Task InitAsync()
    {
        if (_isInit) return;
        _initSemaphoreSlim.Wait();
        try
        {
            if (_isInit) return;
            _isInit = true;
            Enchantments = await ReadDataAsync("Armor", "Weapon", "Others");
            Staffs = await ReadDataAsync("Staff");
        }
        finally
        {
            _initSemaphoreSlim.Release();
        }
    }

    private static async Task<List<EnchantmentModel>> ReadDataAsync(params string[] fileNames)
    {
        List<EnchantmentModel> result = [];
        foreach (string fileName in fileNames)
        {
            string filePath = Path.Combine(_dataPath, $"{fileName}.json");
            if (!File.Exists(filePath)) throw new FileNotFoundException($"文件 {filePath} 不存在");
            string jsonContent = await File.ReadAllTextAsync(filePath);
            result.AddRange(jsonContent.JsonToObject<List<EnchantmentModel>>());
        }
        return [.. result.Where(m => !string.IsNullOrWhiteSpace(m.Enchantment)).OrderBy(m => m.EnchID)];
    }
}
