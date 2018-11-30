using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace FlatFileReader
{
    public class ClaimCollection
    {
        public static ClaimCollection FromFile(string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ClaimCollection));
            using (StreamReader reader = new StreamReader(file))
                return (ClaimCollection)serializer.Deserialize(reader);
        }

        private List<Claim> _claims = new List<Claim>();

        public ClaimCollection()
        { }

        public ClaimCollection(ClaimCollection other)
        {
            foreach (Claim claim in other.Claims)
                AddClaim(new Claim(claim));
        }

        public ClaimCollection(IEnumerable<Claim> claims)
        {
            foreach (Claim claim in claims)
                AddClaim(claim);
        }

        [XmlElement("Claim")]
        public Claim[] Claims
        {
            get
            {
                return _claims.ToArray();
            }
            set
            {
                _claims = new List<Claim>(value);
            }
        }

        public void AddClaim(Claim claim)
        {
            _claims.Add(claim);
        }

        public int GetClaimCount()
        {
            return _claims.Count;
        }

        public Claim GetClaim(int index)
        {
            return _claims[index];
        }

        public void ToFile(string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ClaimCollection));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");
            using (StreamWriter writer = new StreamWriter(file))
                serializer.Serialize(writer, this, namespaces);
        }
    }
}
