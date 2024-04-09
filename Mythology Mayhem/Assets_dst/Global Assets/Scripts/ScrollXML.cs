using System.Xml;
using System.Xml.Serialization;

[XmlRoot(ElementName = "root")]
public class TriggerXml
{
  [XmlElement(ElementName = "scroll")]
  public string scroll;
}

[XmlRoot(ElementName = "scroll")]
public class FontXml
{
  [XmlElement(ElementName = "text")]
  public string text;
}