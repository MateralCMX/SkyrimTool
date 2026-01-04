using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SkyrimTool.Skills;

[Menu("技能等级")]
public sealed partial class SkillPage : Page
{
    public SkillViewModel ViewModel { get; } = new();

    public SkillPage()
    {
        InitializeComponent();
        Loaded += SkillPage_Loaded;
    }

    private void SkillPage_Loaded(object sender, RoutedEventArgs e)
    {
        DispatcherQueue.TryEnqueue(async () => await ViewModel.InitAsync());
        Loaded -= SkillPage_Loaded;
    }
}
