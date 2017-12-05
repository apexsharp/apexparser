namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class FieldDefinition : SObject
	{
		public string DurableId {set;get;}

		public string QualifiedApiName {set;get;}

		public string EntityDefinitionId {set;get;}

		public string NamespacePrefix {set;get;}

		public string DeveloperName {set;get;}

		public string MasterLabel {set;get;}

		public string Label {set;get;}

		public int Length {set;get;}

		public string DataType {set;get;}

		public string ServiceDataTypeId {set;get;}

		public string ValueTypeId {set;get;}

		public string ExtraTypeInfo {set;get;}

		public bool IsCalculated {set;get;}

		public bool IsHighScaleNumber {set;get;}

		public bool IsHtmlFormatted {set;get;}

		public bool IsNameField {set;get;}

		public bool IsNillable {set;get;}

		public bool IsWorkflowFilterable {set;get;}

		public bool IsCompactLayoutable {set;get;}

		public int Precision {set;get;}

		public int Scale {set;get;}

		public bool IsFieldHistoryTracked {set;get;}

		public bool IsIndexed {set;get;}

		public bool IsApiFilterable {set;get;}

		public bool IsApiSortable {set;get;}

		public bool IsListFilterable {set;get;}

		public bool IsListSortable {set;get;}

		public bool IsApiGroupable {set;get;}

		public bool IsListVisible {set;get;}

		public string ControllingFieldDefinitionId {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public string PublisherId {set;get;}

		public string RunningUserFieldAccessId {set;get;}

		public string RelationshipName {set;get;}

		public string ReferenceTo {set;get;}

		public string ReferenceTargetField {set;get;}

		public bool IsCompound {set;get;}

		public bool IsSearchPrefilterable {set;get;}
	}
}
