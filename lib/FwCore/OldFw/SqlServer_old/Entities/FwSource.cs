using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FwCore.SqlServer.Entities
{
    public class FwSource
    {
        //---------------------------------------------------------------------------------------------
        public string SourceId           = string.Empty;
        public string Source             = string.Empty;
        public string SourceType         = string.Empty;
        public string Description        = string.Empty;
        public string FtpHost            = string.Empty;
        public int    FtpPort            = 0;
        public string FtpUserName        = string.Empty;
        public string FtpPath            = string.Empty;
        public string FtpPassword        = string.Empty;
        public string FtpFileName        = string.Empty;
        public string FtpArchive         = string.Empty;
        public string FtpArchivePath     = string.Empty;
        public string TmpFtpFileName     = string.Empty;
        public string TmpSoapPath        = string.Empty;
        public string TmpSoapFileName    = string.Empty;
        public string SystemSource       = string.Empty;
        public bool   FtpSsl             = false;
        public string WebServiceUserName = string.Empty;
        public string WebServicePassword = string.Empty;
        public bool   Disabled           = false;
        //---------------------------------------------------------------------------------------------
        public static FwSource Loads(string description, string sourceid)
        {
            FwSource source;
            FwSqlCommand qry;

            source = new FwSource();
            qry = new FwSqlCommand(FwSqlConnection.AppConnection);
            qry.Add("select top 1 *");
            qry.Add("from source");
            if (!string.IsNullOrEmpty(description))
            {
                qry.Add("where description = @description");
                qry.AddParameter("@description", description);
            }
            if (!string.IsNullOrEmpty(sourceid))
            {
                qry.Add("where sourceid = @sourceid");
                qry.AddParameter("@sourceid", sourceid);
            }
            qry.Execute();
            source.SourceId           = qry.GetField("sourceid").ToString().Trim();
            source.Source             = qry.GetField("source").ToString().Trim();
            source.SourceType         = qry.GetField("sourcetype").ToString().Trim();
            source.Description        = qry.GetField("description").ToString().Trim();
            source.FtpHost            = qry.GetField("ftphost").ToString().Trim();
            source.FtpPort            = qry.GetField("ftpport").ToInt32();
            source.FtpUserName        = qry.GetField("ftpusername").ToString().Trim();
            source.FtpPath            = qry.GetField("ftppath").ToString().Trim().Replace(@"\", "/");
            source.FtpPassword        = qry.GetField("ftppassword").ToString().Trim();
            source.FtpFileName        = qry.GetField("ftpfilename").ToString().Trim();
            source.FtpArchive         = qry.GetField("ftparchive").ToString().Trim();
            source.FtpArchivePath     = qry.GetField("ftparchivepath").ToString().Trim();
            source.TmpFtpFileName     = qry.GetField("tmpftpfilename").ToString().Trim();
            source.TmpSoapPath        = qry.GetField("tmpsoappath").ToString().Trim();
            source.TmpSoapFileName    = qry.GetField("tmpsoapfilename").ToString().Trim();
            source.SystemSource       = qry.GetField("systemsource").ToString().Trim();
            source.FtpSsl             = qry.GetField("ftpssl").ToBoolean();
            source.WebServiceUserName = qry.GetField("webserviceusername").ToString().Trim();
            source.WebServicePassword = qry.GetField("webservicepassword").ToString().Trim();
            source.Disabled           = qry.GetField("disabled").ToBoolean();

            return source;
        }
        //---------------------------------------------------------------------------------------------
    }
}
