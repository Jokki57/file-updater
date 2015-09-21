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
using FileUpdater.Events;

namespace FileUpdater.View {
	/// <summary>
	/// Interaction logic for DestinationField.xaml
	/// </summary>
	public partial class DestinationField : UserControl {
        private List<int> keys;

        public event EventHandler KeyChanged;

        public List<int> Keys
        {
            get { return keys; }
            set
            {
                keys = value;
                for (int i = 0, l = KeysComboBox.Items.Count; i < l; i++)
                {
                    KeysComboBox.Items.RemoveAt(i);
                }
                for (int i = 0, l = keys.Count; i < l; i++)
                {
                    KeysComboBox.Items.Add(keys[i]);
                }
            }

        }
        public SourceField sourceField { get; set; }
        public bool IsSelected { get { return (bool)SelectCheckBox.IsChecked; } }
        public int Key { get { return (int)KeysComboBox.SelectedItem; } }

        public string Path
        {
            set
            {
                PathField.Text = value;
            }
            get
            {
                return PathField.Text;
            }
        }

        public DestinationField() {
			InitializeComponent();
            
		}

        public void OnSourceDeleted(object sender, EventArgs args)
        {
            SourceField sourceField;
            if (sender is SourceField)
            {
                sourceField = sender as SourceField;
                keys.Add(sourceField.Key);
                KeysComboBox.Items.Add(sourceField.Key);
            }
        }

        public void OnSourceAdded(object sender, EventArgs args)
        {
            SourceField sourceField;
            if (sender is SourceField)
            {
                sourceField = sender as SourceField;
                keys.Remove(sourceField.Key);
                KeysComboBox.Items.Remove(sourceField.Key);
            }
        }

        public void OnSourceKeyChange(object sender, KeyChangedEventArgs args)
        {
            SourceField sourceField;
            if (sender is SourceField)
            {
                sourceField = sender as SourceField;
                keys[keys.IndexOf(args.OldKey)] = args.NewKey;
                KeysComboBox.Items.Remove(args.OldKey);
                KeysComboBox.Items.Add(args.NewKey);
            }
        }

        private void KeysComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (KeyChanged != null)
            {
                KeyChanged(this, e);
            }
        }
    }
}
