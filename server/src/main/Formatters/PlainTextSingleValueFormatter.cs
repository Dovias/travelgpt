using Microsoft.AspNetCore.Mvc.Formatters;

namespace TravelGPT.Server.Formatters;

public class PlainTextSingleValueFormatter : InputFormatter
{
	private const string MimeType = "text/plain";

	public PlainTextSingleValueFormatter()
    {
        SupportedMediaTypes.Add(MimeType);
    }
    
	public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
    {
        try
        {
            using var reader = new StreamReader(context.HttpContext.Request.Body);
            string textSingleValue = await reader.ReadToEndAsync();

            //Convert from string to target model type (this is the parameter type in the action method)
            object model = Convert.ChangeType(textSingleValue, context.ModelType);
            return InputFormatterResult.Success(model);
        }
        catch (Exception exception)
        {
            context.ModelState.TryAddModelError("BodyTextValue", $"{exception.Message} ModelType={context.ModelType}");
            return InputFormatterResult.Failure();
        }
    }

	protected override bool CanReadType(Type type)
	{
		return type == typeof(string) ||
			type == typeof(int) ||
			type == typeof(DateTime);
	}
    
	public override bool CanRead(InputFormatterContext context)
    {
        return context.HttpContext.Request.ContentType == MimeType;
    }
}