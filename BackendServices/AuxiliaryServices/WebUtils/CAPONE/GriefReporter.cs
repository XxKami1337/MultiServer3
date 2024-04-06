﻿using CustomLogger;
using System.Text;
using BackendProject.MiscUtils;
using HttpMultipartParser;
using static Community.CsharpSqlite.Sqlite3;
using Newtonsoft.Json.Linq;

namespace WebUtils.CAPONE
{
    public class GriefReporter
    {
        public static string? caponeContentStoreUpload(byte[] PostData, string? ContentType, string workPath)
        {
            string? boundary = HTTPUtils.ExtractBoundary(ContentType);

            using (MemoryStream ms = new(PostData))
            {
                try
                {
                    // Read the multipart content.
                    var provider = MultipartFormDataParser.Parse(ms, boundary);

                    Directory.CreateDirectory(workPath);

                    // Process each part.
                    foreach (var part in provider.Files)
                    {
                        // Only process file parts.
                        if (part.FileName != null)
                        {
                            // Get the filename.
                            string fileName = part.FileName;

                            // Save the file with name.
                            string filePath = Path.Combine(workPath, fileName);

                            LoggerAccessor.LogInfo($"[CAPONE] - Writing evidence file {fileName} to {filePath}!");

                            // Save the file content.
                            using (FileStream fs = new FileStream(filePath, FileMode.Create))
                            {
                                fs.Write(Encoding.UTF8.GetBytes(Convert.ToString(part.Data)), 0, Convert.ToInt32(part.Data.Length));
                                LoggerAccessor.LogInfo($"[CAPONE] GriefReporter - Written evidence file {fileName} to {filePath}!");
                            }
                        }
                    }
                    LoggerAccessor.LogInfo($"[CAPONE] GriefReporter - GriefReport evidence receieved and written to contentStore!");

                    ms.Flush();
                    return "";
                }
                catch (Exception e)
                {
                    return $"InternalServerError with exception {e}";
                }


            }

        }

        public static string? caponeReportCollectorSubmit(byte[] PostData, string? ContentType, string workPath)
        {
            using (MemoryStream ms = new(PostData))
            {
                string fileName = string.Empty;

                JObject jObject = JObject.Parse(Encoding.UTF8.GetString(PostData)); 

                Uri dataURL = (Uri)VariousUtils.GetValueFromJToken(jObject, "dataLocation");
                string finalPath = Path.Combine(workPath, dataURL.AbsolutePath);

                LoggerAccessor.LogWarn($"[CAPONE] GriefReporter - Path check {finalPath}");

                try
                {
                    Directory.CreateDirectory(finalPath);

                    // Save the file with name.
                    string filePath = Path.Combine(finalPath, fileName);

                    LoggerAccessor.LogInfo($"[CAPONE] GriefReporter - Writing JSON Summary {fileName} to {filePath}!");

                    // Save the file content.
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        fs.Write(Encoding.UTF8.GetBytes(Convert.ToString(PostData)), 0, Convert.ToInt32(PostData.Length));
                        LoggerAccessor.LogInfo($"[CAPONE] GriefReporter - Written JSON {fileName} to {filePath}!");
                    }
                    LoggerAccessor.LogInfo($"[CAPONE] GriefReporter - GriefReport JSON receieved and written to contentStore!");

                    ms.Flush();
                    return "";
                }
                catch (Exception e)
                {
                    return $"InternalServerError with exception {e}";
                }


            }

        }

    }
}