using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Xml.Serialization;

namespace FwCore.Services.EmailService
{
    public class BeforeSendEmailRequest
    {
        public bool LetSendEmail { get; set; } = true;
        public string EmailTaskId { get; set; }
        public string AppEmailCategory { get; set; }
        public string EmailTaskBody { get; set; }
        public MailMessage MailMessage { get; set; }

    }

    public class BeforeSendEmailResponse
    {
        public bool LetSendEmail { get; set; } = true;
        public bool AttachMessageBody { get; set; } = true;
        public BeforeSendEmailResponse(BeforeSendEmailRequest request)
        {
            this.LetSendEmail = request.LetSendEmail;
        }
    }


    [XmlRoot(ElementName = "ReportEmail")]
    public class ReportEmail
    {
        // Attributes
        [XmlAttribute(AttributeName = "uniqueid")]
        public string UniqueId { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "attachimage")]
        public bool AttachImage { get; set; } = true;

        [XmlAttribute(AttributeName = "imagename")]
        public string ImageName { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "imagewidth")]
        public int ImageWidth { get; set; } = 480;

        [XmlAttribute(AttributeName = "imageheight")]
        public int ImageHeight { get; set; } = 640;

        [XmlAttribute(AttributeName = "attachpdf")]
        public bool AttachPdf { get; set; } = true;

        [XmlAttribute(AttributeName = "pdfname")]
        public string PdfName { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "usecustomreportlayout")]
        public bool UseCustomReportLayout { get; set; } = false;

        [XmlAttribute(AttributeName = "customreportlayoutid")]
        public string CustomReportLayoutId { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "customreportlayoutcategory")]
        public string CustomReportLayoutCategory { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "customreportlayoutdescription")]
        public string CustomReportLayoutDescription { get; set; } = string.Empty;


        // Elements
        [XmlElement(ElementName = "PlainTextBody")]
        public string PlainTextBody { get; set; } = string.Empty;

        [XmlElement(ElementName = "HtmlBody")]
        public string HtmlBody { get; set; } = string.Empty;

    }
}
