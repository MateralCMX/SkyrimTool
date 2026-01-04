using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace SkyrimTool.Models;

/// <summary>
/// 技能数据
/// </summary>
public partial class SkillData : ObservableObject
{
    /// <summary>
    /// 唯一标识
    /// </summary>
    public string ID { get; set; } = string.Empty;

    /// <summary>
    /// 名字
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 分组
    /// </summary>
    public List<string> Groups { get; set; } = [];
}
