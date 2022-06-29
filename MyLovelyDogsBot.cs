using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TGBot.Telegram_Clients;

namespace TGBot;

public class MyLovelyDogsBot
{
    
     TelegramBotClient botClient = new TelegramBotClient("5560531667:AAG6zJgZnh2QZBgGATpw7nwecAJpcZP1izg");
     CancellationToken cancellationToken = new CancellationToken();
     ReceiverOptions receiverOptions = new ReceiverOptions{AllowedUpdates =  { } };
      TelegramClient Client_1 = new TelegramClient();
     public string PreviuosMessage = "";
     public string BreedNameInfo = "";
     public string BreedNameImage = "";
     public async Task Start()
     {
         botClient.StartReceiving(HandlerUpdateAsync, HandlerError, receiverOptions, cancellationToken);
         var botMe = await botClient.GetMeAsync();
         Console.WriteLine($"Bot {botMe.Username} is activated");
         Thread.Sleep(int.MaxValue);
     }
     
     private Task HandlerError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
     {
         var ErrorMassage = exception switch
         {
             ApiRequestException apiRequestException => $"Bot API error: \n {apiRequestException.ErrorCode}" +
                                                        $"\n {apiRequestException.Message}", _ => exception.ToString()

         };
         Console.WriteLine(ErrorMassage);
         return Task.CompletedTask;
     }

     public string MessageReturn( string Name)
     {
         string message = "";
         if (Client_1.GETInfoByName(Name).Result.name == "Not found")
         {
             message += "Not found";
             return message;
         }
         if (Client_1.GETInfoByName(Name).Result.name != null && Client_1.GETInfoByName(Name).Result.name != "Not found")
         {
             message += $"Name: {Client_1.GETInfoByName(Name).Result.name} \n";
             
         }

         if (Client_1.GETInfoByName(Name).Result.temperament != null)
         {
             message += $"Temperament: {Client_1.GETInfoByName(Name).Result.temperament} \n" ;
             
         }
         if (Client_1.GETInfoByName(Name).Result.life_span != null)
         {
             message += $"Life span: {Client_1.GETInfoByName(Name).Result.life_span} \n";
             
         }
         if (Client_1.GETInfoByName(Name).Result.height.metric != null)
         {
             message += $"Height: {Client_1.GETInfoByName(Name).Result.height.metric} cm at the withers\n";
             
         }
         if (Client_1.GETInfoByName(Name).Result.weight.metric != null)
         {
             message += $"Weight: {Client_1.GETInfoByName(Name).Result.weight.metric} kgs\n";
             
         }
         if (Client_1.GETInfoByName(Name).Result.bred_for != null)
         {
             message += $"This breed is bred for: {Client_1.GETInfoByName(Name).Result.bred_for} \n";
             
         }
         if (Client_1.GETInfoByName(Name).Result.breed_group != null)
         {
             message += $"Breed group: {Client_1.GETInfoByName(Name).Result.breed_group} \n";
             
         }
         if (Client_1.GETInfoByName(Name).Result.country_code != null)
         {
             message += $"Country: {Client_1.GETInfoByName(Name).Result.country_code} \n";
             
         }
         if (Client_1.GETInfoByName(Name).Result.origin != null)
         {
             message += $"Origin: {Client_1.GETInfoByName(Name).Result.origin} \n";
             
         }
/*    public string bred_for { get; set; }
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

*/
         return message;
     }
    
     private async Task HandlerUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
     {
         if (update.Type == UpdateType.Message && update?.Message?.Text != null)
         {
             await HandlerMassage(botClient, update.Message);
         }
         if (update?.Type == UpdateType.CallbackQuery)
         {
             await HandlerCallbackQuery(botClient, update.CallbackQuery);
         }
     }
     
     private async Task HandlerCallbackQuery(ITelegramBotClient botClient, CallbackQuery? callbackQuery)
     {
         if (callbackQuery.Data.StartsWith("POSTFavTheImage"))
         {
             Client_1.POSTFavTheImage(Client_1.GETInfoByName(BreedNameImage).Result.reference_image_id);
             await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "This image was added to favourites");
         }
         if (callbackQuery.Data.StartsWith($"DELETEFavTheImage"))
         {
             Client_1.DELETEFavTheImage(Client_1.GETInfoByName(BreedNameImage).Result.reference_image_id);
             await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "This image was deleted from favourites");
         }

     }
     
     
     
     private async Task HandlerMassage(ITelegramBotClient botClient, Message message)
     {
         if (message.Text == "/start")
         {
             PreviuosMessage = "";
             ReplyKeyboardMarkup replyKeyboardMarkup = new
             (
                 new[]
                 {
                     new KeyboardButton[] {"I want to get info🦮", "I want to get image🌅"},
                     new KeyboardButton[] {"My favourites❤️"}
                 }

             )
             {
                 ResizeKeyboard = true
             };
             await botClient.SendTextMessageAsync(message.Chat.Id, "Welcome to MyLovelyDogsBot 🐶. \n" +
                                                                   "Here you can get information about breeds, get images and fav your best dogs",replyMarkup: replyKeyboardMarkup) ;
             return;
         }

         if (message.Text == "I want to get info🦮")
         {
             PreviuosMessage = "Enter the name of the breed information about which you want to receive: ";
             message.Text = "Enter the name of the breed information about which you want to receive: ";
             await botClient.SendTextMessageAsync(message.Chat.Id, "Enter the name of the breed information about which you want to receive: ", replyMarkup: new ForceReplyMarkup() { Selective = true } );
             return;
             
             
         }

        
             if (PreviuosMessage == "Enter the name of the breed information about which you want to receive: ")
             {
                 PreviuosMessage = "Enter the name of the breed information about which you want to receive:";
                 BreedNameInfo = message.Text;
                 await botClient.SendTextMessageAsync(message.Chat.Id, MessageReturn(BreedNameInfo) );
                 return;
             }
         


         if(message.Text == "I want to get image🌅")
         {
             PreviuosMessage = "Please enter the name of the breed you want to receive an image of: ";
             message.Text = "Please enter the name of the breed you want to receive an image of. ";
             await botClient.SendTextMessageAsync(message.Chat.Id, "Please enter the name of the breed you want to receive an image of: ", replyMarkup: new ForceReplyMarkup() { Selective = true } );
             return;
         }

         if (PreviuosMessage == "Please enter the name of the breed you want to receive an image of: ")
         {
             BreedNameImage = message.Text;
             var FavButton = InlineKeyboardButton.WithCallbackData("", $"");
             if (!(Client_1.exists(Client_1.GETInfoByName(BreedNameImage).Result.reference_image_id).Result))
             {
                 InlineKeyboardMarkup keyboardMarkup = new
                 (
                     new[]
                     {
                         new[]
                         {
                             InlineKeyboardButton.WithCallbackData("Favourite❤", $"POSTFavTheImage")
                         }
                     }
                    

                 );
                 await botClient.SendTextMessageAsync(message.Chat.Id, $"{Client_1.GETImageByName(BreedNameImage).Result.url}", replyMarkup: keyboardMarkup);
                 return;
             }
             else
             {
                 InlineKeyboardMarkup keyboardMarkup = new
                 (
                     new[]
                     {
                         new[]
                         {
                             InlineKeyboardButton.WithCallbackData("🗑", $"DELETEFavTheImage")
                         }
                     }       
                     );
                 await botClient.SendTextMessageAsync(message.Chat.Id, $"{Client_1.GETImageByName(BreedNameImage).Result.url}", replyMarkup: keyboardMarkup);
                 return;
             }
             
         }
         if (message.Text == "My favourites❤️")
         {
             PreviuosMessage = "Here are your favourites dogs images: ";
             message.Text = "Here are your favourites dogs images: ";
             List<string> list_images = new List<string>();
             for (int i = 0; i < Client_1.GETAllFavImages().Result.Count; i++)
             {
                 
                 list_images.Add(Client_1.GETUrlByImageId(Client_1.GETAllFavImages().Result[i].Image).Result.url);
             }
             
             await botClient.SendTextMessageAsync(message.Chat.Id, $"Here are your favourites dogs images: ");
             for (int i = 0; i < list_images.Count; i++)
             { 
                 await botClient.SendTextMessageAsync(message.Chat.Id, $"{list_images[i]} ");
             }
             return;
         }
         
     }
}
/*InlineKeyboardMarkup keyboardMarkup = new
            (
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Breed", $"BreedByName"),
                        InlineKeyboardButton.WithCallbackData("Image", $"ImageByName")
                    }
                }


            );*/
/*if (message.Text == "Primal")
{
diet = message.Text;
LastText = "Enter food you dont like";
message.Text = "Enter food you dont like";
await botClient.SendTextMessageAsync(message.Chat.Id, "💁‍♀️Enter an ingredient you don't like🤢\nor don't want to see in your plan📃", replyMarkup: new ForceReplyMarkup() { Selective = true });
Console.WriteLine($"diet - {diet}");
LastText = "Enter food you dont like";
}             */
/*var breed = message.Text;
await botClient.SendTextMessageAsync(message.Chat.Id, Client_1.GETInfoByName(breed).Result.life_span );*/