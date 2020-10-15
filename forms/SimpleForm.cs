using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TelegramBotBase.Base;
using TelegramBotBase.Enums;
using TelegramBotBase.Form;

namespace StarZ.TelegramBot.forms
{
    public class SimpleForm : AutoCleanForm
    {
        public SimpleForm()
        {
            this.DeleteSide = eDeleteSide.Both;
            this.DeleteMode = eDeleteMode.OnLeavingForm;
        }




        public override async Task Load(MessageResult message)
        {
            //message.MessageText will work also, cause it is a string you could manage a lot different scenerios here

            var messageId = message.MessageId;
            if (message.IsBotCommand || message.IsAction) return;

await this.Device.Send(Utils.TencentAiChatApi.GetChatResp(message.MessageText, $"{message.Device.DeviceId}"));
return;
           switch (message.Command)
            {
                case "hello":
                case "hi":

                    //Send him a simple message
                    await this.Device.Send("Hello you there !");
                    break;

                case "maybe":

                    //Send him a simple message and reply to the one of himself
                    await this.Device.Send("Maybe what?", replyTo: messageId);

                    break;

                case "bye":
                case "ciao":

                    //Send him a simple message
                    await this.Device.Send("Ok, take care !");
                    break;
                default:
                    await this.Device.Send($"You said:\n{message.MessageText}");
                    break;
            }
        }
    }
}

