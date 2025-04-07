using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ProjectService.Models;

public class Project
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("userId")]
    public int UserId { get; set; }

    [BsonElement("name")]
    public string Name { get; set; } = "";

    [BsonElement("charts")]
    public List<Chart> Charts { get; set; } = new();
}


public class Chart
{
    [BsonElement("symbol")]
    public string Symbol { get; set; } = "";

    [BsonElement("timeframe")]
    public string Timeframe { get; set; } = "";

    [BsonElement("indicators")]
    public List<Indicator> Indicators { get; set; } = new();
}

public class Indicator
{
    [BsonElement("name")]
    public string Name { get; set; } = "";

    [BsonElement("parameters")]
    public string Parameters { get; set; } = "";
}
