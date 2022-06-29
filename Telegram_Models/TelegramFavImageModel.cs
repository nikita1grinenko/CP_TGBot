using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TGBot.Telegram_Models;

public class TelegramFavImageModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string Id { get; set; }
    public string Image { get; set; }
 
}