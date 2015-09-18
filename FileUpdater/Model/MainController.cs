using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileUpdater.Model {
	public class MainController {
		private Dictionary<string, FileController> controllersDict;
        private List<FileController> controllers;

		public List<FileController> Controllers { get { return controllers; } }

		public MainController() {
			controllers = new List<FileController>();
            controllersDict = new Dictionary<string, FileController>();
		}

		public void AddController(string source) {
			try {
				FileController controller = new FileController(source);
                controllers.Add(controller);
                controllersDict.Add(source, controller);
			} catch (FileNotFoundException exc) {
				//TODO: here must be notification message
			}
		}

		public void AddDestinationToSource(string source, string destination) {
			FileController controller = controllersDict[source];
			if (controller != null) {
				try {
					controller.AddDestionationFile(destination);
				} catch (FileNotFoundException exc) {
					//TODO: notification message that destination file does not exist
				}
			} else {
				//TODO: notify that such sourche does not exist
			}
		}

        public void RemoveSource(string source)
        {
            FileController controller = controllersDict[source];
            controller.Dispose();
            controllers.Remove(controller);
            controllersDict.Remove(source);
        }

        public void RemoveDestinationFromSource(string source, string destination)
        {
            FileController controller = controllersDict[source];
            controller.RemoveDestinationFile(destination);
        }
	}
}
