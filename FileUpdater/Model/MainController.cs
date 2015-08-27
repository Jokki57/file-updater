using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileUpdater.Model {
	public class MainController {
		private List<FileController> controllers;

		public List<FileController> Controllers { get { return controllers; } }

		public MainController() {
			controllers = new List<FileController>();
		}

		public void AddController(string source) {
			try {
				FileController controller = new FileController(source);
			} catch (FileNotFoundException exc) {
				//TODO: here must be notification message
			}
		}

		public void AddDestinationToSource(string source, string destination) {
			FileController controller = null;
			for (int i = 0, l = controllers.Count; i < l; i++) {
				if (controllers[i].SourceFileInfo.FullName.Equals(source)) {
					controller = controllers[i];
					break;
				}
			}
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
	}
}
