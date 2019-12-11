# Converting Validations to the New Format
## Middle Tier Projects
In the new way of doing things in the middle tier projects, validations functions are added directly to the controller for the main module the user is using.  For example if the user is on the Customer module, they would be calling validation methods from the Customer Controller now.  In this section, I'm going to show you how to declare the functions in the right places to make this work.

### Create a model class for the Validation Request and Response

#### Request Model Class

In most cases, you can share the validation's model class with any similar validations against the same entity.  So for example if we are doing the OfficeLocation validation on Customer, we are going to go to the WebApi project and create a file called OfficeLocationModels.cs in the OfficeLocation folder.  If you don't want to share the model you might want to create it in the Customer folder's CustomerModels.cs file.

~~~~
public class GetManyOfficeLocationRequest : GetRequest
{
    /// <summary>
    /// Filter Expression
    /// </summary>
    [GetRequestProperty(true, false)]
    public string LocationId { get; set; } = null;
    /// <summary>
    /// Filter Expression
    /// </summary>
    [GetRequestProperty(true, true)]
    public string Location { get; set; } = null;
}
~~~~

#### Response Model Class

~~~~
public class GetManyOfficeLocationModel
{
    public string LocationId { get; set; } = null;
    public string Location { get; set; } = null;
}
~~~~

## Updating the Logic.cs files
Notice that we use the Request and Response models we just created here.  Also notice that we can provide additional filtering against OfficeLocation's GetMany method.  For most validations, you will want to exlude inactive records, so it will be very common to have this inactive filter here.  Also notice that when call the GetMany method on OfficeLocation, that the method is generic.  The generic paramter is the Type you want to project the response onto.  So instead of returning OfficeLocationLogic which contains all kinds of insecure data, we project it onto GetManyOfficeLocationModel. 


~~~~
public async Task<GetResponse<GetManyOfficeLocationModel>> GetOfficeLocationsAsync(GetManyOfficeLocationRequest request)
{
    var officeLocationLogic = CreateBusinessLogic<OfficeLocationLogic>(this.AppConfig, this.UserSession);
    request.filters["Inactive"] = new GetManyRequestFilter("Inactive", "eq", "true", false);
    var result = await officeLocationLogic.GetManyAsync<GetManyOfficeLocationModel>(request);
    return result;
}
~~~~

### Updating Controller.cs files

The goal here is to call a method on the module's Logic file that will return a List of records that's been filtered in the Logic file and projected onto a model class.  Please declare a model class for each validation so it's not returning everything from the parent Logic.  If someone adds something insecure on the logic you will unintenionally expose it through the API.

~~~~
// GET api/v1/customer/lookup/officelocations
[HttpGet("lookup/officelocations")]
public async Task<ActionResult<GetResponse<GetManyOfficeLocationModel>>> GetOfficeLocationsAsync([FromQuery]GetManyOfficeLocationRequest request)
{
    try
    {
        var customer = FwBusinessLogic.CreateBusinessLogic<CustomerLogic>(this.AppConfig, this.UserSession);
        var officeLocations = await customer.GetOfficeLocationsAsync(request);
        if (officeLocations == null)
        {
            return NotFound();
        }
        return officeLocations;
    }
    catch(Exception ex)
    {
        return this.GetApiExceptionResult(ex);
    }
}
~~~~

## Front-end Projects

### Option 1:  Update the Validation HTML tag: 
To call the new module validations, you need to add a data-apiurl tag to your validation templates in the html files that has the url for the validation.  This will override the apiurl tag in the validation template.

### Option 2: Supply apiurl with a function
If you need to change the apiurl at runtime, because you need to inject data into the url, you can attach a function to the control that will supply the url:**
~~~~
 openForm(mode: string) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        // using the new anonymous function syntax:
        FwFormField.getDataField($form, 'OfficeLocationId').data('getapiurl', () => 'api/v1/customer/validations/officelocations');
        
        // using the older anonymous function syntax:
        FwFormField.getDataField($form, 'OfficeLocationId').data('getapiurl', function() {
            return 'api/v1/customer/validations/officelocations');
        });
}
~~~~