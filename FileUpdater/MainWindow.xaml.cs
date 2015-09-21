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
using FileUpdater.Model;
using FileUpdater.View;
using FileUpdater.Events;

namespace FileUpdater {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		private MainController mainController;
		private Dictionary<int, SourceField> sources;
		private List<DestinationField> dests;
		//'key' collection using for generating keys automatically
		private List<int> keys;

        public event EventHandler SourceDeleted;
        public event EventHandler SourceAdded;
        public event EventHandler<KeyChangedEventArgs> SourceKeyChanged;

		public MainWindow() {
			InitializeComponent();

			keys = new List<int>();

			mainController = new MainController();
			sources = new Dictionary<int, SourceField>();
			dests = new List<DestinationField>();

			AddSourceBtn.Click += AddSourceBtn_Click;
			RemoveSourceBtn.Click += RemoveSourceBtn_Click;
			AddDestBtn.Click += AddDestBtn_Click;
			RemoveDestBtn.Click += RemoveDestBtn_Click;
		}

		void RemoveDestBtn_Click(object sender, RoutedEventArgs e) {
            foreach (DestinationField destField in dests)
            {
                if (destField.IsSelected)
                {
                    mainController.RemoveDestinationFromSource(destField.sourceField.Path, destField.Path);
                    SourceAdded -= destField.OnSourceAdded;
                    SourceDeleted -= destField.OnSourceDeleted;
                    SourceKeyChanged -= destField.OnSourceKeyChange;
                }
            }
        }

        void AddDestBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DestinationField destField = new DestinationField();
                destField.Path = openFile.FileName;
                destField.Keys = keys;
                SourceAdded += destField.OnSourceAdded;
                SourceDeleted += destField.OnSourceDeleted;
                SourceKeyChanged += destField.OnSourceKeyChange;
                DestStack.Children.Add(destField);
                destField.KeyChanged += DestField_KeyChanged;
            }
        }

        private void DestField_KeyChanged(object sender, EventArgs e)
        {
            mainController.AddDestinationToSource(sources[((DestinationField)sender).Key].Path, ((DestinationField)sender).Path);
        }

        void RemoveSourceBtn_Click(object sender, RoutedEventArgs e) {
			foreach (SourceField sourceField in sources.Values) {
				if (sourceField.IsSelected) {
                    mainController.RemoveSource(sourceField.Path);
                    sources.Remove(sourceField.Key);
                    if (SourceDeleted != null)
                    {
                        SourceDeleted(sourceField, new EventArgs());
                    }
				}
			}
		}

		void AddSourceBtn_Click(object sender, RoutedEventArgs e) {
			System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
			if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				mainController.AddController(openFile.FileName);

				int key = 0;
				SourceField newSource = new SourceField();
				if (sources.Count != 0) {
                    key = keys[keys.Count - 1];
                    while (keys.IndexOf(key) != -1)
                    {
                        key++;
                    }
					keys.Add(++key);
				} else
                {
                    keys.Add(key);
                }
                sources.Add(key, newSource);
                newSource.Key = key;
				newSource.Path = openFile.FileName;
                newSource.KeyChanged += OnSourceKeyChanged;
                SourceStack.Children.Add(newSource);
                if (SourceAdded != null)
                {
                    SourceAdded(newSource, new EventArgs());
                }			
			}
		}

        private void OnSourceKeyChanged(object sender, KeyChangedEventArgs args)
        {
            if (SourceKeyChanged != null)
            {
                SourceKeyChanged(sender, args);
            }
        }
    }
}
