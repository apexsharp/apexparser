namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Campaign : SObject
	{
		public bool IsDeleted {set;get;}

		public string Name {set;get;}

		public string ParentId {set;get;}

		public Campaign Parent {set;get;}

		public string Type {set;get;}

		public string Status {set;get;}

		public DateTime StartDate {set;get;}

		public DateTime EndDate {set;get;}

		public double ExpectedRevenue {set;get;}

		public double BudgetedCost {set;get;}

		public double ActualCost {set;get;}

		public double ExpectedResponse {set;get;}

		public double NumberSent {set;get;}

		public bool IsActive {set;get;}

		public string Description {set;get;}

		public int NumberOfLeads {set;get;}

		public int NumberOfConvertedLeads {set;get;}

		public int NumberOfContacts {set;get;}

		public int NumberOfResponses {set;get;}

		public int NumberOfOpportunities {set;get;}

		public int NumberOfWonOpportunities {set;get;}

		public double AmountAllOpportunities {set;get;}

		public double AmountWonOpportunities {set;get;}

		public int HierarchyNumberOfLeads {set;get;}

		public int HierarchyNumberOfConvertedLeads {set;get;}

		public int HierarchyNumberOfContacts {set;get;}

		public int HierarchyNumberOfResponses {set;get;}

		public int HierarchyNumberOfOpportunities {set;get;}

		public int HierarchyNumberOfWonOpportunities {set;get;}

		public double HierarchyAmountAllOpportunities {set;get;}

		public double HierarchyAmountWonOpportunities {set;get;}

		public double HierarchyNumberSent {set;get;}

		public double HierarchyExpectedRevenue {set;get;}

		public double HierarchyBudgetedCost {set;get;}

		public double HierarchyActualCost {set;get;}

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

		public DateTime LastViewedDate {set;get;}

		public DateTime LastReferencedDate {set;get;}

		public string CampaignMemberRecordTypeId {set;get;}

		public RecordType CampaignMemberRecordType {set;get;}
	}
}
