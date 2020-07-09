using NUnit.Framework;
using Common;
using System;

namespace Common.Tests
{
    [TestFixture()]
    public class PacketExtractorTests
    {
        public class TestPacket
        {
            public string Email;
            public bool Active;
            public DateTime CreatedDate;
            public string[] Roles;
        }

        [Test()]
        public void DoExtractPacketTest()
        {
            string json = @"{
                              'Email': 'james@example.com',
                              'Active': true,
                              'CreatedDate': '2013-01-20T00:00:00Z',
                              'Roles': [
                                'User',
                                'Admin'
                              ]
                            }";

            Assert.IsTrue(PacketExtractor.DoTryConvert_JsonToPacket(json, out TestPacket sPacket));
        }
    }
}