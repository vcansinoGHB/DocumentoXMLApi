﻿using DocumentDomain.Interfaces;
using DocumentDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentInfrastructure.Repositories
{
    public class FormatRepository: IFormat
    {
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public string Base64Decode(string base64EncodedData)
        {
            var xml = base64EncodedData;
            var base64EncodedBytes = System.Convert.FromBase64String(xml);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

        }
    }
}
