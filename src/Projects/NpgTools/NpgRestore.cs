// <copyright file="NpgRestore.cs" company="nPgTools">
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
    using System.Data;
    using System.Data.Common;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using NpgTools;

    /// <summary>
    /// The NpgRestore class that permits to make a pg_restore
    /// </summary>
    public class NpgRestore
    {
        /// <summary>
        /// This contains informations about the temporary
        /// directory used to make restore
        /// </summary>
        private DirectoryInfo temporaryPath;

        /// <summary>
        /// This function launch the restore process, with the NpgsqlConnection parameters. Restore
        /// will be done on the server and database pointed by the NpgsqlConnection
        /// </summary>
        /// <param name="connection">The NpgsqlConnection containg the database location
        /// settings</param>
        /// <param name="login">The login used to restore database</param>
        /// <param name="password">The password associed to login</param>
        /// <param name="backupFile">The backup file to restore</param>
        /// <returns>Returns a Result that tells if operation succeeds</returns>
        public Result Restore(
                                IDbConnection connection,
                                string login,
                                string password,
                                FileSystemInfo backupFile)
        {
            IPAddress connectionIpAddress = IPAddress.None;

            if (connection != null)
            {
                // TODO fix connection.Host and connection.Port
                if (IPAddress.TryParse(/*connection.Host*/ "127.0.0.1", out connectionIpAddress)
                    && (connectionIpAddress != IPAddress.None))
                {
                    return this.Restore(
                            connectionIpAddress,
                            5432, // (short)connection.Port,
                            connection.Database,
                            login,
                            password,
                            backupFile);
                }
                else
                {
                    return Result.InvalidIpAddress;
                }
            }
            
            return Result.InvalidNpgsqlConnection;
        }

        /// <summary>
        /// This function launch the restore process, on the localhost, on the 5432 port
        /// </summary>
        /// <param name="databaseName">The database name where to restore file</param>
        /// <param name="login">The login used to restore database</param>
        /// <param name="password">The password associed to login</param>
        /// <param name="backupFile">The backup file to restore</param>
        /// <returns>Returns a Result that tells if operation succeeds</returns>
        public Result Restore(
                                string databaseName,
                                string login,
                                string password,
                                FileSystemInfo backupFile)
        {
            return this.Restore(
                    Constants.PostgreSQLDefaultIP,
                    Constants.PostgreSQLDefaultPort,
                    databaseName,
                    login,
                    password,
                    backupFile);
        }
        
        /// <summary>
        /// This function launch the restore process
        /// </summary>
        /// <param name="address">The ip address of the server</param>
        /// <param name="port">The port of the server</param>
        /// <param name="databaseName">The database name where to restore file</param>
        /// <param name="login">The login used to restore database</param>
        /// <param name="password">The password associed to login</param>
        /// <param name="backupFile">The backup file to restore</param>
        /// <returns>Returns a Result that tells if operation succeeds</returns>
        public Result Restore(
                                IPAddress address,
                                short port,
                                string databaseName,
                                string login,
                                string password,
                                FileSystemInfo backupFile)
        {
            this.CheckParameters(address, port, databaseName, login, backupFile);
            this.Prepare();
            this.Execute(address, port, databaseName, login, password, backupFile);
            this.PostExecute();

            return Result.NoError;
        }

        /// <summary>
        /// This function checks for te most widely used parameters of a database restore
        /// </summary>
        /// <param name="address">The server IP Address</param>
        /// <param name="port">The server port number</param>
        /// <param name="databaseName">The database name where to restore file</param>
        /// <param name="login">The database login used for restoring database</param>
        /// <param name="backupFile">The location where the backup file is</param>
        /// <returns>An Result code telling the error encountered, or NoError</returns>
        private Result CheckParameters(
                                        IPAddress address,
                                        short port,
                                        string databaseName,
                                        string login,
                                        FileSystemInfo backupFile)
        {
            if (!NpgTools.Check.IsIPAddressValid(address))
            {
                return Result.InvalidIpAddress;
            }

            if (!NpgTools.Check.IsPortValid(port))
            {
                return Result.InvalidPort;
            }

            if (string.IsNullOrEmpty(databaseName))
            {
                return Result.InvalidDatabaseName;
            }

            if (string.IsNullOrEmpty(login))
            {
                return Result.InvalidLogin;
            }

            string fullBackupFileName = backupFile != null ? backupFile.FullName : string.Empty;
            if ((!string.IsNullOrEmpty(fullBackupFileName))
                && File.Exists(fullBackupFileName))
            {
                FileStream testReadFile = null;
                try
                {
                    testReadFile = File.OpenRead(fullBackupFileName);
                }
                catch (Exception)
                {
                    return Result.CouldNotReadBackupFile;
                }
                finally
                {
                    if (testReadFile != null)
                    {
                        testReadFile.Close();
                        testReadFile.Dispose();
                    }
                }
            }

            return Result.NoError;
        }

        /// <summary>
        /// This function prepares the restore, by putting all needed files in a temporary directory
        /// </summary>
        /// <returns>Returns a Result code telling if succeed</returns>
        private Result Prepare()
        {
            this.temporaryPath = NpgTools.Utils.GetTempDirectory();
            if (NpgTools.FileUtils.ExtractFilesToTempPath(this.temporaryPath, "NpgTools.PostgreSQL"))
            {
                return Result.NoError;
            }

            return Result.CouldNotCreateTempFiles;
        }

        /// <summary>
        /// This function launch the restore process
        /// </summary>
        /// <param name="address">The ip address of the server</param>
        /// <param name="port">The port of the server</param>
        /// <param name="databaseName">The database name where to restore file</param>
        /// <param name="login">The login used to restore database</param>
        /// <param name="password">The password associed to login</param>
        /// <param name="backupFile">The backup file to restore</param>
        /// <returns>Returns a Result that tells if operation succeeds</returns>
        private Result Execute(
                                    IPAddress address,
                                    short port,
                                    string databaseName,
                                    string login,
                                    string password,
                                    FileSystemInfo backupFile)
        {
            Process process = null;

            try
            {
                process = new Process();
                ProcessStartInfo informations = process.StartInfo;
                informations.FileName = this.temporaryPath.FullName + "\\pg_restore.exe";
                informations.UseShellExecute = false;
                informations.Arguments = " -p " + port.ToString();
                informations.Arguments += " -h " + address.ToString();
                informations.Arguments += " -U " + login;
                informations.Arguments += " -d " + databaseName; // Specify database bame
                informations.Arguments += " \"" + backupFile.FullName + "\"";
                informations.RedirectStandardError = true;

                Environment.SetEnvironmentVariable("PGPASSWORD", password, EnvironmentVariableTarget.Process);
                process.Start();
                System.Threading.Thread.Sleep(1000);
                Environment.SetEnvironmentVariable("PGPASSWORD", string.Empty, EnvironmentVariableTarget.Process);
                process.WaitForExit();
            }
            catch
            {
                return Result.Error;
            }
            finally
            {
                if (process != null)
                {
                    process.Close();
                    process.Dispose();
                }
            }

            if (process.ExitCode != 0)
            {
                return Result.Error;
            }

            return Result.NoError;
        }

        /// <summary>
        /// This function is called after the restore in order to clean the temporary
        /// directory from binaries
        /// </summary>
        /// <returns>Returns a Result that tells if operation succeeds</returns>
        private Result PostExecute()
        {
            if (!NpgTools.FileUtils.RemoveFilesFromTempPath(this.temporaryPath))
            {
                return Result.CouldNotDeleteTempFiles;
            }

            return Result.NoError;
        }
    }
}
