namespace DuneEdit2.ViewModels
{
    using DuneEdit2.Models;
    using DuneEdit2.Parsers;

    using ReactiveUI;

    internal class SmugglersViewModel : ViewModelBase
    {
        private Smuggler _smuggler;

        public SmugglersViewModel(Smuggler smuggler) => _smuggler = smuggler;

        public int StartOffset => _smuggler.StartOffset;

    }
}
