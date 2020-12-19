using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Library.UI;
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
    /// Interaction logic for BookListView.xaml
    /// </summary>
    public partial class BookListView : UserControl
    {
        public BookListView()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
			BookListViewModel bookListViewModel = (BookListViewModel)DataContext;
            bookListViewModel.AddWindow = new Lazy<IWindow>(() => new AddBookView());
            bookListViewModel.EditWindow = new Lazy<IWindow>(() => new EditBookView());
        }
    }
}
