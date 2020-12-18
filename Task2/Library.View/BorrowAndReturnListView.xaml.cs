using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Library.UI;

namespace Library.View
{
	/// <summary>
	/// Logika interakcji dla klasy BorrowAndReturnListView.xaml
	/// </summary>
	public partial class BorrowAndReturnListView : UserControl
	{
		public BorrowAndReturnListView()
		{
			InitializeComponent();
			BorrowBookListViewModel borrowBookViewModel = (BorrowBookListViewModel)DataContext;
			borrowBookViewModel.MessageBoxShowDelegate = text => MessageBox.Show(text, "Button interaction", MessageBoxButton.OK, MessageBoxImage.Information);
		}

	}
}
