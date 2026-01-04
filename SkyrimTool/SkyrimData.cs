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
    /// <summary>
    /// 附魔
    /// </summary>
    public static IReadOnlyList<EnchantmentData> Enchantments { get; private set; } = [];

    /// <summary>
    /// 法杖附魔
    /// </summary>
    public static IReadOnlyList<EnchantmentData> StaffEnchantments { get; private set; } = [];

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
        }
        finally
        {
            _initSemaphoreSlim.Release();
        }
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
        return [.. result.Where(m => !string.IsNullOrWhiteSpace(m.Enchantment)).OrderBy(m => m.EnchID)];
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
