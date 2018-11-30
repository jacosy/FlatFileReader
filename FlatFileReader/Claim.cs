using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace FlatFileReader
{
    public class Claim
    {
        public static Claim FromFile(string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Claim));
            using (StreamReader reader = new StreamReader(file))
                return (Claim)serializer.Deserialize(reader);
        }

        public string ClaimID { get; set; }
        public string ClientID { get; set; }
        public string FileName { get; set; }
        public int ClaimIndex { get; set; }
        public string AccountNumber { get; set; }
        public string PatientLast { get; set; }
        public string PatientFirst { get; set; }
        public string PatientMiddle { get; set; }
        public decimal? TotalCharges { get; set; }
        public string ClaimStatus { get; set; }
        public string Payer { get; set; }
        public string PayerID { get; set; }
        public string ProviderTaxID { get; set; }
        public string ProviderNPI { get; set; }
        public string ClaimFilingIndicator { get; set; }
        public string MemberID { get; set; }
        public string InvoiceNumber { get; set; }
        public string CarrierCode { get; set; }
        public decimal? TotalChargesMSRP { get; set; }
        public string FacilityCode { get; set; }
        public string ClaimCodeFrequency { get; set; }
        public string FacilityName { get; set; }
        public string InsuredID { get; set; }
        public string SecondaryPayerName { get; set; }
        public string SecondaryPayerID { get; set; }
        public string SecondaryInsuredID { get; set; }
        public string SecondaryClaimStatus { get; set; }
        public string SecondaryClaimFilingIndicator { get; set; }
        public string TertiaryPayerName { get; set; }
        public string TertiaryPayerID { get; set; }
        public string TertiaryInsuredID { get; set; }
        public string TertiaryClaimStatus { get; set; }
        public string TertiaryClaimFilingIndicator { get; set; }
        public DateTime? FirstDOS { get; set; }
        public string ClaimNumber { get; set; }        
        public string SubscriberLast { get; set; }
        public string SubscriberFirst { get; set; }
        public string SubscriberMiddle { get; set; }
        public string GroupNumber { get; set; }

        private List<Line> _lines = new List<Line>();

        public Claim()
        { }

        public Claim(Claim other)
        {
            ClaimID = other.ClaimID;
            ClientID = other.ClientID;
            FileName = other.FileName;
            ClaimIndex = other.ClaimIndex;
            AccountNumber = other.AccountNumber;
            PatientLast = other.PatientLast;
            PatientFirst = other.PatientFirst;
            PatientMiddle = other.PatientMiddle;
            TotalCharges = other.TotalCharges;
            ClaimStatus = other.ClaimStatus;
            Payer = other.Payer;
            PayerID = other.PayerID;
            ProviderTaxID = other.ProviderTaxID;
            ProviderNPI = other.ProviderNPI;
            ClaimFilingIndicator = other.ClaimFilingIndicator;
            MemberID = other.MemberID;
            InvoiceNumber = other.InvoiceNumber;
            CarrierCode = other.CarrierCode;
            TotalChargesMSRP = other.TotalChargesMSRP;
            FacilityCode = other.FacilityCode;
            ClaimCodeFrequency = other.ClaimCodeFrequency;
            FacilityName = other.FacilityName;
            InsuredID = other.InsuredID;
            SecondaryPayerName = other.SecondaryPayerName;
            SecondaryPayerID = other.SecondaryPayerID;
            SecondaryInsuredID = other.SecondaryInsuredID;
            SecondaryClaimStatus = other.SecondaryClaimStatus;
            SecondaryClaimFilingIndicator = other.SecondaryClaimFilingIndicator;
            TertiaryPayerName = other.TertiaryPayerName;
            TertiaryPayerID = other.TertiaryPayerID;
            TertiaryInsuredID = other.TertiaryInsuredID;
            TertiaryClaimStatus = other.TertiaryClaimStatus;
            TertiaryClaimFilingIndicator = other.TertiaryClaimFilingIndicator;
            FirstDOS = other.FirstDOS;
            ClaimNumber = other.ClaimNumber;
            foreach (Line line in other.Lines)
                AddLine(new Line(line));
        }

        [XmlElement("Line")]
        public Line[] Lines
        {
            get
            {
                return _lines.ToArray();
            }
            set
            {
                _lines = new List<Line>(value);
            }
        }

        public void ClearLines()
        {
            _lines.Clear();
        }

        public void AddLine(Line line)
        {
            _lines.Add(line);
        }

        public int GetLineCount()
        {
            return _lines.Count;
        }

        public Line GetLine(int index)
        {
            return _lines[index];
        }

        public bool ShouldSerializeTotalCharges()
        {
            return TotalCharges != null;
        }

        public bool ShouldSerializeTotalChargesMSRP()
        {
            return TotalChargesMSRP != null;
        }

        public bool ShouldSerializeFirstDOS()
        {
            return FirstDOS != null;
        }

        public void ToFile(string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Claim));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");
            using (StreamWriter writer = new StreamWriter(file))
                serializer.Serialize(writer, this, namespaces);
        }
    }
}
