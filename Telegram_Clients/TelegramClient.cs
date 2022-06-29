using MongoDB.Driver;
using TGBot.Telegram_Constants;
using TGBot.Telegram_Models;
using Newtonsoft.Json;

namespace TGBot.Telegram_Clients;

public class TelegramClient
{
    private HttpClient _httpClient;
    private static string _address;
    private static string _apiKey;

    public TelegramClient()
    {
        _address = TelegramConst.address;
        _apiKey = TelegramConst.apiKey;
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(_address);
    }
    
        public async Task<TelegramBreedModel> GETInfoByName(string Name)
        {
            var responce = await _httpClient.GetAsync($"/Info?Name={Name}");
            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<TelegramBreedModel>(content);
            return result;
        }
    

    public async Task<TelegramImageModel> GETImageByName(string Name)
    {
        var responce = await _httpClient.GetAsync($"/Image?Name={Name}");
        var content = responce.Content.ReadAsStringAsync().Result;
        var result = JsonConvert.DeserializeObject<TelegramImageModel>(content);
        return result;
    }

    public async Task<TelegramImageModel> GETUrlByImageId(string image_id)
    {
        var responce = await _httpClient.GetAsync($"/UrlByImageId?image_id={image_id}");
        var content = responce.Content.ReadAsStringAsync().Result;
        var result = JsonConvert.DeserializeObject<TelegramImageModel>(content);
        return result;
    }
    public async Task POSTFavTheImage(string Image_Id)
    {
        string connectionString = "mongodb://localhost:27017";
        string databaseName = "mylovelydogs_db";
        string collectionName = "favourites";
        var fav_1 = new TelegramFavImageModel {Image = Image_Id};
        var client_db = new MongoClient(connectionString);
        var db = client_db.GetDatabase(databaseName);
        var collection_db = db.GetCollection<TelegramFavImageModel>(collectionName);
        await collection_db.InsertOneAsync(fav_1);
    }
    
    public async Task DELETEFavTheImage(string Image_Id)
    {
        string connectionString = "mongodb://localhost:27017";
        string databaseName = "mylovelydogs_db";
        string collectionName = "favourites";
        var fav_1 = new TelegramFavImageModel {Image = Image_Id};
        var client_db = new MongoClient(connectionString);
        var db = client_db.GetDatabase(databaseName);
        var collection_db = db.GetCollection<TelegramFavImageModel>(collectionName);
        var filter = Builders<TelegramFavImageModel>.Filter.Eq("Image", Image_Id);
        await collection_db.DeleteOneAsync(filter);
    }
    public async Task<List<TelegramFavImageModel>> GETAllFavImages()
    {
        string connectionString = "mongodb://localhost:27017";
        string databaseName = "mylovelydogs_db";
        string collectionName = "favourites";
        var client_db = new MongoClient(connectionString);
        var db = client_db.GetDatabase(databaseName);
        var collection_db = db.GetCollection<TelegramFavImageModel>(collectionName);
        var result = await collection_db.FindAsync(_ => true);
        
        
        return result.ToList();
    }
    public async Task<bool> exists(string Image_Id)
    {
        string connectionString = "mongodb://localhost:27017";
        string databaseName = "mylovelydogs_db";
        string collectionName = "favourites";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        var collection = database.GetCollection<TelegramFavImageModel>(collectionName);
        bool Exists = await collection.Find(_ => _.Image == Image_Id).AnyAsync();
        return Exists;
    }
}