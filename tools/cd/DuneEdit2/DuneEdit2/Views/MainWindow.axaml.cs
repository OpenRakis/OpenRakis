using System;
using System.Reactive;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

using ReactiveUI;

namespace DuneEdit2.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ZoomMap = ReactiveCommand.Create<Unit, Unit>(ZoomMapMethod);
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private Unit ZoomMapMethod(Unit arg)
        {
            var map = this.Find<Image>("MapOfDune");
            if(map != null)
            {
                map.Width = double.NaN;
                map.Height = double.NaN;
            }
            return Unit.Default;
        }

        public ReactiveCommand<Unit, Unit> ZoomMap { get; private set; }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}