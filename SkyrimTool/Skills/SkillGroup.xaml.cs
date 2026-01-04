using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SkyrimTool.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Windows.ApplicationModel.DataTransfer;

namespace SkyrimTool.Skills;

public sealed partial class SkillGroup : UserControl
{
    public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(SkillGroup), new PropertyMetadata("È«²¿"));

    public double Level { get => (double)GetValue(LevelProperty); set => SetValue(LevelProperty, value); }
    public static readonly DependencyProperty LevelProperty = DependencyProperty.Register(nameof(Level), typeof(double), typeof(SkillGroup), new PropertyMetadata(0));

    public bool IsExpanded { get => (bool)GetValue(IsExpandedProperty); set => SetValue(IsExpandedProperty, value); }
    public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register(nameof(IsExpanded), typeof(bool), typeof(SkillGroup), new PropertyMetadata(false));

    public ObservableCollection<SkillData> SkillDatas { get => (ObservableCollection<SkillData>)GetValue(SkillDatasProperty); set => SetValue(SkillDatasProperty, value); }
    public static readonly DependencyProperty SkillDatasProperty = DependencyProperty.Register(nameof(SkillDatas), typeof(ObservableCollection<SkillData>), typeof(SkillGroup), new PropertyMetadata(null));

    public SkillGroup() => InitializeComponent();

    private void CopyButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button) return;
        List<SkillData> datas = [];
        if (button.Tag is SkillData skillData)
        {
            datas = [skillData];
        }
        else if (button.Tag is ObservableCollection<SkillData> skillDatas)
        {
            datas.AddRange(skillDatas);
        }
        StringBuilder stringBuilder = new();
        foreach (SkillData data in datas)
        {
            stringBuilder.AppendLine($"player.setav {data.ID} {Level}");
        }
        string consoleCode = stringBuilder.ToString();
        if (string.IsNullOrWhiteSpace(consoleCode)) return;
        DataPackage dataPackage = new();
        dataPackage.SetText(consoleCode);
        Clipboard.SetContent(dataPackage);
    }
}
