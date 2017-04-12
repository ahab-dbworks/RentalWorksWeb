-- added the join to master table to get the trackedby column for unstaging.
/*******************************************************************************
function: funcstageditemsweb
Author:   mv
Notes:
*******************************************************************************/
if exists (select * from sysobjects where name = 'funcstageditemsweb')
begin
   drop function dbo.funcstageditemsweb
end
go
create function dbo.funcstageditemsweb(@orderid     char(08),  
                                       @summary     char(01) = 'F',  
                                       @warehouseid char(08))  
returns table  
as  
return (  
select orderid              = dbo.alltrim(fs.orderid),  
       barcode              = dbo.alltrim(fs.barcode),  
       masterno             = dbo.alltrim(fs.masterno),  
       actualmasterno       = dbo.alltrim(fs.actualmasterno),  
       description          = dbo.alltrim(fs.description),  
       categoryid           = dbo.alltrim(fs.categoryid),  
       quantity             = fs.quantity,  
       qtyordered           = fs.qtyordered,  
       vendorid             = dbo.alltrim(fs.vendorid),  
       vendor               = dbo.alltrim(fs.vendor),  
       masterid             = dbo.alltrim(fs.masterid),  
       warehouseid          = dbo.alltrim(fs.warehouseid),  
       whcode               = dbo.alltrim(fs.whcode),  
       warehouse            = dbo.alltrim(fs.warehouse),  
       masteritemid         = dbo.alltrim(fs.masteritemid),  
       outdatetime          = fs.outdatetime,  
       itemclass            = dbo.alltrim(fs.itemclass),  
       itemorder            = fs.itemorder,  
       orderby              = fs.orderby,  
       notes                = fs.notes,  
       rectype              = dbo.alltrim(fs.rectype),  
       optioncolor          = dbo.alltrim(fs.optioncolor),  
       quoteprint           = dbo.alltrim(fs.quoteprint),  
       orderprint           = dbo.alltrim(fs.orderprint),  
       picklistprint        = dbo.alltrim(fs.picklistprint),  
       contractoutprint     = dbo.alltrim(fs.contractoutprint),  
       returnlistprint      = dbo.alltrim(fs.returnlistprint),  
       invoiceprint         = dbo.alltrim(fs.invoiceprint),  
       contractinprint      = dbo.alltrim(fs.contractinprint),  
       poprint              = dbo.alltrim(fs.poprint),  
       contractreceiveprint = dbo.alltrim(fs.contractreceiveprint),  
       contractreturnprint  = dbo.alltrim(fs.contractreturnprint),  
       outreceiveusersid    = dbo.alltrim(fs.outreceiveusersid),  
       outuser              = dbo.alltrim(fs.outuser),  
       parentid             = dbo.alltrim(fs.parentid),  
       accratio             = fs.accratio,
       trackedby            = m.trackedby  
  
from dbo.funcstaged(@orderid, @summary) fs left outer join master m on (fs.masterid = m.masterid)   
where warehouseid = @warehouseid  
)
go
/*******************************************************************************
function: calqtyreturned
Author:   mv
Notes:
*******************************************************************************/
if exists (select * from sysobjects where name = 'calqtyreturned')
begin
   drop function dbo.calqtyreturned
end
go
create function dbo.calqtyreturned (@orderid           char(08),
                                    @masteritemid      char(08),
                                    @returncontractid char(08))
returns numeric
as
begin
declare @qty numeric

select @qty = sum(ot.qty)
 from  ordertran ot  with (nolock)
 where ot.orderid              = @orderid
 and   ot.masteritemid         = @masteritemid
 and   ot.inreturncontractid   = @returncontractid
select @qty = isnull(@qty, 0)

return @qty
end
go


