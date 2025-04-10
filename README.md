# AI Interviewer

The purpose of this project was to create a mock interview with an AI. Users provide their natural responses to the AI and it will respond through text to speech. This project was meant to be used by Western University. Details below.

## Authors
- Kishor Loganathan
- Henry Chen
- Endreas Yohannes
- Zhihe Yu
- Brandon Luu

## Features
- Built using Unity (2022.3.58f1)
- Interview with an AI instance that can provide feedback on your responses
- VR mode for immersion
- Text to speech to hear what the AI is saying to you
- Speech to text to respond to the AI with your voice
- Two AI instances (Ollama and ChatGPT/External). See below

## Configuration

### AI Connection

The application is connected to two AI instances. An Ollama instance running on a Western Virtual Machine and an external ChatGPT Instance running on the OpenAI Servers.

In this current moment in time, these two instances are correctly connected and no setup is necessary to use the application.

In the future, the Western Virtual Machine or Ollama instance may be taken down or changed. 

In order to connect to any possible replacement, you must make the following changes to the **OllamaChatController.cs** file (located in Assets/Scripts/AI/OllamaChatController.cs):

1. Change the **model** variable to equal whatever the new Ollama AI model is
2. Change the **url** variable to equal whatever the new Ollama AI model's API url is

In the future, OpenAI may change the connection API url for their external ChatGPT instance. Additionally, the current security key used to connect to the external instance is linked to a specifc account. This account may cease to exist or the secuirty key could change. 

In order to connect to any possible replacement, you must make the following changes to the **GPTChatController.cs** file (located in Assets/Scripts/AI/GPTChatController.cs):

1. Change the **model** variable to equal whatever the new ChatGPT AI model is
2. Change the **url** variable to equal whatever the new ChatGPT AI model's API url is
3. Change the **key** variable to equal whatever the new ChatGPT Account security key is

## Running the Application

After cloning this repository from Github:

1. Navigate to Build.zip and extract the zip contents
2. Execute the .exe to run the application

The application will start in Non-VR mode, with the option to switch to VR mode through clicking the option on the main menu.

If you are using the application in Non-VR mode, ensure you have a mouse, keyboard, and microhpone plugged into your computer device.

If you are using the VR mode, connect your VR equipment to the computer device you are using. The way to do this will vary depending on the brand of your particular VR headset.

## Building from Source
Ensure you have a Unity version of **2022.3.58f1** to guarantee no issues. You may be able to open the project in later versions of Unity, but it will not work for earlier versions.

Clone this project repositoy from Github, and open this entire project folder in Unity. 

Then simply do File > Build and Run. You can then execute the newly created application exe.

## External Assets

For Robot Kyle 3d Model:
https://assetstore.unity.com/packages/3d/characters/robots/robot-kyle-urp-4696

Whisper Speech to Text AI along with its associated C# scripts:
https://github.com/Macoron/whisper.unity

Piper Text To Speech AI along with its associated C# scripts:
https://github.com/Macoron/piper.unity

AI Voice Model:
https://huggingface.co/rhasspy/piper-voices/tree/main/en/en_US/norman/medium

Virtual VR Keyboard:
https://github.com/Ayfel/MRTK-Keyboard

For the skybox(background sky) graphics: 
https://assetstore.unity.com/packages/2d/textures-materials/sky/free-stylized-skybox-212257

Various free stock icons and a stock image were used as UI elements