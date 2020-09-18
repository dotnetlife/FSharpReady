# FSharpReady

FSharpReady is a simple dotnet template pack for creating and packing a F# library project using paket. 

At the moment it contains two project templates:
1. FSharpReady (fsr-slim)

   This is a minimal scaffold for building and packaging a F# library. Besides a solution and a library project it contains paket package manager.
   
2. FSharpReadyComplete (fsr-complete)

   This template builds on top of FSharpReady. Additionally it contains automatic documentation website creation supporting FSharpFormatting, Expecto unit testing and additional 
   build targets.

### Basic installation guide:

0. .NET Core 3.1.100 or higher 3.1 build has to be installed

1. Download and install the FSharpReady template pack from nuget

   <pre>
   dotnet new -i FSharpReady
   </pre>

2. Create the project using the template

   <pre>
   dotnet new fsr-slim -n ProjectName
   //or
   dotnet new fsr-complete -n ProjectName
  
   //By default the project will be set to be compiled to netstandard 2.1. This can changed using the `-im` flag. IE: 
   dotnet new fsr-slim -n ProjectName -im net45
   //or
   dotnet new fsr-complete -n ProjectName -im net45
  
   //All available flags can be checked using
   dotnet new fsr-slim -h
   //or
   dotnet new fsr-complete -h
   </pre>

3. Set the dependencies in the `paket.dependancies`([documentation](https://fsprojects.github.io/Paket/dependencies-file.html)) file in the repo base folder and the `paket.references`([documentation](https://fsprojects.github.io/Paket/references-files.html)) file in the project folder

4. Install the dependancies (dotnet tool restore might be necessary first)

   <pre>
   dotnet paket update
   </pre>
  
5. Build
   
   <pre>
   //in cmd
   fake build
  
   //in powershell
   .\fake.cmd build
   </pre>  
  
6. Pack

   <pre>
   //in cmd
   fake build -t pack
  
   //in powershell
   .\fake.cmd build -t pack
   </pre>   
 
 ### FSharpReadyComplete usage guide: 

1. Follow step 1 to 5 of the [Basic installation guide](https://github.com/dotnetlife/FSharpReady#basic-installation-guide) using `fsr-complete`

2. Testing using Expecto:
   
   Add testing cases to to `SomeTests.fs`
   
   Add the new tests to `Main.fs`
   
   The tests will be run at the next build
   
3. Documentation building:

   Add FSharp script to `docsrc/content` folder. Text inside `(**` `*)` brackets is read as markdown. Everything outside is formatted as F#. This `.fsx` will be converted to a `.html` with the same name on build
   
   Add reference to the `.html` in the `docsrc/tools/templates/template.cshtml`. This way the html will be later shown on the webpage.
   
   Test your documentation by using the 'buildlocaldocs' build target:
   
   <pre>
   .\fake.cmd build -t buildlocaldocs
   </pre>
   
   The formatted documents will show up in `temp/localDocs`
   
   When you're happy with the resulting webpage, publish it using the `releaseDocsToGhPages` build target:
   
   <pre>
   .\fake.cmd build -t releaseDocsToGhPages
   </pre>
   
4. Nuget package publishing
   
   Check the `paket.template` file in the project folder ([documentation](https://fsprojects.github.io/Paket/template-files.html))
   
   Update the `RELEASE_NOTES.md` with your latest changes and the resulting package version.
   
   The nuget package will always be built in the standard build process. You can find and check it out in the `temp` folder
   
   When you're happy with the package, you can automatically publish it:

   <pre>
   // To Github
   .\fake.cmd build -t releaseNugetPackageOnGithub
   
   // To Nuget
   .\fake.cmd build -t publishNugetPackages
   </pre>
