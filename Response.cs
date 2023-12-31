﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HTTPServer
{

    public enum StatusCode
    {
        OK = 200,
        InternalServerError = 500,
        NotFound = 404,
        BadRequest = 400,
        Redirect = 301
    }

    class Response
    {
        string responseString;
        public string ResponseString
        {
            get
            {
                return responseString;
            }
        }
        StatusCode code;
        List<string> headerLines = new List<string>();
        public Response(StatusCode code, string contentType, string content, string redirectoinPath)
        {
            // TODO: Add headlines (Content-Type, Content-Length,Date, [location if there is redirection])
            headerLines.Add("Content-Type: " + contentType);
            headerLines.Add("Content-Length: " + content.Length);
            headerLines.Add("Date: " + DateTime.Now);
            if(redirectoinPath != string.Empty)
                headerLines.Add("Location: " +  redirectoinPath);

            string headerlinesSection = string.Empty;
            foreach (string header in headerLines)
            {
                headerlinesSection += header + "\r\n";
            }

            // TODO: Create the request string
            string statusLine = GetStatusLine(code);
            responseString = statusLine +                                       //status line
                headerlinesSection +                                            //header line
                "\r\n" +                                                        //blank line
                content + "\r\n";                                               //content
        }

        private string GetStatusLine(StatusCode code)
        {
            // TODO: Create the response status line and return it
            string message = string.Empty;
            if ((int)code == 200) message = "OK";
            if ((int)code == 500) message = "InternalServerError";
            if ((int)code == 404) message = "Not Found";
            if ((int)code == 400) message = "Bad Request";
            if ((int)code == 301) message = "Redirect";
            string statusLine = "HTTP/1.1 " + (int)code + " " + message + "\r\n";

            return statusLine;
        }
    }
}
