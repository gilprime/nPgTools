// <copyright file="Options.cs" company="nPgTools">
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
    /// Restore only the data, not the schema (data definitions). 
    /// </summary>
    /// <remarks>Correspond to the "-a" or "--data-only" option</remarks>
    public enum NpgOptOnlyData
    {
        /// <summary>
        /// Dump the data and the schema.
        /// </summary>
        DataAndSchema,

        /// <summary>
        /// Dump only the data, not the schema (data definitions).
        /// </summary>
        OnlyData
    }

    /// <summary>
    /// Include large objects in the dump
    /// </summary>
    /// <remarks>Correspond to the "-b" or "--blobs" option</remarks>
    public enum NpgOptBlob
    {
        /// <summary>
        /// Include large objects in the dump.
        /// </summary>
        IncludeBlobs,

        /// <summary>
        /// Exclude large objects from dump.
        /// </summary>
        ExcludeBlobs
    }

    /// <summary>
    /// Clean (drop) database objects before recreating them. 
    /// </summary>
    /// <remarks>Correspond to the "-c" or "--clean" option</remarks>
    public enum NpgOptClean
    {
        /// <summary>
        /// Output commands to clean (drop) database objects prior to (the commands for) creating
        /// them. 
        /// </summary>
        Clean,

        /// <summary>
        /// No Output commands to clean (drop) database objects prior to (the commands for)
        /// creating them. 
        /// </summary>
        DontClean
    }

    /// <summary>
    /// Begin the output with a command to create the database itself
    /// and reconnect to the created database.
    /// </summary>
    /// <remarks>Correspond to the "-C" or "--create" option</remarks>
    public enum NpgOptCreate
    {
        /// <summary>
        /// Begin the output with a command to create the database itself
        /// </summary>
        Create,

        /// <summary>
        /// Do not begin the output with a command to create the database itself
        /// </summary>
        DontCreate
    }

    /// <summary>
    /// Selects the archive format
    /// </summary>
    /// <remarks>Correspond to the "-F" or "--format" option</remarks>
    public enum NpgOptFormat
    {
        /// <summary>
        /// Output a plain-text SQL script file.
        /// </summary>
        Plain = 'p',

        /// <summary>
        /// Output a custom archive suitable for input into pg_restore.
        /// </summary>
        Custom = 'c',

        /// <summary>
        /// Output a tar archive suitable for input into pg_restore.
        /// </summary>
        Tar = 't'
    }

    /// <summary>
    /// Dump object identifiers (OIDs) as part of the data for every table
    /// </summary>
    /// <remarks>Correspond to the "-o" or "--oids" option</remarks>
    public enum NpgOptOid
    {
        /// <summary>
        /// Dump object identifiers (OIDs) as part of the data for every table.
        /// </summary>
        ForceDumpOids,

        /// <summary>
        /// Do not dump object identifiers (OIDs) as part of the data for every table.
        /// </summary>
        DontForceDumpOids
    }

    /// <summary>
    /// Specify the compression level to use
    /// </summary>
    /// <remarks>Correspond to the "-Z" or "--compress" option</remarks>
    public enum NpgOptCompression
    {        
        /// <summary>
        /// No compression used.
        /// </summary>
        NoCompression,

        /// <summary>
        /// First compression level
        /// </summary>
        Compress1,

        /// <summary>
        /// Second compression level
        /// </summary>
        Compress2,

        /// <summary>
        /// Third compression level
        /// </summary>
        Compress3,

        /// <summary>
        /// Fourth compression level
        /// </summary>
        Compress4,

        /// <summary>
        /// Fifth compression level
        /// </summary>
        Compress5,

        /// <summary>
        /// Sixth compression level
        /// </summary>
        Compress6,

        /// <summary>
        /// Seventh compression level
        /// </summary>
        Compress7,

        /// <summary>
        /// Eighth compression level
        /// </summary>
        Compress8,

        /// <summary>
        /// Ninth compression level
        /// </summary>
        Compress9
    }    
}