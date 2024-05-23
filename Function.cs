using Amazon.Lambda.Core;
using MyFunctionLambdaNetCore.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace MyFunctionLambdaNetCore;

/*public class Function
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
}*/
public class Function
{

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public string FunctionHandler(TestModels input, ILambdaContext context)
    {
        string mensaje = "";
        string connectionString = @"Data Source=sqlpaco2825.database.windows.net;Initial Catalog=AZURETAJAMAR;User ID=adminsql;Encrypt=True;Password=Admin123";

        try
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();

                string sqlUpdate = "UPDATE EMP SET SALARIO = SALARIO + 1 WHERE EMP_NO = @IdEmpleado";
                using (SqlCommand com = new SqlCommand(sqlUpdate, cn))
                {
                    com.Parameters.AddWithValue("@IdEmpleado", input.Key1);
                    com.ExecuteNonQuery();
                }

                string sqlSelect = "SELECT * FROM EMP WHERE EMP_NO = @IdEmpleado";
                using (SqlCommand com = new SqlCommand(sqlSelect, cn))
                {
                    com.Parameters.AddWithValue("@IdEmpleado", input.Key1);
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            mensaje = "El empleado " + reader["APELLIDO"].ToString() +
                                      " con oficio " + reader["OFICIO"].ToString() +
                                      " ha incrementado su salario a " + reader["SALARIO"].ToString();
                        }
                        else
                        {
                            mensaje = "No existe el empleado con ID " + input.Key1;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            mensaje = "Error: " + ex.Message;
        }

        var data = new
        {
            status = 200,
            key1 = input.Key1,
            key2 = input.Key2,
            key3 = input.Key3,
            mensaje = mensaje
        };

        return JsonConvert.SerializeObject(data);
    }
}



