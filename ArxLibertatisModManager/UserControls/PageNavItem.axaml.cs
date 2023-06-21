using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.ComponentModel;
using System.Windows.Input;
using PropertyChanged.SourceGenerator;

namespace ArxLibertatisModManager.UserControls;

public partial class PageNavItem : UserControl, INotifyPropertyChanging
{
    public static readonly DirectProperty<PageNavItem, ICommand?> CommandProperty =
            AvaloniaProperty.RegisterDirect<PageNavItem, ICommand?>(nameof(Command),
                pageNavItem => pageNavItem.Command, (pageNavItem, command) => pageNavItem.Command = command, enableDataValidation: true);

    [Notify] string title = "";
    [Notify] ICommand? command;

    public new event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public PageNavItem()
    {
        InitializeComponent();

        LblTitle.DataContext = this;
        BtnButton.DataContext = this;
    }
    public void CallCommand()
    {
        Command?.Execute(null);
    }
}