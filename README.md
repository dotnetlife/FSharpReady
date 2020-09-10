# FSharpReady

FSharpReady is a simple dotnet template for creating and packing a F# library project using paket. 

### Quick installation guide:

0. .NET Core 3.1.100 or higher 3.1 build has to be installed

1. Download and install the FSharpReady template from nuget
  <pre>
  dotnet new -i FSharpReady
  </pre>

2. Create the project using the template
  <pre>
  dotnet new FSharpReady -n ProjectName
  
  //By default the project will be set to be compiled to netstandard 2.1. This can changed using the `-im` flag. IE: 
  dotnet new FSharpReady -n ProjectName -im net45
  
  //All available flags can be checked using
  dotnet new FSharpReady -h
  </pre>

3. Set the dependencies in the paket.dependancies and the paket.references file

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
 
  
