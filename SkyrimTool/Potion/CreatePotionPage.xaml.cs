using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SkyrimTool.Models;
using Windows.ApplicationModel.DataTransfer;

namespace SkyrimTool.Potion;

[Menu("创建药剂")]
public sealed partial class CreatePotionPage : Page
{
    public CreatePotionViewModel ViewModel { get; } = new();

    public CreatePotionPage()
    {
        InitializeComponent();
        Loaded += CreatePotionPage_Loaded;
    }

    private void CreatePotionPage_Loaded(object sender, RoutedEventArgs e)
    {
        DispatcherQueue.TryEnqueue(async () => await ViewModel.InitAsync());
        Loaded -= CreatePotionPage_Loaded;
    }

    private void Effect1Button_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button || button.Tag is not EnchantmentData enchantment) return;
        ViewModel.EffectCode1 = enchantment.EnchID;
        ViewModel.EffectDescription1 = enchantment.EnchantmentZH;
    }

    private void Effect2Button_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button || button.Tag is not EnchantmentData enchantment) return;
        ViewModel.EffectCode2 = enchantment.EnchID;
        ViewModel.EffectDescription2 = enchantment.EnchantmentZH;
    }
    private void Effect3Button_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button || button.Tag is not EnchantmentData enchantment) return;
        ViewModel.EffectCode3 = enchantment.EnchID;
        ViewModel.EffectDescription3 = enchantment.EnchantmentZH;
    }

    private void CopyButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ViewModel.ConsoleCode)) return;
        DataPackage dataPackage = new();
        dataPackage.SetText(ViewModel.ConsoleCode);
        Clipboard.SetContent(dataPackage);
    }
}
