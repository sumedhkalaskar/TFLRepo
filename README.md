# Project Title

TfL Coding Challenge

## Getting Started

The document is aimed to describe how to use and run Unit tests, IntegrationTests, Begaviour Tests and Command line execution

### Prerequisites
Updating app_id and app_key and correct API url.

There are app.congig files int the below list of projects and both keys should be updated.

## ____Installation____

### Download and install.

- [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet/3.1) SDK and .NET Runtime. _(Required)_

Install Visual Studio professional Edition 2019 and open the solution. Because all the packages are deleted, first build the solution to automatically download missing packages and build the solution.


Installing PowerShell please read and install from the link - https://docs.microsoft.com/en-us/powershell/scripting/setup/installing-windows-powershell?view=powershell-6

### Build with Visual Studio
- In the menubar, using the dropdown, change _`Debug`_ to _`Release`_. Build following the instructions [here](https://docs.microsoft.com/en-us/visualstudio/get-started/csharp/run-program?view=vs-2019).
- Open _File Explorer (Windows) , navigate to _and select_: _`
.\TFLRoadStatus\TFLRoadStatus.UI\bin\Release\netcoreapp3.1\TFLRoadStatus.UI\TFLRoadStatus.UI.exe (Windows) 
`_ 


## Running the tests

- Open Test Explorer in Visual Studio professional Edition 2019
I have try to achive almost 94% code coverage using Xunit test cases.Find the screenshot in assets folder.

After the Test Explorer is opened three test projects available are:
- TFLRoadStatus.Unit.Test - All the test cases included.Moq and Unit Testing the implementation by decoupling WebRequests.
							Integration Tests to test integration with TFL url

Clicking 'Run All' will run all tests together.




## Usage
The expected result is:
PS C:\deploy> .\TFLRoadStatus.UI.exe A2
The status of the A2 is as follows
        Road Status is Good
        Road Status Description is No Exceptional Delays


```bash
PS C:\> .\TFLRoadStatus.UI.exe


##Assumptions

- Kept IHttpClientFactory logs enabled to check URL formation and time laps. it can be disabled using app.setting.json configuration.


TFLRoadStatus.UI  1.0.0

## Authors

* **Sumedh Kalaskar** - *Tfl Codding Challange*