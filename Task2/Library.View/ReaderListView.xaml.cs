using Library.UI;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library.View
{
    /// <summary>
    /// Interaction logic for ReaderListView.xaml
    /// </summary>
    public partial class ReaderListView : UserControl
    {
        ReaderListViewModel readerListViewModel = new ReaderListViewModel();
        public ReaderListView()
        {
            InitializeComponent();
            this.DataContext = this.readerListViewModel;
        }
    }
}
