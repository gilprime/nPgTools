// <copyright file="Check.cs" company="nPgTools">
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
    using System.Net;

    /// <summary>
    /// This class contains several functions that helps to check different parameters
    /// before using them
    /// </summary>
    public static class Check
    {
        /// <summary>
        /// This function checks that an IP address is valid
        /// </summary>
        /// <param name="ip">The IP address to test</param>
        /// <returns>true if it's a real address, false otherwise</returns>
        public static bool IsIPAddressValid(IPAddress ip)
        {
            if ((ip != IPAddress.None) &&
                (ip != IPAddress.Any) &&
                (ip != IPAddress.Broadcast) &&
                (ip != IPAddress.IPv6None) &&
                (ip != IPAddress.IPv6Any))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// This function checks that a Port (TCP) is valid
        /// </summary>
        /// <param name="port">The port to test</param>
        /// <returns>true if it's valid, false otherwise</returns>
        /// <remarks>Well-known ports are unallowed (0 to 1023)</remarks>
        public static bool IsPortValid(short port)
        {
            if ((port > 1023) && (port <= short.MaxValue))
            {
                return true;
            }

            return false;
        }
    }
}