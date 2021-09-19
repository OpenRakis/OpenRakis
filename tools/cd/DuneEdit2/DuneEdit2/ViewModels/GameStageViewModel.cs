namespace DuneEdit2.ViewModels
{
    using ReactiveUI;

    public class GameStageViewModel : ViewModelBase
    {
        private byte _gameStage;
        private string _gameDesc = "";
        private string _fullDesc = "";

        public GameStageViewModel(byte stage, string desc)
        {
            GameStage = stage;
            GameDesc = desc;
            _fullDesc = "{GameStage} - {GameDesc}";
        }

        public byte GameStage { get => _gameStage; private set => this.RaiseAndSetIfChanged(ref _gameStage, value); }
        public string GameDesc { get => _gameDesc; private set => this.RaiseAndSetIfChanged(ref _gameDesc, value); }
        public string FullDesc { get => _fullDesc; private set => this.RaiseAndSetIfChanged(ref _fullDesc, value); }
    }
}