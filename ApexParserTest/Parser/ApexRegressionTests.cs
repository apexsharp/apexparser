using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Parser;
using NUnit.Framework;
using Sprache;

namespace ApexParserTest.Parser
{
    [TestFixture]
    public class ApexRegressionTests
    {
        private ApexGrammar Apex { get; } = new ApexGrammar();

        [Test]
        public void AccountDaoExtendsBaseDaoIsParsed()
        {
            var cd = Apex.ClassDeclaration.Parse("public with sharing class AccountDAO extends BaseDAO { }");
            Assert.AreEqual("AccountDAO", cd.Identifier);
            Assert.AreEqual("BaseDAO", cd.BaseType.Identifier);
        }

        [Test]
        public void AccountDaoTestIsParsed()
        {
            var cd = Apex.ClassDeclaration.Parse("@IsTest(seeAllData = false) private class AccountDAOTest { }");
            Assert.AreEqual("AccountDAOTest", cd.Identifier);
            Assert.AreEqual(1, cd.Annotations.Count);
            Assert.AreEqual("IsTest", cd.Annotations[0].Identifier);
            Assert.AreEqual("seeAllData = false", cd.Annotations[0].Parameters);
        }

        [Test]
        public void AccountTeamBatchClassIsParsed()
        {
            var nt = Apex.NonGenericType.Parse("Database");
            var pt = Apex.TypeReference.Parse("Database.batchable<sObject>");

            var cd = Apex.ClassDeclaration.Parse("global class AccountTeamBatchClass implements Database.batchable<sObject> { }");
            Assert.AreEqual("AccountTeamBatchClass", cd.Identifier);
            Assert.AreEqual(1, cd.Interfaces.Count);
            Assert.AreEqual(1, cd.Interfaces[0].Namespaces.Count);
            Assert.AreEqual("Database", cd.Interfaces[0].Namespaces[0]);
            Assert.AreEqual("batchable", cd.Interfaces[0].Identifier);
        }

        [Test]
        public void TestMethodMyUnitTestIsParsed()
        {
            var md = Apex.MethodDeclaration.Parse("static testMethod void myUnitTest() { }");
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.AreEqual("myUnitTest", md.Identifier);
        }

        [Test]
        public void AccountTeamScheduleBatchIsParsed()
        {
            var cd = Apex.ClassDeclaration.Parse("global class AccountTeamschduleBatch implements Schedulable { }");
            Assert.AreEqual("AccountTeamschduleBatch", cd.Identifier);
            Assert.AreEqual(1, cd.Interfaces.Count);
            Assert.False(cd.Interfaces[0].Namespaces.Any());
            Assert.AreEqual("Schedulable", cd.Interfaces[0].Identifier);
        }

        [Test]
        public void StaticVariableToCheckWhetherItsRecurringOrNotIsParsed()
        {
            var cd = Apex.ClassDeclaration.Parse(@"
            class RecurrencyHelper
            {
                //Static Variable to check whether its recurring or not.
                static bool recurring = false;
            }");

            Assert.AreEqual(1, cd.Fields.Count);
        }

        [Test]
        public void PublicStringStrVinIsParsed()
        {
            var cd = Apex.ClassDeclaration.Parse(@"
            class DummyController
            {
                Public String strVIN { get;set; }
            }");

            Assert.AreEqual(1, cd.Properties.Count);
            Assert.AreEqual("strVIN", cd.Properties[0].Identifier);
            Assert.AreEqual("String", cd.Properties[0].Type.Identifier);
        }

        [Test]
        public void PostPaymentResponseExtendsGmosasApi2ResponseObjectsResponseIsParsed()
        {
            var cd = Apex.ClassDeclaration.Parse("global class PostPaymentResponse extends GMOSAS_API2_ResponseObjects.Response {}");
            Assert.AreEqual("PostPaymentResponse", cd.Identifier);
            Assert.AreEqual(1, cd.BaseType.Namespaces.Count);
            Assert.AreEqual("GMOSAS_API2_ResponseObjects", cd.BaseType.Namespaces[0]);
            Assert.AreEqual("Response", cd.BaseType.Identifier);
        }

        [Test]
        public void CreateAgreementAndAttachmentIsParsed()
        {
            var cd = Apex.ClassDeclaration.Parse(@"
            class Dummy
            {
                void Dummy()
                {
                    s=true;
                    //Create Agreement & attachment
                }
            }");

            Assert.AreEqual(1, cd.Methods.Count);
            Assert.AreEqual(1, cd.Methods[0].Body.Statements.Count);
        }

        [Test]
        public void TestMethodForAgreementControllerXIsParsed()
        {
            var cd = Apex.ClassDeclaration.Parse(@"
            class AgreementControllerX_Test
            {
                static testMethod void testMethodForAgreementControllerX()
                {
                }
            }");

            Assert.AreEqual(1, cd.Methods.Count);
            Assert.AreEqual("testMethodForAgreementControllerX", cd.Methods[0].Identifier);
        }

        [Test]
        public void PrivateIntegerBatchSizeIsParsed()
        {
            var cd = Apex.ClassDeclaration.Parse(@"
            class Dummy
            {
                private bool flag = false;
                private integer batchSize = 0;
            }");

            Assert.AreEqual(2, cd.Fields.Count);
        }

        [Test]
        public void AppShopPublishingQueueDaoIsParsed()
        {
            var cd = Apex.ClassDeclaration.Parse(@"
            public with sharing class AppShopPublishingQueueDAO
            {
                public List<AppShop_Publishing_Queue__c> getRecordsByVersionIdsStatusType(Set<Id> mmyvIds, String jobStatus, String jobType)
                {
                }
            }");

            Assert.AreEqual("AppShopPublishingQueueDAO", cd.Identifier);
            Assert.AreEqual(1, cd.Methods.Count);
            Assert.AreEqual("getRecordsByVersionIdsStatusType", cd.Methods[0].Identifier);
            Assert.AreEqual(3, cd.Methods[0].Parameters.Count);
            Assert.AreEqual("mmyvIds", cd.Methods[0].Parameters[0].Identifier);
            Assert.AreEqual("jobStatus", cd.Methods[0].Parameters[1].Identifier);
            Assert.AreEqual("jobType", cd.Methods[0].Parameters[2].Identifier);
        }

        [Test]
        public void BillingAdjustmentControllerIsParsed()
        {
            var cd = Apex.ClassDeclaration.Parse(@"
            public without sharing class BillingAdjustmentController
            {
                public Zuora__CustomerAccount__c billingAccount { get; set; }
                private String billAcctId { get; set; }
                public Case c { get; set; }
                private String contactId { get; set; }
            }");
        }
    }
}
