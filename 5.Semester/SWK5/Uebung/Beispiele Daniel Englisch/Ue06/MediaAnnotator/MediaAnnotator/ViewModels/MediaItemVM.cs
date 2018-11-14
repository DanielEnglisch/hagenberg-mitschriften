using Swk5.MediaAnnotator.BL;
using Swk5.MediaAnnotator.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Swk5.MediaAnnotator.ViewModels
{
    public class MediaItemVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private MediaItem item;

        public MediaItemVM(MediaItem item, IMediaManager mediaManager)
        {
            this.item = item;
            this.SaveCommand = new RelayCommand(p => mediaManager.UpdateAnnotation(item));
        }

        public string Name => item.Name;
        public string Url => item.Url;

        public ICommand SaveCommand { get; }

        public string Annotation
        {
            get
            {
                return item.Annotation;
            }
            set
            {
                if(value != item.Annotation)
                {
                    item.Annotation = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Annotation)));
                }
            }
        }
        
    }
}
