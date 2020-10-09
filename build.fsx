//--------------------------------------------------------------------------------------
// FAKE build script
// --------------------------------------------------------------------------------------

#r "paket:
nuget BlackFox.Fake.BuildTask
nuget Fake.Core.Target
nuget Fake.Core.Process
nuget Fake.Core.ReleaseNotes
nuget Fake.IO.FileSystem
nuget Fake.DotNet.Cli
nuget Fake.DotNet.MSBuild
nuget Fake.DotNet.AssemblyInfoFile
nuget Fake.DotNet.Paket
nuget Fake.DotNet.FSFormatting
nuget Fake.DotNet.Fsi
nuget Fake.DotNet.NuGet
nuget Fake.Api.Github
nuget Fake.DotNet.Testing.Expecto //"

#load ".fake/build.fsx/intellisense.fsx"

open BlackFox.Fake
open System.IO
open Fake.Core
open Fake.Core.TargetOperators
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing
open Fake.IO.Globbing.Operators
open Fake.DotNet.Testing
open Fake.Tools
open Fake.Api
open Fake.Tools.Git

Target.initEnvironment ()

let release = Fake.Core.ReleaseNotes.load ("RELEASE_NOTES.md")

//Nuget package info
let authors = "HLWeil, Kevin Schneider"
let title = "FSharpReady"
let owners = "HLWeil, Kevin Schneider"
let description = "Simple FSharp project templates"
let projectUrl = "https://github.com/dotnetlife/FSharpReady"
let tags = "dotnet-new templates F# FSharp"
let releaseNotes = (release.Notes |> String.concat "\r\n")
let repositoryUrl = "https://github.com/dotnetlife/FSharpReady"

let stableVersion = SemVer.parse release.NugetVersion

let pkgDir = "pkg"

let runDotNet cmd workingDir =
    let result =
        Fake.DotNet.DotNet.exec (Fake.DotNet.DotNet.Options.withWorkingDirectory workingDir) cmd ""
    if result.ExitCode <> 0 then failwithf "'dotnet %s' failed in %s" cmd workingDir

let clean = BuildTask.create "clean" [] {
    !! "templates/**/bin"
    ++ "templates/**/obj"
    ++ "tests"
    ++ "pkg"
    |> Shell.cleanDirs 
}

let pack = BuildTask.create "Pack" [clean] {
    "FSharpReady.csproj"
    |> Fake.DotNet.DotNet.pack (fun p ->
        let msBuildParams =
            {p.MSBuildParams with 
                Properties = ([
                    "Version",(sprintf "%i.%i.%i" stableVersion.Major stableVersion.Minor stableVersion.Patch )
                    "Authors",      authors
                    "Title",        title
                    "Owners",       owners
                    "Description",  description
                    "PackageProjectUrl",   projectUrl
                    "PackageTags",         tags
                    "PackageReleaseNotes", releaseNotes
                    "RepositoryUrl",repositoryUrl
                    "RepositoryType","git"
                ] @ p.MSBuildParams.Properties)
            }
        {
            p with 
                MSBuildParams = msBuildParams
                OutputPath = Some pkgDir
        }
    )
}

let uninstallTemplate =  BuildTask.create "uninstallTemplate" [] {
    runDotNet "new -u FSharpReady" __SOURCE_DIRECTORY__
}

let installTemplate =  BuildTask.create "installTemplate" [pack] {
    runDotNet 
        (sprintf "new -i %s" (pkgDir @@ (sprintf "FSharpReady.%i.%i.%i.nupkg" stableVersion.Major stableVersion.Minor stableVersion.Patch)))
        __SOURCE_DIRECTORY__
}

let testTemplate =  BuildTask.create "testTemplate" [installTemplate] {

    Directory.create (__SOURCE_DIRECTORY__ @@ "tests/slim")
    Directory.create (__SOURCE_DIRECTORY__ @@ "tests/complete")

    runDotNet "new fsr-slim" (__SOURCE_DIRECTORY__ @@ "tests/slim")
    //runDotNet "fake build" (__SOURCE_DIRECTORY__ @@ "tests/slim")

    runDotNet "new fsr-complete" (__SOURCE_DIRECTORY__ @@ "tests/complete")
    //runDotNet "fake build" (__SOURCE_DIRECTORY__ @@ "tests/complete")

}

let bootstrapBuildDependencies = BuildTask.createEmpty "bootstrapBuildDependencies" []

BuildTask.runOrDefaultWithArguments pack