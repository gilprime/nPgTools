// <copyright file="Check.cs" company="nPgTools">
// THIS SOFTWARE IS PROVIDED BY THE FREEBSD PROJECT ``AS IS'' AND ANY EXPRESS OR IMPLIED
// WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
// FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE FREEBSD PROJECT OR CONTRIBUTORS
// BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA,
// OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
// OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace NpgTools
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    /// This class contains several functions that helps to check different parameters
    /// before using them
    /// </summary>
    public static class Check
    {
        /// <summary>
        /// This function checks that an IP address is valid
        /// </summary>
        /// <param name="ip">The IP address to test</param>
        /// <returns>true if it's a real address, false otherwise</returns>
        public static bool IsIPAddressValid(IPAddress ip)
        {
            if ((ip != IPAddress.None) &&
                (ip != IPAddress.Any) &&
                (ip != IPAddress.Broadcast) &&
                (ip != IPAddress.IPv6None) &&
                (ip != IPAddress.IPv6Any))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// This function checks that a Port (TCP) is valid
        /// </summary>
        /// <param name="port">The port to test</param>
        /// <returns>true if it's valid, false otherwise</returns>
        /// <remarks>Well-known ports are unallowed (0 to 1023)</remarks>
        public static bool IsPortValid(short port)
        {
            if ((port > 1023) && (port <= short.MaxValue))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified encoding is valid a encoding.
        /// </summary>
        /// <param name="encoding">The encoding (UTF8 for example, see "Character Set Support"
        /// chapter in PostgreSQL documentation).</param>
        /// <returns>
        ///   <c>true</c> if the specified encoding is valid, otherwise <c>false</c>.
        /// </returns>
        public static bool IsValidEncoding(string encoding)
        {
            return IsValidEncoding(encoding, null);
        }

        /// <summary>
        /// Determines whether the specified encoding is valid a encoding.
        /// </summary>
        /// <param name="encoding">The encoding (UTF8 for example, see "Character Set Support"
        /// chapter in PostgreSQL documentation).</param>
        /// <param name="postgresqlVersion">The PostgreSQL version.</param>
        /// <returns>
        ///   <c>true</c> if [is valid encoding] [the specified encoding]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidEncoding(string encoding, Version postgresqlVersion)
        {
            if (!string.IsNullOrEmpty(encoding))
            {
                string[] authorizedEncodingsTab =
                    {
                        "BIG5", "EUC_CN", "EUC_JP", "EUC_JIS_2004", "EUC_KR", "EUC_TW", "GB18030",
                        "GBK", "ISO_8859_5", "ISO_8859_6", "ISO_8859_7", "ISO_8859_8", "JOHAB",
                        "LATIN1", "LATIN2", "LATIN3", "LATIN4", "LATIN5", "LATIN6", "LATIN7",
                        "LATIN8", "LATIN9", "LATIN10", "MULE_INTERNAL", "SJIS", "SHIFT_JIS_2004",
                        "SQL_ASCII", "UHC", "UTF8", "WIN866", "WIN874", "WIN1250", "WIN1251",
                        "WIN1252", "WIN1253", "WIN1254", "WIN1255", "WIN1256", "WIN1257", "WIN1258"
                    };

                List<string> authorizedEncodings = new List<string>(authorizedEncodingsTab);

                string encodingUpper = encoding.ToUpper();
                if (!authorizedEncodings.Contains(encodingUpper))
                {
                    // Specific cases
                    if (postgresqlVersion != null)
                    {
                        if (postgresqlVersion.Major == 8 && postgresqlVersion.Minor == 3)
                        {
                            // in 8.3, KOI8 is managed
                            return encodingUpper == "KOI8";
                        }
                        
                        // in 8.4 and later, KOI8R and KOI8U are managed
                        return encodingUpper == "KOI8R" || encodingUpper == "KOI8U";
                    }
                    else
                    {
                        return encodingUpper == "KOI8" || encodingUpper == "KOI8R" || encodingUpper == "KOI8U";
                    }
                }

                return true;
            }

            return false;
        }
    }
}