// <copyright file="Utils.cs" company="nPgTools">
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
    using System.Data.Common;
    using System.IO;

    /// <summary>
    /// This class contains utilities for NpgTools
    /// </summary>
    internal static class Utils
    {
        /// <summary>
        /// This function creates and gives a Temporary directory for working
        /// </summary>
        /// <returns>The temporary directory where we can work</returns>
        public static DirectoryInfo GetTempDirectory()
        {
            string tempPath = Path.GetTempPath();
            string tempDirectoryName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            return Directory.CreateDirectory(Path.Combine(tempPath, tempDirectoryName));
        }

        /// <summary>
        /// This function returns login and password of user for a passed NpgsqlConnection
        /// </summary>
        /// <param name="connection">the current opened NpgsqlConnection</param>
        /// <param name="login">returned login corresponding to the NpgsqlConnection passed</param>
        /// <param name="password">returned password corresponding to the NpgsqlConnection passed</param>
        /// <returns>true if succeed, false otherwise (connection null or not opened)</returns>
        public static bool GetConnectionInformationsFrom(
                                      DbConnection connection,
                                      out string login,
                                      out string password)
        {
            login = string.Empty;
            password = string.Empty;

            if ((connection != null) && (connection.State == System.Data.ConnectionState.Open))
            {
                DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
                builder.ConnectionString = connection.ConnectionString;

                if (builder != null)
                {
                    object value = null;
                    bool result = builder.TryGetValue("User Id", out value);
                    if (result)
                    {
                        login = (string)value;
                    }

                    result &= builder.TryGetValue("Password", out value);
                    if (result)
                    {
                        password = (string)value;
                    }

                    builder.Clear();
                    return result;
                }
            }

            return false;
        }
    }
}