using Library.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;

namespace PresentationTest
{
    [TestClass]
    public class ViewModelTest
    {
        [TestMethod]
        public void CreatorTestMethodListView()
        {
            ReaderListViewModel vm = new ReaderListViewModel();
            Assert.IsNull(vm.Book);
            Assert.IsNull(vm.Readers);
            Assert.IsNull(vm.Events);
            Assert.IsNull(vm.CurrentEvent);
            Assert.IsNull(vm.CurrentReader);
            Assert.IsNotNull(vm.AddReaderCommand);
            Assert.IsNotNull(vm.EditReaderCommand);
            Assert.IsNotNull(vm.RefreshReadersCommand);
            Assert.IsNotNull(vm.DeleteReaderCommand);

            Assert.IsTrue(vm.AddReaderCommand.CanExecute(null));
            Assert.IsTrue(vm.EditReaderCommand.CanExecute(null));
            Assert.IsTrue(vm.RefreshReadersCommand.CanExecute(null));
            Assert.IsTrue(vm.DeleteReaderCommand.CanExecute(null));
        }

        [TestMethod]
        public void CreatorTestMethodAddEditView()
        {
            ReaderViewModel vm = new ReaderViewModel();
            Assert.IsNull(vm.FName);
            Assert.IsNull(vm.LName);
            Assert.IsNull(vm.Id);
            Assert.IsTrue(String.IsNullOrEmpty(vm.ActionText));
            Assert.IsNotNull(vm.MessageBoxShowDelegate);

            Assert.IsNotNull(vm.AddReaderCommand);
            Assert.IsNotNull(vm.EditReaderCommand);

            Assert.IsTrue(vm.AddReaderCommand.CanExecute(null));
            Assert.IsTrue(vm.EditReaderCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddReaderPopUpWasShownTest()
        {
            ReaderViewModel vm = new ReaderViewModel();
            int _boxShowCount = 0;
            vm.MessageBoxShowDelegate = (messageBoxText) =>
            {
                _boxShowCount++;
                Assert.AreEqual("Reader Added", messageBoxText);
            };
            vm.ActionText = "Reader Added";
            Assert.IsTrue(vm.AddReaderCommand.CanExecute(null));
            vm.AddReaderCommand.Execute(null);
            Assert.AreEqual(1, _boxShowCount);
        }

        [TestMethod]
        public void RefreshReaders()
        {
            ReaderListViewModel vm = new ReaderListViewModel();

            //view loads readers after 3s
            Thread.Sleep(3000);
            Assert.IsTrue(vm.Readers.Count() > 0);
        }

        [TestMethod]
        public void RefreshEventsCurrentReader()
        {
            ReaderListViewModel vm = new ReaderListViewModel();

            Thread.Sleep(3000);

            var eventRefreshEventRaised = false;
            vm.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Events")
                {
                    eventRefreshEventRaised = true;
                }
            };

            vm.CurrentReader = vm.Readers.Skip(1).First();
            Assert.IsNotNull(vm.CurrentReader);

            Thread.Sleep(3000);
            Assert.IsTrue(eventRefreshEventRaised);
        }

        [TestMethod]
        public void RefreshBookCurrentEvent()
        {
            ReaderListViewModel vm = new ReaderListViewModel();

            Thread.Sleep(3000);

            var bookRefreshEventRaised = false;
            vm.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Book")
                {
                    bookRefreshEventRaised = true;
                }
            };

            vm.CurrentEvent = vm.Events.Skip(1).First();
            Assert.IsNotNull(vm.CurrentEvent);

            Thread.Sleep(3000);
            Assert.IsTrue(bookRefreshEventRaised);
        }
    }
}
