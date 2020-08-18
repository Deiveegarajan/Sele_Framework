using System;
using System.Reflection;

namespace Selenium.Tests.Base.Selenium.Core
{
    public static class GlobalEnum
    {

        #region String value implementation
        /// <summary>
        /// String value implementation for the Global Enums
        /// </summary>
        public class StringValue : System.Attribute
        {
            public string Value { get; private set; }

            public StringValue(string value)
            {
                Value = value;
            }
        }
        #endregion

        #region string value from the Enum values
        /// <summary>
        /// Get the string value from the Enum values
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStringValue(this Enum value)
        {
            string stringValue = value.ToString();
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            StringValue[] attrs = fieldInfo.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
            if (attrs.Length > 0)
            {
                stringValue = attrs[0].Value;
            }
            return stringValue;
        }
        #endregion

        #region Action Click
        /// <summary>
        /// Various option of mouse and keyboard actions
        /// </summary>
        public enum ActionClick
        {
            DoubleClick,
            RightClick,
            MouseHover,
            MouseHoverKeysSpace,
            MouseHoverClick,
            Click,
            DragAndDrop,
            MouseHoverKeysEnter
        }
        #endregion

        #region Registry Entry type
        /// <summary>
        /// Entry Type
        /// </summary>
        public enum RegistryEntryType
            {
                [StringValue("Utgående post/Outbound")]
                OutgoingType,
                [StringValue("Internt notat med oppfølging/Internal")]
                InternalNoteMedType,
                [StringValue("Internt notat uten oppfølging/internal")]
                InternalNoteUtenType,
                [StringValue("Saksframlegg/Case draft")]
                CaseDraftType,
                [StringValue("Inngående/Incoming")]
                IncomingType,
                [StringValue("Dokumentpost i saksmappe")]
                DocumentPostType,
                [StringValue("Basisregistrering")]
                BasicRegistrationType,
                [StringValue("Registrering")]
                RegistrationType
            };
        #endregion

        #region Attachment options
        /// <summary>
        /// Select Attachment options
        /// </summary>
        public enum AttachmentType
        {
            [StringValue("Document template")]
             DocumentTemplate,
            [StringValue("File")]
             File,
            [StringValue("Message")]
             Message
        };
        #endregion

        #region Document Template Type
        /// <summary>
        /// Select Document Template type
        /// </summary>
        public enum DocumentTemplateType
        {
            [StringValue("Dokumentmal")]
            Dokumentmal,
            [StringValue("Standardbrev")]
            Standardbrev
        };
        #endregion

        #region Document sub type

        /// <summary>
        /// Select Document sub type
        /// </summary>
        public enum DocumentSubType
        {
            [StringValue("Brev - Word")]
            BrevDocument,
            [StringValue("Standard brev")]
            Standardbrev,
            [StringValue("E-post")]
            EPost,
            [StringValue("Saksframlegg")]
            Saksframlegg
        };
        #endregion

        #region Registry entry read/unread marking
        /// <summary>
        /// Mark as Read/Unread in Registry Entry
        /// </summary>
        public enum SelectRegistryEntryMarkAs
        {
            [StringValue("Read")]
            Read,
            [StringValue("UnRead")]
            Unread
        };
        #endregion

        #region Appliaction modules
        /// <summary>
        /// Application modules
        /// </summary>
        public enum ApplicationModules
        {
            [StringValue("rm")]
            RecordManagement,
            [StringValue("mm")]
            MeetingModule,
            [StringValue("sa")]
            Administrator,
            [StringValue("eb")]
            eBuildingCase
        };
        #endregion

        #region Document list values
        /// <summary>
        /// Meeting Document list values
        /// </summary>
        public enum MeetingDocListValues
        {
            [StringValue("Møteprotokoll")]
            MeetingProtocol,
            [StringValue(" Møteprotokoll umiddelbart godkjente saker Meeting")]
            MeetingProtocolApprovedInMeeting,
            [StringValue("Møteinnkalling")]
            MeetingNotice,
            [StringValue("Saksliste")]
            CaseList,
            [StringValue("Saksfremlegg")]
            CaseDraft,
            [StringValue("Forside")]
            FirstPageMeetingNotice,
            [StringValue("Notat")]
            Notat
        };
        #endregion

        #region Meeting Tab values
        /// <summary>
        /// Meeting list of Tab values
        /// </summary>
        public enum MeetingListTabValues
        {
            [StringValue("Case protocols")]
            Caseprotocols,
            [StringValue("Meeting documents")]
            MeetingDocuments,
            [StringValue("Case documents")]
            Casedocuments
        };
        #endregion

        #region Meeting Document List values
        /// <summary>
        /// Meeting Document List
        /// </summary>
        public enum MeetingDocumentListValues
        {
            [StringValue("Møteprotokoll")]
            MeetingProtocol,
            [StringValue(" Møteprotokoll umiddelbart godkjente saker Meeting")]
            MeetingProtocolApprovedInMeeting,
            [StringValue("Møteinnkalling")]
            MeetingNotice,
            [StringValue("Saksliste")]
            CaseList,
            [StringValue("Saksfremlegg")]
            CaseDraft,
            [StringValue("Forside")]
            FirstPageMeetingNotice,
            [StringValue("Notat")]
            Notat
        };
        #endregion

        #region Meeting tab control
        /// <summary>
        /// meeting tab control
        /// </summary>
        public enum MeetingTabControl
        {
            [StringValue("Case list")]
            CaseList,
            [StringValue("Documents")]
            Documents,
            [StringValue("Attendance")]
            Attendance,
            [StringValue("Distributionlist")]
            Distributionlist
        };
        #endregion

        #region Meeting sort by
        /// <summary>
        /// Meeting sort by
        /// </summary>
        public enum MeetingTypes
        {
            [StringValue("formannskapet")]
            Formannskapet,
            [StringValue("kommunestyret")]
            Kommunestyret
        };
        #endregion

        #region Attendee Secretary names
        /// <summary>
        /// Attendee Secretary names
        /// </summary>
        public enum AttendeeSecretaryNames
        {
            [StringValue("guilt")]
            guilt,
            [StringValue("steinar Abrahamsen")]
            steinar,
            [StringValue("Kiran Kumar")]
            Kiran,
            [StringValue("K Kumar")]
            KKumar
        };
        #endregion

        #region Manu name sorting
        /// <summary>
        /// Meeting sort by the menu names
        /// </summary>
        public enum MeetingSortByMenuNames
        {
            [StringValue("Type")]
            SortByType,
            [StringValue("No.")]
            SortByNo,
            [StringValue("Casetitle")]
            SortByCaseTitle
        };
        #endregion

        #region Refort format
        /// <summary>
        /// Report format names
        /// </summary>
        public enum ReportFormatNames
        {
            [StringValue("PDF")]
            PDF,
            [StringValue("HTML")]
            HTML,
            [StringValue("Word")]
            Word,
            [StringValue("Excel")]
            Excel,
            [StringValue("Text")]
            Text
        };
        #endregion

        #region Receivers
        public enum Receiver
        {
            [StringValue("AA")]
            AndaApotek,
        }
        #endregion

        #region Roles
        public enum RolesInApplication
        {
            [StringValue("Caseworker - Eng - ENG")]
            CaseWorker,
            Administrator,
            Arkivansvarlig,
            Leader,
            [StringValue("Main registrar - Eng")]
            MainRegistrar,
            Registrar,
            Saksbehandler,
            [StringValue("Secretary of the Board - Eng")]
            SecretaryOfTheBoard,
            [StringValue("Caseworker - ENG")]
            CaseWorkerENG,
        }
        #endregion

        #region Title modification options
        /// <summary>
        /// Title modification options
        /// </summary>
        public enum TitleModify
        {
            [StringValue("PersonName")]
            PersonName,
            [StringValue("Screened")]
            Screened,           
            [StringValue("RemoveScreeningFromText")]
            RemoveScreeningFromText,
            [StringValue("RemoveMarkingPersonName")]
            RemoveMarkingPersonName,
        }
        #endregion

        #region Text Color and Format
        /// <summary>
        /// title backgorund text format
        /// </summary>
        public enum TitleBackgroundFormat
        {
            [StringValue("Red")]
            Red,
            [StringValue("Italic")]
            Italic,
            [StringValue("")]
            Blue,
            [StringValue("RedWithItalic")]
            RedWithItalic,
        }
        #endregion

        #region Report Format
        public enum ReportFormat
        {
            [StringValue("PDF")]
            PDF,
            [StringValue("HTML")]
            HTML,
            [StringValue("Word")]
            Word,
            [StringValue("Excel")]
            Excel,
            [StringValue("Text")]
            Text
        };
        #endregion

        #region Admin Module Description
        public enum Description
        {
            DropdownHTMLComboBox,
            TextBox,
            KendoDropdown
        };
        #endregion

    }
}

