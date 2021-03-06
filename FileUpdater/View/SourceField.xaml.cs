﻿using System;
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
using FileUpdater.Events;

namespace FileUpdater.View {
	/// <summary>
	/// Interaction logic for SourceField.xaml
	/// </summary>
	public partial class SourceField : UserControl {
		private bool selected;
		private int key;

        public event EventHandler<KeyChangedEventArgs> KeyChanged;

		public bool IsSelected { get { return (bool)SelectCheckBox.IsChecked; } }
		public int Key {
			set {
				key = value;
				KeyField.Text = value.ToString();
			}
			get {
				return key;
			}
		}

		public string Path {
			set {
				PathField.Text = value;
			}
			get {
				return PathField.Text;
			}
		}

		public SourceField() {
			InitializeComponent();

            KeyField.TextChanged += KeyField_TextChanged;	
		}

        private void KeyField_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (KeyChanged != null)
            {
                KeyChanged(this, new KeyChangedEventArgs(key, int.Parse(KeyField.Text)));
            }
        }

        public SourceField(int key, string path) : this() {
			Key = key;
			Path = path;
		}
	}
}
