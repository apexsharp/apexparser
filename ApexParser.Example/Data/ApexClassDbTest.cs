using NUnit.Framework;

namespace ApexSharpDemo.Data
{
    [TestFixture]
    public class ApexClassDbTest
    {
        private readonly ApexClassDb db;

        public ApexClassDbTest()
        {
            db = new ApexClassDb();

        }



    [Test]
        public void NameSpaceTest()
        {
            Assert.AreEqual("System", db.GetCorrectNameSpace("system"));
            Assert.IsNull(db.GetCorrectNameSpace("systemWrong"));
        }

        [Test]
        public void ClassNameNoNameSpaceTest()
        {
            Assert.AreEqual("System", db.GetCorrectClassName("", "system"));
        }


        [Test]
        public void ClassNameTest()
        {
            Assert.AreEqual("System", db.GetCorrectClassName("system","system"));
            Assert.IsNull(db.GetCorrectClassName("system","systemWrong"));
        }

        [Test]
        public void MethodNameTest()
        {
            Assert.AreEqual("debug", db.GetCorrectMethodName("system","system","DEBUG"));
            Assert.IsNull(db.GetCorrectMethodName("system","system","DEBUGWrong"));
        }

        [Test]
        public void ValidateApexMethodTest()
        {
            // ConnectApi.Zones.setTestSearchInZone(String communityId, String zoneId, String q, ConnectApi.ZoneSearchResultType filter, String pageParam, Integer pageSize, ConnectApi.ZoneSearchPage result)
            var methodDto = new ApexMethodDto
            {
                NameSpace = "ConnectApi",
                ClassName = "Zones",
                MethodName = "setTestSearchInZone",
                ParameterList = "String:String:String:ConnectApi.ZoneSearchResultType:String:Integer:ConnectApi.ZoneSearchPage"
            };

            methodDto = db.ValidateApexMethod(methodDto);
            Assert.NotNull(methodDto);
        }
    }
}