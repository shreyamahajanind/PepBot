### Create a QnAMaker Application to enable QnA Knowledge Bases

QnA knowledge base setup and application configuration steps can be found [here](https://aka.ms/qna-instructions).

## To try this sample
- Create a Knowledge Base in QnAMaker Portal.
- Import "PepBotKnowledgeBase.tsv" file, in QnAMaker Portal.
- Save and Train the model.
- Create Bot from Publish page.
- Test bot with Web Chat.
- Capture values of settings like"QnAAuthKey" from 
- "Configuration" page of created bot, in Azure Portal.
- Updated appsettings.json with values as needed.
- Use value of "QnAAuthKey" for setting "QnAEndpointKey".
- Capture KnowledgeBase Id, HostName and EndpointKey current published app 

Further,
- Make sure QnABotML.Model is loaded properly, if not one can add the model mannualy using the file "training_data_pep_bot" in "TrainingData" folder.

## Testing the bot using Bot Framework Emulator

[Bot Framework Emulator](https://github.com/microsoft/botframework-emulator) is a desktop application that allows bot developers to test and debug their bots on localhost or running remotely through a tunnel.

- Install the Bot Framework Emulator version 4.3.0 or greater from [here](https://github.com/Microsoft/BotFramework-Emulator/releases)

### Connect to the bot using Bot Framework Emulator

- Launch Bot Framework Emulator
- File -> Open Bot
- Enter a Bot URL of `http://localhost:3978/api/messages`

