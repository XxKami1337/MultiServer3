using CyberBackendLibrary.DataTypes;
using CyberBackendLibrary.HTTP;
using MozaicHTTP.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace MozaicHTTP.Extensions
{
    public class PHP
    {
        public static (byte[]?, string[][]) ProcessPHPPage(string FilePath, string phppath, string phpver, string ip, string? port, HttpRequest request)
        {
            // We want to check if the router allows external IPs first.
            string ServerIP = CyberBackendLibrary.TCP_IP.IPUtils.GetPublicIPAddress(true);
            try
            {
                using TcpClient client = new(ServerIP, request.ServerPort);
                client.Close();
            }
            catch // Failed to connect, so we fallback to local IP.
            {
                ServerIP = CyberBackendLibrary.TCP_IP.IPUtils.GetLocalIPAddress(true).ToString();
            }

            if (!string.IsNullOrEmpty(request.Url) && !string.IsNullOrEmpty(port))
            {
                int index = request.Url.IndexOf("?");
                string? queryString = index == -1 ? string.Empty : request.Url[(index + 1)..];

                // Get paths for PHP
                string? documentRootPath = Path.GetDirectoryName(FilePath);
                string? scriptFilePath = Path.GetFullPath(FilePath);
                string? scriptFileName = Path.GetFileName(FilePath);
                string? tempPath = Path.GetTempPath();

                string[][] HeadersLocal = Array.Empty<string[]>();
                byte[]? returndata = null;
                byte[]? postData = null;

                // Extract POST data (if available)
                if (request.Method == "POST")
                    postData = request.DataAsBytes;

                Process proc = new();

                proc.StartInfo.FileName = $"{phppath}/{phpver}/php-cgi";

                proc.StartInfo.Arguments = $"-q -d \"error_reporting=E_ALL\" -d \"display_errors={MozaicHTTPConfiguration.PHPDebugErrors}\" -d \"expose_php=Off\" -d \"include_path='{documentRootPath}'\" " +
                             $"-d \"extension_dir='{$@"{phppath}/{phpver}/ext/"}'\" \"{FilePath}\"";

                proc.StartInfo.CreateNoWindow = false;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardInput = true;

                proc.StartInfo.StandardOutputEncoding = Encoding.UTF8;

                proc.StartInfo.EnvironmentVariables.Clear();

                // Set environment variables for PHP

                // Set content length for POST data
                if (postData != null)
                    proc.StartInfo.EnvironmentVariables.Add("CONTENT_LENGTH", postData.Length.ToString());

                proc.StartInfo.EnvironmentVariables.Add("GATEWAY_INTERFACE", "CGI/1.1");
                proc.StartInfo.EnvironmentVariables.Add("SERVER_PROTOCOL", "HTTP/1.1");
                proc.StartInfo.EnvironmentVariables.Add("REDIRECT_STATUS", "200");
                proc.StartInfo.EnvironmentVariables.Add("DOCUMENT_ROOT", documentRootPath);
                proc.StartInfo.EnvironmentVariables.Add("SCRIPT_NAME", scriptFileName);
                proc.StartInfo.EnvironmentVariables.Add("SCRIPT_FILENAME", scriptFilePath);
                proc.StartInfo.EnvironmentVariables.Add("QUERY_STRING", queryString);
                proc.StartInfo.EnvironmentVariables.Add("CONTENT_TYPE", request.GetContentType());
                proc.StartInfo.EnvironmentVariables.Add("REQUEST_METHOD", request.Method);
                proc.StartInfo.EnvironmentVariables.Add("USER_AGENT", request.RetrieveHeaderValue("User-Agent"));
                proc.StartInfo.EnvironmentVariables.Add("SERVER_ADDR", ServerIP);
                proc.StartInfo.EnvironmentVariables.Add("SERVER_PORT", request.ServerPort.ToString());
                proc.StartInfo.EnvironmentVariables.Add("REMOTE_ADDR", ip);
                proc.StartInfo.EnvironmentVariables.Add("REMOTE_HOST", ip);
                proc.StartInfo.EnvironmentVariables.Add("REMOTE_PORT", port);
                proc.StartInfo.EnvironmentVariables.Add("REFERER", request.RetrieveHeaderValue("Referer"));
                proc.StartInfo.EnvironmentVariables.Add("REQUEST_URI", $"http://{ip}:{port}{request.Url}");
                proc.StartInfo.EnvironmentVariables.Add("HTTP_COOKIE", request.RetrieveHeaderValue("Cookie"));
                proc.StartInfo.EnvironmentVariables.Add("HTTP_ACCEPT", request.RetrieveHeaderValue("Accept"));
                proc.StartInfo.EnvironmentVariables.Add("HTTP_ACCEPT_CHARSET", request.RetrieveHeaderValue("Accept-Charset"));
                proc.StartInfo.EnvironmentVariables.Add("HTTP_ACCEPT_ENCODING", request.RetrieveHeaderValue("Accept-Encoding"));
                proc.StartInfo.EnvironmentVariables.Add("HTTP_ACCEPT_LANGUAGE", request.RetrieveHeaderValue("Accept-Language"));
                proc.StartInfo.EnvironmentVariables.Add("TMPDIR", tempPath);
                proc.StartInfo.EnvironmentVariables.Add("TEMP", tempPath);

                proc.Start();

                if (postData != null)
                {
                    // Write request body to standard input, for POST data
                    using StreamWriter? sw = proc.StandardInput;
                    sw.BaseStream.Write(postData, 0, postData.Length);
                }

                // Write headers and content to response stream
                bool headersEnd = false;
                using (MemoryStream ms = new())
                using (StreamReader sr = proc.StandardOutput)
                using (StreamWriter output = new(ms))
                {
                    int i = 0;
                    string? line = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!headersEnd)
                        {
                            if (line == string.Empty)
                            {
                                headersEnd = true;
                                continue;
                            }

                            // The first few lines are the headers, with a
                            // key and a value. Catch those, to write them
                            // into our response headers.
                            index = line.IndexOf(':');

                            HeadersLocal = DataTypesUtils.AddElement(HeadersLocal, new string[] { line[..index], line[(index + 2)..] });
                        }
                        else
                            // Write non-header lines into the output as is.
                            output.WriteLine(line);

                        i++;
                    }

                    output.Flush();
                    returndata = ms.ToArray();
                }

                proc.WaitForExit(); // Wait for the PHP process to complete
                proc.Close();
                proc.Dispose();

                return (returndata, HeadersLocal);
            }

            return (null, Array.Empty<string[]>());
        }
    }
}