// <copyright file="PostgresqlInstallation_8_3_WinInst.cs" company="nPgTools">
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
    /// <summary>
    /// This class contains specificities of the 8.3 version of PostgreSQL (installed by Windows®
    /// installer)
    /// </summary>
    internal sealed class PostgresqlInstallation_8_3_WinInst : PostgresqlInstallation
    {
        /// <summary>
        /// Gets the major version, i.e. the two first numbers.
        /// </summary>
        public override PostgresqlVersion MajorVersion
        {
            get { return PostgresqlVersion.Version_8_3; }
        }

        /// <summary>
        /// Gets the registry entry concerning the 8.3 version.
        /// </summary>
        /// <returns>A string containing the registry entry that is inserted on Windows®
        /// installation</returns>
        protected override string GetRegistryEntry()
        {
            return "{B823632F-3B72-4514-8861-B961CE263224}";
        }
    }
}
