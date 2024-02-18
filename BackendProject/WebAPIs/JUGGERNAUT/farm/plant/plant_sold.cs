﻿using System.Xml.Linq;
using System.Xml;

namespace BackendProject.WebAPIs.JUGGERNAUT.farm.plant
{
    public class plant_sold
    {
        public static string? ProcessSold(Dictionary<string, string>? QueryParameters, string apiPath)
        {
            if (QueryParameters != null)
            {
                string? user = QueryParameters["user"];
                string? type = QueryParameters["type"];
                string? id = QueryParameters["id"];
                string? amount = QueryParameters["amount"];

                if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(amount))
                {
                    Directory.CreateDirectory($"{apiPath}/juggernaut/farm/User_Data");

                    if (File.Exists($"{apiPath}/juggernaut/farm/User_Data/{user}.xml"))
                    {
                        // Load the XML string into an XmlDocument
                        XmlDocument xmlDoc = new();
                        xmlDoc.LoadXml(RemovePlantEntry(File.ReadAllText($"{apiPath}/juggernaut/farm/User_Data/{user}.xml"), type, id));

                        // Find the <gold> element
                        XmlElement? goldElement = xmlDoc.SelectSingleNode("/xml/resources/gold") as XmlElement;

                        if (goldElement != null)
                        {
                            try
                            {
                                // Replace the value of <gold> with a new value
                                goldElement.InnerText = (int.Parse(goldElement.InnerText) + int.Parse(amount)).ToString();
                            }
                            catch (Exception)
                            {
                                // Not Important
                            }

                            File.WriteAllText($"{apiPath}/juggernaut/farm/User_Data/{user}.xml", xmlDoc.OuterXml);
                        }
                    }

                    return string.Empty;
                }
            }

            return null;
        }

        private static string RemovePlantEntry(string xmlData, string type, string id)
        {
            XDocument xdoc = XDocument.Parse(xmlData);

            XElement? plantToRemove = xdoc.Descendants("plant")
                .FirstOrDefault(a =>
                    a.Element("t")?.Value == type &&
                    a.Element("id")?.Value == id
                );

            if (plantToRemove != null)
                plantToRemove.Remove();

            return xdoc.ToString();
        }
    }
}
