// <copyright file="NpgDump.cs" company="nPgTools">
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
    using System.Data.Common;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Security;
    using NpgTools;

    /// <summary>
    /// The NpgDump class that permits to make a pg_dump 
    /// </summary>
    public class NpgDump
    {
        /// <summary>
        /// This contains informations about the temporary
        /// directory used to make dump
        /// </summary>
        private DirectoryInfo temporaryPath;

        /// <summary>
        /// This contains the database name to save
        /// </summary>
        private string databaseName;
        
        /// <summary>
        /// This contains the host IP address
        /// </summary>
        private IPAddress host;

        /// <summary>
        /// This contains the host TCP port
        /// </summary>
        private short port;

        /// <summary>
        /// This contains the login used to make the dump
        /// </summary>
        private string login;

        /// <summary>
        /// This contains the login's password used to make the dump
        /// </summary>
        private string password;

        /// <summary>
        /// Selects the format of the output
        /// </summary>
        private NpgOptFormat format = NpgOptFormat.Plain;

        /// <summary>
        /// Specify the compression level to use, from 0 to 9 (Zero means no compression)
        /// </summary>
        private short compress = 0;

        /// <summary>
        /// Do not wait forever to acquire shared table locks at the beginning of the dump.
        /// Allowed values vary depending on the server version you are dumping from, but
        /// an integer number of milliseconds is accepted by all versions since 7.3. 
        /// </summary>
        private int lockWaitTimeout = -1;

        /// <summary>
        /// Dump only the data, not the schema (data definitions). 
        /// </summary>
        private bool dataOnly = false;

        /// <summary>
        /// Include large objects in the dump.
        /// </summary>
        private bool blobs = true;

        /// <summary>
        /// Output commands to clean (drop) database objects prior to (the commands for)
        /// creating them. 
        /// </summary>
        private bool clean = false;
        
        /// <summary>
        /// Begin the output with a command to create the database itself and reconnect to the
        /// created database. 
        /// </summary>
        private bool create = false;
        
        /// <summary>
        /// Initializes a new instance of the NpgDump class, with the NpgsqlConnection parameters
        /// </summary>
        /// <param name="connection">The NpgsqlConnection containing all settings</param>
        public NpgDump(DbConnection connection)
        {
            this.GetDatasFromNpgsqlConnection(connection);
        }

        /// <summary>
        /// Initializes a new instance of the NpgDump class, on the localhost, on the 5432 port
        /// </summary>
        /// <param name="databaseName">The database name to save</param>
        /// <param name="login">The login used to save database</param>
        /// <param name="password">The password associed to login</param>
        public NpgDump(string databaseName, string login, string password)
        {
            this.login = login;
            this.password = password;
            this.databaseName = databaseName;
            this.host = Constants.PostgreSQLDefaultIP;
            this.port = Constants.PostgreSQLDefaultPort;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the name of the database to be dumped
        /// </summary>
        public string DatabaseName
        {
            get { return this.databaseName; }
            set { this.databaseName = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the host name of the machine on which the
        /// server is running
        /// </summary>
        public IPAddress Host
        {
            get { return this.host; }
            set { this.host = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the TCP port on which the server is
        /// listening for connections
        /// </summary>
        public short Port
        {
            get { return this.port; }
            set { this.port = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether login used to perform the dump
        /// </summary>
        public string Login
        {
            get { return this.login; }
            set { this.login = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the password of the user that performs the
        /// database dump
        /// </summary>
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the format of the output of the database dump
        /// </summary>
        public NpgOptFormat Format
        {
            get { return this.format; }
            set { this.format = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the compression level used for the custom and
        /// plain format
        /// </summary>
        public short Compress
        {
            get
            {
                return this.compress;
            }

            set
            {
                if ((value >= 0) && (value <= 9))
                {
                    this.compress = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the number of milliseconds to acquire shared
        /// table locks at the beginning of the dump.
        /// </summary>
        public int LockWaitTimeout
        {
            get
            {
                return this.lockWaitTimeout;
            }

            set
            {
                if (value > 0)
                {
                    this.lockWaitTimeout = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether even the dump will only dump the datas
        /// </summary>
        public bool DataOnly
        {
            get { return this.dataOnly; }
            set { this.dataOnly = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dump will include the large objects
        /// </summary>
        public bool Blobs
        {
            get { return this.blobs; }
            set { this.blobs = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dump will include database cleaning before
        /// restore
        /// </summary>
        public bool Clean
        {
            get { return this.clean; }
            set { this.clean = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dump will include database creation before
        /// restore
        /// </summary>
        public bool Create
        {
            get { return this.create; }
            set { this.create = value; }
        }

        /// <summary>
        /// This function launch the dump process and generates the a dump file
        /// with the current date and time in the application directory
        /// </summary>
        /// <returns>Returns a Result that tells if operation succeeds</returns>
        public Result Dump()
        {
            string processArgs = this.GetDumpArguments(new FileInfo(Constants.DefaultBackupFileName));
            if (!string.IsNullOrEmpty(processArgs))
            {
                return this.Dump(processArgs);
            }

            return Result.CouldNotBuildDumpArguments;
        }

        /// <summary>
        /// This function launch the dump process and generates the a dump file
        /// with the current date and time in the application directory
        /// </summary>
        /// <param name="processArguments">Contains the arguments to pass to the pg_dump
        /// process</param>
        /// <returns>Returns a Result that tells if operation succeeds</returns>
        public Result Dump(string processArguments)
        {
            return this.Dump(processArguments, new FileInfo(Constants.DefaultBackupFileName));
        }

        /// <summary>
        /// This function launch the dump process and generates the file given in parameter
        /// </summary>
        /// <param name="processArguments">Contains the arguments to pass to the pg_dump
        /// process</param>
        /// <param name="backupFile">Were the backup should be done</param>
        /// <returns>Returns a Result that tells if operation succeeds</returns>
        public Result Dump(string processArguments, FileInfo backupFile)
        {
            Result current = Result.NoError;
            if ((current = this.CheckParameters(backupFile)) != Result.NoError)
            {
                return current;
            }

            if ((current = this.Prepare()) != Result.NoError)
            {
                return current;
            }

            if ((current = this.Execute(processArguments)) != Result.NoError)
            {
                return current;
            }

            if ((current = this.PostExecute()) != Result.NoError)
            {
                return current;
            }

            return current;
        }

        /// <summary>
        /// This function will get the necessary parameters that can be getted from the
        /// NpgsqlConnection given in parameter
        /// </summary>
        /// <param name="connection">The NpgsqlConnection containng the database location
        /// settings</param>
        private void GetDatasFromNpgsqlConnection(DbConnection connection)
        {
            Utils.GetConnectionInformationsFrom(connection, out this.login, out this.password);

            // TODO: fix connection.Host and connection.Port
            // IPAddress.TryParse(connection.Host, out this.host);
            // this.port = (short)connection.Port;
        }

        /// <summary>
        /// This function checks for te most widely used parameters of a database dump
        /// </summary>
        /// <param name="backupFile">The location where to put the backup file</param>
        /// <returns>An error code telling the error encountered, or NoError</returns>
        private Result CheckParameters(FileInfo backupFile)
        {
            if (!NpgTools.Check.IsIPAddressValid(this.host))
            {
                return Result.InvalidIpAddress;
            }

            if (!NpgTools.Check.IsPortValid(this.port))
            {
                return Result.InvalidPort;
            }

            if (string.IsNullOrEmpty(this.databaseName))
            {
                return Result.InvalidDatabaseName;
            }

            if (string.IsNullOrEmpty(this.login))
            {
                return Result.InvalidLogin;
            }

            if ((backupFile != null) && (!string.IsNullOrEmpty(backupFile.FullName)))
            {
                FileStream testCreateFile = null;
                using (testCreateFile = backupFile.Create())
                {
                    if (testCreateFile != null)
                    {
                        testCreateFile.Close();
                        testCreateFile.Dispose();
                    }
                    else
                    {
                        return Result.CouldNotCreateBackupFile;
                    }

                    testCreateFile.Dispose();
                }

                if (testCreateFile != null)
                {
                    testCreateFile.Dispose();
                }
            }

            return Result.NoError;
        }

        /// <summary>
        /// This function prepares the dump, by putting all needed files in a temporary directory
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
        /// This function generates the arguments to pass to the pg_dump process, in order to
        /// execute the process with the parameters setted
        /// </summary>
        /// <param name="backupFile">The location where to put the backup file</param>
        /// <returns>the argument to pass to the pg_dump process or null if it can't build
        /// the process arguments</returns>
        private string GetDumpArguments(FileInfo backupFile)
        {
            if (backupFile != null)
            {
                try
                {
                    string arguments = " -p " + this.port;
                    arguments += " -h " + this.host.ToString();
                    arguments += " -U " + this.login;
                    arguments += " -F" + ((char)this.format).ToString();

                    if (this.format != NpgOptFormat.Tar)
                    {
                        arguments += " -Z" + this.compress.ToString();
                    }

                    if (this.lockWaitTimeout != -1)
                    {
                        arguments += " --lock-wait-timeout=" + this.lockWaitTimeout.ToString();
                    }

                    if (this.dataOnly)
                    {
                        arguments += " -a";
                    }

                    if (this.blobs)
                    {
                        arguments += " -b";
                    }

                    if (this.clean)
                    {
                        arguments += " -c";
                    }

                    if (this.create)
                    {
                        arguments += " -C";
                    }

                    arguments += " -f \"" + backupFile.FullName + "\"";
                    arguments += " " + this.databaseName;
                    return arguments;
                }
                catch (SecurityException)
                {
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// This function launch the dump process
        /// </summary>
        /// <param name="processArguments">the arguments to give to the process</param>
        /// <returns>Returns a Result that tells if operation succeeds</returns>
        private Result Execute(string processArguments)
        {
            Process process = null;

            using (process = new Process())
            {
                process.StartInfo.FileName = this.temporaryPath.FullName + "\\pg_dump.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.Arguments = processArguments;
                process.StartInfo.RedirectStandardError = true;

                try
                {
                    Environment.SetEnvironmentVariable("PGPASSWORD", this.password, EnvironmentVariableTarget.Process);
                    process.Start();
                    System.Threading.Thread.Sleep(1000);
                    Environment.SetEnvironmentVariable("PGPASSWORD", string.Empty, EnvironmentVariableTarget.Process);
                    process.WaitForExit();
                }
                catch (System.ArgumentNullException)
                {
                    return Result.Error;    // SetEnvironmentVariable
                }
                catch (System.ArgumentException)
                {
                    return Result.Error;    // SetEnvironmentVariable
                }
                catch (System.NotSupportedException)
                {
                    return Result.Error;    // SetEnvironmentVariable
                }
                catch (System.Security.SecurityException)
                {
                    return Result.Error;    // SetEnvironmentVariable
                }
                catch (System.ObjectDisposedException)
                {
                    return Result.Error;    // Start
                }
                catch (System.InvalidOperationException)
                {
                    return Result.Error;    // Start
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    return Result.Error;    // Start and WaitForExit
                }
                catch
                {
                    throw;
                }

                if (process.ExitCode != 0)
                {
                    return Result.Error;
                }
            }

            return Result.NoError;
        }

        /// <summary>
        /// This function is called after the dump in order to clean the temporary
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
