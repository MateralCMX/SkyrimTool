using Materal.Extensions;
using SkyrimTool.Enchantments.Models;

namespace SkyrimTool.Enchantments;

/// <summary>
/// 附魔工具
/// </summary>
public partial class EnchantmentTool
{
    public IReadOnlyList<EnchantmentModel> Enchantments { get; private set; } = [];
    public IReadOnlyList<EnchantmentModel> Staffs { get; private set; } = [];

    private readonly string _dataPath = Path.Combine(typeof(EnchantmentTool).Assembly.GetDirectoryPath(), "Data");

    public async Task InitAsync()
    {
        Enchantments = await ReadDataAsync("Armor", "Weapon", "Others");
        Staffs = await ReadDataAsync("Staff");
    }

    private async Task<List<EnchantmentModel>> ReadDataAsync(params string[] fileNames)
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
