using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebshopHPWcore.Models
{
    //DataContract for Serializing Data - required to serve in JSON format
    [DataContract]
    public class DataPoint
    {
        public DataPoint(string label, int y)
        {
            this.Label = label;
            this.Y = y;
        }
        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<int> Y = null;

        //Explicitly setting the name to be used while serializing to JSON. 
        [DataMember(Name = "label")]
        public string Label = null;


    }

    [DataContract]
    public class DataPoint2
    {
        public DataPoint2(decimal y, string label)
        {
            this.Label = label;
            this.Y = y;
        }
        [DataMember(Name = "label")]
        public string Label = null;

        [DataMember(Name = "y")]
        public Nullable<decimal> Y = null;
    }
}
