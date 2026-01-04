using CommunityToolkit.Mvvm.ComponentModel;
using SkyrimTool.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SkyrimTool.Skills;

public partial class SkillViewModel : ObservableObject
{
    /// <summary>
    /// 等级
    /// </summary>
    [ObservableProperty]
    public partial double Level { get; set; } = 100;

    /// <summary>
    /// 技能组
    /// </summary>
    public ObservableCollection<SkillData> AllSkills { get; } = [];

    /// <summary>
    /// 技能组-三神
    /// </summary>
    public ObservableCollection<SkillData> Skills1 { get; } = [];

    /// <summary>
    /// 技能组-辅助
    /// </summary>
    public ObservableCollection<SkillData> Skills2 { get; } = [];

    /// <summary>
    /// 技能组-法术
    /// </summary>
    public ObservableCollection<SkillData> Skills3 { get; } = [];

    /// <summary>
    /// 技能组-物理
    /// </summary>
    public ObservableCollection<SkillData> Skills4 { get; } = [];

    /// <summary>
    /// 初始化
    /// </summary>
    /// <returns></returns>
    public async Task InitAsync()
    {
        await SkyrimData.InitAsync();
        foreach (SkillData skill in SkyrimData.Skills)
        {
            AllSkills.Add(skill);
            if (skill.Groups.Contains("三神"))
            {
                Skills1.Add(skill);
            }
            if (skill.Groups.Contains("辅助"))
            {
                Skills2.Add(skill);
            }
            if (skill.Groups.Contains("法术"))
            {
                Skills3.Add(skill);
            }
            if (skill.Groups.Contains("物理"))
            {
                Skills4.Add(skill);
            }
        }
    }
}