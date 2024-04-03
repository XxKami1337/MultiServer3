using BackendProject.MiscUtils;
using CustomLogger;
using HttpMultipartParser;
using System.Text;
using System.Text.RegularExpressions;

namespace WebUtils.PREMIUMAGENCY
{
    public class PREMIUMAGENCYClass : IDisposable
    {
        private string workpath;
        private string absolutepath;
        private string method;
        private bool disposedValue;

        public PREMIUMAGENCYClass(string method, string absolutepath, string workpath)
        {
            this.workpath = workpath;
            this.absolutepath = absolutepath;
            this.method = method;
        }

        public string? ProcessRequest(byte[] PostData, string ContentType)
        {
            if (string.IsNullOrEmpty(absolutepath))
                return null;

            string eventId = string.Empty;
            string? boundary = HTTPUtils.ExtractBoundary(ContentType);
            using (MemoryStream ms = new(PostData))
            {
                var data = MultipartFormDataParser.Parse(ms, boundary);

                eventId = data.GetParameterValue("evid");

                ms.Flush();
            }

            switch (method)
            {
                case "POST":
                    switch (absolutepath)
                    {
                        case "/eventController/getResource.do":
                            return Resource.getResourcePOST(PostData, ContentType, workpath);
                        case "/eventController/checkEvent.do":
                            return Event.checkEventRequestPOST(PostData, ContentType, eventId, workpath);
                        case "/eventController/entryEvent.do":
                            return Event.entryEventRequestPOST(PostData, ContentType, eventId, workpath);
                        case "/eventController/clearEvent.do":
                            return Event.clearEventRequestPOST(PostData, ContentType, eventId, workpath);
                        case "/eventController/getEventTrigger.do":
                            return Trigger.getEventTriggerRequestPOST(PostData, ContentType, workpath, eventId);
                        case "/eventController/getEventTriggerEx.do":
                            return Trigger.getEventTriggerExRequestPOST(PostData, ContentType, workpath, eventId);
                        case "/eventController/confirmEventTrigger.do":
                            return Trigger.confirmEventTriggerRequestPOST(PostData, ContentType, workpath, eventId);
                        case "/eventController/setUserEventCustom.do":
                            return Custom.setUserEventCustomPOST(PostData, ContentType, workpath, eventId);
                        case "/eventController/getUserEventCustom.do":
                            return Custom.getUserEventCustomRequestPOST(PostData, ContentType, workpath, eventId);
                        case "/eventController/getUserEventCustomList.do":
                            return Custom.getUserEventCustomRequestListPOST(PostData, ContentType, workpath, eventId);
                        case "/eventController/getItemRankingTable.do":
                            return Ranking.getItemRankingTableHandler(PostData, ContentType, workpath, eventId);
                        case "/eventController/entryItemRankingPoints.do":
                            return Ranking.entryItemRankingPointsHandler(PostData, ContentType, workpath, eventId);
                        case "/eventController/getItemRankingTargetList.do":
                            return Ranking.getItemRankingTargetListHandler(PostData, ContentType, workpath, eventId);
                        case "/eventController/getInformationBoardSchedule.do":
                            return InfoBoard.getInformationBoardSchedulePOST(PostData, ContentType, workpath, eventId);
                        default:
                            {
                                LoggerAccessor.LogError($"[PREMIUMAGENCY] - Unhandled server request discovered: {absolutepath.Replace("/eventController/", "")} | DETAILS: \n{Encoding.UTF8.GetString(PostData)}");
                            }
                            break;
                    }
                    break;

                default:
                    {
                        LoggerAccessor.LogError($"[PREMIUMAGENCY] - Unhandled Server method: {method}");
                    }
                    break;
            }

            return null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    workpath = string.Empty;
                    absolutepath = string.Empty;
                    method = string.Empty;
                }

                // TODO: libérer les ressources non managées (objets non managés) et substituer le finaliseur
                // TODO: affecter aux grands champs une valeur null
                disposedValue = true;
            }
        }

        // // TODO: substituer le finaliseur uniquement si 'Dispose(bool disposing)' a du code pour libérer les ressources non managées
        // ~PREMIUMAGENCYClass()
        // {
        //     // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public static void WriteFormDataToFile(string formData, string filePath)
        {
            try
            {
                // Regular expression to match each key-value pair
                Regex regex = new Regex(@"name=""([^""]+)""\s*([\s\S]*?)\s*---------");
                MatchCollection matches = regex.Matches(formData);

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (Match match in matches)
                    {
                        string key = match.Groups[1].Value.Trim();
                        string value = match.Groups[2].Value.Trim();

                        // Write key-value pair to the file
                        writer.WriteLine($"{key}: {value}");
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerAccessor.LogError($"Fatal exception occured in WriteFormDataToFile with exception:\n", ex);
            }
        }

        public static List<(string, string)>? ReadFormDataFromFile(string filePath)
        {
            try
            {

                List<(string, string)> formData = new List<(string, string)>();

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    string currentKey = string.Empty;
                    string currentValue = string.Empty;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(":"))
                        {
                            string[] parts = line.Split(new char[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries);
                            if (parts.Length == 2)
                            {
                                if (currentKey != null)
                                {
                                    formData.Add((currentKey.Trim(), currentValue.Trim()));
                                }
                                currentKey = parts[0].Trim();
                                currentValue = parts[1].Trim();
                            }
                        }
                        else
                        {
                            currentValue += "\n" + line.Trim();
                        }
                    }

                    // Add the last key-value pair
                    if (currentKey != null)
                    {
                        formData.Add((currentKey.Trim(), currentValue.Trim()));
                    }
                }

                return formData;
            }
            catch (Exception ex)
            {
                LoggerAccessor.LogError($"Fatal exception occured in ReadFormDataFromFile with exception:\n", ex);
                return null;
            }


        }
    }
}