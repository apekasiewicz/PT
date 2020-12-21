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
using Library.UI;
using System.ComponentModel;

namespace Library.View
{
	/// <summary>
	/// Logika interakcji dla klasy AddBookView.xaml
	/// </summary>
	public partial class AddBookView : Window, IWindow
	{
		public AddBookView()
		{
			InitializeComponent();
			BookViewModel bookViewModel = (BookViewModel)DataContext;
			bookViewModel.MessageBoxShowDelegate = text => MessageBox.Show(text, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		protected override void OnClosing(CancelEventArgs e)    //makes window be reusable
		{
			e.Cancel = true;
			this.Hide();
		}
	}
}
