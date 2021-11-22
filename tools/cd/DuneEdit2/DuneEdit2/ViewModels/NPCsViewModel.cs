namespace DuneEdit2.ViewModels
{
    using DuneEdit2.Models;

    using ReactiveUI;

    internal class NPCsViewModel : ViewModelBase
    {
       
        private NPC _npc;
        public NPCsViewModel(NPC npc) => _npc = npc;


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


        public byte StartOffset
        {
            get => _npc.StartOffset;
            set
            {
                _npc.StartOffset = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(StartOffset));
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
            }
        }

        /// <summary>
        /// 2nd byte
        /// </summary>
        public byte UnknownByte
        {
            get => _npc.UnknownByte;
            set
            {
                _npc.UnknownByte = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(UnknownByte));
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
        /// 5th byte
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
        /// 6th byte
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
        /// 7th byte
        /// </summary>
        public byte UnknownByte2
        {
            get => _npc.UnknownByte2;
            set
            {
                _npc.UnknownByte2 = value;
                HasChanged = true;
                this.RaisePropertyChanged(nameof(UnknownByte2));
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
