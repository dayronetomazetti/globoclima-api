using Amazon.DynamoDBv2.DataModel;

namespace GloboClima.Api.Models
{
    [DynamoDBTable("CidadeFavorita")]
    public class CidadeFavorita
    {
        [DynamoDBHashKey]
        public string Username { get; set; } = "";

        [DynamoDBRangeKey]
        public string NomeCidade { get; set; } = "";
    }
}
