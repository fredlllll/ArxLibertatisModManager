using ArxLibertatisModManager.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.ComponentModel;
using System.Windows.Input;

namespace ArxLibertatisModManager.UserControls;

public partial class PageNavItem : UserControl
{
    public static readonly DirectProperty<PageNavItem, ICommand?> CommandProperty =
            AvaloniaProperty.RegisterDirect<PageNavItem, ICommand?>(nameof(Command),
                pageNavItem => pageNavItem.Command, (pageNavItem, command) => pageNavItem.Command = command, enableDataValidation: true);

    public ICommand? Command
    {
        get { return viewModel.Command; }
        set { viewModel.Command = value; }
    }

    public string Title
    {
        get { return viewModel.Title; }
        set { viewModel.Title = value; }
    }

    public readonly PageNavItemViewModel viewModel;
    public PageNavItem()
    {
        viewModel = new();
        InitializeComponent();

        LblTitle.DataContext = viewModel;
        BtnButton.DataContext = viewModel;
    }
}