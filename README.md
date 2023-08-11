# AdafruitGFXFontTool
### A custom font creator for microcontrollers using the Adafruit GFX font format

![Running project example](https://raw.githubusercontent.com/nikguin04/AdafruitGFXFontTool/readme_files/build_example1.png)

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

![Initial project screen](https://raw.githubusercontent.com/nikguin04/AdafruitGFXFontTool/readme_files/usecase_example1.png)
You have two options when getting started with the project. Either Creating your own custom font with **Set Font Settings** or importing a font **Import Font**

When importing a custom font, all font properties: character range, name and height, will automatically be imported aswell

When creating a new font, you will need to choose these variables on your own
The font name can be any valid variable name in C/C++ [Rules for Naming Variable](https://www.programtopia.net/cplusplus/docs/variables#rules-naming)
