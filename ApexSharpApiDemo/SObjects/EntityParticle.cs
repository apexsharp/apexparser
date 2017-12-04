namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class EntityParticle : SObject
	{
		public string DurableId {set;get;}
		public string QualifiedApiName {set;get;}
		public string EntityDefinitionId {set;get;}
		public string FieldDefinitionId {set;get;}
		public string NamespacePrefix {set;get;}
		public string DeveloperName {set;get;}
		public string MasterLabel {set;get;}
		public string Label {set;get;}
		public int Length {set;get;}
		public string DataType {set;get;}
		public string ServiceDataTypeId {set;get;}
		public string ValueTypeId {set;get;}
		public string ExtraTypeInfo {set;get;}
		public bool IsAutonumber {set;get;}
		public int ByteLength {set;get;}
		public bool IsCaseSensitive {set;get;}
		public bool IsUnique {set;get;}
		public bool IsCreatable {set;get;}
		public bool IsUpdatable {set;get;}
		public bool IsDefaultedOnCreate {set;get;}
		public bool IsWriteRequiresMasterRead {set;get;}
		public bool IsCalculated {set;get;}
		public bool IsHighScaleNumber {set;get;}
		public bool IsHtmlFormatted {set;get;}
		public bool IsNameField {set;get;}
		public bool IsNillable {set;get;}
		public bool IsPermissionable {set;get;}
		public bool IsEncrypted {set;get;}
		public int Digits {set;get;}
		public string InlineHelpText {set;get;}
		public string RelationshipName {set;get;}
		public string ReferenceTargetField {set;get;}
		public string Name {set;get;}
		public string Mask {set;get;}
		public string MaskType {set;get;}
		public bool IsWorkflowFilterable {set;get;}
		public bool IsCompactLayoutable {set;get;}
		public int Precision {set;get;}
		public int Scale {set;get;}
		public bool IsFieldHistoryTracked {set;get;}
		public bool IsApiFilterable {set;get;}
		public bool IsApiSortable {set;get;}
		public bool IsApiGroupable {set;get;}
		public bool IsListVisible {set;get;}
		public bool IsLayoutable {set;get;}
		public bool IsDependentPicklist {set;get;}
		public bool IsDeprecatedAndHidden {set;get;}
		public bool IsDisplayLocationInDecimal {set;get;}
		public string DefaultValueFormula {set;get;}
		public bool IsIdLookup {set;get;}
		public bool IsNamePointing {set;get;}
		public int RelationshipOrder {set;get;}
		public string ReferenceTo {set;get;}
		public bool IsComponent {set;get;}
		public bool IsCompound {set;get;}
	}
}
