using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System;
using static WebApi.Modules.Home.InventorySearchPreview.InventorySearchPreviewController;

namespace WebApi.Modules.Home.InventorySearchPreview
{
    [FwSqlTable("inventoryview")]
    public class InventorySearchPreviewLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "id", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string Id { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text)]
        public string ParentId { get; set; } 
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; } 
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masternocolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ICodeColor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mastercolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DescriptionColor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "class", modeltype: FwDataTypes.Text)]
        public string Classification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "classdesc", modeltype: FwDataTypes.Text)]
        public string ClassificationDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "classcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ClassificationColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyavailable", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityAvailable{ get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "conflictdate", modeltype: FwDataTypes.Date)]
        public string ConflictDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyin", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyqcrequired", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityQcRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? DailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week2rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week2Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week3rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week3Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week4rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week4Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week5rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week5Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtycolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string QuantityColor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "appimageid", modeltype: FwDataTypes.Text)]
        public string ImageId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "thumbnail", modeltype: FwDataTypes.JpgDataUrl)]
        public string Thumbnail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "imageheight", modeltype: FwDataTypes.Integer)]
        public int? ImageHeight { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "imagewidth", modeltype: FwDataTypes.Integer)]
        public int? ImageWidth { get; set; }
        //------------------------------------------------------------------------------------ 


        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> PreviewAsync(InventorySearchPreviewBrowseRequest request)
        {
            FwJsonDataTable dt = null;

            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getinventorysearchpreview", this.AppConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, request.SessionId);
                    qry.AddParameter("@showavail", SqlDbType.NVarChar, ParameterDirection.Input, (request.ShowAvailability ? "T" : "F"));
                    if ((request.FromDate != null) && (request.FromDate > DateTime.MinValue))
                    {
                        qry.AddParameter("@fromdate", SqlDbType.DateTime, ParameterDirection.Input, request.FromDate);
                    }
                    if ((request.ToDate != null) && (request.ToDate > DateTime.MinValue))
                    {
                        qry.AddParameter("@todate", SqlDbType.DateTime, ParameterDirection.Input, request.ToDate);
                    }
                    PropertyInfo[] propertyInfos = typeof(InventorySearchPreviewLoader).GetProperties();
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        FwSqlDataFieldAttribute sqlDataFieldAttribute = propertyInfo.GetCustomAttribute<FwSqlDataFieldAttribute>();
                        if (sqlDataFieldAttribute != null)
                        {
                            qry.AddColumn(sqlDataFieldAttribute.ColumnName, propertyInfo.Name, sqlDataFieldAttribute.ModelType, sqlDataFieldAttribute.IsVisible, sqlDataFieldAttribute.IsPrimaryKey, false);
                        }
                    }
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
            }
            return dt;
        }
        //------------------------------------------------------------------------------------

    }
}