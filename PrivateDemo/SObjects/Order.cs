namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Order : SObject
	{
		public string OwnerId {set;get;}

		public User Owner {set;get;}

		public string ContractId {set;get;}

		public Contract Contract {set;get;}

		public string AccountId {set;get;}

		public Account Account {set;get;}

		public string Pricebook2Id {set;get;}

		public Pricebook2 Pricebook2 {set;get;}

		public string OriginalOrderId {set;get;}

		public Order OriginalOrder {set;get;}

		public string OpportunityId {set;get;}

		public Opportunity Opportunity {set;get;}

		public DateTime EffectiveDate {set;get;}

		public DateTime EndDate {set;get;}

		public bool IsReductionOrder {set;get;}

		public string Status {set;get;}

		public string Description {set;get;}

		public string CustomerAuthorizedById {set;get;}

		public Contact CustomerAuthorizedBy {set;get;}

		public DateTime CustomerAuthorizedDate {set;get;}

		public string CompanyAuthorizedById {set;get;}

		public User CompanyAuthorizedBy {set;get;}

		public DateTime CompanyAuthorizedDate {set;get;}

		public string Type {set;get;}

		public string BillingStreet {set;get;}

		public string BillingCity {set;get;}

		public string BillingState {set;get;}

		public string BillingPostalCode {set;get;}

		public string BillingCountry {set;get;}

		public double BillingLatitude {set;get;}

		public double BillingLongitude {set;get;}

		public string BillingGeocodeAccuracy {set;get;}

		public Address BillingAddress {set;get;}

		public string ShippingStreet {set;get;}

		public string ShippingCity {set;get;}

		public string ShippingState {set;get;}

		public string ShippingPostalCode {set;get;}

		public string ShippingCountry {set;get;}

		public double ShippingLatitude {set;get;}

		public double ShippingLongitude {set;get;}

		public string ShippingGeocodeAccuracy {set;get;}

		public Address ShippingAddress {set;get;}

		public string Name {set;get;}

		public DateTime PoDate {set;get;}

		public string PoNumber {set;get;}

		public string OrderReferenceNumber {set;get;}

		public string BillToContactId {set;get;}

		public Contact BillToContact {set;get;}

		public string ShipToContactId {set;get;}

		public Contact ShipToContact {set;get;}

		public DateTime ActivatedDate {set;get;}

		public string ActivatedById {set;get;}

		public User ActivatedBy {set;get;}

		public string StatusCode {set;get;}

		public string OrderNumber {set;get;}

		public double TotalAmount {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public bool IsDeleted {set;get;}

		public DateTime SystemModstamp {set;get;}

		public DateTime LastViewedDate {set;get;}

		public DateTime LastReferencedDate {set;get;}
	}
}
