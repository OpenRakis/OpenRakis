namespace DuneEdit2.ViewModels
{
    public class GameStageViewModel
    {
        public GameStageViewModel(byte stage, string desc)
        {
            GameStage = stage;
            GameDesc = desc;
        }

        public byte GameStage { get; private set; }
        public string GameDesc { get; private set; }

        public string FullDesc => $"{GameStage} - {GameDesc}";
    }
}