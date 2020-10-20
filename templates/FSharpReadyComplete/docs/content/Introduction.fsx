(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I @"../../bin/FSharpReady/implementationPlaceHolder/"
#r @"../../packages/formatting/Newtonsoft.Json/lib/netstandard2.0/Newtonsoft.Json.dll"
#r @"../../packages/formatting/FSharp.Plotly/lib/netstandard2.0/FSharp.Plotly.dll"
open FSharp.Plotly
(**
Introduction
============

This is just some placeholder text

*)
//Your path may differ
#r "FSharpReady.dll"

open FSharpReady

(**

Include Values
=======================

Little example for how to include values


*)

///string form of our hello world protein
let myList = [1;2;3]

(*** include-value:myList ***)

myList |> List.zip myList |> Chart.Point
|> GenericChart.toChartHTML

(*** include-it ***)