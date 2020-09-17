module SomeTests

open FSharpReady
open Expecto


[<Tests>]
let testStuff = 
    testList "AminoAcids" [
        testCase "example" (fun () -> 
            let testSymbols = ['a','b','c']
            Expect.sequenceEqual
                testSymbols
                ['a','b','c']
                "wrooong"
        )
    ]