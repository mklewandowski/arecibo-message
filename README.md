# arecibo-message
Arecibo Message is an interactive where users can create and play an arecibo message.

## Gameplay
The Arecibo Message was a radio message sent into outer space in 1974. The goal of the message was to communicate with extraterrestrial life! The message consisted of 1679 binary digits which can be displayed as an image.

Create your own message by selecting squares in the grid. Then use the Play Message button to hear what your message might sound like if broadcast into space! Write letters, draw pictures, or create something totally unique!

![Arecibo Message gameplay](https://github.com/mklewandowski/arecibo-message/blob/main/arecibo-message-gameplay.gif?raw=true)

## Supported Platforms
Arecibo Message is designed for use on the following platform:
- Web

## Running Locally
Use the following steps to run locally:
1. Clone this repo
2. Open repo folder using Unity 2021.3.35f1
3. Install Text Mesh Pro

## Building the Project

### WebGL Build
For embedding within itch.io and other web pages, we use the `better-minimal-webgl-template` seen here:
https://seansleblanc.itch.io/better-minimal-webgl-template

Setup of the `better-minimal-webgl-template` is as follows:
1. Download and unzip the template.
2. Copy the `WebGLTemplates` folder into the `Assets` folder.
3. File -> Build Settings... -> WebGL -> Player Settings... -> Select the "BetterMinimal" template.
4. Enter color in the "Background" field.
5. Enter "false" in the "Scale to fit" field to disable scaling.
6. Enter "true" in the "Optimize for pixel art" field to use CSS more appropriate for pixel art.

### Running a Unity WebGL Build
1. Install the "Live Server" VS Code extension.
2. Open the WebGL build output directory with VS Code.
3. Right-click `index.html`, and select "Open with Live Server".

## Development Tools
- Created using Unity
- Code edited using Visual Studio Code