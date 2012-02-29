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
    using System.Collections.Generic;
    using System.Data;
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
        private short compress = short.MinValue;

        /// <summary>
        /// Dump only the data, not the schema (data definitions). 
        /// </summary>
        private bool dataOnly;

        /// <summary>
        /// Include large objects in the dump.
        /// </summary>
        private bool blobs = true;

        /// <summary>
        /// Output commands to clean (drop) database objects prior to (the commands for)
        /// creating them. 
        /// </summary>
        private bool clean;
        
        /// <summary>
        /// Begin the output with a command to create the database itself and reconnect to the
        /// created database. 
        /// </summary>
        private bool create;

        /// <summary>
        /// Dump data as INSERT commands (rather than COPY). 
        /// </summary>
        private bool insert;

        /// <summary>
        /// Dump data as INSERT commands with explicit column names
        /// </summary>
        private bool explicitInsert;

        /// <summary>
        /// Create the dump in the specified character set encoding.
        /// </summary>
        private string encoding;

        /// <summary>
        /// Ignore version mismatch between pg_dump and the database server.
        /// </summary>
        private bool ignoreVersion;

        /// <summary>
        /// Dump only the shemas given.
        /// </summary>
        private List<string> schemas;

        /// <summary>
        /// Do not dump any tables matching the shema pattern.
        /// </summary>
        private List<string> excludedSchemas;

        /// <summary>
        /// Dump only tables (or views or sequences) matching table.
        /// </summary>
        private List<string> tables;

        /// <summary>
        /// Do not dump any tables matching the table pattern.
        /// </summary>
        private List<string> excludedTables;

        /// <summary>
        /// Dump object identifiers (OIDs) as part of the data for every table.
        /// </summary>
        private bool oids;

        /// <summary>
        /// Do not output commands to set ownership of objects to match the original database. 
        /// </summary>
        private bool noOwner;

        /// <summary>
        /// Dump only the object definitions (schema), not data.
        /// </summary>
        private bool schemaOnly;

        /// <summary>
        /// Specify the superuser user name to use when disabling triggers. 
        /// </summary>
        private string superUser;

        /// <summary>
        /// Prevent dumping of access privileges (grant/revoke commands).
        /// </summary>
        private bool noPrivileges;

        /// <summary>
        /// This option disables the use of dollar quoting for function bodies, and forces them to
        /// be quoted using SQL standard string syntax.
        /// </summary>
        private bool disableDollarQuoting;

        /// <summary>
        /// Instructs pg_dump to include commands to temporarily disable triggers on the target
        /// tables while the data is reloaded.
        /// </summary>
        private bool disableTriggers;

        /// <summary>
        /// Output SQL-standard SET SESSION AUTHORIZATION commands instead of ALTER OWNER commands
        /// </summary>
        private bool useSetSessionAuthorization;
        
        /// <summary>
        /// Initializes a new instance of the NpgDump class, with the NpgsqlConnection parameters
        /// </summary>
        /// <param name="connection">The NpgsqlConnection containing all settings</param>
        public NpgDump(IDbConnection connection)
        {
            this.GetDatasFromNpgsqlConnection(connection);
        }

        /// <summary>
        /// Initializes a new instance of the NpgDump class, on the localhost, on the 5432 port
        /// </summary>
        /// <param name="theDatabaseName">The database name to save</param>
        /// <param name="userLogin">The login used to save database</param>
        /// <param name="userPassword">The password associed to login</param>
        public NpgDump(string theDatabaseName, string userLogin, string userPassword)
        {
            this.login = userLogin;
            this.password = userPassword;
            this.databaseName = theDatabaseName;
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

        #region pg_dump options
        /// <summary>
        /// Gets or sets a value indicating whether even the dump will only dump the datas, not the
        /// schema (data definitions).
        /// </summary>
        /// <remarks>Correspond to the -a or --data-only parameter</remarks>
        public bool DataOnly
        {
            get { return this.dataOnly; }
            set { this.dataOnly = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dump will include the large objects
        /// </summary>
        /// <remarks>Correspond to the -b or --blobs parameter</remarks>
        public bool Blobs
        {
            get { return this.blobs; }
            set { this.blobs = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dump will include database cleaning before
        /// restore
        /// </summary>
        /// <remarks>Correspond to the -c or --clean parameter</remarks>
        public bool Clean
        {
            get { return this.clean; }
            set { this.clean = value; }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the dump will include database creation before
        /// restore
        /// </summary>
        /// <remarks>Correspond to the -C or --create parameter</remarks>
        public bool Create
        {
            get { return this.create; }
            set { this.create = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dump will dump data as INSERT commands
        /// (rather than COPY). 
        /// </summary>
        /// <remarks>Correspond to the -d or --inserts parameter</remarks>
        public bool Insert
        {
            get { return this.insert; }
            set { this.insert = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dump will dump data as INSERT commands with
        /// explicit column names (INSERT INTO table (column, ...) VALUES ...).
        /// </summary>
        /// <remarks>Correspond to the -D or --column-inserts or --attribute-inserts parameter</remarks>
        public bool ExplicitInsert
        {
            get { return this.explicitInsert; }
            set { this.explicitInsert = value; }
        }

        /// <summary>
        /// Gets or sets  a value indicating whether the dump will create the dump in the specified
        /// character set encoding. Only if encoding is valid (Use Check.IsValidEncoding to verify).
        /// </summary>
        /// <remarks>Correspond to the -E or --encoding parameter</remarks>
        public string Encoding
        {
            get
            {
                return this.encoding;
            }

            set
            {
                if (Check.IsValidEncoding(value))
                {
                    this.encoding = value;
                }
            }
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
        /// Gets or sets a value indicating whether the dump will ignore version mismatch between
        /// pg_dump and the database server.
        /// </summary>
        /// <remarks>Correspond to the -i or --ignore-version parameter</remarks>
        public bool IgnoreVersion
        {
            get { return this.ignoreVersion; }
            set { this.ignoreVersion = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dump will dump only schemas matching the
        /// given strings
        /// </summary>
        /// <remarks>Correspond to the -n or --schema parameter</remarks>
        public List<string> Schemas
        {
            get { return this.schemas; }
            set { this.schemas = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dump will not dump any schemas matching the
        /// schema pattern given in the list of strings
        /// </summary>
        /// <remarks>Correspond to the -N or --exclude-schema parameter</remarks>
        public List<string> ExcludedSchemas
        {
            get { return this.excludedSchemas; }
            set { this.excludedSchemas = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dump will dump object identifiers (OIDs) as
        /// part of the data for every table.
        /// </summary>
        /// <remarks>Correspond to the -o or --oids parameter</remarks>
        public bool Oids
        {
            get { return this.oids; }
            set { this.oids = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dump will not output commands to set
        /// ownership of objects to match the original database. 
        /// </summary>
        /// <remarks>Correspond to the -O or --no-owner parameter</remarks>
        public bool NoOwner
        {
            get { return this.noOwner; }
            set { this.noOwner = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dump will dump only the object definitions
        /// (schema), not data.
        /// </summary>
        /// <remarks>Correspond to the -s or --schema-only parameter</remarks>
        public bool SchemaOnly
        {
            get { return this.schemaOnly; }
            set { this.schemaOnly = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dump will specify the superuser user name
        /// to use when disabling triggers (This is only relevant if --disable-triggers is used).
        /// </summary>
        /// <remarks>Correspond to the -S or --superuser parameter</remarks>
        public string SuperUser
        {
            get { return this.superUser; }
            set { this.superUser = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dump will dump only tables (or views or
        /// sequences) matching table.
        /// </summary>
        /// <remarks>Correspond to the -t or --table parameter</remarks>
        public List<string> Tables
        {
            get { return this.tables; }
            set { this.tables = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dump will not dump any tables matching the
        /// table pattern.
        /// </summary>
        /// <remarks>Correspond to the -T or --exclude-table parameter</remarks>
        public List<string> ExcludedTables
        {
            get { return this.excludedTables; }
            set { this.excludedTables = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dump will prevent dumping of access
        /// privileges (grant/revoke commands).
        /// </summary>
        /// <remarks>Correspond to the -x, --no-privileges or --no-acl parameter</remarks>
        public bool NoPrivileges
        {
            get { return this.noPrivileges; }
            set { this.noPrivileges = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dump will disable the use of dollar quoting
        /// for function bodies, and forces them to be quoted using SQL standard string syntax.
        /// </summary>
        /// <remarks>Correspond to the --disable-dollar-quoting parameter</remarks>
        public bool DisableDollarQuoting
        {
            get { return this.disableDollarQuoting; }
            set { this.disableDollarQuoting = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dump will include commands to temporarily
        /// disable triggers on the target tables while the data is reloaded. This option is only
        /// relevant when creating a data-only dump.
        /// </summary>
        /// <remarks>Correspond to the --disable-triggers parameter</remarks>
        public bool DisableTriggers
        {
            get { return this.disableTriggers; }
            set { this.disableTriggers = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dump will Output SQL-standard
        /// SET SESSION AUTHORIZATION commands instead of ALTER OWNER commands
        /// </summary>
        /// <remarks>Correspond to the --use-set-session-authorization parameter</remarks>
        public bool UseSetSessionAuthorization
        {
            get { return this.useSetSessionAuthorization; }
            set { this.useSetSessionAuthorization = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the compression level used for the custom and
        /// plain format (from 0 to 9)
        /// </summary>
        /// <remarks>Correspond to the -Z or --compress parameter</remarks>
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

        #endregion

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
        private void GetDatasFromNpgsqlConnection(IDbConnection connection)
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
        /// <returns>
        /// the argument to pass to the pg_dump process or null if it can't build
        /// the process arguments
        /// </returns>
        private string GetDumpArguments(FileSystemInfo backupFile)
        {
            if (backupFile != null)
            {
                try
                {
                    string arguments = " -p " + this.port.ToString();
                    arguments += " -h " + this.host.ToString();
                    arguments += " -U " + this.login;
                    arguments += " -F" + ((char)this.format).ToString();

                    if (this.dataOnly)
                    {
                        arguments += " -a";
                    }

                    if (this.blobs &&
                            (this.schemaOnly ||
                            (this.schemas != null && this.schemas.Count > 0) ||
                            (this.tables != null && this.tables.Count > 0)))
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

                    if (this.insert)
                    {
                        arguments += " -d";
                    }

                    if (this.explicitInsert)
                    {
                        arguments += " -D";
                    }

                    if (!string.IsNullOrEmpty(this.encoding))
                    {
                        arguments += " -E " + this.encoding;
                    }

                    if (this.ignoreVersion)
                    {
                        arguments += " -i";
                    }

                    if (this.schemas != null && this.schemas.Count > 0)
                    {
                        foreach (string schema in this.schemas)
                        {
                            arguments += " -n " + schema;
                        }
                    }

                    if (this.excludedSchemas != null && this.excludedSchemas.Count > 0)
                    {
                        foreach (string excludedSchema in this.excludedSchemas)
                        {
                            arguments += " -N " + excludedSchema;
                        }
                    }

                    if (this.oids)
                    {
                        arguments += " -o";
                    }

                    if (this.noOwner)
                    {
                        arguments += " -O";
                    }

                    if (this.schemaOnly)
                    {
                        arguments += " -s";
                    }

                    if (!string.IsNullOrEmpty(this.superUser))
                    {
                        arguments += " -S " + this.superUser;
                    }

                    if (this.tables != null && this.tables.Count > 0)
                    {
                        foreach (string table in this.tables)
                        {
                            arguments += " -t " + table;
                        }
                    }

                    if (this.excludedTables != null && this.excludedTables.Count > 0)
                    {
                        foreach (string excludedTable in this.excludedTables)
                        {
                            arguments += " -T " + excludedTable;
                        }
                    }

                    if (this.noPrivileges)
                    {
                        arguments += " -x";
                    }

                    if (this.disableDollarQuoting)
                    {
                        arguments += " --disable-dollar-quoting";
                    }

                    if (this.disableTriggers)
                    {
                        arguments += " --disable-triggers";
                    }

                    if (this.useSetSessionAuthorization)
                    {
                        arguments += "--use-set-session-authorization";
                    }

                    if (this.format == NpgOptFormat.Custom && this.compress != short.MinValue)
                    {
                        arguments += " -Z" + this.compress.ToString();
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
