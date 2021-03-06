﻿using System;
using System.Drawing;
using System.IO;
using Fw.Json.Utilities;
using System.Threading;
using Fw.Json.ValueTypes;

namespace Fw.Json.SqlServer
{
    public class FwDatabaseField : IConvertible
    {
        private object fieldValue = new object();
        public object FieldValue
        {
            get{ return fieldValue; }
            set{ fieldValue = value; }
        }
        //--------------------------------------------------------------------------------
        public FwDatabaseField()
        {

        }
        //--------------------------------------------------------------------------------
        public FwDatabaseField(object value) : this()
        {
            this.FieldValue = value;
        }
        //--------------------------------------------------------------------------------
        // Globalizing Dates: http://msdn.microsoft.com/en-us/goglobal/bb688124.aspx
        public string ToShortDateString()
        {
            string str = string.Empty;
            FwDateTime dateTime;

            if (FieldValue is DateTime)
            {
                dateTime = (DateTime)FieldValue;
            }
            else if ((FieldValue is String) && (!string.IsNullOrEmpty((string)FieldValue)))
            {
                dateTime = (string)FieldValue;
            }
            else
            {
                dateTime = DateTime.MinValue;
            }
            if (dateTime != DateTime.MinValue)
            {
                //switch(Thread.CurrentThread.CurrentCulture.Name)
                //{
                //    case "en-US":
                //        str = dateTime.ToDateTime().ToString("MM/dd/yyyy");
                //        break;
                //    case "en-CA":
                //        str = dateTime.ToDateTime().ToString("dd/MM/yyyy");
                //        break;
                //    default:
                //        str = dateTime.ToDateTime().ToString("d", new System.Globalization.CultureInfo(Thread.CurrentThread.CurrentCulture.Name));
                //        break;
                //}
                // need to globalize the dates correctly
                str = dateTime.ToDateTime().ToString("MM/dd/yyyy");
            }

            return str;
        }
        //--------------------------------------------------------------------------------
        public string ToLongDateString()
        {
            string str = string.Empty;
            FwDateTime dateTime;
            if (FieldValue is DateTime)
            {
                dateTime = (DateTime)FieldValue;   
                str = dateTime.ToDateTime().ToString("D", new System.Globalization.CultureInfo(Thread.CurrentThread.CurrentCulture.Name));
            }
            else if ((FieldValue is String) && (!string.IsNullOrEmpty((string)FieldValue)))
            {
                dateTime = (string)FieldValue;
                str = dateTime.ToDateTime().ToString("D", new System.Globalization.CultureInfo(Thread.CurrentThread.CurrentCulture.Name));
            }

            return str;
        }
        //--------------------------------------------------------------------------------
        public string ToUniversalIso8601DateTimeString()
        {
            string str = string.Empty;
            DateTime dateTime;
            if (FieldValue is DateTime)
            {
                dateTime = (DateTime)FieldValue;   
                str = dateTime.ToString("u", new System.Globalization.CultureInfo(Thread.CurrentThread.CurrentCulture.Name));
            }
            return str;
        }
        //--------------------------------------------------------------------------------
        public string ToDateTimeStringIso8601()
        {
            string str = string.Empty;
            DateTime dateTime;
            if (FieldValue is DateTime)
            {
                dateTime = (DateTime)FieldValue;   
                str = dateTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
            }
            return str;
        }
        //--------------------------------------------------------------------------------
        public string ToShortTimeString()
        {
            string str = string.Empty;
            FwDateTime dateTime;
            if (FieldValue is DateTime)
            {
                dateTime = (DateTime)FieldValue;
                //str = dateTime.ToDateTime().ToString("t", new System.Globalization.CultureInfo(Thread.CurrentThread.CurrentCulture.Name));
                // need to globalize the times correctly
                str = dateTime.ToDateTime().ToString("HH:mm:ss");
            }
            else if ((FieldValue is String) && (!string.IsNullOrEmpty((string)FieldValue)))
            {
                dateTime = (string)FieldValue;
                //str = dateTime.ToDateTime().ToString("t", new System.Globalization.CultureInfo(Thread.CurrentThread.CurrentCulture.Name));
                // need to globalize the times correctly
                str = dateTime.ToDateTime().ToString("HH:mm:ss");
            }
            return str;
        }
        //--------------------------------------------------------------------------------
        public string ToShortDateTimeString()
        {
            string result;
            result = this.ToShortDateString() + " " + this.ToShortTimeString();
            return result;
        }
        //--------------------------------------------------------------------------------
        public string ToLongTimeString()
        {
            string str = string.Empty;
            FwDateTime dateTime;
            if (FieldValue is DateTime)
            {
                dateTime = (DateTime)FieldValue;   
                str = dateTime.ToDateTime().ToString("T", new System.Globalization.CultureInfo(Thread.CurrentThread.CurrentCulture.Name));
            }
            else if ((FieldValue is String) && (!string.IsNullOrEmpty((string)FieldValue)))
            {
                dateTime = (string)FieldValue;
                str = dateTime.ToDateTime().ToString("T", new System.Globalization.CultureInfo(Thread.CurrentThread.CurrentCulture.Name));
            }
            return str;
        }
        //--------------------------------------------------------------------------------
        public string ToAMPMString()
        {
            string str = "AM";
            DateTime dateTime;
            if (FieldValue is DateTime)
            {
                dateTime = (DateTime)FieldValue;   
                str = dateTime.ToString("tt").Trim();
            }
            return str;       
        }
        //--------------------------------------------------------------------------------
        public bool ToBoolean()
        {
            bool isTrue = false;
            if (this.IsDbNull())
            {
                isTrue = false;
            }
            else if (fieldValue is System.Byte)
            {
                isTrue = (Convert.ToByte(fieldValue) == 1);
            }
            else if (fieldValue is System.Char)
            {
                isTrue = (Convert.ToChar(fieldValue) == 'T');
            }
            else if (fieldValue is System.Decimal)
            {
                isTrue = (Convert.ToDecimal(fieldValue) == 1);
            }
            else if (fieldValue is System.Double)
            {
                isTrue = (Convert.ToDouble(fieldValue) == 1);
            }
            else if (fieldValue is System.Int16)
            {
                isTrue = (Convert.ToUInt16(fieldValue) == 1);
            }
            else if (fieldValue is System.Int32)
            {
                isTrue = (Convert.ToInt32(fieldValue) == 1);
            }
            else if (fieldValue is System.Int64)
            {
                isTrue = (Convert.ToInt64(fieldValue) == 1);
            }
            else if (fieldValue is System.SByte)
            {
                isTrue = (Convert.ToSByte(fieldValue) == 1);
            }
            else if (fieldValue is System.Single)
            {
                isTrue = (Convert.ToSingle(fieldValue) == 1);
            }
            else if (fieldValue is System.String)
            {
                isTrue = (Convert.ToString(fieldValue).Trim().ToUpper() == "T");
            }
            else
            {
                throw new ApplicationException("ToBoolean() has not been implemented for this data type");
            }
            return isTrue;
        }
        //--------------------------------------------------------------------------------
        public Bitmap ToBitmap()
        {
            byte[] buffer = fieldValue as byte[];
            MemoryStream stream = null;
            Bitmap image = null;
            if (buffer != null)
            {
                stream = new MemoryStream(buffer);
                image = new Bitmap(stream);
            }
            return image;
        }
        //--------------------------------------------------------------------------------
        public byte ToByte()
        {
            return Convert.ToByte(fieldValue);
        }
        //--------------------------------------------------------------------------------
        public byte[] ToByteArray()
        {
            return fieldValue as byte[];
        }
        
        //--------------------------------------------------------------------------------
        public char ToChar()
        {
            return Convert.ToChar(fieldValue);
        }
        //--------------------------------------------------------------------------------
        public DateTime ToDateTime()
        {
            return FwConvert.ToDateTime(fieldValue.ToString().Trim());
        }
        //--------------------------------------------------------------------------------
        public decimal ToDecimal()
        {
            return FwConvert.ToDecimal(fieldValue);
        }
        //--------------------------------------------------------------------------------
        public double ToDouble()
        {
            return FwConvert.ToDouble(fieldValue);
        }
        //--------------------------------------------------------------------------------
        public FwDateTime ToFwDateTime()
        {
            return new FwDateTime(fieldValue.ToString().Trim());
        }
        //--------------------------------------------------------------------------------
        public Guid ToGuid()
        {
            return (  (fieldValue != null) && (fieldValue.ToString().Length > 0)  ) ? new Guid(fieldValue.ToString()) : Guid.Empty;
        }
        //--------------------------------------------------------------------------------
        public short ToInt16()
        {
            return Convert.ToInt16(fieldValue);
        }
        //--------------------------------------------------------------------------------
        public int ToInt32()
        {
            return FwConvert.ToInt32(fieldValue);
        }
        //--------------------------------------------------------------------------------
        public long ToInt64()
        {
            return Convert.ToInt64(fieldValue);
        }
        //--------------------------------------------------------------------------------
        public sbyte ToSByte()
        {
            return Convert.ToSByte(fieldValue);
        }
        //--------------------------------------------------------------------------------
        public float ToSingle()
        {
            return Convert.ToSingle(fieldValue);
        }
        //--------------------------------------------------------------------------------
        public override string ToString()
        {
            return Convert.ToString(fieldValue);
        }
        //--------------------------------------------------------------------------------
        public object ToType(Type conversionType)
        {
            return Convert.ChangeType(fieldValue, conversionType);
        }
        //--------------------------------------------------------------------------------
        public ushort ToUInt16()
        {
            return Convert.ToUInt16(fieldValue);
        }
        //--------------------------------------------------------------------------------
        public uint ToUInt32()
        {
            return Convert.ToUInt32(fieldValue);
        }
        //--------------------------------------------------------------------------------
        public ulong ToUInt64()
        {
            return Convert.ToUInt64(fieldValue);
        }
        //--------------------------------------------------------------------------------
        public bool IsDbNull()
        {
            return (fieldValue == DBNull.Value);
        }
        //--------------------------------------------------------------------------------
        public string ToHtmlColor()
        {
            return ColorTranslator.ToHtml(ColorTranslator.FromOle(FwConvert.ToInt32(fieldValue)));
        }
        //--------------------------------------------------------------------------------
        public string ToBase64String()
        {
            return Convert.ToBase64String((byte[])fieldValue);
        }
        //--------------------------------------------------------------------------------
        public string ToJpgDataURL()
        {
            return String.Format("data:image/jpg;charset=utf-8;base64,{0}", Convert.ToBase64String((byte[])fieldValue));
        }
        //--------------------------------------------------------------------------------

        #region IConvertible Members

        public TypeCode GetTypeCode()
        {
            return Type.GetTypeCode(fieldValue.GetType());
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(fieldValue, provider);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(fieldValue, provider);
        }

        public char ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(fieldValue, provider);
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            DateTime dateTime;
            if (fieldValue.ToString().Trim() != string.Empty)
            {
                dateTime = Convert.ToDateTime(fieldValue, provider);
            }
            else
            {
                dateTime = DateTime.MinValue;
            }
            return dateTime;
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(fieldValue, provider);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(fieldValue, provider);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(fieldValue, provider);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(fieldValue, provider);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(fieldValue, provider);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(fieldValue, provider);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(fieldValue, provider);
        }

        public string ToString(IFormatProvider provider)
        {
            return Convert.ToString(fieldValue, provider).Trim();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(fieldValue, conversionType, provider);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(fieldValue, provider);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(fieldValue, provider);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(fieldValue, provider);
        }

        #endregion
    }
}
