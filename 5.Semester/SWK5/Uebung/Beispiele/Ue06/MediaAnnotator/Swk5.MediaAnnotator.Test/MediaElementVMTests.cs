using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swk5.MediaAnnotator.BL;
using Swk5.MediaAnnotator.ViewModels;

namespace Swk5.MediaAnnotator.Test
{
    [TestClass]
    public class MediaElementVMTests
    {
        private class MockMediaManager : IMediaManager
        {
            public bool WasUpdateAnnotationCalled { get; private set; }

            public void UpdateAnnotation(MediaItem mediaElement)
            {
                this.WasUpdateAnnotationCalled = true;
            }

            public IEnumerable<MediaFolder> GetMediaFolders(string baseUrl, string[] mediaExtensions)
            {
                throw new NotImplementedException();
            }

            public MediaFolder GetMediaFolder(string url, string[] mediaExtensions)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<MediaItem> GetMediaItems(MediaFolder folder, string[] mediaExtensions)
            {
                throw new NotImplementedException();
            }
        }

        [TestMethod]
        public void TestSaveCommand()
        {
            // create a fake media manager and media item
            var manager = new MockMediaManager();

            var mediaItem = new MediaItem()
            {
                Annotation = "Old annotation"
            };

            // create a view model and execute the command
            var viewModel = new MediaItemVM(mediaItem, manager);

            viewModel.Annotation = "New annotation";
            viewModel.SaveCommand.Execute(null);

            // assertions
            Assert.IsTrue(manager.WasUpdateAnnotationCalled);
            Assert.AreEqual(mediaItem.Annotation, "New annotation");
        }
    }
}
