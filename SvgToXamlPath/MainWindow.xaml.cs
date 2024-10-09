using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace SvgToXamlPath
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var folderDialog = new OpenFolderDialog();

            XamlPathBuilder xamlPathBuilder = new XamlPathBuilder();

            if(folderDialog.ShowDialog() == true)
            {
                //MessageBox.Show(folderDialog.FolderName);

                DirectoryInfo directoryInfo = new DirectoryInfo(folderDialog.FolderName);

                FileInfo[] Files = directoryInfo.GetFiles("*.svg");

                string sum = "";

                foreach (FileInfo file in Files)
                {
                    string key = file.Name.Split('.')[0];
                    XElement newGeometryGroup = xamlPathBuilder.AddGeometryGroup(key);

                    XNamespace xNamespace = "http://www.w3.org/2000/svg";
                    XDocument svgDocument = XDocument.Load(file.FullName);

                    var arr = svgDocument.Descendants(xNamespace + "path").ToArray();

                    foreach (var node in arr)
                    {
                        if(node.Attribute("d") != null)
                            newGeometryGroup.Add(new XElement("PathGeometry", new XAttribute("Figures", node.Attribute("d").Value)));
                    }
                }
            }

            xamlPathBuilder.Save(folderDialog.FolderName);
        }
    }   
}