using System.Globalization;
using System.Xml.Linq;

namespace ResxGen;

public class ResxMerger
{
     private readonly CultureInfo[] supportedCultures;
        private readonly string outputDirectory;
        private readonly List<string> inputDirectories;

        public ResxMerger(CultureInfo[] supportedCultures, string outputDirectory, List<string> inputDirectories)
        {
            this.supportedCultures = supportedCultures;
            this.outputDirectory = outputDirectory;
            this.inputDirectories = inputDirectories;
        }

        public void Merge(string outputFileName)
        {
            foreach (var culture in supportedCultures)
            {
                var cultureName = culture.Name;
                var mergedXml = new XDocument(
                    new XDeclaration("1.0", "utf-8", null),
                    new XElement("root",
                        new XComment("Microsoft ResX Schema"),
                        new XComment("Version 2.0"),
                        new XElement("resheader",
                            new XElement("value", "text/microsoft-resx"),
                            new XAttribute("name", "resmimetype")),
                        new XElement("resheader",
                            new XElement("value", "2.0"),
                            new XAttribute("name", "version")),
                        new XElement("resheader",
                            new XElement("value", "System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                            new XAttribute("name", "reader")),
                        new XElement("resheader",
                            new XElement("value", "System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                            new XAttribute("name", "writer"))
                    )
                );

                var outputPath = Path.Combine(outputDirectory, $"{outputFileName}.{cultureName}.resx");
                
                if (!File.Exists(outputPath))
                {
                    File.WriteAllText(outputPath, mergedXml.ToString());
                }

                foreach (var inputDirectory in inputDirectories)
                {
                    var resxFiles = Directory.GetFiles(inputDirectory, $"*.{cultureName}.resx", SearchOption.AllDirectories);

                    foreach (var resxFile in resxFiles)
                    {
                        var inputXml = XDocument.Load(resxFile);

                        var dataElements = inputXml?.Root?.Elements("data");
                        foreach (var dataElement in dataElements!)
                        {
                            mergedXml?.Root?.Add(dataElement);
                        }
                    }
                }

                File.WriteAllText(outputPath, mergedXml?.ToString());
            }
        }
}
