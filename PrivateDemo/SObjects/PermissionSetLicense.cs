namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class PermissionSetLicense : SObject
	{
		public bool IsDeleted {set;get;}

		public string DeveloperName {set;get;}

		public string Language {set;get;}

		public string MasterLabel {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string PermissionSetLicenseKey {set;get;}

		public int TotalLicenses {set;get;}

		public string Status {set;get;}

		public DateTime ExpirationDate {set;get;}

		public bool MaximumPermissionsEmailSingle {set;get;}

		public bool MaximumPermissionsEmailMass {set;get;}

		public bool MaximumPermissionsEditTask {set;get;}

		public bool MaximumPermissionsEditEvent {set;get;}

		public bool MaximumPermissionsExportReport {set;get;}

		public bool MaximumPermissionsImportPersonal {set;get;}

		public bool MaximumPermissionsManageUsers {set;get;}

		public bool MaximumPermissionsEditPublicTemplates {set;get;}

		public bool MaximumPermissionsModifyAllData {set;get;}

		public bool MaximumPermissionsManageCases {set;get;}

		public bool MaximumPermissionsMassInlineEdit {set;get;}

		public bool MaximumPermissionsEditKnowledge {set;get;}

		public bool MaximumPermissionsManageKnowledge {set;get;}

		public bool MaximumPermissionsManageSolutions {set;get;}

		public bool MaximumPermissionsCustomizeApplication {set;get;}

		public bool MaximumPermissionsEditReadonlyFields {set;get;}

		public bool MaximumPermissionsRunReports {set;get;}

		public bool MaximumPermissionsViewSetup {set;get;}

		public bool MaximumPermissionsTransferAnyEntity {set;get;}

		public bool MaximumPermissionsNewReportBuilder {set;get;}

		public bool MaximumPermissionsActivateContract {set;get;}

		public bool MaximumPermissionsActivateOrder {set;get;}

		public bool MaximumPermissionsImportLeads {set;get;}

		public bool MaximumPermissionsManageLeads {set;get;}

		public bool MaximumPermissionsTransferAnyLead {set;get;}

		public bool MaximumPermissionsViewAllData {set;get;}

		public bool MaximumPermissionsEditPublicDocuments {set;get;}

		public bool MaximumPermissionsViewEncryptedData {set;get;}

		public bool MaximumPermissionsEditBrandTemplates {set;get;}

		public bool MaximumPermissionsEditHtmlTemplates {set;get;}

		public bool MaximumPermissionsChatterInternalUser {set;get;}

		public bool MaximumPermissionsManageEncryptionKeys {set;get;}

		public bool MaximumPermissionsDeleteActivatedContract {set;get;}

		public bool MaximumPermissionsChatterInviteExternalUsers {set;get;}

		public bool MaximumPermissionsSendSitRequests {set;get;}

		public bool MaximumPermissionsManageRemoteAccess {set;get;}

		public bool MaximumPermissionsCanUseNewDashboardBuilder {set;get;}

		public bool MaximumPermissionsManageCategories {set;get;}

		public bool MaximumPermissionsConvertLeads {set;get;}

		public bool MaximumPermissionsPasswordNeverExpires {set;get;}

		public bool MaximumPermissionsUseTeamReassignWizards {set;get;}

		public bool MaximumPermissionsEditActivatedOrders {set;get;}

		public bool MaximumPermissionsInstallPackaging {set;get;}

		public bool MaximumPermissionsPublishPackaging {set;get;}

		public bool MaximumPermissionsChatterOwnGroups {set;get;}

		public bool MaximumPermissionsEditOppLineItemUnitPrice {set;get;}

		public bool MaximumPermissionsCreatePackaging {set;get;}

		public bool MaximumPermissionsBulkApiHardDelete {set;get;}

		public bool MaximumPermissionsSolutionImport {set;get;}

		public bool MaximumPermissionsManageCallCenters {set;get;}

		public bool MaximumPermissionsManageSynonyms {set;get;}

		public bool MaximumPermissionsViewContent {set;get;}

		public bool MaximumPermissionsManageEmailClientConfig {set;get;}

		public bool MaximumPermissionsEnableNotifications {set;get;}

		public bool MaximumPermissionsManageDataIntegrations {set;get;}

		public bool MaximumPermissionsDistributeFromPersWksp {set;get;}

		public bool MaximumPermissionsViewDataCategories {set;get;}

		public bool MaximumPermissionsManageDataCategories {set;get;}

		public bool MaximumPermissionsAuthorApex {set;get;}

		public bool MaximumPermissionsManageMobile {set;get;}

		public bool MaximumPermissionsApiEnabled {set;get;}

		public bool MaximumPermissionsManageCustomReportTypes {set;get;}

		public bool MaximumPermissionsEditCaseComments {set;get;}

		public bool MaximumPermissionsTransferAnyCase {set;get;}

		public bool MaximumPermissionsContentAdministrator {set;get;}

		public bool MaximumPermissionsCreateWorkspaces {set;get;}

		public bool MaximumPermissionsManageContentPermissions {set;get;}

		public bool MaximumPermissionsManageContentProperties {set;get;}

		public bool MaximumPermissionsManageContentTypes {set;get;}

		public bool MaximumPermissionsManageExchangeConfig {set;get;}

		public bool MaximumPermissionsManageAnalyticSnapshots {set;get;}

		public bool MaximumPermissionsScheduleReports {set;get;}

		public bool MaximumPermissionsManageBusinessHourHolidays {set;get;}

		public bool MaximumPermissionsManageDynamicDashboards {set;get;}

		public bool MaximumPermissionsCustomSidebarOnAllPages {set;get;}

		public bool MaximumPermissionsManageInteraction {set;get;}

		public bool MaximumPermissionsViewMyTeamsDashboards {set;get;}

		public bool MaximumPermissionsModerateChatter {set;get;}

		public bool MaximumPermissionsResetPasswords {set;get;}

		public bool MaximumPermissionsFlowUFLRequired {set;get;}

		public bool MaximumPermissionsCanInsertFeedSystemFields {set;get;}

		public bool MaximumPermissionsManageKnowledgeImportExport {set;get;}

		public bool MaximumPermissionsEmailTemplateManagement {set;get;}

		public bool MaximumPermissionsEmailAdministration {set;get;}

		public bool MaximumPermissionsManageChatterMessages {set;get;}

		public bool MaximumPermissionsAllowEmailIC {set;get;}

		public bool MaximumPermissionsChatterFileLink {set;get;}

		public bool MaximumPermissionsForceTwoFactor {set;get;}

		public bool MaximumPermissionsViewEventLogFiles {set;get;}

		public bool MaximumPermissionsManageNetworks {set;get;}

		public bool MaximumPermissionsManageAuthProviders {set;get;}

		public bool MaximumPermissionsRunFlow {set;get;}

		public bool MaximumPermissionsCreateCustomizeDashboards {set;get;}

		public bool MaximumPermissionsCreateDashboardFolders {set;get;}

		public bool MaximumPermissionsViewPublicDashboards {set;get;}

		public bool MaximumPermissionsManageDashbdsInPubFolders {set;get;}

		public bool MaximumPermissionsCreateCustomizeReports {set;get;}

		public bool MaximumPermissionsCreateReportFolders {set;get;}

		public bool MaximumPermissionsViewPublicReports {set;get;}

		public bool MaximumPermissionsManageReportsInPubFolders {set;get;}

		public bool MaximumPermissionsEditMyDashboards {set;get;}

		public bool MaximumPermissionsEditMyReports {set;get;}

		public bool MaximumPermissionsViewAllUsers {set;get;}

		public bool MaximumPermissionsAllowUniversalSearch {set;get;}

		public bool MaximumPermissionsConnectOrgToEnvironmentHub {set;get;}

		public bool MaximumPermissionsWorkCalibrationUser {set;get;}

		public bool MaximumPermissionsCreateCustomizeFilters {set;get;}

		public bool MaximumPermissionsWorkDotComUserPerm {set;get;}

		public bool MaximumPermissionsGovernNetworks {set;get;}

		public bool MaximumPermissionsSalesConsole {set;get;}

		public bool MaximumPermissionsTwoFactorApi {set;get;}

		public bool MaximumPermissionsDeleteTopics {set;get;}

		public bool MaximumPermissionsEditTopics {set;get;}

		public bool MaximumPermissionsCreateTopics {set;get;}

		public bool MaximumPermissionsAssignTopics {set;get;}

		public bool MaximumPermissionsIdentityEnabled {set;get;}

		public bool MaximumPermissionsIdentityConnect {set;get;}

		public bool MaximumPermissionsAllowViewKnowledge {set;get;}

		public bool MaximumPermissionsContentWorkspaces {set;get;}

		public bool MaximumPermissionsManageSearchPromotionRules {set;get;}

		public bool MaximumPermissionsCustomMobileAppsAccess {set;get;}

		public bool MaximumPermissionsViewHelpLink {set;get;}

		public bool MaximumPermissionsManageProfilesPermissionsets {set;get;}

		public bool MaximumPermissionsAssignPermissionSets {set;get;}

		public bool MaximumPermissionsManageRoles {set;get;}

		public bool MaximumPermissionsManageIpAddresses {set;get;}

		public bool MaximumPermissionsManageSharing {set;get;}

		public bool MaximumPermissionsManageInternalUsers {set;get;}

		public bool MaximumPermissionsManagePasswordPolicies {set;get;}

		public bool MaximumPermissionsManageLoginAccessPolicies {set;get;}

		public bool MaximumPermissionsManageCustomPermissions {set;get;}

		public bool MaximumPermissionsManageUnlistedGroups {set;get;}

		public bool MaximumPermissionsModifySecureAgents {set;get;}

		public bool MaximumPermissionsManageTwoFactor {set;get;}

		public bool MaximumPermissionsChatterForSharePoint {set;get;}

		public bool MaximumPermissionsLightningExperienceUser {set;get;}

		public bool MaximumPermissionsConfigCustomRecs {set;get;}

		public bool MaximumPermissionsSubmitMacrosAllowed {set;get;}

		public bool MaximumPermissionsBulkMacrosAllowed {set;get;}

		public bool MaximumPermissionsShareInternalArticles {set;get;}

		public bool MaximumPermissionsManageSessionPermissionSets {set;get;}

		public bool MaximumPermissionsSendAnnouncementEmails {set;get;}

		public bool MaximumPermissionsChatterEditOwnPost {set;get;}

		public bool MaximumPermissionsChatterEditOwnRecordPost {set;get;}

		public bool MaximumPermissionsImportCustomObjects {set;get;}

		public bool MaximumPermissionsDelegatedTwoFactor {set;get;}

		public bool MaximumPermissionsChatterComposeUiCodesnippet {set;get;}

		public bool MaximumPermissionsSelectFilesFromSalesforce {set;get;}

		public bool MaximumPermissionsModerateNetworkUsers {set;get;}

		public bool MaximumPermissionsMergeTopics {set;get;}

		public bool MaximumPermissionsSubscribeToLightningReports {set;get;}

		public bool MaximumPermissionsManagePvtRptsAndDashbds {set;get;}

		public bool MaximumPermissionsCampaignInfluence2 {set;get;}

		public bool MaximumPermissionsViewDataAssessment {set;get;}

		public bool MaximumPermissionsRemoveDirectMessageMembers {set;get;}

		public bool MaximumPermissionsCanApproveFeedPost {set;get;}

		public bool MaximumPermissionsAddDirectMessageMembers {set;get;}

		public bool MaximumPermissionsAllowViewEditConvertedLeads {set;get;}

		public bool MaximumPermissionsShowCompanyNameAsUserBadge {set;get;}

		public bool MaximumPermissionsAccessCMC {set;get;}

		public bool MaximumPermissionsViewHealthCheck {set;get;}

		public bool MaximumPermissionsManageHealthCheck {set;get;}

		public bool MaximumPermissionsManageCertificates {set;get;}

		public bool MaximumPermissionsCreateReportInLightning {set;get;}

		public bool MaximumPermissionsPreventClassicExperience {set;get;}

		public bool MaximumPermissionsHideReadByList {set;get;}

		public bool MaximumPermissionsViewAllActivities {set;get;}

		public bool MaximumPermissionsSubscribeReportToOtherUsers {set;get;}

		public bool MaximumPermissionsLightningConsoleAllowedForUser {set;get;}

		public bool MaximumPermissionsSubscribeReportsRunAsUser {set;get;}

		public bool MaximumPermissionsEnableCommunityAppLauncher {set;get;}

		public int UsedLicenses {set;get;}
	}
}
