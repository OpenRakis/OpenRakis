using System;
using System.Reactive;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;

using ReactiveUI;

namespace DuneEdit2.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ZoomIn = ReactiveCommand.Create<Unit, Unit>(ZoomInMethod);
            ZoomOut = ReactiveCommand.Create<Unit, Unit>(ZoomOutMethod);
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private Unit ZoomInMethod(Unit arg)
        {
            var map = this.Find<Image>("MapOfDuneZoomedIn");
            if(map != null)
            {
                map.IsVisible = true;
            }
            var map2 = this.Find<Image>("MapOfDuneZoomedOut");
            if (map2 != null)
            {
                map2.IsVisible = false;
            }
            return Unit.Default;
        }

        private Unit ZoomOutMethod(Unit arg)
        {
            var map = this.Find<Image>("MapOfDuneZoomedIn");
            if (map != null)
            {
                map.IsVisible = false;
            }
            var map2 = this.Find<Image>("MapOfDuneZoomedOut");
            if (map2 != null)
            {
                map2.IsVisible = true;
            }
            return Unit.Default;
        }

        public ReactiveCommand<Unit, Unit> ZoomOut { get; private set; }
        public ReactiveCommand<Unit, Unit> ZoomIn { get; private set; }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}