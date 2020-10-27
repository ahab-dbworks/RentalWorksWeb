using System;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;

namespace FwStandard.SqlServer
{
    public class FwDateTime : System.Xml.Serialization.IXmlSerializable
    {
        //------------------------------------------------------------------------------
        public string Val = null;
        private string FORMAT_STRING = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffzzz";
        //------------------------------------------------------------------------------
        public static FwDateTime Now {get{return new FwDateTime(DateTime.Now);}}
        //------------------------------------------------------------------------------
        public FwDateTime()
        {
        }
        //------------------------------------------------------------------------------
        public FwDateTime(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                DateTime result = DateTime.Parse(value, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal);            
                Val = result.ToString(FORMAT_STRING, DateTimeFormatInfo.InvariantInfo);
            }
        }
        //------------------------------------------------------------------------------
        public enum BlankTimeOptions{ BeginningOfDay, EndOfDay }
        public FwDateTime(string date, string time, BlankTimeOptions blankTimeOption )
        {
            //if (string.IsNullOrEmpty(date) && blankTimeOption == BlankTimeOptions.EndOfDay)
            //{
            //    DateTime result = DateTime.UtcNow;
            //    Val = result.ToString(FORMAT_STRING, DateTimeFormatInfo.InvariantInfo);
            //}
            if (!string.IsNullOrEmpty(date))
            {
                if (time.Length == 0)
                {
                    if (blankTimeOption == BlankTimeOptions.BeginningOfDay)
                    {
                        time = "00:00:00 am";
                        DateTime result = DateTime.Parse(date + " " + time, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal);
                        Val = result.ToString(FORMAT_STRING, DateTimeFormatInfo.InvariantInfo);
                    }
                    else if (blankTimeOption == BlankTimeOptions.EndOfDay)
                    {
                        time = "12:00:00 am";
                        DateTime result = DateTime.Parse(date + " " + time, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal);
                        Val = result.AddDays(1).AddTicks(-1).ToString(FORMAT_STRING, DateTimeFormatInfo.InvariantInfo);
                    }
                }
                else if (time.Length > 0)
                {
                    string[] timearray = time.Split(new char[] {' '});
                    string ampm = string.Empty;
                    if ( time.Length != 0 && timearray[0].Length != 5 && timearray[0].Length != 8 )
                    {
                        throw new Exception("Time: " + time + " is not in the expected format.");
                    }
                    if (timearray[1].Length != 0 && timearray[1] != "am" && timearray[1] != "pm")
                    {
                        throw new Exception("Expected am or pm for: " + date + " " + time);
                    }
                    DateTime result = DateTime.Parse(date + " " + time, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal);
                    Val = result.ToString(FORMAT_STRING, DateTimeFormatInfo.InvariantInfo);
                }
            }
        }
        ////------------------------------------------------------------------------------
        //public FwDateTime(string value, string time, string ampm)
        //{
        //    if (!string.IsNullOrEmpty(value))
        //    {
        //        DateTime result = DateTime.Parse(value, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal);
        //        result = FwConvert.ToDateTime(result, FwConvert.ToTime24(time, ampm));
        //        Val = result.ToString(FORMAT_STRING, DateTimeFormatInfo.InvariantInfo);
        //    }
        //}
        //------------------------------------------------------------------------------
        public FwDateTime(DateTime value)
        {
            Val = value.ToString(FORMAT_STRING, DateTimeFormatInfo.InvariantInfo);
        }
        //------------------------------------------------------------------------------
        public FwDateTime(DateTime? value)
        {
            if (value != null)
            {
                DateTime dateTime = (DateTime)value;
                Val = dateTime.ToString(FORMAT_STRING, DateTimeFormatInfo.InvariantInfo);
            }
        }
        //------------------------------------------------------------------------------
        public static implicit operator string(FwDateTime value)
        {
            return value.Val;
        }
        //------------------------------------------------------------------------------
        public static implicit operator FwDateTime(DateTime value)
        {
            return new FwDateTime(value);
        }
        //------------------------------------------------------------------------------
        public static implicit operator FwDateTime(string value)
        {
            return new FwDateTime(value);
        }
        //------------------------------------------------------------------------------
        public static implicit operator DateTime(FwDateTime value)
        {
            return value.ToDateTime();
        }
        //------------------------------------------------------------------------------
        public override string ToString()
        {
            return Val;
        }
        //------------------------------------------------------------------------------
        public DateTime ToDateTime()
        {            
            return FwConvert.ToDateTime(this.Val);            
        }
        //------------------------------------------------------------------------------
        public static TimeSpan DateDiff(FwDateTime startTime, FwDateTime endTime)
        {
            return endTime.ToDateTime().Subtract(startTime.ToDateTime());
        }
        //------------------------------------------------------------------------------
        public object GetSqlValue()
        {
            DateTime result;
            if (string.IsNullOrEmpty(Val))
            {
                return DBNull.Value;
            }
            else
            {
                result = DateTime.Parse(Val, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None);            
                return result; 
            }
        }
        //------------------------------------------------------------------------------
        public object GetSqlDate()
        {
            DateTime datetime, result;
            if (string.IsNullOrEmpty(Val))
            {
                return DBNull.Value;
            }
            else
            {
                datetime = DateTime.Parse(Val, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None);            
                result   = datetime.Date;
                return result; 
            }
        }
        //------------------------------------------------------------------------------
        public object GetSqlTime()
        {
            DateTime datetime;
            string result;
            if (string.IsNullOrEmpty(Val))
            {
                return DBNull.Value;
            }
            else
            {
                datetime = DateTime.Parse(Val, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None);            
                result   =  FwConvert.ToShortTime(datetime);
                return result; 
            }
        }
        //------------------------------------------------------------------------------        
        public bool IsNull()
        {
            return string.IsNullOrWhiteSpace(Val);
        }

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement();
            Val = reader.ReadContentAsString();
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(Val);
        }
        //------------------------------------------------------------------------------        
    }
}
