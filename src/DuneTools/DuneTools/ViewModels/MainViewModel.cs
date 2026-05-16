namespace DuneTools.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _greeting = "Welcome to Avalonia!";
}
