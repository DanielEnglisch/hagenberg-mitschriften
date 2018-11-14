using Swk5.MediaAnnotator;
using Swk5.MediaAnnotator.BL;
using Swk5.MediaAnnotator.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaAnnotator.ViewModels
{
    public class MediaFolderCollectionVM: INotifyPropertyChanged
    {
        private IMediaManager mediaManager;

        public MediaFolderCollectionVM(IMediaManager mediaManager)
        {
            this.mediaManager = mediaManager;
            Folders = new ObservableCollection<MediaFolderVM>();
            LoadFolders();
        }

        public ObservableCollection<MediaFolderVM> Folders { get; }
        private MediaFolderVM mediaFolderVM;
        public MediaFolderVM CurrentFolder
        {
            get
            {
                return mediaFolderVM;
            }
            set
            {
                if(value != mediaFolderVM)
                {
                    mediaFolderVM = value;
                    CurrentFolder.LoadItems();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentFolder)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void LoadFolders()
        {
            Folders.Clear();
            IEnumerable<MediaFolder> folders = mediaManager.GetMediaFolders(Constants.BaseMediaFolder, Constants.MediaExt);

            foreach(var f in folders)
            {
                Folders.Add(new MediaFolderVM(f, mediaManager));
            }
        }
    }
}
