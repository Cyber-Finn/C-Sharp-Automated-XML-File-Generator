using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AutomatedXMLTestFileGenerator
{
    public partial class Form1 : Form
    {
        // Load the XSD schema
        XmlSchemaSet schemaSet = new XmlSchemaSet();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerateTestFiles_Click(object sender, EventArgs e)
        {
            try
            {
                schemaSet = new XmlSchemaSet();

                if (ValidateInputs()) 
                {
                    if (OpenXSD())
                    {
                        int numberOfFiles = HandleSelectedNumberOfFiles();

                        for (int i = 0; i < numberOfFiles; i++)
                        {
                            string testFileName = "TestFile_" + (i+1) + ".xml";
                            HandleXMLGeneration(testFileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool OpenXSD()
        {
            try
            {
                //open the XSD doc
                schemaSet.Add(null, txtXsdFilePath.Text);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("OpenXSD: " + ex.Message);
            }
            return false;
        }
        private void HandleXMLGeneration(string filename)
        {
            //creates the XML file according to spec
            try
            {
                string outputPath = Path.Combine(txtOutputTestFilePath.Text, filename);

                //ensure the schemaset is compiled
                schemaSet.Compile();

                string rootElementName = GetRootElementName();
                string namespaceUri = GetNamespace();
                if (string.IsNullOrEmpty(rootElementName)) 
                {
                    throw new InvalidOperationException("No root element found in schema");
                }

                //create xml writer to write the xml file
                // this slightly beautifies output files for us, so that it's not all on one line
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "\t", 
                    NewLineOnAttributes = true,
                };

                using (XmlWriter writer = XmlWriter.Create(outputPath, settings))
                {
                    writer.WriteStartElement("Document", GetNamespace());
                    //gen the xmls based on the schemaset definition
                    foreach (XmlSchema schema in schemaSet.Schemas())
                    {
                        foreach(XmlSchemaElement element in schema.Elements.Values)
                        {
                            if(element.Name == rootElementName)
                            {
                                WriteChildElements(writer, element);
                            }
                        }
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HandleXMLGeneration: " + ex.Message);
            }
        }

        private void WriteChildElements(XmlWriter writer, XmlSchemaElement element)
        {
            try
            {

                if (element == null)
                {
                    return;
                }
                //write child elements (if there are any)
                if (element.ElementSchemaType is XmlSchemaComplexType complexType)
                {
                    if (complexType.ContentTypeParticle is XmlSchemaSequence sequence)
                    {
                        foreach (XmlSchemaObject item in sequence.Items)
                        {
                            if (item is XmlSchemaElement childElement)
                            {
                                WriteElement(writer, childElement);
                            }
                            else if (item is XmlSchemaChoice choice)
                            {
                                WriteChoice(writer, choice);
                            }
                            //todo: handle other types too
                        }
                    }
                    else if (complexType.ContentTypeParticle is XmlSchemaChoice choice)
                    {
                        WriteChoice(writer, choice);
                    }
                    else if (complexType.ContentTypeParticle is XmlSchemaAll all)
                    {
                        foreach (XmlSchemaObject item in all.Items)
                        {
                            if (item is XmlSchemaElement childElement)
                            {
                                WriteElement(writer, childElement);
                            }
                            //todo: handle other types too
                        }
                    }
                }
                else if (element.ElementSchemaType is XmlSchemaSimpleType simpleType)
                {
                    writer.WriteString(GenerateSampleValue(simpleType));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string GenerateSampleValue(XmlSchemaSimpleType simpleType)
        {
            var restriction = simpleType.Content as XmlSchemaSimpleTypeRestriction;
            if (restriction != null)
            {
                foreach (XmlSchemaFacet facet in restriction.Facets)
                {
                    if(facet is XmlSchemaEnumerationFacet enumerationFacet)
                    {
                        return enumerationFacet.Value;
                    }
                    else if (facet is XmlSchemaMinLengthFacet minimumFacet)
                    {
                        return new string('a', int.Parse(minimumFacet.Value)); //build some test data to the minimum length
                    }
                    else if (facet is XmlSchemaMaxLengthFacet maximumFacet)
                    {
                        return new string('a', int.Parse(maximumFacet.Value)); //build some test data to the maximum length
                    }
                    else if (facet is XmlSchemaPatternFacet patternFacet)
                    {
                        //gen a sample value that matches the pattern
                        return GenerateSampleValueForPattern(patternFacet.Value);
                    }
                }
            }
            switch (simpleType.Datatype.TypeCode)
            {
                case XmlTypeCode.String:
                    return "TestData";
                case XmlTypeCode.Integer:
                    return "123";
                case XmlTypeCode.Int:
                    return "123";
                case XmlTypeCode.Decimal:
                    return "1.0";
                case XmlTypeCode.Boolean:
                    return "true";
                case XmlTypeCode.Date:
                    return DateTime.Now.ToString("yyyy-MM-dd");
                //add more cases for more complex types

                default:
                    return "TestData";
            }
        }

        private string GenerateSampleValueForPattern(string pattern)
        {
            //  this is very basic and should 100% be adjusted for more complex patterns
            switch (pattern)
            {
                case "[a-z]":
                    return "a";
                case "[A-Z]":
                    return "A";
                case "[0-9]":
                    return "0";
                case "[a-zA-Z]":
                    return "A";
                case "[a-zA-Z0-9]":
                    return "A1";
                    //add more cases as needed
                default:
                    return "TestData";
            }
        }

        public void WriteChoice(XmlWriter writer, XmlSchemaChoice choice)
        {
            try
            {
                foreach (XmlSchemaObject item in choice.Items)
                {
                    if (item is XmlSchemaElement childElement)
                    {
                        WriteElement(writer, childElement);
                    }
                    //todo: handle other types too
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void WriteElement(XmlWriter writer, XmlSchemaElement element)
        {
            try
            {
                writer.WriteStartElement(element.Name);

                //write any child elements
                WriteChildElements(writer, element);

                writer.WriteEndElement();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string GetRootElementName()
        {
            //find the root element
            foreach (XmlSchema schema in schemaSet.Schemas())
            {
                foreach (XmlSchemaElement element in schema.Elements.Values)
                {
                    return element.Name; //this just takes the first one (the root)
                }
            }
            return string.Empty;
        }
        private string GetNamespace()
        {
            //find the root element
            foreach (XmlSchema schema in schemaSet.Schemas())
            {
                return schema.TargetNamespace;
            }
            return string.Empty;
        }

        private int HandleSelectedNumberOfFiles()
        {
            return (int) txtNumberOfFiles.Value;
        }
        private bool ValidateInputs()
        {
            return !string.IsNullOrEmpty(txtOutputTestFilePath.Text) && !string.IsNullOrEmpty(txtXsdFilePath.Text);
        }
    }
}
