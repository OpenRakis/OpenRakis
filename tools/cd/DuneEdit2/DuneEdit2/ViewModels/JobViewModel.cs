namespace DuneEdit2.ViewModels
{
    using ReactiveUI;

    public class JobViewModel : ViewModelBase
    {
        private byte _job;
        private string _jobDesc = "";

        public JobViewModel(byte value, string desc)
        {
            Job = value;
            JobDesc = desc;
        }

        public byte Job { get => _job; private set => _job = this.RaiseAndSetIfChanged(ref _job, value); }

        public string JobDesc { get => _jobDesc; private set => _jobDesc = this.RaiseAndSetIfChanged(ref _jobDesc, value); }
    }
}