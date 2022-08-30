namespace DuneEdit2.ViewModels
{
    using DuneEdit2.Models;
    using DuneEdit2.Parsers;

    using ReactiveUI;

    public class NPCViewModel : ViewModelBase
    {

        private NPC _npc;
        public NPCViewModel(NPC npc)
        {
            _npc = npc;
            HasChanged = false;
        }

        public NPC NPC => _npc;

        public string? Name => NPCNameFinder.GetNPCName(_npc.SpriteId);
        public int StartOffset => _npc.StartOffset;

        private bool _hasChanged = false;

        public bool HasChanged
        {
            get => _hasChanged;
            set
            {
                _hasChanged = value;
                this.RaisePropertyChanged(nameof(HasChanged));
            }
        }

        /// <summary>
        /// 1st byte
        /// </summary>
        public byte SpriteId
        {
            get => _npc.SpriteId;
            set
            {
                _npc.SpriteId = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(SpriteId));
                this.RaisePropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// 2nd byte
        /// </summary>
        public byte UnknownByte1
        {
            get => _npc.UnknownByte1;
            set
            {
                _npc.UnknownByte1 = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(UnknownByte1));
            }
        }

        /// <summary>
        /// 3rd byte
        /// </summary>
        public byte RoomLocation
        {
            get => _npc.RoomLocation;
            set
            {
                _npc.RoomLocation = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(RoomLocation));
            }
        }

        /// <summary>
        /// 4th byte
        /// </summary>
        public byte TypeOfPlace
        {
            get => _npc.TypeOfPlace;
            set
            {
                _npc.TypeOfPlace = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(TypeOfPlace));
            }
        }

        /// <summary>
        /// 5th byte (Field E)
        /// </summary>
        public byte DialogueAvailable
        {
            get => _npc.DialogueAvailable;
            set
            {
                _npc.DialogueAvailable = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(DialogueAvailable));
            }
        }

        /// <summary>
        /// 6th byte
        /// </summary>
        public byte ExactPlace
        {
            get => _npc.ExactPlace;
            set
            {
                _npc.ExactPlace = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(ExactPlace));
            }
        }

        /// <summary>
        /// 7th byte
        /// </summary>
        public byte ForDialogue
        {
            get => _npc.ForDialogue;
            set
            {
                _npc.ForDialogue = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(ForDialogue));
            }
        }

        /// <summary>
        /// 8th byte
        /// </summary>
        public byte UnknownByte3
        {
            get => _npc.UnknownByte3;
            set
            {
                _npc.UnknownByte3 = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(UnknownByte3));
            }
        }

    }
}
