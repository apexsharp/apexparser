namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Opportunity : SObject
	{
		public bool IsDeleted {set;get;}

		public string AccountId {set;get;}

		public Account Account {set;get;}

		public bool IsPrivate {set;get;}

		public string Name {set;get;}

		public string Description {set;get;}

		public string StageName {set;get;}

		public double Amount {set;get;}

		public double Probability {set;get;}

		public double ExpectedRevenue {set;get;}

		public double TotalOpportunityQuantity {set;get;}

		public DateTime CloseDate {set;get;}

		public string Type {set;get;}

		public string NextStep {set;get;}

		public string LeadSource {set;get;}

		public bool IsClosed {set;get;}

		public bool IsWon {set;get;}

		public string ForecastCategory {set;get;}

		public string ForecastCategoryName {set;get;}

		public string CampaignId {set;get;}

		public Campaign Campaign {set;get;}

		public bool HasOpportunityLineItem {set;get;}

		public string Pricebook2Id {set;get;}

		public Pricebook2 Pricebook2 {set;get;}

		public string OwnerId {set;get;}

		public User Owner {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public DateTime LastActivityDate {set;get;}

		public int FiscalQuarter {set;get;}

		public int FiscalYear {set;get;}

		public string Fiscal {set;get;}

		public DateTime LastViewedDate {set;get;}

		public DateTime LastReferencedDate {set;get;}

		public string ContractId {set;get;}

		public Contract Contract {set;get;}

		public bool HasOpenActivity {set;get;}

		public bool HasOverdueTask {set;get;}

		public string DeliveryInstallationStatus__c {set;get;}

		public string TrackingNumber__c {set;get;}

		public string OrderNumber__c {set;get;}

		public string CurrentGenerators__c {set;get;}

		public string MainCompetitors__c {set;get;}
	}
}
