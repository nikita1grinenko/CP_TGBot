namespace TGBot.Telegram_Models;

public class TelegramBreedModel
{
    public string bred_for { get; set; }
    public string breed_group { get; set; }
    public string id { get; set; }
    public string name { get; set; }
    public string temperament { get; set; }
    public string life_span { get; set; }
    public string alt_names { get; set; }
    public string wikipedia_url { get; set; }
    public string origin { get; set; }
    public string country_code { get; set; }
    public string reference_image_id { get; set; }
    public Height height { get; set; }
    public Weight weight { get; set; }


}

public class Height
{
    public string imperial { get; set; }
    public string metric { get; set; }
}

public class Weight
{
    public string imperial { get; set; }
    public string metric { get; set; }
}