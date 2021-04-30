namespace DuneEdit2
{
    internal class SaveGameEditorCli : IStandardOutput
    {
        private Options _options;

        public SaveGameEditorCli(Options options)
        {
            this._options = options;
        }

        public string GetStandardOutput()
        {
            return "";
        }
    }
}
