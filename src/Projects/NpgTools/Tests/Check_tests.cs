// <copyright file="Check_tests.cs" company="nPgTools">
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

namespace NpgTools.Tests
{
    using System.Collections.Generic;
    using NUnit.Framework;

    /// <summary>
    /// Tests the common checks methods
    /// </summary>
    internal class Check_tests
    {
        /// <summary>
        /// Tests the installations getted from registry.
        /// </summary>
        [Test]
        public void TestsCheckIpAddress()
        {
            Assert.IsFalse(Check.IsIPAddressValid(System.Net.IPAddress.Any));
            Assert.IsFalse(Check.IsIPAddressValid(System.Net.IPAddress.Broadcast));
            Assert.IsFalse(Check.IsIPAddressValid(System.Net.IPAddress.None));
            Assert.IsFalse(Check.IsIPAddressValid(System.Net.IPAddress.IPv6Any));
            Assert.IsFalse(Check.IsIPAddressValid(System.Net.IPAddress.IPv6None));

            Assert.IsTrue(Check.IsIPAddressValid(System.Net.IPAddress.Parse("192.168.0.1")));
            Assert.IsTrue(Check.IsIPAddressValid(System.Net.IPAddress.Loopback));
            Assert.IsTrue(Check.IsIPAddressValid(System.Net.IPAddress.IPv6Loopback));
        }

        /// <summary>
        /// Tests the installations getted from registry.
        /// </summary>
        [Test]
        public void TestsCheckIsPortValid()
        {
            Assert.IsTrue(Check.IsPortValid(1024));
            Assert.IsTrue(Check.IsPortValid(5432));
            Assert.IsTrue(Check.IsPortValid(short.MaxValue));

            Assert.IsFalse(Check.IsPortValid(-1));
            Assert.IsFalse(Check.IsPortValid(short.MinValue));
            Assert.IsFalse(Check.IsPortValid(1000));
            Assert.IsFalse(Check.IsPortValid(1023));
        }

        /// <summary>
        /// Tests the installations getted from registry.
        /// </summary>
        [Test]
        public void TestsCheckIsValidEncoding()
        {
            Assert.IsTrue(Check.IsValidEncoding("UTF8"));
            Assert.IsTrue(Check.IsValidEncoding("WIN1252"));
            Assert.IsTrue(Check.IsValidEncoding("ISO_8859_5"));
            Assert.IsTrue(Check.IsValidEncoding("KOI8", new System.Version(8, 3)));
            Assert.IsTrue(Check.IsValidEncoding("KOI8R", new System.Version(8, 4)));
            Assert.IsTrue(Check.IsValidEncoding("KOI8U", new System.Version(9, 1)));

            Assert.IsFalse(Check.IsValidEncoding(null));
            Assert.IsFalse(Check.IsValidEncoding(string.Empty));
            Assert.IsFalse(Check.IsValidEncoding("Unknown"));
            Assert.IsFalse(Check.IsValidEncoding("KOI8", new System.Version(9, 0)));
            Assert.IsFalse(Check.IsValidEncoding("KOI8R", new System.Version(8, 3)));
            Assert.IsFalse(Check.IsValidEncoding("KOI8U", new System.Version(8, 3)));
        }
    }
}
