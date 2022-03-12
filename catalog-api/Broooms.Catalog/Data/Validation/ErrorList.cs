namespace Broooms.Catalog.Data.Validation;

using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class ErrorList : Dictionary<string, List<string>>
{
    public ErrorList AddError(string propertyName, string errorMessage)
    {
        var prop = propertyName.ToLower(CultureInfo.InvariantCulture);
        try
        {
            this[prop].Add(errorMessage);
            return this;
        }
        catch (KeyNotFoundException)
        {
            this[prop] = new List<string> { errorMessage };
            return this;
        }
    }

    public ErrorList(string propertyName, string errorMessage) =>
        AddError(propertyName, errorMessage);

    public ErrorList() { }
}

public static class ModelStateDictionayExtensions
{
    public static ErrorList ToErrorList(this ModelStateDictionary modelState)
    {
        var errorList = new ErrorList();
        foreach (var state in modelState)
        {
            foreach (var error in state.Value.Errors)
            {
                errorList.AddError(state.Key, error.ErrorMessage);
            }
        }
        return errorList;
    }
}
