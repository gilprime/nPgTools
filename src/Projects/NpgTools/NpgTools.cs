// <copyright file="NpgTools.cs" company="nPgTools">
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
    /// <summary>
    /// Lists the differents result codes returned by a dump
    /// </summary>
    public enum Result
    {
        /// <summary>
        /// Dump finished with no error
        /// </summary>
        NoError,

        /// <summary>
        /// The given NpgsqlConnection is invalid
        /// </summary>
        InvalidNpgsqlConnection,

        /// <summary>
        /// The given Ip address is invalid
        /// </summary>
        InvalidIpAddress,

        /// <summary>
        /// The given port number is invalid
        /// </summary>
        InvalidPort,

        /// <summary>
        /// The given database name is invalid
        /// </summary>
        InvalidDatabaseName,

        /// <summary>
        /// The given login is invalid
        /// </summary>
        InvalidLogin,

        /// <summary>
        /// Unable to create the resulting backup file
        /// </summary>
        CouldNotCreateBackupFile,

        /// <summary>
        /// Unable to delete the temporary files
        /// </summary>
        CouldNotDeleteTempFiles,

        /// <summary>
        /// Unable to create the temporary files
        /// </summary>
        CouldNotCreateTempFiles,

        /// <summary>
        /// Unable to create the dump process arguments files
        /// </summary>
        CouldNotBuildDumpArguments,

        /// <summary>
        /// Unable to read the backup file to restore
        /// </summary>
        CouldNotReadBackupFile,

        /// <summary>
        /// Unhandeled error
        /// </summary>
        Error
    }
}
