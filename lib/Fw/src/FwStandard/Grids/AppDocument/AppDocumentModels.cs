using FwStandard.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FwStandard.Grids.AppDocument
{
    [GetRequest(DefaultSort: "DateStamp:asc")]
    public class AppDocumentGetRequest : GetRequest
    {
        /// <summary>
        /// Unique Identifier [Filter|Sort]
        /// </summary>
        [GetRequestProperty(true, true), MaxLength(8)]
        public string DocumentTypeId { get; set; }

        /// <summary>
        /// Document description [Filter|Sort]
        /// </summary>
        [GetRequestProperty(true, true), MaxLength(8)]
        public string Description { get; set; }

        /// <summary>
        /// Document last modified data [Filter|Sort]
        /// </summary>
        [GetRequestProperty(true, true), MaxLength(8)]
        public string DateStamp { get; set; }
    }

    public class AppDocumentGetManyResponse
    {
        public string DocumentId { get; set; }
        public string DocumentTypeId { get; set; }
        public string DocumentType { get; set; }
        public string Description { get; set; }
        public bool? HasImages { get; set; }
        public bool? HasFile { get; set; }
        public bool? Inactive { get; set; }
        public string DateStamp { get; set; }
    }


    public class AppDocumentPostRequest
    {
        public string DocumentTypeId { get; set; }
        public string Description { get; set; }
        public bool? AttachToEmail { get; set; }
        public bool? Inactive { get; set; }
        public bool? FileIsModified { get; set; }
        public string FileDataUrl { get; set; }
        public string FilePath { get; set; }
    }

    public class AppDocumentPutRequest : AppDocumentPostRequest
    {
        public string DocumentId { get; set; }
    }

    public class GetDocumentThumbnailsResponse
    {
        public List<DocumentImage> Thumbnails { get; set; }

    }

    public class DocumentImage
    {
        public string ImageId { get; set; }
        public string Description { get; set; }
        public string ImageNumber { get; set; }
        public string DataUrl { get; set; }
    }

    public class GetDocumentImageResponse
    {
        public DocumentImage Image { get; set; }

    }

    public class GetDocumentFileResponse
    {
        public DocumentFile File { get; set; }
    }

    public class DocumentFile
    {
        public string ImageId { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
        public string Extension { get; set; }
    }

    public class PutDocumentFileRequest
    {
        public string DataUrl { get; set; }
        public string FileExtension { get; set; }
    }

    public class PutDocumentImageRequest
    {
        public string DataUrl { get; set; }
        public string FileExtension { get; set; }
    }
}
