using Amazon.DynamoDBv2.DataModel;

namespace GloboClima.Api.Models
{
    [DynamoDBTable("PaisFavorito")]
    public class PaisFavorito
    {
        [DynamoDBHashKey("Username")]
        public string Username { get; set; } = "";

        [DynamoDBRangeKey("NomePais")]
        public string NomePais { get; set; } = "";
    }
}