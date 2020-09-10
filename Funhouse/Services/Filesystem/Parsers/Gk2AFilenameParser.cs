﻿using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Funhouse.Services.Filesystem.Parsers
{
    /// <summary>
    ///     Parses filenames of satellite imagery received from GK-2A satellite and processed by <c>xrit-rc</c>.
    /// </summary>
    public class Gk2AFilenameParser : IFilenameParser
    {
        private static readonly Regex Regex = new Regex("IMG_FD_.*_([0-9]{8}_[0-9]{6})\\.jpg", RegexOptions.Compiled);
        private const string TimestampFormat = "yyyyMMdd_HHmmss";

        public DateTime? GetTimestamp(string filename)
        {
            if (!Regex.IsMatch(filename)) return null;

            // Extract timestamp from filename
            var match = Regex.Match(filename);
            var filenameTimestamp = match.Groups[1].Value;

            // parse timestamp
            return DateTime.TryParseExact(filenameTimestamp, TimestampFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var timestamp)
                ? (DateTime?) timestamp.ToUniversalTime()
                : null;
        }
    }
}