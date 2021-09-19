namespace DuneEdit2.ViewModels
{
    using ReactiveUI;

    public class StatusViewModel : ViewModelBase
    {
        private byte _status;
        private string _statusDesc = "";

        public StatusViewModel(byte value, string desc)
        {
            Status = value;
            StatusDesc = desc;
        }

        public byte Status { get => _status; private set => this.RaiseAndSetIfChanged(ref _status, value); }

        public string StatusDesc { get => _statusDesc; private set => this.RaiseAndSetIfChanged(ref _statusDesc, value); }
    }
}