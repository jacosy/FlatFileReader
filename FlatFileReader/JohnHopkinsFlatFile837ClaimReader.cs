using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FlatFileReader
{
    class JohnHopkinsFlatFile837ClaimReader
    {
        public static ClaimCollection Read(string flatFilePath, string clientID)
        {
            string fileName = Path.GetFileName(flatFilePath);
            DateTime timeStamp = DateTime.Now;
            ClaimCollection claims = new ClaimCollection();
            Claim claim = null;

            foreach (string line in File.ReadAllLines(flatFilePath))
            {
                string[] segments = line.Split(',');
                switch (GerRecordType(segments))
                {
                    case RecordType.Claim:
                        claim = CreateClaim(segments, clientID, fileName, timeStamp, claims.GetClaimCount() + 1);
                        claims.AddClaim(claim);
                        break;
                    case RecordType.Line:
                        Line lineItem = CreateLine(segments, clientID, claim.ClaimID, claim.Lines.Length + 1);
                        claim.AddLine(lineItem);
                        break;
                    case RecordType.NoMatch:
                        throw new ArrayTypeMismatchException(string.Format("Content: {0} does not match to either Claim or Line Format.", line));
                }
            }

            return claims;
        }

        private static RecordType GerRecordType(string[] segments)
        {
            if (segments.Length == 9)
            {
                return RecordType.Claim;
            }
            else if (segments.Length == 7)
            {
                return RecordType.Line;
            }
            return RecordType.NoMatch;
        }

        private static string GetStringValue(string value, string defaultValue = null)
        {
            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }

        private static DateTime? GetDateTimeValue(string value)
        {
            DateTime dTime;
            if (DateTime.TryParse(value, out dTime))
            {
                return dTime;
            }
            else
            {
                return null;
            }
        }

        private static decimal? GetDecimalValue(string value)
        {
            decimal dNumber;
            if (decimal.TryParse(value, out dNumber))
            {
                return dNumber;
            }
            else
            {
                return null;
            }
        }

        private static double? GetDoubleValue(string value)
        {
            double dNumber;
            if (double.TryParse(value, out dNumber))
            {
                return dNumber;
            }
            else
            {
                return null;
            }
        }

        private static Claim CreateClaim(string[] segments, string clientID, string fileName, DateTime timeStamp, int claimNum)
        {
            Claim claim = new Claim();
            claim.ClaimID = string.Format("{0}.{1:yyMMdd}.{2:000000}", fileName, timeStamp, claimNum);
            claim.ClientID = clientID;
            claim.FileName = fileName;
            claim.ClaimIndex = claimNum;

            claim.PatientFirst = GetStringValue(segments[0]);
            claim.PatientLast = GetStringValue(segments[1]);
            claim.InvoiceNumber = GetStringValue(segments[2]);
            claim.FirstDOS = GetDateTimeValue(segments[3]);
            claim.TotalCharges = GetDecimalValue(segments[4]);
            claim.Payer = GetStringValue(segments[5]);

            string claimStatus = segments[6];
            if (!string.IsNullOrEmpty(claimStatus) && claimStatus.ToUpper() != "CLAIMSTATUS")
            {
                claim.ClaimStatus = claimStatus;
            }
            else
            {
                claim.ClaimStatus = null;
            }

            claim.InsuredID = segments[7];
            claim.ProviderTaxID = segments[8];

            return claim;
        }

        private static Line CreateLine(string[] segments, string clientID, string claimID, int lineNum)
        {
            Line line = new Line();
            line = new Line();
            line.LineID = string.Format("{0}.{1:000}", claimID, lineNum);
            line.ClientID = clientID;
            line.ClaimID = claimID;
            line.LineNumber = lineNum;

            line.StartDate = GetDateTimeValue(segments[0]);
            line.EndDate = GetDateTimeValue(segments[1]);
            line.ProcedureCode = GetStringValue(segments[2]);
            line.Mod1 = GetStringValue(segments[3]);
            line.RevenueCode = GetStringValue(segments[4]);
            line.ServiceUnits = GetDoubleValue(segments[5]);
            line.ServiceCharge = GetDecimalValue(segments[6]);

            return line;
        }
    }

    public enum RecordType
    {
        Claim,
        Line,
        NoMatch
    }
}
