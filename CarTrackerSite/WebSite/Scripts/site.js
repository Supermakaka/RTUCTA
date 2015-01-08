$(document).ajaxError(function myErrorHandler(event, xhr, ajaxOptions, thrownError)
{
    alert("An error occured during your request: " + thrownError);
});