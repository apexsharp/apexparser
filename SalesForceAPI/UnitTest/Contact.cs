using System;
using SalesForceAPI.ApexApi;

namespace SalesForceAPI.UnitTest
{
    public class Contact : SObject
    {
        public static string SOQL = "SELECT Id, Email, Phone FROM Contact'";

        public bool IsDeleted { set; get; }
        public string MasterRecordId { set; get; }
        public Contact MasterRecord { set; get; }
        public string AccountId { set; get; }

        public string LastName { set; get; }
        public string FirstName { set; get; }
        public string Salutation { set; get; }
        public string Name { set; get; }
        public string OtherStreet { set; get; }
        public string OtherCity { set; get; }
        public string OtherState { set; get; }
        public string OtherPostalCode { set; get; }
        public string OtherCountry { set; get; }
        public double OtherLatitude { set; get; }
        public double OtherLongitude { set; get; }
        public string OtherGeocodeAccuracy { set; get; }

        public string MailingStreet { set; get; }
        public string MailingCity { set; get; }
        public string MailingState { set; get; }
        public string MailingPostalCode { set; get; }
        public string MailingCountry { set; get; }
        public double MailingLatitude { set; get; }
        public double MailingLongitude { set; get; }
        public string MailingGeocodeAccuracy { set; get; }

        public string Phone { set; get; }
        public string Fax { set; get; }
        public string MobilePhone { set; get; }
        public string HomePhone { set; get; }
        public string OtherPhone { set; get; }
        public string AssistantPhone { set; get; }
        public string ReportsToId { set; get; }
        public Contact ReportsTo { set; get; }
        public string Email { set; get; }
        public string Title { set; get; }
        public string Department { set; get; }
        public string AssistantName { set; get; }
        public string LeadSource { set; get; }
        public DateTime? Birthdate = null;
        public string Description { set; get; }
        public string OwnerId { set; get; }

        public DateTime? CreatedDate = null;
        public string CreatedById { set; get; }

        public DateTime? LastModifiedDate { set; get; }
        public string LastModifiedById { set; get; }

        public DateTime? SystemModstamp = null;
        public DateTime? LastActivityDate = null;
        public DateTime? LastCURequestDate = null;
        public DateTime? LastCUUpdateDate = null;
        public DateTime? LastViewedDate = null;
        public DateTime? LastReferencedDate = null;
        public string EmailBouncedReason { set; get; }
        public DateTime? EmailBouncedDate = null;
        public bool? IsEmailBounced { set; get; }
        public string PhotoUrl { set; get; }
        public string Jigsaw { set; get; }
        public string JigsawContactId { set; get; }
        public string CleanStatus { set; get; }
        public string Level__c { set; get; }
        public string Languages__c { set; get; }
    }
}