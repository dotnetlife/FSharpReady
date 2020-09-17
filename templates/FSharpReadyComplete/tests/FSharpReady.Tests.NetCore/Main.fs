module FSharpReady.Tests.NetCore

open Expecto

[<EntryPoint>]
let main argv =

    //FSharpReady core tests
    Tests.runTestsWithCLIArgs [] argv SomeTests.testStuff         |> ignore
    0