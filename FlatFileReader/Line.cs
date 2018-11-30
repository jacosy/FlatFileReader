using System;
using System.Collections.Generic;
using System.Text;

namespace FlatFileReader
{
    public class Line
    {
        public string LineID { get; set; }
        public string ClientID { get; set; }
        public string ClaimID { get; set; }
        public int LineNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string RevenueCode { get; set; }
        public string ProcedureCode { get; set; }
        public decimal? ServiceCharge { get; set; }
        public double? ServiceUnits { get; set; }
        public string Mod1 { get; set; }
        public string Mod2 { get; set; }
        public string Mod3 { get; set; }
        public string Mod4 { get; set; }
        public decimal? ServiceChargeMSRP { get; set; }
        public decimal? TaxAmt { get; set; }
        public decimal? TotalChargeAmt { get; set; }
        public decimal? AmtRemaining { get; set; }

        public string LocationCode { get; set; }

        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(ProcedureCode) && ServiceCharge.HasValue;
            }
        }

        public bool IsBlank
        {
            get
            {
                return !StartDate.HasValue && !EndDate.HasValue && string.IsNullOrEmpty(RevenueCode) && string.IsNullOrEmpty(ProcedureCode) && !ServiceCharge.HasValue && !ServiceUnits.HasValue
                    && string.IsNullOrEmpty(Mod1) && string.IsNullOrEmpty(Mod2) && string.IsNullOrEmpty(Mod3) && string.IsNullOrEmpty(Mod4) && !ServiceChargeMSRP.HasValue && !TaxAmt.HasValue
                    && !TotalChargeAmt.HasValue && !AmtRemaining.HasValue;
            }
        }

        public Line()
        { }

        public Line(Line other)
        {
            LineID = other.LineID;
            ClientID = other.ClientID;
            ClaimID = other.ClaimID;
            LineNumber = other.LineNumber;
            StartDate = other.StartDate;
            EndDate = other.EndDate;
            RevenueCode = other.RevenueCode;
            ProcedureCode = other.ProcedureCode;
            ServiceCharge = other.ServiceCharge;
            ServiceUnits = other.ServiceUnits;
            Mod1 = other.Mod1;
            Mod2 = other.Mod2;
            Mod3 = other.Mod3;
            Mod4 = other.Mod4;
            ServiceChargeMSRP = other.ServiceChargeMSRP;
            TaxAmt = other.TaxAmt;
            TotalChargeAmt = other.TotalChargeAmt;
            AmtRemaining = other.AmtRemaining;
        }

        public bool ShouldSerializeStartDate()
        {
            return StartDate != null;
        }

        public bool ShouldSerializeEndDate()
        {
            return EndDate != null;
        }

        public bool ShouldSerializeServiceCharge()
        {
            return ServiceCharge != null;
        }

        public bool ShouldSerializeServiceUnits()
        {
            return ServiceUnits != null;
        }

        public bool ShouldSerializeServiceChargeMSRP()
        {
            return ServiceChargeMSRP != null;
        }

        public bool ShouldSerializeTaxAmt()
        {
            return TaxAmt != null;
        }

        public bool ShouldSerializeTotalChargeAmt()
        {
            return TotalChargeAmt != null;
        }

        public bool ShouldSerializeAmtRemaining()
        {
            return AmtRemaining != null;
        }

        public bool ShouldSerializeLocationCode()
        {
            return false;
        }

        public bool ShouldSerializeIsValid()
        {
            return false;
        }

        public bool ShouldSerializeIsBlank()
        {
            return false;
        }
    }
}
