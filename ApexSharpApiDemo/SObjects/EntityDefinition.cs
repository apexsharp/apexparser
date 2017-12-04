namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class EntityDefinition : SObject
	{
		public string DurableId {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public string QualifiedApiName {set;get;}
		public string NamespacePrefix {set;get;}
		public string DeveloperName {set;get;}
		public string MasterLabel {set;get;}
		public string Label {set;get;}
		public string PluralLabel {set;get;}
		public string DefaultCompactLayoutId {set;get;}
		public bool IsCustomizable {set;get;}
		public bool IsApexTriggerable {set;get;}
		public bool IsWorkflowEnabled {set;get;}
		public bool IsProcessEnabled {set;get;}
		public bool IsCompactLayoutable {set;get;}
		public string KeyPrefix {set;get;}
		public bool IsCustomSetting {set;get;}
		public bool IsDeprecatedAndHidden {set;get;}
		public bool IsReplicateable {set;get;}
		public bool IsRetrieveable {set;get;}
		public bool IsSearchLayoutable {set;get;}
		public bool IsSearchable {set;get;}
		public bool IsTriggerable {set;get;}
		public bool IsIdEnabled {set;get;}
		public bool IsEverCreatable {set;get;}
		public bool IsEverUpdatable {set;get;}
		public bool IsEverDeletable {set;get;}
		public bool IsFeedEnabled {set;get;}
		public bool IsQueryable {set;get;}
		public bool IsMruEnabled {set;get;}
		public string DetailUrl {set;get;}
		public string EditUrl {set;get;}
		public string NewUrl {set;get;}
		public string EditDefinitionUrl {set;get;}
		public string HelpSettingPageName {set;get;}
		public string HelpSettingPageUrl {set;get;}
		public string RunningUserEntityAccessId {set;get;}
		public string PublisherId {set;get;}
		public bool IsLayoutable {set;get;}
		public string RecordTypesSupported {set;get;}
		public string InternalSharingModel {set;get;}
		public string ExternalSharingModel {set;get;}
		public bool HasSubtypes {set;get;}
		public bool IsSubtype {set;get;}
	}
}
