using ArxLibertatisModManager.UserControls;
using Avalonia;
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
        [ObservableProperty] string title = "";
        [ObservableProperty] ICommand? command;

        public void CallCommand()
        {
            Command?.Execute(null);
        }
    }
}
