--procBarcode.sql
--activitytype was misspelled for RETURN and error message had mispelling
/*******************************************************************************
* Procedure: pdasubreceivereturnitem
* Author:    eg
* Notes:     
*******************************************************************************/
if exists (select name from sysobjects where name = 'pdasubreceivereturnitem')
begin
    drop procedure dbo.pdasubreceivereturnitem
end   
go  
create  procedure dbo.pdasubreceivereturnitem(@poid               varchar(255),
                                              @code               varchar(255),
                                              @barcode            varchar(255),
                                              @usersid            varchar(255),
                                              @qty                numeric,
                                              @activitytype       varchar(255), --//RECEIVE/RETURN
                                              @assignbc           varchar(255),
                                              @contractid         varchar(255)    output, --//input/output
                                              @isbarcode          varchar(255)    output,
                                              @poitemid           varchar(255)    output,
                                              @description        varchar(255)    output,
                                              @qtyordered         numeric(12, 02) output,
                                              @qtyinsession       numeric(12, 02) output,
                                              @qtyreceived        numeric(12, 02) output,
                                              @qtyreturned        numeric(12, 02) output,
                                              @qtyremaining       numeric(12, 02) output,
                                              @genericmsg         varchar(255)    output,
                                              @status             numeric         output,
                                              @msg                varchar(255)    output
)as
begin
declare @bcorderid             varchar(255),
        @orderno               varchar(255),
        @orderdesc             varchar(255),
        @dealid                varchar(255),
        @dealno                varchar(255),
        @deal                  varchar(255),
        @warehouseid           varchar(255),
        @warehouse             varchar(255),
        @vendorid              varchar(255),
        @vendor                varchar(255),
        @contractby            varchar(255),
        @linetype              varchar(255),
        @receivereturndate     datetime
        
set @status            = 0
set @isbarcode         = 'F'
set @poitemid          = ''
set @msg               = ''
set @genericmsg        = ''
set @contractby        = ''
set @qtyordered        = 0
set @qtyinsession      = 0
set @qtyreceived       = 0
set @qtyremaining      = 0     
set @receivereturndate = getdate()        

set @poid            = isnull(rtrim(@poid        ), '')      
set @code            = isnull(rtrim(@code        ), '')      
set @barcode         = isnull(rtrim(@barcode     ), '')      
set @qty             = isnull(@qty                ,  0)
set @usersid         = isnull(rtrim(@usersid     ), '')      
set @assignbc        = isnull(rtrim(@assignbc    ), 'F')   
set @contractid      = isnull(rtrim(@contractid  ), '')   
       
if (@status =  0) 
begin      
   if (@qty > 9999) 
   begin
     set @status = 1018
     set @msg    = 'Max Qty is 9,999'
   end    
end    

if (@status =  0) 
begin      
   if (@poid = '')
   begin
     set @status = 1
     set @msg    = 'Invalid PO!'
   end    
end    

if (@status =  0) 
begin      
   if (@contractid > '')
   begin
      select @contractby = dbo.getcontractcancelledby(@contractid)
      if (isnull(@contractby, '') > '')
      begin
        set @status = 1020
        set @msg    = 'Cancelled By ' + rtrim(@contractby)
      end                         
   end
end

if (@status =  0) 
begin      
   if (@contractid > '')
   begin
      select @contractby = dbo.getcontractcreatedby(@contractid)
      if (isnull(@contractby, '') > '')
      begin
        set @status = 1021
        set @msg    = rtrim(@contractby)
      end                         
   end
end

if (@status = 0)
begin
   exec applyinvmask @code, @code output, ''
   select  top 1 @description  = p.description,
                 @qtyordered   = p.qtyordered,
                 @qtyreceived  = p.qtyreceived,
                 @qtyreturned  = p.qtyreturned,
                 @qtyremaining = p.qtyremaining,
                 @poitemid     = p.masteritemid,
                 @linetype     = linetype,
                 @isbarcode    = (case 
                                    when (p.trackedby = 'BARCODE') then 'T'
                                    else                                'F'
                                  end)
   from poitemtotalview p with (nolock), 
        master          m with (nolock)
   where  p.masterid      = m.masterid
     and  p.orderid       = @poid
     and  m.masterno      = @code

   set @description  = isnull(@description  , '')
   set @isbarcode    = isnull(@isbarcode    , '')
   set @poitemid     = isnull(@poitemid     , '')
   set @qtyordered   = isnull(@qtyordered   , 0)
   set @qtyreceived  = isnull(@qtyreceived  , 0)
   set @qtyinsession = isnull(@qtyinsession , 0)

   if (@poitemid = '')
   begin
      set @status = 203
      set @msg    = 'I-Code ' + rtrim(@code) + ' Not found on PO!'  
   end
end

if (@status = 0) and (@activitytype = 'RECEIVE')
begin
   select @qtyremaining = @qtyordered - @qtyreceived
   if (@qtyremaining <= 0)
   begin
     set @status = 202
     set @msg    = 'All Items have been received!'  
   end
end
if (@status = 0) and (@activitytype = 'RETURN')
begin
   if (@qtyremaining <= 0)
   begin
     set @status = 202
     set @msg    = 'All Items have been returned!'  
   end
end


if (@status = 0)
begin
   if (@qty > @qtyordered) 
   begin
     set @status = 202
     set @msg    = 'Cannot receive/return more than ordered(' + rtrim(ltrim(str(@qtyordered))) + ')!'
   end
end


if (@status = 0) 
begin   
   if (@contractid = '') 
   begin
      if (@activitytype = 'RECEIVE')
         begin
         exec createreceivecontract @poid        = @poid       ,
                                    @usersid     = @usersid    ,
                                    @contractid  = @contractid  output
   
   
         if (@@error <> 0) 
         begin
            set @status = 1
            set @msg    = 'Receive contract not created!'  
         end
         end
      else if (@activitytype = 'RETURN')
         begin
         exec createreturncontract @poid        = @poid       ,
                                   @usersid     = @usersid    ,
                                   @contractid  = @contractid  output
   
   
         if (@@error <> 0) 
         begin
            set @status = 1
            set @msg    = 'Return contract not created!'  
         end
         end
      
      if (@status = 0) 
      begin
         update contract 
           set  forcedsuspend = 'G'
         where  contractid    = @contractid                       
     end    
   end
end
   
if (@status = 0) and (@qty > 0)
begin
   if (@barcode > '')
   begin
      select @qty = 1
   end
   if (@activitytype = 'RECEIVE')
      begin
      exec dbo.receiveitem @contractid      = @contractid,
                           @orderid         = @poid,
                           @masteritemid    = @poitemid,
                           @usersid         = @usersid,
                           @barcode         = @barcode,
                           @qty             = @qty,
                           --//@receivedatetime = @receivereturndate,
                           @status          = @status output,
                           @msg             = @msg    output
      if (@@error <> 0)
      begin
         set @status = 1
         set @msg    = 'Could not receive items'
      end
      end
   else if (@activitytype = 'RETURN')
      begin
      exec dbo.returnitem @contractid      = @contractid    ,
                          @orderid         = @poid          ,
                          @masteritemid    = @poitemid      ,
                          @usersid         = @usersid       ,
                          @barcode         = @barcode       ,
                          @qty             = @qty           ,
                          --//@returndatetime  = @receivereturndate,
                          @status          = @status        output,
                          @msg             = @msg           output
      if (@@error <> 0)
      begin
         set @status = 1
         set @msg    = 'Could not receive items'
      end
      end
end     
if (@status = 0)
begin
   select  top 1 @qtyreceived  = p.qtyreceived,
                 @qtyreturned  = p.qtyreturned,
                 @qtyremaining = p.qtyremaining
   from poitemtotalview p with (nolock)
   where p.orderid       = @poid
     and p.masteritemid  = @poitemid

   if (@activitytype = 'RECEIVE')
      begin
      select @qtyinsession = sum(ot.qty)                                                
      from   ordertran  ot with (nolock), 
             masteritem mi with (nolock)                                
      where  ot.orderid              = mi.orderid                     
        and  ot.masteritemid         = mi.masteritemid                     
        and  mi.poorderid            = @poid                            
        and  mi.pomasteritemid       = @poitemid                         
        and  ot.outreceivecontractid = @contractid
      end  
   else if (@activitytype = 'RETURN')
      begin
      set @qtyremaining = @qtyreceived - @qtyreturned
      select @qtyinsession = sum(ot.qty)                                                
      from   ordertran  ot with (nolock), 
             masteritem mi with (nolock)                                
      where  ot.orderid              = mi.orderid                     
        and  ot.masteritemid         = mi.masteritemid                     
        and  mi.poorderid            = @poid                            
        and  mi.pomasteritemid       = @poitemid                         
        and  ot.inreturncontractid   = @contractid
      end  
      
      
   set @qtyreceived  = isnull(@qtyreceived  , 0)
   set @qtyreturned  = isnull(@qtyreturned  , 0)
   set @qtyremaining = isnull(@qtyremaining , 0)
   set @qtyinsession = isnull(@qtyinsession , 0)
                   
end

if (@status > 0)
   begin
   exec getgenericerror @status, @genericmsg output
   end   
else if (@status = 0)   
   begin
   select @genericmsg = rtrim(ltrim(@activitytype)) + space(01) + 
                        ltrim(rtrim(str(@qty))) + space(01) + 
                        rtrim(ltrim(@linetype)) + space(01) + 
                        'I-Code' + space(01) + ltrim(rtrim(@code))
   end

return
end
go



--procBarcode.sql
--was giving an error from appending @status to @msg which wasn't really very informative so I changed the error message to show what statuses were expected
/*******************************************************************************
* Procedure: pdaselectpo
* Author:    eg
* Notes:     
*******************************************************************************/
if exists (select name from sysobjects where name = 'pdaselectpo')
begin
    drop procedure dbo.pdaselectpo
end   
go  
create procedure dbo.pdaselectpo(@pono            varchar(255),
                                 @potype          varchar(255), --//'RECEIVE'-Sub-Receive, 'RETURN'-Sub-Return
                                 @usersid         varchar(255), 

                                 @poid            varchar(255)     output,
                                 @podesc          varchar(255)     output,
                                 @activitytype    varchar(255)     output, --//'R'-Rental, 'S'-Sales
                                 @orderid         varchar(255)     output,
                                 @orderno         varchar(255)     output,
                                 @orderdesc       varchar(255)     output,
                                 @dealid          varchar(255)     output,
                                 @dealno          varchar(255)     output,
                                 @deal            varchar(255)     output,
                                 @vendorid        varchar(255)     output,
                                 @vendor          varchar(255)     output,
                                 @warehouseid     varchar(255)     output,
                                 @warehouse       varchar(255)     output,

                                 @status          numeric          output,
                                 @msg             varchar(255)     output

)as
begin

declare @onhold           varchar(255),
        @rental           varchar(255),
        @sales            varchar(255),
        @postatus         varchar(255),
        @userlocationid   varchar(255),
        @polocationid     varchar(255),
        @userwarehouseid  varchar(255),
        @podepartmentid	  varchar(255),
        @subrent 		     varchar(255),
        @subsale 		     varchar(255)

set @onhold           = ''
set @rental           = ''
set @sales            = ''
set @postatus         = ''
set @userlocationid   = ''
set @polocationid     = ''
set @userwarehouseid  = ''
set @podepartmentid   = ''

set @poid             = ''
set @podesc           = ''
set @orderid          = ''
set @orderno          = ''
set @orderdesc        = ''
set @dealid           = ''
set @dealno           = ''
set @deal             = ''
set @vendorid         = ''
set @vendor           = ''
set @warehouseid      = ''
set @warehouse        = ''
set @status           = 0
set @msg              = ''
set @pono             = rtrim(ltrim(upper(isnull(@pono, ''))))
--//set @activitytype     = upper(isnull(@activitytype    , ''))
set @potype           = upper(isnull(@potype          , ''))
set @usersid          = upper(isnull(@usersid         , ''))


if (@status = 0)
begin
   if (@pono = '')
   begin
      select @status = 1
      select @msg    = 'PO No cannot be blank!'
   end
end


if (@status = 0)
begin
   if (@usersid = '')
   begin
      set @status = 1
      set @msg    = 'Users ID cannot be blank!'
   end
end

if (@status = 0)
begin
   if (@potype not in ('RECEIVE', 'RETURN'))
   begin
      set @status = 1
      set @msg    = 'Invalid po type: (' + @potype + ').'
   end
end

--//if (@status = 0)
--//begin
--//   if (@activitytype not in ('R', 'S'))
--//   begin
--//      select @status = 1
--//      select @msg    = 'Invalid activity type: (' + @activitytype + ').'
--//   end
--//end

if (@status = 0)
begin
   select top 1 @userlocationid  = u.locationid,
                @userwarehouseid = u.warehouseid
   from  users u  with (nolock)
   where u.usersid = @usersid
end

if (@status = 0)
begin
   select top 1 @poid           = po.orderid  ,
                @podesc         = po.orderdesc,
                @orderno        = dbo.getpoorderno(po.orderid),
                @vendor         = (select top 1 vendor      from vendor    v  with (nolock) where v.vendorid    = po.vendorid)    ,
                @warehouse      = (select top 1 warehouse   from warehouse w  with (nolock) where w.warehouseid = po.warehouseid) ,
                @vendorid       = po.vendorid                                                                                    ,
                @warehouseid    = po.warehouseid                                                                                 ,
                @polocationid   = po.locationid,
                @podepartmentid = po.departmentid,    
                @postatus       = po.status    ,
                @subrent        = po.subrent   ,
                @subsale        = po.subsale
   from  dealorder po  with (nolock)
   where po.orderno   = @pono
     and po.ordertype = 'C'
   set @orderno = isnull(@orderno, '')

  select @orderid = dbo.getpoorderid(@poid)                                     
  if (@orderno = 'MULTI')
     begin
     set @orderid    = @orderno
     set @orderno    = @orderno
     set @orderdesc  = @orderno 
     set @dealid     = @orderno
     set @dealno     = @orderno
     set @deal       = @orderno
     end
  else if (@orderid > '')
     begin    
     select top 1 @orderid        = orderid,
                  @orderno        = o.orderno,
                  @orderdesc      = o.orderdesc, 
                  @dealid         = o.dealid,
                  @dealno         = d.dealno, 
                  @deal           = d.deal
     from dealorder o with (nolock) 
               join deal d with (nolock) on (d.dealid = o.dealid)
     where orderid = @orderid 
     end

   
--//     and po.qtyholding > 0
--//     and po.subrent = (case 
--//                         when @activitytype = 'R' then 'T'
--//                         else                          po.subrent
--//                       end)  
--//     and po.subsale = (case 
--//                         when @activitytype = 'S' then 'T'
--//                         else                          po.subsale
--//                       end)  

--//    if ((select count(*) 
--//         from   departmentaccess da
--//         where  da.departmentid = @departmentid
--//           and  da.poaccess     = 'T') > 0) and
--//        (@deaprtmentid not in (select departmentaccessid
--//                               from   departmentaccess da
--//                               where  da.departmentid = @departmentid
--//                                 and  da.poaccess     = 'T') > 0) and
     


    set @orderid           = isnull(@orderid          , '')   
    set @dealid            = isnull(@dealid           , '')   
    set @podepartmentid    = isnull(@podepartmentid     , '')   
    set @userlocationid    = isnull(@userlocationid   , '')   
    set @polocationid      = isnull(@polocationid  , '') 
    set @userwarehouseid   = isnull(@userwarehouseid  , '') 
    set @warehouseid       = isnull(@warehouseid      , '') 
    set @subrent           = isnull(@subrent          , '') 
    set @subsale           = isnull(@subsale          , '') 
    set @postatus          = rtrim(ltrim(isnull(@postatus, ''))) 
end  
    
if (@status = 0)
begin
   if (@poid = '')
   begin
      set @status = 1
      set @msg    = 'Invalid Purchase Order.'
   end
end  


if (@status = 0)
begin
   if (@userlocationid <> @polocationid)
   begin
      set @status = 1
      set @msg    = 'User and PO Locations do not match.'
   end
end

if (@status = 0)
begin
   if (@subrent = 'T') and (@subsale = 'T')
   begin
      set @status = 1
      set @msg    = 'PO ' + @pono + ' is not suported from the RentalWorks Mobile. PO has both Sub-Rentals and Sub-Sales on the same PO.'
   end
end

if (@status = 0)
begin
   if (@subrent <> 'T') and (@subsale <> 'T')
   begin
      set @status = 1
      set @msg    = 'PO ' + @pono + ' is not a Sub-Rental or Sub-Sale PO.'
   end
end


if (@status = 0)
begin
   if (@potype = 'RECEIVE') 
      begin
      if (@postatus not in ('NEW','OPEN'))
       begin
         set @status = 1
         set @msg    = 'PO Status must be NEW or OPEN.'
      end
      end
   else if (@potype = 'RETURN') 
      begin
      if (@postatus not in ('OPEN','RECEIVED','COMPLETE'))
      begin
         set @status = 1
         set @msg    = 'PO Status must be OPEN, RECEIVED, or COMPLETE.'
      end
      end

end     

if (@status = 0)
begin
   if (@poid         = '') or
      (@orderid      = '') or
      (@dealid       = '') or
      (@vendorid     = '') or
      (@warehouseid  = '') or
      (@podepartmentid = '')
   begin
      set @status = 1
      set @msg    = (case
                       when (@poid           = '') then  'Invalid PO'
                       when (@orderid        = '') then  'PO Has No Order'
                       when (@dealid         = '') then  'PO Has No Deal'
                       when (@vendorid       = '') then  'PO Has No Vendor'
                       when (@podepartmentid = '') then  'PO Has No Department'
                       when (@warehouseid    = '') then  'PO Has No Warehouse'
                     end)
   end
end


if (@status = 0)
begin
   if (@subrent = 'T') and (not exists (select *
                                        from   masteritem
                                        where  orderid = @poid
                                          and  rectype = 'R'))
   begin
      set @status = 1
      set @msg    = 'PO ' + @pono + ' does not have any PO Sub-Rent items.'
   end
end

if (@status = 0)
begin
   if (@subsale = 'T') and (not exists (select *
                                        from   masteritem
                                        where  orderid = @poid
                                          and  rectype = 'S'))
   begin
      set @status = 1
      set @msg    = 'PO ' + @pono + ' does not have any PO Sub-Sale items.'
   end
end


if (@status = 0)
begin
   set @activitytype = ''
   if (@subrent = 'T') 
   begin
      set @activitytype = @activitytype + 'R'
   end
   if (@subsale = 'T')
   begin
      set @activitytype = @activitytype + 'S'
   end
end


--//if (@status <> 0)
--//begin
--//   exec fw_raiserror @msg
--//end

return
end
go



--procBarcode.sql
-- set masteritemid = isnull(masteritemid,'') instead of setmasteritemid = ''
/************************************************************************************
* Procedure: pdastageitem
* Author:    eg
* Notes: 
*************************************************************************************/
if exists (select name from sysobjects where name = 'pdastageitem')
begin
    drop procedure dbo.pdastageitem
end   
go  
create  procedure dbo.pdastageitem(@orderid                 char(08),
                                   @code                    varchar(40),
                                   @usersid                 char(08),
                                   @qty                     numeric,
                                   @additemtoorder          char(01),
                                   @addcompletetoorder      char(01),
                                   @unstage                 char(01),     --//T=Unstage item(s)
                                   @isicode                 char(01)        output,
                                   @masterid                char(08)        output,
                                   @masteritemid            char(08)        output, --input/output
                                   @rentalitemid            char(08)        output,
                                   @masterno                char(12)        output,
                                   @description             char(40)        output,
                                   @qtyordered              numeric(09, 02) output,
                                   @qtysub                  numeric(09, 02) output,
                                   @qtystaged               numeric(09, 02) output,
                                   @qtyout                  numeric(09, 02) output,
                                   @qtyin                   numeric(09, 02) output,
                                   @qtyremaining            numeric(09, 02) output,
                                   @showadditemtoorder      char(01)        output,
                                   @showaddcompletetoorder  char(01)        output,
                                   @showunstage             char(01)        output,
                                   @genericmsg              varchar(40)     output,
                                   @status                  numeric         output,
                                   @msg                     varchar(255)    output
)as
begin
declare @errno                 numeric,
        @scandatetime          datetime,
        @bcorderid             char(08),
        @orderno               char(16),
        @orderdesc             char(30),
        @dealid                char(08),
        @dealno                varchar(20),
        @deal                  varchar(100),
        @warehouseid           char(08),
        @warehouse             char(08),
        @vendorid              char(08),
        @vendor                char(100),
        @rentalstatus          char(10),
        @statustype            char(10),
        @statusdate            datetime,
        @retiredreason         char(20),
        @isbarcode             char(01),
        @iscomplete            char(01),
        @found                 char(01),
        @autoaddtoorder        char(01),
        @addaccessories        char(01),
        @rectype               char(01),
        @hasstaged             char(01),
        @meter                 numeric(11,2),
        @location              char(30),
        @spaceid               char(08),
        @autostageacc          char(01),
        @availfor              char(01),
        @trackedby             char(10)
            
set @errno                  = 0
                            
set @scandatetime           = getdate()
set @status                 = null
set @rentalitemid           = ''
set @isicode                = 'F'
set @found                  = 'F'
set @qty                    = isnull(@qty        ,  0)
set @orderid                = isnull(@orderid    , '')      
set @code                   = isnull(rtrim(@code), '')      
set @usersid                = isnull(@usersid    , '')      
set @additemtoorder         = isnull(@additemtoorder , 'F')   
set @addcompletetoorder     = isnull(@addcompletetoorder , 'F')   
set @unstage                = isnull(@unstage    , 'F')   
set @masterid               = ''
set @masteritemid           = isnull(@masteritemid, '')
set @rentalitemid           = ''
set @showadditemtoorder     = 'F'
set @showaddcompletetoorder = 'F'
set @showunstage            = 'F'
set @qtystaged              = 0
set @msg                    = ''
set @genericmsg             = ''
set @addaccessories         = 'F'
set @rectype                = ''
set @hasstaged              = 'F'
set @autostageacc           = 'T'

if (@qty > 9999) 
begin
  set @errno  = 1018
  set @status = 1018
  set @msg    = 'Max Qty is 9,999'
end    

if (@errno = 0) and (@unstage = 'T') 
   begin
   exec advancedmovebarcode @orderid    = @orderid      ,
                            @barcode    = @code         ,
                            @contractid = ''            ,
                            @usersid    = @usersid      ,
                            @movemode   = 2             ,  --//@movemode  0: to contract, 1: to staged, 2: staged to inventory, 3: contract to inventory, 4: either to inventory
                            @status     = @status       output,
                            @msg        = @msg          output
   end
else if (@errno = 0)
   begin   
   if (@errno = 0) and (@additemtoorder <> 'T')
   begin
      select top 1 @additemtoorder = d.autoaddtoorder
      from   deptloc   d with (nolock), 
             dealorder o with (nolock)
      where  d.departmentid   = o.departmentid 
        and  d.locationid     = o.locationid
        and  o.orderid        = @orderid

      set @additemtoorder = isnull(@additemtoorder, 'F')  
   end

   select top 1 @autostageacc = (case
                                   when accstagingopt = 'AUTO' then 'T'
                                   else                             'F'
                                 end)
   from warehouse w with (nolock), 
        users     u with (nolock)
   where w.warehouseid = u.warehouseid
     and u.usersid     = @usersid

   exec stageitem @orderid                 = @orderid          ,
                  @masteritemid            = @masteritemid     output,  --// input/output
                  @code                    = @code             ,
                  @vendorid                = @vendorid         ,
                  @qty                     = @qty              ,
                  @meter                   = @meter            ,
                  @location                = @location         ,
                  @spaceid                 = @spaceid          ,
                  @additemtoorder          = @additemtoorder  ,
                  @addcompletetoorder      = @addcompletetoorder  ,
                  @overridereservation     = 'F'               ,
                  @transferrepair          = 'F'               ,
                  @autostageacc            = @autostageacc     ,
                  @usersid                 = @usersid          ,
                  @masterid                = @masterid         output,
                  @rentalitemid            = @rentalitemid     output,
                  @qtystaged               = @qtystaged        output,
                  @status                  = @status           output,
                  @msg                     = @msg              output

   set @masterid               = isnull(@masterid              , '')
   set @rentalitemid           = isnull(@rentalitemid          , '')
   set @qtystaged              = isnull(@qtystaged             , 00)
   set @showadditemtoorder     = isnull(@showadditemtoorder    , '')
   set @showaddcompletetoorder = isnull(@showaddcompletetoorder, '')
   set @showunstage            = isnull(@showunstage           , '')
   set @status                 = isnull(@status                , 00)
   set @msg                    = isnull(@msg                   , '')

   --//STAGING_STATUS_ITEM_STAGED_ON_THIS_ORDER = 106;
   --//STAGING_STATUS_QTY_EXCEEDS_ORDER_CAN_ADD_ITEM = 207;
   --//STAGING_STATUS_QTY_EXCEEDS_ORDER_CAN_ADD_ITEM_OR_COMPLETE = 208;
   --//STAGING_STATUS_ITEM_NOT_ON_ORDER_CAN_ADD_ITEM = 209;
   --//STAGING_STATUS_ITEM_NOT_ON_ORDER_CAN_ADD_ITEM_OR_COMPLETE = 210;
   

   select @showadditemtoorder      = (case 
                                         when (@status in (207,209, 208, 210)) then 'T'
                                         else                                       'F'
                                      end)   
   select @showaddcompletetoorder  = (case 
                                         when (@status in (208, 210)) then 'T'
                                         else                              'F'
                                      end)   
   select @showunstage             = (case 
                                         when (@status in (106)) then 'T'
                                         else                         'F'
                                      end)   

   if (@masterid > '') and (@masteritemid = '')
   begin
      select top 1 @masteritemid = masteritemid 
      from masteritem  with (nolock)
      where orderid  = @orderid
        and masterid = @masterid
   end

   exec getscannediteminfo @orderid      = @orderid             ,
                           @masteritemid = @masteritemid        ,
                           @rentalitemid = @rentalitemid        ,
                           @code         = @code          output,
                           @masterno     = @masterno      output,
                           @description  = @description   output,
                           @qtyordered   = @qtyordered    output,
                           @qtysub       = @qtysub        output,
                           @qtystaged    = @qtystaged     output,
                           @qtyout       = @qtyout        output,
                           @qtyin        = @qtyin         output,
                           @qtyremaining = @qtyremaining  output
   
   set @code          = isnull(rtrim(@code)  , '')
   set @masterno      = isnull(@masterno     , '')
   set @description   = isnull(@description  , '')
   set @qtyordered    = isnull(@qtyordered   , 00)
   set @qtysub        = isnull(@qtysub       , 00)
   set @qtystaged     = isnull(@qtystaged    , 00)
   set @qtyout        = isnull(@qtyout       , 00)
   set @qtyin         = isnull(@qtyin        , 00)
   set @qtyremaining  = isnull(@qtyremaining , 00)

   select top 1 @isicode   = (case 
                                when (trackedby = 'BARCODE')  then 'F'
                                when (trackedby = 'SERIALNO') then 'F'
                                else                               'T'
                              end),
                @trackedby = trackedby,
                @availfor  = availfor
   from   master  with (nolock)
   where  masterid = @masterid

   if (@masteritemid = '') and (@masterid > '')
   begin
      select top 1 @masterno      = masterno,
                   @description   = master
      from   master  with (nolock)
      where  masterid = @masterid
   end
                                          
   select @isicode = isnull(@isicode, '')
    
   if ((@isicode <> 'T')  and (@status = 106)) 
      begin
      set @showunstage = 'T'
      end
   else if ((@isicode <> 'T')  and (@status = 107)) 
      begin
      set @isicode = 'T'
      end
   else if ((@isicode = 'T')  and (@status = 107) and (@qty = 0)) 
      begin
      set @status = 0
      set @msg    = ''
      end
   end

if (@status > 0)
   begin
   exec getgenericerror @status, @genericmsg output
   end   
else if (@status = 0)   
   begin
   if (@trackedby in ('BARCODE', 'SERIALNO'))
   begin
      set @qty = 1
   end
   select @genericmsg = 'Staged ' + ltrim(rtrim(str(@qty))) + ' for ' +
                        (case @availfor 
                            when 'R' then 'Rental I-Code '
                            when 'S' then 'Sales I-Code '
                            else          'I-Code '
                         end) + ltrim(rtrim(@masterno))
   end

set @isicode                = isnull(@isicode          , '')
set @masterid               = isnull(@masterid         , '')
set @masteritemid           = isnull(@masteritemid     , '')
set @rentalitemid           = isnull(@rentalitemid     , '')
set @masterno               = isnull(@masterno         , '')
set @description            = isnull(@description      , '')
set @qtyordered             = isnull(@qtyordered       , 00)
set @qtysub                 = isnull(@qtysub           , 00)
set @qtystaged              = isnull(@qtystaged        , 00)
set @qtyout                 = isnull(@qtyout           , 00)
set @qtyin                  = isnull(@qtyin            , 00)
set @qtyremaining           = isnull(@qtyremaining     , 00)
set @showadditemtoorder     = isnull(@showadditemtoorder   , '')
set @showaddcompletetoorder = isnull(@showaddcompletetoorder   , '')
set @showunstage            = isnull(@showunstage      , '')
set @genericmsg             = isnull(@genericmsg       , '')
set @status                 = isnull(@status           , 01)
set @msg                    = isnull(@msg              , '')
    
if (@errno <> 0)
begin
   exec fw_raiserror @msg
end
return
end
go

--procBarcode.sql
-- if (isnull(@masteritemid, '') = '') begin set @masteritemid = null end
/************************************************************************************
* Procedure: pdacheckingetnonbcinfo
* Author:    eg
* Notes: 
*************************************************************************************/
if exists (select * from sysobjects where name = 'pdacheckingetnonbcinfo')
begin
   drop procedure dbo.pdacheckingetnonbcinfo
end
go
create procedure dbo.pdacheckingetnonbcinfo(@code          varchar(40),
                                            @incontractid  char(08), 
                                            @usersid       char(08),
                                            @orderid       char(08),
                                            @dealid        char(08),
                                            @departmentid  char(08),
                                            @vendorid      char(08) = '',
                                            @itemorderid   char(08)     output,
                                            @masterid      char(08)     output,
                                            @masteritemid  char(08)     output, --//input/output 
                                            @warehouseid   char(08)     output,
                                            @masterno      char(12)     output,
                                            @description   char(40)     output,
                                            @issales       char(01)     output,
                                            @status        numeric      output,
                                            @msg           varchar(255) output
) as                                   
begin
declare @found           char(01),
        @applied         char(01),
        @ordertype       varchar(15),
        @fromwarehouseid char(08),
        @availfor        char(01),
                     @trackedby      char(10)

set @code           = isnull(rtrim(@code) , '')         
set @incontractid   = isnull(@incontractid, '')
set @usersid        = isnull(@usersid     , '')
set @orderid        = isnull(@orderid     , '')  
set @dealid         = isnull(@dealid      , '')  
set @departmentid   = isnull(@departmentid, '')
set @vendorid       = isnull(@vendorid    , '')
if (isnull(@masteritemid, '') = '') begin set @masteritemid = null end
set @ordertype      = 'O'
set @status         = 0
set @msg                    = ''
set @found          = 'F'
set @issales        = 'F'
set @trackedby      = ''
set @itemorderid    = ''

--//print '01'
exec dbo.applyinvmask @masterno    = @code           ,
                      @newmasterno = @masterno output,
                      @applied     = @applied  output

select top 1 @warehouseid = warehouseid
from   users with (nolock)
where  usersid = @usersid

set @warehouseid = isnull(@warehouseid, '')

if (@found <> 'T')
begin
   select top 1 @masterid    = m.masterid,
                @description = m.master,
                @availfor    = availfor,
                @trackedby   = m.trackedby
   from   master m with (nolock)
             join masterwh mw with (nolock) on (m.masterid = mw.masterid)
   where  m.masterno     = @masterno
     and  mw.warehouseid = @warehouseid

--//print '02'
                                
   set @masterid     = isnull(@masterid   , '')
   set @description  = isnull(@description, '')
   set @availfor     = isnull(@availfor   , '')
   set @trackedby    = isnull(@trackedby  , '')
end           

if (@trackedby = 'BARCODE') and (@vendorid = '')
   begin
   set @status = 301
   set @msg    = 'I-Code ' + ltrim(rtrim(@masterno)) + ' is a Bar Coded Item.'
   end
else if (@masterid = '')
   begin
   set @status = 301
   set @msg    = 'I-Code ' + ltrim(rtrim(@masterno)) + ' Not Found in Inventory!'
   end
else if (@masterid > '') and (@orderid  = '') and 
        ((@incontractid = '') or (not exists (select contractid 
                                              from   ordercontract with (nolock)            
                                              where  contractid = @incontractid)))
   begin
--//print '03'
   if (@availfor = 'S')       
      begin
      set @status = 1
      set @msg    = 'Cannot Scan a Sales I-Code Without Scanning a Bar Code First'
      end
   else if (@availfor = 'R')  and (@dealid = '')
      begin
      set @status = 1
      set @msg    = 'Cannot Scan a Rental I-Code Without Scanning a Bar Code First'
      end
   end
if (@masterid > '') and (@status = 0)
begin
   set @found    = 'T'
   set @status   =  0
   set @issales  = (case @availfor 
                      when 'R' then 'F'
                      when 'S' then 'T'
                      else          ''
                    end)
   if (@orderid > '')
   begin
      select top 1 @ordertype       = ordertype,
                   @fromwarehouseid = fromwarehouseid
      from   dealorder with (nolock)
      where  orderid   = @orderid
      set @ordertype = isnull(@ordertype, 'O')
   end
--//print '04'
               
   select top 1 @masteritemid = masteritemid,
                @itemorderid  = orderid 
   from   ordertranview  ot with (nolock)
   where  rectype      = @availfor
     and  masterid     = @masterid
     and  masteritemid = isnull(@masteritemid, masteritemid)  
     and  dealid       = (case
                              when @ordertype = 'O' then @dealid
                              when @ordertype = 'T' then ''
                              when @ordertype = 'P' then ''
                            end)  
     and  warehouseid    = (case
                              when @ordertype = 'O' then @warehouseid
                              when @ordertype = 'T' then @fromwarehouseid
                              when @ordertype = 'P' then @warehouseid
                            end)  
     and itemstatus   = 'O'
     and cinid        = ''
     and vendorid     = (case 
                            when (@vendorid > '')  then @vendorid
                            when (availfor  = 'S') then ''
                            else                        vendorid
                         end)   
     and ((orderid     = (case 
                            when (@orderid      > '') then @orderid
                            else                           orderid
                          end)) 
          or   
          (orderid in (select orderid            
                                                                                        from   ordercontract with (nolock)           
                                                                                        where  contractid = @incontractid)))
   set @masteritemid = isnull(@masteritemid, '')
--//print '05'
                                      
   if (@masteritemid = '')
   begin
       set @status = 1001
       set @msg    = (case @availfor 
                        when 'R' then 'Rental ' 
                        when 'S' then 'Sales ' 
                        else          '' 
                      end) + 'I-Code ' + ltrim(rtrim(@masterno)) + ' Not Checked-Out.'
   end                    
end             
   
end        
return
go                               

-- procBarcode.sql
-- modified to return vendorid, vendor
/************************************************************************************
* Procedure: pdacheckinitem
* Author:    eg
* Notes: 
*************************************************************************************/
if exists (select name from sysobjects where name = 'pdacheckinitem')
begin
    drop procedure dbo.pdacheckinitem
end   
go
create  procedure dbo.pdacheckinitem(@code           varchar(40),
                                     @usersid        char(08),
                                     @qty            numeric ,
                                     @neworderaction char(01),            --//Y = yes check-In, S = Swap else do nothing
                                     @moduletype     char(01),            --//O = Order       , T = Transfer,     P = Package Truck 
                                     @checkinmode    char(01),            --//M = Multi Order , O = Single Order, D = Deal, S = Session
                                     @aisle          varchar(255)    = null,
                                     @shelf          varchar(255)    = null,
                                     @vendorid       char(08)        = '' output, --//input/output
                                     @vendor         char(100)       = '' output, 
                                     @incontractid   char(08)        = '' output, --//input/output
                                     @orderid        char(08)        = '' output, --//input/output
                                     @dealid         char(08)        = '' output, --//input/output
                                     @departmentid   char(08)        = '' output, --//input/output

                                     @itemorderid    char(08)        = '' output,
                                     @masteritemid   char(08)        = '' output,
                                     @masterid       char(08)        = '' output,
                                     @warehouseid    char(08)        = '' output,
                                     @ordertranid    integer         = 0  output,
                                     @internalchar   char(01)        = '' output,
                                     @rentalitemid   char(08)        = '' output,
                                     @isicode        char(01)        = '' output,
                                     @orderno        char(16)        = '' output,
                                     @masterno       char(12)        = '' output,
                                     @description    char(40)        = '' output,
                                     @allowswap      char(01)        = '' output,
                                     @qtyordered     numeric(09, 02) = 0  output,
                                     @subqty         numeric(09, 02) = 0  output,
                                     @stillout       numeric(09, 02) = 0  output,
                                     @totalin        numeric(09, 02) = 0  output,
                                     @sessionin      numeric(09, 02) = 0  output,
                                     @genericmsg     varchar(40)     = '' output,
                                     @status         numeric         = 0  output,
                                     @msg            varchar(255)    = '' output
)as                                     
begin
declare @errno         numeric,
        @errmsg        varchar(255),
        @found         char(01)  ,
        @scandatetime  datetime  ,
        @orderdesc     char(30)  ,
        @exchange      char(01)  ,
        @issales       char(01)  ,
        @contractby    varchar(255),
        @itemdealid    char(08),
        @tmpvendorid   char(08)
                                     
set @errno          = 0
set @found          = 'F'
set @scandatetime   = getdate()
set @rentalitemid   = ''
set @exchange       = 'F'
set @isicode        = 'F'
set @code           = isnull(rtrim(@code)     , '')
set @usersid        = isnull(@usersid         , '')
set @qty            = isnull(@qty             , 0 )
set @neworderaction = isnull(@neworderaction  , '')
set @moduletype     = isnull(@moduletype      , '')
set @checkinmode    = isnull(@checkinmode     , '')
set @incontractid   = isnull(@incontractid    , '')
set @orderid        = isnull(@orderid         , '')
set @dealid         = isnull(@dealid          , '')
set @departmentid   = isnull(@departmentid    , '')
set @aisle          = isnull(@aisle           , '') 
set @shelf          = isnull(@shelf           , '') 
set @status         = 1  
set @issales        = 'F'
set @allowswap      = 'F'
set @contractby     = ''
set @itemdealid     = @dealid
set @tmpvendorid    = @vendorid

if (@moduletype = 'T') or (@checkinmode = 'O')
begin
   set @itemorderid = @orderid
end
       
if (@qty > 9999) 
begin
  set @status = 1018
  set @msg    = 'Max Qty is 9,999'
end       

if (@status =  1) and (@incontractid > '')
begin
   select @contractby = dbo.getcontractcancelledby(@incontractid)
   if (isnull(@contractby, '') > '')
   begin
     set @status = 1020
     set @msg    = 'Cancelled By ' + rtrim(@contractby)
   end                         
end

if (@status =  1) and (@incontractid > '')
begin
      select @contractby = dbo.getcontractcreatedby(@incontractid)
      if (isnull(@contractby, '') > '')
      begin
        set @status = 1021
        set @msg    = rtrim(@contractby)
      end                         
end

if (@status = 1) and (@errno = 0)   --//is bc item?
begin
   set @itemdealid = @dealid
   exec checkingetiteminfo @barcode       = @code                 ,
                           @incontractid  = @incontractid         ,               
                           @usersid       = @usersid              ,
                           @bctype        = @moduletype           , --//O=Order, T=Transfer, P=Package Truck
                           @orderid       = @itemorderid    output, --//input/output
                           @dealid        = @itemdealid     output, --//input/output
                           @departmentid  = @departmentid   output, --//input/output
                           @masterid      = @masterid       output,
                           @warehouseid   = @warehouseid    output,
                           @ordertranid   = @ordertranid    output,
                           @internalchar  = @internalchar   output,
                           @masteritemid  = @masteritemid   output,
                           @rentalitemid  = @rentalitemid   output,
                           @vendorid      = @vendorid       output,
                           @masterno      = @masterno       output,
                           @description   = @description    output,
                           @vendor        = @vendor         output,
                           @orderno       = @orderno        output,
                           @orderdesc     = @orderdesc      output,
                           @msg           = @msg            output,
                           @status        = @status         output

   set @itemorderid  = isnull(@itemorderid , '')
   set @itemdealid   = isnull(@itemdealid  , '')
   set @departmentid = isnull(@departmentid, '')
   set @internalchar = isnull(@internalchar, '')

   set @dealid = (case 
                     when (@itemdealid = '') then @dealid
                     else                         @itemdealid                       
                   end)  
                                   
   if (@departmentid = '') and 
      (@usersid      > '') 
   begin
      select top 1 @departmentid = (case defaultdepttype 
                                      when 'R'  then rentaldepartmentid
                                      when 'S'  then salesdepartmentid
                                      when 'P'  then partsdepartmentid
                                      when 'L'  then labordepartmentid
                                      when 'M'  then miscdepartmentid
                                      when 'SP' then spacedepartmentid
                                   end)   
      from   users  with (nolock)
      where  usersid = @usersid
      
      set @departmentid = isnull(@departmentid, '')
   end
           
   if (@status = 0)     --//no errors
   begin
      set @found = 'T' 
   end

   if (@status in (1005, 1019))    --//on new order                  
   begin
      set @found  = (case 
                       when @neworderaction = 'Y' then 'T'  --//check item in (single -> multiorder)
                       when @neworderaction = 'S' then 'T'  --//swap item
                       when (@incontractid   = '') and (@orderid     = @itemorderid) then 'T'
                       when (@incontractid   = '') and (@checkinmode = 'M'         ) then 'T'
                       else                             @found
                     end)
      set @status = (case 
                       when @found = 'T'  then 0      
                       else                    @status
                     end)

      select @allowswap = dbo.funccheckincanswap(@masterid   ,
                                                 @code    ,
                                                 @status,
                                                 @dealid ,
                                                 @incontractid )
   end
   
   if (@found = 'T') and (@status = 0) 
   begin   
      if (@incontractid = '') 
      begin
         exec createincontract @orderid      = @orderid  ,
                               @dealid       = @dealid       ,   
                               @departmentid = @departmentid ,
                               @usersid      = @usersid      ,
                               @contractid   = @incontractid output
         update contract 
           set  forcedsuspend = 'G' --//created from guns
         where  contractid    = @incontractid                       
      end

      if (@neworderaction = 'S') --//swap
      begin
         set @exchange = 'T'
      end
      exec checkinbc @contractid    = @incontractid ,
                     @orderid       = @itemorderid  ,            
                     @masteritemid  = @masteritemid ,            
                     @ordertranid   = @ordertranid  ,
                     @internalchar  = @internalchar ,
                     @vendorid      = @vendorid     ,
                     @usersid       = @usersid      ,
                     @exchange      = @exchange     ,
                     @spaceid       = ''            ,
                     @aisle         = @aisle        ,
                     @shelf         = @shelf
      if (@@error <> 0)
         begin
         set @status = 1
         end
      else
         begin
         set @status     = 0
         set @genericmsg = 'BAR CODE ' + ltrim(rtrim(@code)) + ' CHECKED-IN'
         end    
   end
end

if (@status in (1000, 1)) --// bc not found in inventory 
begin
      exec dbo.pdacheckingetnonbcinfo @code          = @code          ,
                                      @incontractid  = @incontractid  , 
                                      @usersid       = @usersid       ,              
                                      @orderid       = @orderid       ,
                                      @dealid        = @dealid        ,
                                      @departmentid  = @departmentid  ,
                                      @vendorid      = @tmpvendorid   ,
                                      @itemorderid   = @itemorderid   output,
                                      @masterid      = @masterid      output,
                                      @masteritemid  = @masteritemid  output,
                                      @warehouseid   = @warehouseid   output,
                                      @masterno      = @masterno      output,
                                      @description   = @description   output,
                                      @issales       = @issales       output,
                                      @status        = @status        output,
                                      @msg           = @msg           output
         
      if (@masterid  > '')
      begin
         set @isicode = 'T'
         set @found   = 'T'
      end
      if (@status = 0) and (@qty > 0)
      begin       
         if (@incontractid = '') 
         begin
            exec createincontract @orderid      = @orderid          ,
                                  @dealid       = @dealid           ,   
                                  @departmentid = @departmentid     ,
                                  @usersid      = @usersid          ,
                                  @contractid   = @incontractid output
            update contract 
              set  forcedsuspend = 'G'
            where  contractid    = @incontractid                       
         end   
         if (@issales = 'T') or
            (@orderid > '') 
            begin
            exec checkinqty @contractid   = @incontractid,
                            @orderid      = @orderid,
                            @usersid      = @usersid,
                            @vendorid     = @tmpvendorid,
                            @masteritemid = @masteritemid,
                            @aisle        = @aisle,
                            @shelf        = @shelf,
                            @qty          = @qty
            end
         else 
            begin   
            exec insertunassigned @contractid   = @incontractid ,
                                  @masterid     = @masterid     ,
                                  @description  = @description  ,
                                  @warehouseid  = @warehouseid  ,
                                  @vendorid     = @tmpvendorid     ,
                                  @usersid      = @usersid      ,
                                  @aisle        = @aisle        ,
                                  @shelf        = @shelf        ,
                                  @applyqty     = @qty   
            end                     
      end                                  
end      

exec dbo.checkingetcounts @incontractid = @incontractid , 
                          @orderid      = @itemorderid  , 
                          @masterid     = @masterid     ,  
                          @masteritemid = @masteritemid , 
                          @warehouseid  = @warehouseid  ,
                          @vendorid     = @vendorid     ,
                          @qtyordered   = @qtyordered   output,
                          @subqty       = @subqty       output,
                          @stillout     = @stillout     output,
                          @totalin      = @totalin      output,
                          @sessionin    = @sessionin    output
                      
if (@itemorderid > '')
begin
   select top 1 @orderno = orderno
   from   dealorder with (nolock) 
   where  orderid = @itemorderid
   
   set @orderno = isnull(@orderno, '')
end                             
                    
if (@status > 0) 
begin
   exec getgenericerror @status, @genericmsg output
end   

if (@status = 1005) 
begin
   select top 1 @dealid = dealid
   from   dealorder o with (nolock)
              join location  l with (nolock) on (o.locationid   = l.locationid)
   where o.orderid      = @orderid       

   set @dealid       = isnull(@dealid      , '')
   set @internalchar = isnull(@internalchar, '')
end

set @status        = isnull(@status      , 01)
set @masterno      = isnull(@masterno    , '')
set @description   = isnull(@description , '')
set @qtyordered    = isnull(@qtyordered  , 00)
set @subqty        = isnull(@subqty      , 00)
set @stillout      = isnull(@stillout    , 00)
set @totalin       = isnull(@totalin     , 00)
set @sessionin     = isnull(@sessionin   , 00)
set @genericmsg    = isnull(@genericmsg  , '')
set @itemorderid   = isnull(@itemorderid , '')
set @vendorid      = isnull(@vendorid , '')
if ((@vendorid <> '') and (isnull(@vendor, '') = ''))
begin
    select top 1 @vendor = vendor 
    from vendor with (nolock) 
    where vendorid = @vendorid
end
set @vendor        = isnull(@vendor , '')
                                            
if (@errno <> 0)
begin
   exec fw_raiserror @msg
end
return
end
go

-- procweb.sql
-- modified to return vendorid, vendor
/*******************************************************************************
* Procedure: webcheckinitem
* Author:    mv
*******************************************************************************/
if exists (select name from sysobjects where name = 'webcheckinitem')
begin
    drop procedure dbo.webcheckinitem
end
go
create procedure dbo.webcheckinitem(@code           varchar(40),  
                                    @usersid        char(08),  
                                    @qty            numeric ,  
                                    @neworderaction char(01),            --//Y = yes check-In, S = Swap else do nothing  
                                    @moduletype     char(01),            --//O = Order       , T = Transfer,     P = Package Truck  
                                    @checkinmode    char(01),            --//M = Multi Order , O = Single Order, D = Deal, S = Session  
                                    @aisle          varchar(255)= null,  
                                    @shelf          varchar(255)= null,  
                                    @vendorid       char(08)        = '' output, --//input/output  
                                    @vendor         char(100)       = '' output,
                                    @incontractid   char(08)        = '' output, --//input/output  
                                    @orderid        char(08)        = '' output, --//input/output  
                                    @dealid         char(08)        = '' output, --//input/output  
                                    @departmentid   char(08)        = '' output, --//input/output  
                                    @itemorderid    char(08)        = '' output,
                                    @masteritemid   char(08)        = '' output,
                                    @masterid       char(08)        = '' output,
                                    @warehouseid    char(08)        = '' output,
                                    @ordertranid    integer         = 0  output,
                                    @internalchar   char(01)        = '' output,
                                    @rentalitemid   char(08)        = '' output,
                                    @isicode        char(01)        = '' output,
                                    @orderno        char(16)        = '' output,
                                    @masterno       char(12)        = '' output,
                                    @description    char(40)        = '' output,
                                    @allowswap      char(01)        = '' output,
                                    @qtyordered     numeric(09, 02) = 0  output,
                                    @subqty         numeric(09, 02) = 0  output,
                                    @stillout       numeric(09, 02) = 0  output,
                                    @totalin        numeric(09, 02) = 0  output,
                                    @sessionin      numeric(09, 02) = 0  output,
                                    @genericmsg     varchar(40)     = '' output,
                                    @status         numeric         = 0  output,
                                    @msg            varchar(255)    = '' output
)as
begin
exec dbo.pdacheckinitem @code           = @code,
                        @usersid        = @usersid,
                        @qty            = @qty,
                        @neworderaction = @neworderaction,         --//Y = yes check-In, S = Swap else do nothing  
                        @moduletype     = @moduletype,             --//O = Order       , T = Transfer,     P = Package Truck  
                        @checkinmode    = @checkinmode,            --//M = Multi Order , O = Single Order, D = Deal, S = Session
                        @aisle          = @aisle,           
                        @shelf          = @shelf,           
                        @vendorid       = @vendorid        output, --//input/output
                        @vendor         = @vendor          output, 
                        @incontractid   = @incontractid    output, --//input/output  
                        @orderid        = @orderid         output, --//input/output  
                        @dealid         = @dealid          output, --//input/output  
                        @departmentid   = @departmentid    output, --//input/output  
                        @itemorderid    = @itemorderid     output,
                        @masteritemid   = @masteritemid    output,
                        @masterid       = @masterid        output,
                        @warehouseid    = @warehouseid     output,
                        @ordertranid    = @ordertranid     output,
                        @internalchar   = @internalchar    output,
                        @rentalitemid   = @rentalitemid    output,
                        @isicode        = @isicode         output,
                        @orderno        = @orderno         output,
                        @masterno       = @masterno        output,
                        @description    = @description     output,
                        @allowswap      = @allowswap       output,
                        @qtyordered     = @qtyordered      output,
                        @subqty         = @subqty          output,
                        @stillout       = @stillout        output,
                        @totalin        = @totalin         output,
                        @sessionin      = @sessionin       output,
                        @genericmsg     = @genericmsg      output,
                        @status         = @status          output,
                        @msg            = @msg             output
return
end
go

-- procCheckIn.sql
-- set masteritemid = isnull(masteritemid,'') instead of set masteritemid = ''
/*******************************************************************************
* Procedure: checkingetiteminfo
* Author:    eg
*******************************************************************************/
if exists (select * from sysobjects where name = 'checkingetiteminfo')
begin
   drop procedure dbo.checkingetiteminfo
end
go
create procedure dbo.checkingetiteminfo(@barcode       varchar(40),           --// can be barcode, rfid, serial number, or rentalitemid
                                        @incontractid  char(08),
                                        @usersid       char(08), 
                                        @bctype        char(01),              --//O=Order, T=Transfer, P=Package Truck
                                        @orderid       char(08)     output,   --//input/output
                                        @dealid        char(08)     output,   --//input/output
                                        @departmentid  char(08)     output,   --//input/output
                                        @masterid      char(08)     output,
                                        @warehouseid   char(08)     output,
                                        @ordertranid   int          output,
                                        @internalchar  char(01)     output,
                                        @masteritemid  char(08)     output,
                                        @rentalitemid  char(08)     output,   --//input/output
                                        @vendorid      char(08)     output,
                                        @masterno      char(12)     output,
                                        @description   char(40)     output,
                                        @vendor        char(100)    output,
                                        @orderno       varchar(16)  output,
                                        @orderdesc     varchar(30)  output,
                                        @msg           varchar(255) output,
                                        @status        numeric      output
                                        ) 
as    
begin
declare @userswarehouseid    char(08),
        @dealno              varchar(20),
        @deal                varchar(100),
        @statustype          char(10),
        @iscomplete          char(01),
        @found               char(01),
        @itemstatus          char(01),
        @bcorderid           char(08),
        @bcdealid            char(08),
        @bcdepartmentid      char(08),
        @ordertype           varchar(15),
        @outcontractid       char(08),
        @trackedby           char(08),
        @hasquiklocate       char(01),
        @hasdepartmentfilter char(01),
        @ispending           char(01),
        @issub               char(01),
        @codetype            char(20),
        @usersrentaldepartmentid char(08),
        @transferfromwarehouseid char(08),
        @flag                    char(01),
        @suspendedmsg            varchar(255),
        @suspendedincontractid   char(08),
        @suspendedinusersid               char(08),
        @suspendeddealid   char(08)
        

set @barcode           = ltrim(rtrim((isnull(@barcode, ''))))
set @orderid           = isnull(@orderid     , '')
set @dealid            = isnull(@dealid      , '')
set @departmentid      = isnull(@departmentid, '')
set @bctype            = isnull(@bctype      , 'O')
set @masterid          = ''
set @warehouseid       = ''
set @ordertranid       = null
set @masteritemid      = isnull(@masteritemid, '')
set @vendorid          = isnull(@vendorid, '')
set @masterno          = ''
set @description       = ''
set @vendor            = ''
set @orderno           = ''
set @orderdesc         = ''
set @status            = null
set @msg               = ''
set @bcorderid         = ''
set @bcdealid          = ''
set @bcdepartmentid    = ''
set @found             = 'F'
set @trackedby         = ''
set @hasquiklocate     = ''
set @ispending         = 'F'
set @issub             = 'F'
set @ordertranid       = 0
set @codetype          = ''
set @hasdepartmentfilter      = 'F'
set @transferfromwarehouseid  = ''
if (isnull(@rentalitemid, '') = '') begin set @rentalitemid = null end


--//if (@barcode = '')
if (@barcode = '') and (@rentalitemid = '') --//jh 09/12/2012 CAS-10045-DCF1
begin
   set @status = 1000
   set @msg    = 'No Code scanned.'
end

if (@status is null)
begin
   if (dbo.hasappoption('quiklocate') = 'T') and (isnull((select top 1 nocrosswhcheckin
                                                          from   syscontrol sc with (nolock)), '') <> 'T')   
   begin
      set @hasquiklocate  = 'T'
   end   
   if (dbo.hasappoption('department filter') = 'T')
   begin
      set @hasdepartmentfilter  = 'T'
   end   

   select top 1 @userswarehouseid        = warehouseid,
                @usersrentaldepartmentid = rentaldepartmentid
   from   users with (nolock)
   where  usersid = @usersid
   set @userswarehouseid        = isnull(@userswarehouseid, '')
   set @usersrentaldepartmentid = isnull(@usersrentaldepartmentid, '')

   declare @tmpdepartmentids table (inventorydepartmentid char(08))
   
   insert into @tmpdepartmentids(inventorydepartmentid)  --//eg 3/14/11 CAS-08049-QH97 
   values ('')
   if (@hasdepartmentfilter = 'T')  --//eg 3/3/11 CAS-08024-YHK5
      begin
      insert into @tmpdepartmentids(inventorydepartmentid)
      select da.inventorydepartmentid                         
       from  inventorydepartmentaccess da  with (nolock)      
       where da.departmentid = @usersrentaldepartmentid
      end
   else
      begin
      insert into @tmpdepartmentids(inventorydepartmentid)
      select inventorydepartmentid                         
       from  inventorydepartment with (nolock)      
      end
end          

if (@status is null)
begin
   if (isnull(@rentalitemid, '') = '')
      begin 
      select top 1 @codetype = (case @barcode
                                   when ri.barcode      then 'BC'
                                   when ri.rfid         then 'RFID'
                                   when ri.mfgserial    then 'SERIAL'
                                   else                   ''
                                end),
                    @rentalitemid = ri.rentalitemid,
                    @masterid     = ri.masterid               
      from   rentalitemview ri with (nolock)
      where (
             ((ri.barcode      = @barcode) and (ri.trackedby = 'BARCODE' )) or     --//eg 10/25/10 CAS-7634-ZYKM
             ((ri.rfid         = @barcode) and (ri.trackedby = 'RFID'    )) or
             ((ri.mfgserial    = @barcode) and (ri.trackedby = 'SERIALNO')) 
             --//or ((ri.rentalitemid = @barcode))  --//jh 07/03/2012
             )
         and ri.inventorydepartmentid in (select inventorydepartmentid from @tmpdepartmentids)       
      order by ri.trackedby
      end
   else 
      begin   
      select top 1 @codetype = 'RENTALITEMID',
                   @rentalitemid = ri.rentalitemid,
                   @masterid     = ri.masterid               
      from   rentalitemview ri with (nolock)
      where ri.rentalitemid = @rentalitemid
        and ri.inventorydepartmentid in (select inventorydepartmentid from @tmpdepartmentids)       
      order by ri.trackedby
      end

   set @rentalitemid = isnull(@rentalitemid, '')
   set @masterid     = isnull(@masterid    , '')
   set @codetype     = isnull(@codetype    , '')

   if (@codetype = '')
      begin
      if (not exists (select *
                       from  dealorder o  with (nolock),
                             ordertran ot with (nolock)
                       where ot.orderid    = o.orderid
                         and ot.barcode    = @barcode
                         and ot.vendorid   > ''
                         and ot.itemstatus = (case @bctype
                                                when 'P' then 'O'
                                                else          ot.itemstatus
                                              end)
                         and o.ordertype in ('O', 'T', 'P')))
      begin   
         set @found = 'F'
         set @status = 1000
         set @msg    = rtrim(ltrim(@barcode)) + ' Not Found in Inventory.'
      end
      end
   else 
      begin
      set @found = 'T'
      end             
end   

if (@status is null)
begin
   select top 1 @ordertranid  = ot.ordertranid,
                @internalchar = ot.internalchar
    from  ordertran ot with (nolock)
    where ot.orderid       = ''
     and  ot.rentalitemid  = @rentalitemid
     and  ot.itemstatus    = 'P'
   order by ot.outreceiveglobaldatetime desc
   set @ordertranid  = isnull(@ordertranid , 00)
   set @internalchar = isnull(@internalchar, '')
   if (@ordertranid > 0)
   begin
      set @ispending = 'T'
      set @found     = 'T'   
   end   
end

if (@status is null) and (@ispending <> 'T')
begin
   set @ordertranid  = 0
   set @internalchar = ''
   set @issub        = 'F'

   if (@codetype = 'RENTALITEMID')  --// 07/03/2012
      begin
      select top 1 @ordertranid  = ot.ordertranid,
                   @internalchar = ot.internalchar,
                   @issub        = 'F'
      from   ordertran ot with (nolock)
                join dealorder      o  with (nolock) on (ot.orderid = o.orderid)
                join masteritemview mi with (nolock) on ((o.orderid = mi.orderid)
                                                         and
                                                         (ot.masteritemid = mi.masteritemid))
      where  ot.rentalitemid = @rentalitemid
        and  ot.itemstatus   = (case
                                   when (@bctype = 'P') then 'O'
                                   else                      ot.itemstatus
                                end)
        and   o.ordertype in ('O', 'T', 'P')
        and   mi.inventorydepartmentid in (select inventorydepartmentid from @tmpdepartmentids)
      order by ot.outreceiveglobaldatetime desc
      end
   else
      begin
      select top 1 @ordertranid  = ot.ordertranid,
                   @internalchar = ot.internalchar,
                   @issub        = (case
                                       when (ot.vendorid > '') then 'T'
                                       else                         'F'
                                   end)
      from   ordertran ot with (nolock)
                join dealorder      o  with (nolock) on (ot.orderid = o.orderid)
                join masteritemview mi with (nolock) on ((o.orderid = mi.orderid)
                                                         and
                                                         (ot.masteritemid = mi.masteritemid))
      where  ot.barcode      = @barcode
        and  ot.rentalitemid = @rentalitemid --//eg 4/15/11 CAS-08126-84FR
        and  ot.itemstatus   = (case
                                   when (@bctype = 'P')    then 'O'
                                   when (ot.vendorid > '') then 'O'  --//jh 06/11/2013  CAS-11503-V1K1
                                   else                      ot.itemstatus
                                end)
        and   o.ordertype in ('O', 'T', 'P')
        and   mi.inventorydepartmentid in (select inventorydepartmentid from @tmpdepartmentids)
      order by ot.outreceiveglobaldatetime desc
      end

   set @ordertranid  = isnull(@ordertranid , 00)
   set @internalchar = isnull(@internalchar, '')
   
  
   if (@ordertranid > 0)
   begin
     set @found = 'T'
     if (@issub = 'T')
     begin
        set @statustype       = 'OUT'
        set @userswarehouseid = isnull(@userswarehouseid, '')
        set @warehouseid      = isnull(@userswarehouseid, '')
        set @trackedby        = 'BARCODE'
     end   
   end   
end   

if (@status is null) and (@found = 'T') and (@ordertranid = 0)
begin
   set @status = 1000
   set @msg    = rtrim(ltrim(@barcode)) + ' was found but has no Transaction History. The bar code may have been assigned a new bar code number.' --//eg 5/22/13 CAS-11453-Q2V1
end

if (@status is null) and (@found = 'T') 
begin
   if (@ispending = 'T')
      begin
      --// pending exchange detected
      select top 1 @bcdepartmentid = '',
                   @bcdealid       = '',
                   @orderno        = '',
                   @orderdesc      = '',
                   @ordertranid    = ot.ordertranid,
                   @masteritemid   = mi.masteritemid,
                   @masterid       = mi.masterid,
                   @masterno       = (select top 1 m.masterno
                                      from   master m with (nolock)
                                      where  m.masterid = mi.masterid),
                   @rentalitemid   = ot.rentalitemid,
                   @description    = mi.description,
                   @vendorid       = ot.vendorid,
                   @itemstatus     = ot.itemstatus,
                   @vendor         = (select top 1 vendor
                                      from   vendor with (nolock)
                                      where  vendorid = ot.vendorid),
                   @iscomplete     = (case
                                         when mi.itemclass in ('C', 'K', 'S') then 'T'
                                         else                                      'F'
                                      end),
                   @ordertype      = '',
                   @internalchar   = ot.internalchar,
                   @outcontractid  = ot.outreceivecontractid,
                   @suspendedincontractid   = ot.inreturncontractid,
                   @suspendedinusersid      = ot.inreturnusersid
       from  ordertran ot with (nolock) 
                   join masteritemview mi with (nolock) on ((ot.masteritemid = mi.masteritemid))
       where ot.internalchar = @internalchar
       and   ot.ordertranid  = @ordertranid
       and   mi.inventorydepartmentid in (select inventorydepartmentid from @tmpdepartmentids)       
       end
    else
      begin
      select top 1 @bcorderid      = ot.orderid,
                   @bcdepartmentid = o.departmentid,
                   @bcdealid       = o.dealid,
                   @orderno        = o.orderno,
                   @orderdesc      = o.orderdesc,
                   @ordertranid    = ot.ordertranid,
                   @masteritemid   = ot.masteritemid,
                   @masterid       = mi.masterid,
                   @masterno       = (select top 1 masterno
                                      from   master with (nolock)
                                      where  masterid = mi.masterid),
                   @rentalitemid   = ot.rentalitemid,
                   @description    = mi.description,
                   @vendorid       = ot.vendorid,
                   @itemstatus     = ot.itemstatus,
                   @vendor         = (select top 1 vendor
                                      from   vendor with (nolock)
                                      where  vendorid = ot.vendorid),
                   @iscomplete     = (case
                                         when mi.itemclass in ('C', 'K', 'S') then 'T'
                                         else                                      'F'
                                      end),
                   @ordertype      = o.ordertype,
                   @internalchar   = ot.internalchar,
                   @outcontractid  = ot.outreceivecontractid,
                   @suspendedincontractid   = ot.inreturncontractid,
                   @suspendedinusersid      = ot.inreturnusersid
      from   ordertran ot with (nolock)
                 join dealorder      o  with (nolock) on (ot.orderid = o.orderid)
                 join masteritemview mi with (nolock) on ((ot.orderid      = mi.orderid) and
                                                      (ot.masteritemid = mi.masteritemid))
      where  ot.internalchar  = @internalchar
        and  ot.ordertranid   = @ordertranid
        and  mi.inventorydepartmentid in (select inventorydepartmentid from @tmpdepartmentids)       
      end                                

   set @rentalitemid  = isnull(@rentalitemid , '')
   set @dealid        = isnull(@dealid       , '')
   set @internalchar  = isnull(@internalchar , '')
   set @bcorderid     = isnull(@bcorderid    , '')
   set @outcontractid = isnull(@outcontractid, '') 
   set @suspendedincontractid  = isnull(@suspendedincontractid, '') 
   set @suspendedinusersid     = isnull(@suspendedinusersid, '') 

   if (@bcdealid > '') 
   begin
      select top 1 @dealno = d.dealno,                               
                   @deal   = d.deal
      from   deal d with (nolock)
      where  d.dealid = @dealid
   end   

   if (@rentalitemid > '') 
   begin
      select top 1 @statustype    = rs.statustype,
                   @warehouseid   = ris.warehouseid,
                   @trackedby     = m.trackedby
      from rentalitem       ri  with (nolock), 
           rentalitemstatus ris with (nolock), 
           rentalstatus     rs  with (nolock), 
           master           m   with (nolock)
      where ri.rentalitemid    = ris.rentalitemid
        and ris.rentalstatusid = rs.rentalstatusid
        and m.masterid         = ri.masterid
        and ri.rentalitemid    = @rentalitemid
   end

   if (@barcode > '') and 
      ((@rentalitemid > '') or (@vendorid > ''))
   begin
       set @found = 'T'
   end   

   if (@found <> 'T')
   begin
      set @status = 1000
      set @msg    = rtrim(ltrim(@barcode)) + ' has no Transaction History.'
   end
end

--//if (@found = 'T') eg 6/25/13 CAS-11562-V3Z7
if (@status is null) and (@found = 'T') 
begin               
   if (@bctype = 'T')  
   begin
     if (@orderid > '')     
        begin
        select top 1 @transferfromwarehouseid = fromwarehouseid
        from   dealorder with (nolock)
        where  orderid = @orderid
        end
     else if (@bcorderid > '')
        begin
        select top 1 @transferfromwarehouseid = warehouseid
        from   dealorder with (nolock)
        where  orderid = @bcorderid
        end
     set @transferfromwarehouseid = isnull(@transferfromwarehouseid, '')
   end  
   if (@bctype in ('O', 'P')) and (@userswarehouseid <> @warehouseid) and (@hasquiklocate <> 'T')
      begin
      set @status = 401
      set @msg    = ltrim(rtrim(@barcode)) + ' belongs to Warehouse ' + (select top 1 warehouse
                                                                         from   warehouse with (nolock)
                                                                         where  warehouseid = @warehouseid)
      end
   else if (@bctype = 'T') and 
           (((@orderid = '') and (@transferfromwarehouseid <> @userswarehouseid)) or
            ((@orderid > '') and (@transferfromwarehouseid > '') and (@transferfromwarehouseid <> @warehouseid)))
      begin
         set @status = 401
         set @msg    = ltrim(rtrim(@barcode)) + ' belongs to Warehouse ' + (select top 1 warehouse
                                                                            from   warehouse with (nolock)
                                                                            where  warehouseid = @warehouseid)
      end
--//   eg 6/30/11 CAS-08266-015R
--//   else if (@bctype = 'T') and (@tranfromwhid > '') and (@tranfromwhid <> @warehouseid)
--//      begin
--//      set @status = 401
--//      set @msg    = ltrim(rtrim(@barcode)) + ' belongs to Warehouse ' + (select top 1 warehouse
--//                                                                         from   warehouse with (nolock)
--//                                                                         where  warehouseid = @warehouseid)
--//      end
   else if (@statustype = 'INREPAIR')        
      begin
      set @status = 100
      set @msg    = ltrim(rtrim(@barcode)) + ' is in Repair on Repair Order ' + rtrim(@orderno)
      end
   else if (@statustype = 'STAGED')
      begin
      set @status = 1009
      set @msg    = ltrim(rtrim(@barcode)) + ' is Staged on Order No. ' + ltrim(rtrim(@orderno))
      end  
   else if (@statustype = 'RETIRED')
      begin
      set @status = 1010
      set @msg    = ltrim(rtrim(@barcode)) + ' is Retired.'
      end
   --//jh 02/22/2013 CAS-10946-GWY7
   else if (@statustype = 'CONTAINED')
      begin
      set @status = 1011
      set @msg    = ltrim(rtrim(@barcode)) + ' is Contained.'
      end
   else if (@statustype = 'IN')
      begin
      set @status = 1001
      set @msg    = ltrim(rtrim(@barcode)) + ' Not Checked-Out.'

      set @flag   = 'F'
      if (@codetype = 'RENTALITEMID')  --// 07/03/2012
         begin
         if (exists (select *
                      from  ordertran ot with (nolock)
                      where ot.rentalitemid       = @rentalitemid
                      and   ot.inreturncontractid = @incontractid
                      and   ot.itemstatus         = 'I'))
         begin
            set @flag = 'T'
         end
         end
      else
         begin
         if (exists (select *
                      from  ordertran ot with (nolock)
                      where ot.barcode            = @barcode
                      and   ot.inreturncontractid = @incontractid
                      and   ot.itemstatus         = 'I'))
         begin
            set @flag = 'T'
         end
         end

      if (@flag = 'T')
         begin
         set @status       = 1002
         set @msg          = ltrim(rtrim(@barcode)) + ' Already in this Session.'
         set @orderid      = @bcorderid
         set @dealid       = @bcdealid
         set @departmentid = @bcdepartmentid
         end         
      else
         begin
         select top 1 @suspendedmsg = ltrim(rtrim(str(sessionno))),    --//eg 11/16/12 CAS-10402-JXY5
                      @suspendeddealid       = dealid   
         from   contract c with (nolock)
         where  c.contractid = @suspendedincontractid
           and  c.suspend    = 'T'
           and  c.forcedsuspend <> 'E'
         if (isnull(@suspendedmsg, '') > '')
         begin
            set @suspendedmsg = 'Suspended on In Session No ' + @suspendedmsg   
            set @suspendedmsg = ltrim(rtrim(@suspendedmsg)) + ' by '+ (isnull((select top 1 namefml
                                                                               from   users u with (nolock)
                                                                               where  usersid = @suspendedinusersid), 'N/A'))
            set @suspendedmsg = ltrim(rtrim(@suspendedmsg))  + ' for ' + (isnull((select top 1 deal
                                                                                  from   deal d with (nolock)
                                                                                  where  dealid = @suspendeddealid), 'N/A'))
            set @msg = @msg + space(01) + ltrim(rtrim(@suspendedmsg))
         end 
         end 
      end 
   else if (@itemstatus = 'P')
      begin
      set @status = 1012
      set @msg    = 'Item is out as a Pending Exchange. Use Exchange module to complete the Exchange.'
      end       
   else if (@itemstatus = 'L')
      begin
      set @status = 1013
      set @msg    = 'Item on Lost Contract'
      end       
   else if (@statustype = 'INTRANSIT') and (@bctype = 'O')
      begin
      set @status = 102
      set @msg    = ltrim(rtrim(@barcode)) + ' In-Transit on Order No. ' + ltrim(rtrim(@orderno))
      end  
--//   else if (isnull((select top 1 c.suspend
--//                    from  contract c with (nolock)
--//                    where c.contractid = @outcontractid), 'F') = 'T')   --//eg 1/5/12 CAS-08879-DQ0N
   else if (exists (select *
                    from  contract c with (nolock)
                    where c.contractid = @outcontractid
                      and c.suspend    = 'T'
                      and c.forcedsuspend <> 'E'))
      begin                  
      set @status = 300
      set @msg    = 'Item is Checked-Out on a Suspended OUT Contract.'
      end
   else if (@itemstatus = 'O') and (@itemstatus <> substring(@statustype, 01, 01)) and (@bctype = 'O')
      begin
      set @status = 30001
      set @msg    = 'Please call Database Works, Inc'
      end
   else if (@bctype = 'T') and (@statustype = 'OUT')
      begin
      set @status = 1016
      set @msg    = ltrim(rtrim(@barcode)) + ' Checked-Out not In Transit'
      end  
   else if ((@itemstatus = 'O') and (@statustype = 'OUT'      )) or --//and (@bctype = 'O')) or
           ((@itemstatus = 'O') and (@statustype = 'ONTRUCK'  )) or --//and (@bctype = 'P')) or
           ((@itemstatus = 'O') and (@statustype = 'INTRANSIT'))    --//and (@bctype = 'T'))
      begin
      if (@outcontractid = '')
         begin
         set @status = 30001
         set @msg    = 'OUT Contract Missing.'
         end  
      else if (@bcorderid = '')
         begin
         set @status = 30001
         set @msg    = 'Order Missing.'
         end  

      if (@status is null) 
      begin
         if (@bctype =  'O') and (@ordertype = 'P') 
            begin
            set @status       = 1019
            set @msg          = 'Item on Truck ' +  ltrim(rtrim(@orderno)) + ' - ' + ltrim(rtrim(@orderdesc)) + '.'
            set @orderid      = @bcorderid
            set @departmentid = @bcdepartmentid
            set @dealid       = @bcdealid
            end
         else if (@departmentid <> '')              and 
                 (@departmentid <> @bcdepartmentid)
              begin
              set @status = 1003
              set @msg    = ltrim(rtrim(@barcode)) + ' Checked-Out on Order No: ' + ltrim(rtrim(@orderno)) + 
                            ' - Dept: ' + (select top 1 department
                                           from   department with (nolock)
                                           where  departmentid = @bcdepartmentid)
              end  
         else if (@bctype  = 'O')       and
                 (@dealid <> '')        and 
                 (@dealid <> @bcdealid)
             begin
             set @status = 1004
             set @msg    = ltrim(rtrim(@barcode)) + ' Checked-Out on Order No: ' + ltrim(rtrim(@orderno)) +
                           ' - Deal: ' + (select top 1 deal
                                          from   deal with (nolock)
                                          where  dealid = @bcdealid)
             end
         else if (@bctype <> @ordertype) --//item should be checked in as an order item but was found on a truck/transfer order
            begin
            set @status = 1014
            set @msg    = ltrim(rtrim(@barcode)) + ' Out on ' + (case
                                                                   when @ordertype = 'O' then 'ORDER '
                                                                   when @ordertype = 'T' then 'TRANSFER '
                                                                   when @ordertype = 'P' then 'PACKAGE TRUCK '
                                                                 end) +
                          'No: ' + ltrim(rtrim(@orderno))
            end
          else if (@orderid <> @bcorderid)                        and
                  (not exists (select orderid
                               from   ordercontract with (nolock)
                               where  contractid = @incontractid
                                 and  orderid    = @bcorderid))   and
                  ((select count(*)
                    from   ordercontract with (nolock)
                    where  contractid = @incontractid) > 0)
                                 
             begin
             if (@ordertype = 'O') 
                begin
                set @status       = 1005
                set @msg          = 'Item on New Order ' +  ltrim(rtrim(@orderno)) + ' - ' + ltrim(rtrim(@orderdesc))
                set @orderid      = @bcorderid
                set @departmentid = @bcdepartmentid
                set @dealid       = @bcdealid
                end
             else if (@ordertype = 'T') 
                begin
                set @status       = 1015
                set @msg          = 'Item on Transfer Order ' +  ltrim(rtrim(@orderno)) + ' - ' + ltrim(rtrim(@orderdesc))
                end
             else if (@ordertype = 'P') 
                begin
                set @status       = 1015
                set @msg          = 'Item on Package Truck ' +  ltrim(rtrim(@orderno)) + ' - ' + ltrim(rtrim(@orderdesc))
                end
             end                            
          else             
             begin
             set @orderid      = @bcorderid
             set @dealid       = @bcdealid
             set @departmentid = @bcdepartmentid
             set @status       = 0
             set @msg          = ''
             end
      end            
      end            
   else 
     begin
     set @status = 1999
     set @msg    = 'Item ' + rtrim(@barcode) + ' has invalid status.  Please call Database Works - 714-203-8805'  --//jh 06/25/2010 CAS-4581-KJZQ
     end
end
end
return
go


