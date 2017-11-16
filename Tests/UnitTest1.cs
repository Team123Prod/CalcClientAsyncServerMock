using System;
using NUnit.Framework;
using CalcClientAsync;
using System.Threading.Tasks;
using HttpMock;

namespace Tests
{
    [TestFixture]
    public class UnitTest1
    {
        Client client = null;
        IHttpServer serverMock;
        string uri = "http://localhost:1234";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            serverMock = HttpMockRepository.At(uri);
        }

        [SetUp]
        public void SetUp()
        {
            client = new Client(uri);
        }

        [Test]
        [TestCase(1, 5, '+', "6")]
        [TestCase(10, 2, '-', "8")]
        [TestCase(12, 2, '*', "24")]
        [TestCase(15, 3, '/', "5")]
        public void Test_RequestCalcResult(int x, int y, char op, string res)
        {
            serverMock.Stub(z => z.Get("/*"))
                    .Return(res)
                    .OK();

            Task.Run(() =>
            {
                int result = Task.Run(() => client.RequestCalcResult(x, y, op)).Result;
                return result;
            }).ContinueWith((e) => { Assert.AreEqual(res, e); });           
        }
    }
}
