using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SvgToXamlPath
{
    public class XamlPathBuilder
    {
        XNamespace x = "http://schemas.microsoft.com/winfx/2006/xaml";
        XNamespace presentation = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";
        XDocument xDocument;
        public XamlPathBuilder()
        {
           xDocument = new XDocument(new XElement("root", new XAttribute(XNamespace.Xmlns + "x", x)));
        }

        public XElement AddGeometryGroup(string Key)
        {
            var newGeometryGroup = new XElement("GeometryGroup", new XAttribute(x + "Key", Key));

            xDocument.Root.Add(newGeometryGroup);

            return newGeometryGroup;
        }

        public void Save(string path)
        {
            path += ".xaml";
            xDocument.Save(path);
        }
    }
}
