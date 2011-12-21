// <copyright file="Constants.cs" company="nPgTools">
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
    using System.Net;

    /// <summary>
    /// This file contains all the PostgreSQL default values, and other constants for the
    /// NpgTools Projects
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Gets the default IP address to connect to a PostgreSQL database, this is localhost
        /// </summary>
        public static IPAddress PostgreSQLDefaultIP
        {
            get { return IPAddress.Loopback; }
        }

        /// <summary>
        /// Gets the default port number of PostgreSQL database, this is 5432
        /// </summary>
        public static short PostgreSQLDefaultPort
        {
            get { return 5432; }
        }

        /// <summary>
        /// Gets the default name for dumps launched without precising the file name, by default it
        /// is the UTC (24h format) date that is used (for ex. : 2010-01-29_15-55-59_GMT.backup)
        /// </summary>
        public static string DefaultBackupFileName
        {
            get { return DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss") + "_GMT.backup"; }
        }
    }
}