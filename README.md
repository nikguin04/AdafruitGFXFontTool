# AdafruitGFXFontTool
![](https://img.shields.io/badge/.NET%20Framework-4.8-blue) ![](https://img.shields.io/github/contributors/nikguin04/AdafruitGFXFontTool) ![](https://img.shields.io/github/v/release/nikguin04/AdafruitGFXFontTool?include_prereleases&label=pre-release&logo=github) ![](https://img.shields.io/github/issues/nikguin04/AdafruitGFXFontTool) ![](https://img.shields.io/github/languages/top/nikguin04/AdafruitGFXFontTool) <br>
### A custom font creator for microcontrollers using the Adafruit GFX font format

![Running project example](/readme_files/build_example1.png)<br>

How to install or build, and run the project
---------------

### Runtime requirements:
 - Any windows computer (desktop/laptop)
 - [.NET Framework 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)

### Build requirements:
 - Any windows computer (desktop/laptop)
 - [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
 - [.NET Framework 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)

### Downloading and running the project
 - Download pre-built and ready to run project at [AdafruitGFXFontTool-Releases](https://github.com/nikguin04/AdafruitGFXFontTool/releases/)
 - Unzip **Release.zip**
 - Open Release folder and run **AdafruitGFXFont.exe**

### Building and running the project
 - Download and extract the source code folder
 - Open **AdafruitGFXFont/AdafruitGFXFont.csproj** in Visual Studio 2022
 - In Visual Studio, select Build->Build Solution
 - Project will be built at **AdafruitGFXFont\bin\Debug** or **AdafruitGFXFont\bin\Release** (Debug by defualt)
 - Generated executable will be called **AdafruitGFXFont.exe**

Using the project
---------------

![Initial project screen](/readme_files/usecase_example1.png)<br>
You have two options when getting started with the project. Either Creating your own custom font with **Set Font Settings** or importing a font with **Import Font**

When importing a custom font, all font properties: character range, name and height, will automatically be imported aswell

![Initializing a font](/readme_files/usecase_example2.png)<br>
When creating a new font, you will need to choose these variables on your own
 - The font name can be any valid variable name in C/C++ [Rules for Naming Variable](https://www.programtopia.net/cplusplus/docs/variables#rules-naming)
 - Font height needs to be a number (0-255) as uint8_t. Note: font height is only used to determine new line distance (aka. yAdvance)
 - Range minimum and maximum determines the range of [UTF-8 characters](https://www.utf8-chartable.de/) to be included in the font. Range can be input as either a number (0-65535) or hex number (0x00-0xfFF) as uint16_t.
 - When all variables are entered as desired, press **Submit**

![Creating and modifying characters](/readme_files/usecase_example3.png)<br>
Under the "Choose Char" label is a dropdown menu with all available characters in font range, listed as (int / hex = char) format. Pick one to get started.

After choosing a character, you can change the properties for the character:
 - Width: The width (x coordinate size) of your character - a number (0-255) as uint8_t
 - Height: The height (y coordinate size) of your character - a number (0-255) as uint8_t
 - xAdvance: The amount of pixels to move the cursor (x coordinate) after drawing the character - a number (0-255) as uint8_t
 - xOffset: The amount of movement applied to the cursor (x coordinate) before drawing character (does not affect cursor after drawing) - a number (-128-127) as int8_t
 - yOffset: The amount of movement applied to the cursor (u coordinate) before drawing character (does not affect cursor after drawing) - a number (-128-127) as int8_t
After choosing the desired variables, click **Apply new char options**

When applying new character properties, the previously drawn character will be wiped, this leaves an array of white squares at the rightmost point of the screen.<br>
Click the squares to get started painting! when clicked, the squares will toggle from white to black, where black is the pixels which will be drawn<br>
*Note: The squares are built from an array of C# PictureBox elements, which are highly inefficient for this task, but easy to debug and develop. This means there will be a delay for how fast you can click the same square multiple times*

When you are done painting the desired characters, click **Build Font** and choose a folder<br>
This will output a single file under the folder Called "{fontname}GFXFONT.h"<br>
When this header file is imported into a project, the GFXfont element can be referred to as the font name set at the start


Resources 
---------------
 - [Adafruit GFX Graphics library](https://learn.adafruit.com/adafruit-gfx-graphics-library/overview)<br>
 - [LED Matrix library i used for this font: ESP32-HUB75-MatrixPanel](https://github.com/mrfaptastic/ESP32-HUB75-MatrixPanel-DMA)




MIT License
---------------

Copyright (c) 2023 Niklas Jensen (https://github.com/nikguin04)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

