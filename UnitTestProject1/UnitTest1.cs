using System.Net.Http;
using System.Threading.Tasks;
using CommonOldProgram;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod, Description("����n")]
        public async Task API����̃��X�|���X������ȂƂ�DB�֒l���ۑ�����Ă��邱��()
        {
            var api = new RequestAPIWrapperMockNormalResponse(23);
            var setting = new CorporateSetting(api)
            {
                Item1 = "�ݒ���1",
                Item2 = "�ݒ���2"
            };
            await setting.Save();

            var actual = new CorporateSetting(api);
            await actual.ReadFromDB();

            Assert.AreEqual(setting.CooperatedId, actual.CooperatedId);
            Assert.AreEqual(setting.Item1, actual.Item1);
            Assert.AreEqual(setting.Item2, actual.Item2);
        }

        [TestMethod, Description("�ُ�n1")]
        public async Task API����̃��X�|���X������łȂ������Ƃ���HTTPRequestException���������邱��()
        {
            var api = new RequestAPIWrapperMockAbNormalResponse();
            var setting = new CorporateSetting(api)
            {
                Item1 = "�ݒ���1",
                Item2 = "�ݒ���2"
            };

            try
            {
                await setting.Save();
                Assert.Fail("��O���f����Ă��܂���");
            }
            catch (HttpRequestException e)
            {
                Assert.AreEqual("API�A�g�Ɏ��s���܂���", e.Message);
            }
            catch
            {
                Assert.Fail("�قȂ��O���������܂���");
            }
        }

        [TestMethod, Description("�ُ�n2")]
        public async Task API����̃��X�|���X������łȂ������Ƃ�DB�֒l���ۑ�����Ă��Ȃ�����()
        {
            var api = new RequestAPIWrapperMockAbNormalResponse();
            var setting = new CorporateSetting(api)
            {
                Item1 = "�ݒ���1",
                Item2 = "�ݒ���2"
            };

            try
            {
                await setting.Save();
                Assert.Fail("��O���f����Ă��܂���");
            }
            catch (HttpRequestException e)
            {
                Assert.AreEqual("API�A�g�Ɏ��s���܂���", e.Message);
            }
            catch
            {
                Assert.Fail("�قȂ��O���������܂���");
            }
        }
    }

    public class RequestAPIWrapperMockNormalResponse : IRequestAPIWrapper
    {
        private readonly int cooperatedId;
        public RequestAPIWrapperMockNormalResponse(int testCooperatedId)
        {
            cooperatedId = testCooperatedId;
        }

        public async Task<(bool isSucess, int cooperatedId)> PostCorporateSetting()
        {
            return await Task.Run(() => (true, cooperatedId));
        }
    }

    public class RequestAPIWrapperMockAbNormalResponse : IRequestAPIWrapper
    {
        public async Task<(bool isSucess, int cooperatedId)> PostCorporateSetting()
        {
            return await Task.Run(() => (false, -1));
        }
    }
}
