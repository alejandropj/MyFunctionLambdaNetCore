using Amazon.Lambda.Core;
using MyFunctionLambdaNetCore.Models;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace MyFunctionLambdaNetCore;

public class Function
{
    
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public string FunctionHandler(TestModels input,ILambdaContext context)
    {
        var data = new
        {
            status = 200,
            key1 = input.Key1,
            key2 = input.Key2,
            key3 = input.Key3,
            mensaje = "Soy una funcion Lambda desde C#"
        };
        string json = JsonConvert.SerializeObject(data);
        return json;
    }
}
