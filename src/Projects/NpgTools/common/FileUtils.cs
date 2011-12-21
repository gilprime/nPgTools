// <copyright file="FileUtils.cs" company="nPgTools">
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
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// This class contains tools in order to simplify management of files in the
    /// different NpgTools
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        /// This function extract files inside the assembly to the temp path given
        /// in parameter
        /// </summary>
        /// <param name="tempPath">the temp path were to put files</param>
        /// <returns>true if extracting succeed, false otherwise</returns>
        /// <remarks>we assume that the files are located into the root
        /// of the project</remarks>
        public static bool ExtractFilesToTempPath(FileSystemInfo tempPath)
        {
            if (tempPath != null)
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                if (asm != null)
                {
                    return ExtractFilesToTempPath(tempPath, asm.ManifestModule.Name);
                }
            }

            return false;
        }

        /// <summary>
        /// This function extract files inside the assembly into the folder given
        /// to the temp path given in parameter
        /// </summary>
        /// <param name="tempPath">the temp path were to put files</param>
        /// <param name="embeddedRessourceFullFolder">the folder of the assembly
        /// where files are located</param>
        /// <returns>true if extracting succeed, false otherwise</returns>
        public static bool ExtractFilesToTempPath(
                FileSystemInfo tempPath,
                string embeddedRessourceFullFolder)
        {
            if ((tempPath != null) && (!string.IsNullOrEmpty(embeddedRessourceFullFolder)))
            {
                Assembly asm = Assembly.GetExecutingAssembly();

                if (!embeddedRessourceFullFolder.EndsWith("."))
                {
                    embeddedRessourceFullFolder = embeddedRessourceFullFolder + ".";
                }

                if (asm != null)
                {
                    string[] embeddedResourcesNames = asm.GetManifestResourceNames();

                    foreach (string embeddedResourceName in embeddedResourcesNames)
                    {
                        byte[] bytes = null;

                        using (Stream stream = asm.GetManifestResourceStream(embeddedResourceName))
                        {
                            // read file to byte array
                            bytes = new byte[stream.Length];
                            stream.Read(bytes, 0, (int)stream.Length);
                            stream.Close();
                        }

                        // copy file
                        string realFile = embeddedResourceName.Replace(
                                                                embeddedRessourceFullFolder,
                                                                string.Empty);

                        using (FileStream sw = File.Create(tempPath.FullName + "\\" + realFile))
                        {
                            sw.Write(bytes, 0, (int)bytes.Length);
                            sw.Close();
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// This function removes all the files located in the tempPath directory
        /// passed in parameter
        /// </summary>
        /// <param name="tempPath">the temp path were to remove files</param>
        /// <returns>true if remove succeed, false otherwise</returns>
        public static bool RemoveFilesFromTempPath(FileSystemInfo tempPath)
        {
            try
            {
                if ((tempPath != null) && (!string.IsNullOrEmpty(tempPath.FullName)))
                {
                    Directory.Delete(tempPath.FullName, true);
                    return true;
                }

                return false;
            }
            catch (System.IO.IOException)
            {
                return false;
            }
            catch (System.UnauthorizedAccessException)
            {
                return false;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}