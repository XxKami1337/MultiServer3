// Copyright (C) 2016 by Barend Erasmus and donated to the public domain
using MozaicHTTP.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MozaicHTTP.Models
{
    public enum HttpStatusCode
    {
        // for a full list of status codes, see..
        // https://en.wikipedia.org/wiki/List_of_HTTP_status_codes

        Continue = 100,
        OK = 200,
        Created = 201,
        Accepted = 202,
        No_Content = 204,
        Partial_Content = 206,
        MultiStatus = 207,
        MovedPermanently = 301,
        Found = 302,
        Not_Modified = 304,
        Permanent_Redirect = 308,
        Temporary_Redirect = 307,
        BadRequest = 400,
        Forbidden = 403,
        Not_Found = 404,
        MethodNotAllowed = 405,
        RangeNotSatisfiable = 416,
        Missing_parameters = 422,
        InternalServerError = 500,
        NotImplemented = 501,
        BadGateway = 502,
        ServiceUnavailable = 503
    }

    public class HttpResponse : IDisposable
    {
        private bool disposedValue;
        #region Properties

        public HttpStatusCode HttpStatusCode { get; set; }
        public Stream? ContentStream { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        #endregion

        #region Constructors

        public HttpResponse(bool keepalive, string? HttpVersionOverride = null)
        {
            string HttpVersion = (!string.IsNullOrEmpty(HttpVersionOverride)) ? HttpVersionOverride : MozaicHTTPConfiguration.HttpVersion;

            Headers = new Dictionary<string, string>();

            if (keepalive)
                Headers.Add("Connection", "Keep-Alive");

            if (HttpVersion == "1.1" && MozaicHTTPConfiguration.EnableHTTPChunkedTransfers)
                Headers.Add("Transfer-Encoding", "chunked");
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return string.Format("{0} {1}", (int)HttpStatusCode, HttpStatusCode.ToString());
        }

        public static HttpResponse Send(bool keepalive, string? stringtosend, string mimetype = "text/plain", string[][]? HeaderInput = null, HttpStatusCode statuscode = HttpStatusCode.OK)
        {
            HttpResponse response = new(keepalive)
            {
                HttpStatusCode = statuscode
            };
            response.Headers["Content-Type"] = mimetype;
            if (HeaderInput != null)
            {
                foreach (string[] innerArray in HeaderInput)
                {
                    // Ensure the inner array has at least two elements
                    if (innerArray.Length >= 2)
                        response.Headers.Add(innerArray[0], innerArray[1]);
                }
            }
            if (stringtosend != null)
                response.ContentAsUTF8 = stringtosend;
            else
                response.ContentStream = null;

            return response;
        }

        public static HttpResponse Send(bool keepalive, byte[]? bytearraytosend, string mimetype = "text/plain", string[][]? HeaderInput = null, HttpStatusCode statuscode = HttpStatusCode.OK)
        {
            HttpResponse response = new(keepalive)
            {
                HttpStatusCode = statuscode
            };
            response.Headers["Content-Type"] = mimetype;
            if (HeaderInput != null)
            {
                foreach (var innerArray in HeaderInput)
                {
                    // Ensure the inner array has at least two elements
                    if (innerArray.Length >= 2)
                    {
                        // Extract two values from the inner array
                        string value1 = innerArray[0];
                        if (!response.Headers.ContainsKey(value1))
                            response.Headers.Add(value1, innerArray[1]);
                    }
                }
            }
            if (bytearraytosend != null)
                response.ContentStream = new MemoryStream(bytearraytosend);
            else
                response.ContentStream = null;

            return response;
        }

        public static HttpResponse Send(bool keepalive, Stream? streamtosend, string mimetype = "text/plain", string[][]? HeaderInput = null, HttpStatusCode statuscode = HttpStatusCode.OK, string? HttpVersionOverride = null)
        {
            HttpResponse response = new(keepalive, HttpVersionOverride)
            {
                HttpStatusCode = statuscode
            };
            response.Headers["Content-Type"] = mimetype;
            if (HeaderInput != null)
            {
                foreach (string[]? innerArray in HeaderInput)
                {
                    // Ensure the inner array has at least two elements
                    if (innerArray.Length >= 2)
                        // Extract two values from the inner array
                        response.Headers.Add(innerArray[0], innerArray[1]);
                }
            }
            if (streamtosend != null)
            {
                if (streamtosend.CanSeek)
                    response.ContentStream = streamtosend;
                else
                {
                    response.ContentStream = new CyberBackendLibrary.Extension.HugeMemoryStream(streamtosend, MozaicHTTPConfiguration.BufferSize)
                    {
                        Position = 0
                    };
                    streamtosend.Close();
                    streamtosend.Dispose();
                }
            }
            else
                response.ContentStream = null;

            return response;
        }

        public string ContentAsUTF8
        {
            set
            {
                ContentStream = value.ToStream();
            }
        }

        public string ToHeader()
        {
            StringBuilder strBuilder = new();

            strBuilder.Append(string.Format("HTTP/{0} {1} {2}\r\n", MozaicHTTPConfiguration.HttpVersion, (int)HttpStatusCode, HttpStatusCode.ToString().Replace("_", " ")));
            strBuilder.Append(Headers.ToHttpHeaders());
            strBuilder.Append("\r\n\r\n");

            return strBuilder.ToString();
        }

        public bool IsValid()
        {
            if (ContentStream == null)
                return false;

            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    try
                    {
                        ContentStream?.Close();
                        ContentStream?.Dispose();
                    }
                    catch (ObjectDisposedException)
                    {
                        // Always check for disposed object according to the C# documentation.
                    }
                }

                // TODO: libérer les ressources non managées (objets non managés) et substituer le finaliseur
                // TODO: affecter aux grands champs une valeur null
                disposedValue = true;
            }
        }

        // // TODO: substituer le finaliseur uniquement si 'Dispose(bool disposing)' a du code pour libérer les ressources non managées
        // ~HttpResponse()
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

        #endregion
    }
}