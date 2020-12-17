﻿using Library.UI;
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
using System.Windows.Shapes;

namespace Library.View
{
    /// <summary>
    /// Interaction logic for EditReaderView.xaml
    /// </summary>
    public partial class EditReaderView : Window, IWindow
    {
        public EditReaderView()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)    //makes window be reusable
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
