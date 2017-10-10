using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;
using System.Collections.Generic;

namespace Microsoft.Bot.Sample.SimpleEchoBot
{
    [Serializable]
    public class EchoDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            var response = context.MakeMessage();

            var card = new HeroCard
            {
                Title = "Cyrano de Bergerac",
                Text = "Cyrano de Bergerac is a play written in 1897 by Edmond Rostand. Although there was a real Cyrano de Bergerac, the play is a fictionalization of his life that follows the broad outlines of it.",
                Images = new List<CardImage> { new CardImage("https://i.imgur.com/o7X5ETT.jpg") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "View on Gutenberg.org", value: "http://www.gutenberg.org/ebooks/1254") }

            };

            response.Speak = "<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"en-US\"><audio src=\"https://ia802702.us.archive.org/31/items/cyranodebergerac_1411_librivox/cyranodebergerac_01_rostand_128kb.mp3\" /></speak>";

            response.Attachments.Add(card.ToAttachment());

            await context.PostAsync(response);
            context.Wait(MessageReceivedAsync);
        }
    }
}