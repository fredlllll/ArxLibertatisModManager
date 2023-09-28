using ArxLibertatisModManager.UserControls;
using Avalonia;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ArxLibertatisModManager.ViewModels
{
    public partial class PageNavItemViewModel : ViewModelBase
    {
        public static readonly IBrush ActiveColor = new SolidColorBrush(Color.FromRgb(0x85, 0xca, 0xcc));
        public static readonly IBrush InactiveColor = new SolidColorBrush(Color.FromRgb(0x85/3*2, 0xc5 / 3 * 2, 0xc7 / 3 * 2));

        [ObservableProperty] string title = "";
        [ObservableProperty] ICommand? command;
        [ObservableProperty] IBrush buttonBackgroundColor = ActiveColor;

        public void CallCommand()
        {
            Command?.Execute(null);
        }
    }
}
