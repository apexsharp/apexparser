namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class DatacloudCompany : SObject
	{
		public string ExternalId {set;get;}

		public string CompanyId {set;get;}

		public string Name {set;get;}

		public string Description {set;get;}

		public bool IsInactive {set;get;}

		public string Phone {set;get;}

		public string Fax {set;get;}

		public string Street {set;get;}

		public string City {set;get;}

		public string State {set;get;}

		public string StateCode {set;get;}

		public string Country {set;get;}

		public string CountryCode {set;get;}

		public string Zip {set;get;}

		public string Site {set;get;}

		public string Industry {set;get;}

		public int NumberOfEmployees {set;get;}

		public double AnnualRevenue {set;get;}

		public string DunsNumber {set;get;}

		public string NaicsCode {set;get;}

		public string NaicsDesc {set;get;}

		public string Sic {set;get;}

		public string SicDesc {set;get;}

		public string Ownership {set;get;}

		public bool IsOwned {set;get;}

		public string TickerSymbol {set;get;}

		public string TradeStyle {set;get;}

		public string Website {set;get;}

		public string YearStarted {set;get;}

		public int ActiveContacts {set;get;}

		public DateTime UpdatedDate {set;get;}

		public int FortuneRank {set;get;}

		public string IncludedInSnP500 {set;get;}

		public int PremisesMeasure {set;get;}

		public string PremisesMeasureReliability {set;get;}

		public string PremisesMeasureUnit {set;get;}

		public double EmployeeQuantityGrowthRate {set;get;}

		public double SalesTurnoverGrowthRate {set;get;}

		public int PriorYearEmployees {set;get;}

		public double PriorYearRevenue {set;get;}

		public bool IsInCrm {set;get;}

		public string FullAddress {set;get;}

		public string SicCodeDesc {set;get;}
	}
}
