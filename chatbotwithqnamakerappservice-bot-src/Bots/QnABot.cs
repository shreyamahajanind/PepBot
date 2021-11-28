// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.5.0

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using QnABotML.Model;
using AdaptiveCards;
using System.IO;
using System.Text;
using System;
using Newtonsoft.Json;
using System.Linq;

namespace Microsoft.BotBuilderSamples.Bots
{
    public class QnABot<T> : ActivityHandler where T : Microsoft.Bot.Builder.Dialogs.Dialog
    {
        protected readonly BotState ConversationState;
        protected readonly Microsoft.Bot.Builder.Dialogs.Dialog Dialog;
        protected readonly BotState UserState;

        public QnABot(ConversationState conversationState, UserState userState, T dialog)
        {
            ConversationState = conversationState;
            UserState = userState;
            Dialog = dialog;
        }

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
            await base.OnTurnAsync(turnContext, cancellationToken);

            // Save any state changes that might have occurred during the turn.
            await ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            await UserState.SaveChangesAsync(turnContext, false, cancellationToken);
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var txt = turnContext.Activity.Text;
            dynamic val = turnContext.Activity.Value;

            ModelInput sampleData = new ModelInput()
            {
                Sentence = turnContext.Activity.Text,
            };

            var predictionResult = ConsumeModel.Predict(sampleData);
            
            // Check if the activity came from a submit action
            if (string.IsNullOrEmpty(txt) && val != null)
            {
                if (val.prop1 == "quote")
                {
                    switch (val.prop2)
                    {
                        case "love":
                            string file1 = @"C:\Users\RIYA\Downloads\chatbotwithqnamakerappservice-bot-src\Quotes\lovequotes.txt";
                            Random rnd = new Random();
                            if (File.Exists(file1))
                            {
                                string[] lines = File.ReadAllLines(file1);
                                int i = rnd.Next(1, 20);
                                await turnContext.SendActivityAsync(MessageFactory.Text($"Here is a Quote for you : " + lines[i]));
                            }
                            break;

                        case "anger":
                            string file2 = @"C:\Users\RIYA\Downloads\chatbotwithqnamakerappservice-bot-src\Quotes\angerquotes.txt";
                            Random rnd2 = new Random();
                            if (File.Exists(file2))
                            {
                                string[] lines = File.ReadAllLines(file2);
                                int i = rnd2.Next(1, 20);
                                await turnContext.SendActivityAsync(MessageFactory.Text($"Here is a Quote for you : " + lines[i]));
                            }
                            break;

                        case "sadness":
                            string file3 = @"C:\Users\RIYA\Downloads\chatbotwithqnamakerappservice-bot-src\Quotes\sadnessquotes.txt";
                            Random rnd3 = new Random();
                            if (File.Exists(file3))
                            {
                                string[] lines = File.ReadAllLines(file3);
                                int i = rnd3.Next(1, 20);
                                await turnContext.SendActivityAsync(MessageFactory.Text($"Here is a Quote for you : " + lines[i]));
                            }
                            break;
                        case "fear":
                            string file4 = @"C:\Users\RIYA\Downloads\chatbotwithqnamakerappservice-bot-src\Quotes\fearquotes.txt";
                            Random rnd4 = new Random();
                            if (File.Exists(file4))
                            {
                                string[] lines = File.ReadAllLines(file4);
                                int i = rnd4.Next(1, 20);
                                await turnContext.SendActivityAsync(MessageFactory.Text($"Here is a Quote for you : " + lines[i]));
                            }
                            break;
                        default:
                            string file5 = @"C:\Users\RIYA\Downloads\chatbotwithqnamakerappservice-bot-src\Quotes\happyquotes.txt";
                            Random rnd5 = new Random();
                            if (File.Exists(file5))
                            {
                                string[] lines = File.ReadAllLines(file5);
                                int i = rnd5.Next(1, 20);
                                await turnContext.SendActivityAsync(MessageFactory.Text($"Here is a Quote for you : " + lines[i]));
                            }
                            break;
                    }
                }
                else if(val.question != "")
                {
                    turnContext.Activity.Text = val.question;
                    await Dialog.RunAsync(turnContext, ConversationState.CreateProperty<DialogState>(nameof(DialogState)), cancellationToken);
                }
            }

            if (!string.IsNullOrEmpty(txt))
            {
                List<string> specialWords = new List<string> { "hello", "hi", "yo", "hey", "how are you", "what's up", "how is it going", "how do you do", "bye", "thank", "thx", "thankyou", "tq", "ok","no" };
                var match = specialWords.Where(x => x.Contains(turnContext.Activity.Text)).FirstOrDefault();
                if (match != null)
                {
                    await Dialog.RunAsync(turnContext, ConversationState.CreateProperty<DialogState>(nameof(DialogState)), cancellationToken);
                }
            }

            var json = File.ReadAllText(@"C:\Users\RIYA\Downloads\chatbotwithqnamakerappservice-bot-src\Card\AdaptiveCard.json");
            dynamic obj = JsonConvert.DeserializeObject(json);
            obj["actions"][0]["data"]["prop2"] = predictionResult.Prediction;

            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = obj,
            };
            await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment));         
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Hi there!! I am PeP-Bot.\n I boost people up with my awesome quotes & also answer your questions related to stress, anxiety & depression."));
                    await turnContext.SendActivityAsync(MessageFactory.Text($"How are you feeling? I’m here to listen to you and support you."), cancellationToken);
                }
            }
        }
    }
}
