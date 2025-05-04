using Amazon.DynamoDBv2.DataModel;

namespace GloboClima.Api.Models;

[DynamoDBTable("Usuario")]
public class Usuario
{
    [DynamoDBHashKey] 
    public string Username { get; set; }
    public string PasswordHash { get; set; }
}