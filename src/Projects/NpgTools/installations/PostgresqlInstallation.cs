// <copyright file="PostgresqlInstallation.cs" company="nPgTools">
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

namespace NpgTools.PostgreSQL
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using Microsoft.Win32;

    /// <summary>
    /// Contains the managed versions of PostgreSQL
    /// </summary>
    public enum PostgresqlVersion
    {
        /// <summary>
        /// Unknown PostgreSQL version, should never arrive... :)
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// PostgreSQL version 8.0
        /// </summary>
        Version_8_0,

        /// <summary>
        /// PostgreSQL version 8.1
        /// </summary>
        Version_8_1,

        /// <summary>
        /// PostgreSQL version 8.2
        /// </summary>
        Version_8_2,

        /// <summary>
        /// PostgreSQL version 8.3
        /// </summary>
        Version_8_3,

        /// <summary>
        /// PostgreSQL version 8.4
        /// </summary>
        Version_8_4,

        /// <summary>
        /// PostgreSQL version 9.0
        /// </summary>
        Version_9_0,

        /// <summary>
        /// PostgreSQL version 9.1
        /// </summary>
        Version_9_1
    }

    /// <summary>
    /// This class permits to get PostgreSQL installations on the current computer, and permits to
    /// know some informations on them, like installation paths, full version, if it's already
    /// supported by PostgreSQL group, etc...
    /// </summary>
    public abstract class PostgresqlInstallation
    {
        /// <summary>
        /// This is the base location in the ergistry fir the PostgreSQL registry keys
        /// </summary>
        /// <remarks>x86 Microsoft Windows</remarks>
        private string x86BaseRegistry = @"SOFTWARE\PostgreSQL\Installations\";
        
        /// <summary>
        /// This is the base location in the ergistry fir the PostgreSQL registry keys
        /// </summary>
        /// <remarks>x64 Microsoft Windows</remarks>
        private string x64BaseRegistry = @"SOFTWARE\Wow6432Node\PostgreSQL\Installations\";

        /// <summary>
        /// This is the location of the binaries of PostgreSQL installation
        /// </summary>
        private DirectoryInfo binariesPath;
        
        /// <summary>
        /// This is the location of the binaries of PostgreSQL datas
        /// </summary>
        private DirectoryInfo datasPath;

        /// <summary>
        /// This is the service name of the PostgreSQL installation
        /// </summary>
        private string serviceId;

        /// <summary>
        /// This is the full version of the PostgreSQL installation (i.e. : w.x.y.z format)
        /// </summary>
        private Version version;

        /// <summary>
        /// Gets the binaries path.
        /// </summary>
        public DirectoryInfo BinariesPath
        {
            get { return this.binariesPath; }
        }

        /// <summary>
        /// Gets the datas path.
        /// </summary>
        public DirectoryInfo DatasPath
        {
            get { return this.datasPath; }
        }

        /// <summary>
        /// Gets the windows service identifier
        /// </summary>
        public string ServiceId
        {
            get { return this.serviceId; }
        }

        /// <summary>
        /// Gets the full version installed.
        /// </summary>
        public Version Version
        {
            get { return this.version; }
        }

        /// <summary>
        /// Gets the major version, i.e. the two first numbers.
        /// </summary>
        public abstract PostgresqlVersion MajorVersion
        {
            get;
        }

        /// <summary>
        /// Gets all the current installed versions on the computer (based on the registry values).
        /// </summary>
        /// <returns>A List of installations found, ordered by versions number, from the oldest to
        /// the newest</returns>
        public static PostgresqlInstallation[] GetInstalledVersions()
        {
            List<PostgresqlInstallation> installations = new List<PostgresqlInstallation>();
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsSubclassOf(typeof(PostgresqlInstallation)))
                {
                    PostgresqlInstallation inst = (PostgresqlInstallation)Activator.CreateInstance(type);
                    if (inst.IsInstalled())
                    {
                        installations.Add(inst);
                    }
                }
            }

            // Sort installations by version number
            installations.Sort(delegate(PostgresqlInstallation version1, PostgresqlInstallation version2)
            {
                return version1.MajorVersion - version2.MajorVersion;
            });

            return installations.ToArray();
        }

        /// <summary>
        /// Gets the registry entry concerning the 8.0 version.
        /// </summary>
        /// <returns>A string containing the registry entry that is inserted on Windows®
        /// installation</returns>
        protected abstract string GetRegistryEntry();

        /// <summary>
        /// Determines whether this instance is installed.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is installed; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool IsInstalled()
        {
            string registrylocation = this.x86BaseRegistry + this.GetRegistryEntry();
            if (UIntPtr.Size == 8)      
            {
                // 64 bits systems
                registrylocation = this.x64BaseRegistry + this.GetRegistryEntry();
            }

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registrylocation))
            {
                if (key == null)
                {
                    return false;
                }

                try
                {
                    object binDir = key.GetValue("Base Directory");
                    if (binDir != null)
                    {
                        this.binariesPath = new DirectoryInfo(Path.Combine(binDir.ToString(), "bin"));
                    }

                    object dataDir = key.GetValue("Data Directory");
                    if (dataDir != null)
                    {
                        this.datasPath = new DirectoryInfo(dataDir.ToString());
                    }

                    object serviceIdentifier = key.GetValue("Service ID");
                    if (serviceIdentifier != null)
                    {
                        this.serviceId = serviceIdentifier.ToString();
                    }

                    object registryVersion = key.GetValue("Version");
                    if (registryVersion != null)
                    {
                        if (registryVersion.ToString().Length > 3)
                        {
                            this.version = new Version(registryVersion.ToString());
                        }
                        else
                        {
                            string postgresFile = Path.Combine(this.binariesPath.ToString(), "postgres.exe");
                            if (File.Exists(postgresFile))
                            {
                                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(postgresFile);
                                if (fvi != null)
                                {
                                    this.version = new Version(fvi.FileVersion);
                                }
                            }
                        }
                    }
                }
                catch (System.ArgumentNullException)
                {
                    return false;
                }
                catch (System.ArgumentException)
                {
                    return false;
                }
                catch (System.UnauthorizedAccessException)
                {
                    return false;
                }
                catch (System.ObjectDisposedException)
                {
                    return false;
                }
                catch (System.IO.PathTooLongException)
                {
                    return false;
                }
                catch (System.IO.IOException)
                {
                    return false;
                }
                catch (System.Security.SecurityException)
                {
                    return false;
                }
            }

            return true;            
        }
    }
}
