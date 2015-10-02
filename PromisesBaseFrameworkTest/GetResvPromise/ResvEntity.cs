using System;
using System.Runtime.Serialization;

namespace PromisesBaseFrameworkTest.GetResvPromise
{
    [DataContract]
    public class ResvEntity
    {
        [DataMember(Name = "siteId")]
        public int SiteId { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "siteName")]
        public string SiteName { get; set; }

        [DataMember(Name = "resvId")]
        public int ResvId { get; set; }

        [DataMember(Name = "resvNum")]
        public string ResvNum { get; set; }

        [DataMember(Name = "resvStatus")]
        public string ResvStatus { get; set; }

        [DataMember(Name = "resvTypeId")]
        public int ResvTypeId { get; set; }

        [DataMember(Name = "resvTypeName")]
        public string ResvTypeName { get; set; }

        [DataMember(Name = "resvSubTypeId")]
        public int ResvSubTypeId { get; set; }

        [DataMember(Name = "resvSubTypeName")]
        public string ResvSubTypeName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "streetAddress")]
        public string StreetAddress { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "state")]
        public string State { get; set; }

        [DataMember(Name = "postalCode")]
        public string PostalCode { get; set; }

        [DataMember(Name = "countryId")]
        public int CountryId { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "phone1")]
        public string Phone1 { get; set; }

        [DataMember(Name = "inDate")]
        public DateTime InDate { get; set; }

        [DataMember(Name = "outDate")]
        public DateTime OutDate { get; set; }

        [DataMember(Name = "roomFolio")]
        public int RoomFolio { get; set; }

        [DataMember(Name = "incidentalsFolio")]
        public int IncidentalsFolio { get; set; }

        [DataMember(Name = "creditCardTypeId")]
        public int CreditCardTypeId { get; set; }

        [DataMember(Name = "creditCardType")]
        public string CreditCardType { get; set; }

        [DataMember(Name = "creditCardNum")]
        public string CreditCardNum { get; set; }

        [DataMember(Name = "creditCardExpDate")]
        public DateTime CreditCardExpDate { get; set; }
    }
}