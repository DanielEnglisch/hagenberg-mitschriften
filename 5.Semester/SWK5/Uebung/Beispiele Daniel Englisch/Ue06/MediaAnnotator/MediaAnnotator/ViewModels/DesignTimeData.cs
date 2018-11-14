using System.Linq;
using MediaAnnotator.ViewModels;
using Swk5.MediaAnnotator.BL;
using Swk5.MediaAnnotator.ViewModels;

namespace Swk5.MediaAnnotator.ViewModels
{
    /* Include the following into the Window element of your XAML code.
       This assigns a DesignTimeData object to the DataContext property.
       This assignment is only done at design time. The runtime data context has
       to be defined in the constructor of the window.
    
    
    
    */

    public class DesignTimeData : MediaFolderCollectionVM
    {
        public DesignTimeData() : base(MediaManagerFactory.GetMediaManager())
        {
            this.CurrentFolder = this.Folders.First();
        }            
    }
}
