using MediaAnnotator.ViewModels;
using Swk5.MediaAnnotator.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Swk5.MediaAnnotator.views
{
    /// <summary>
    /// Interaktionslogik für MediaAnnotator.xaml
    /// </summary>
    public partial class AnnotationWindow : Window
    {
        public AnnotationWindow()
        {
            InitializeComponent();
            DataContext = new MediaFolderCollectionVM(MediaManagerFactory.GetMediaManager());
        }
    }
}
