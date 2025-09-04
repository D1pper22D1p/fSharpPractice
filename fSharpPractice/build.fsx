//#r "paket: nuget Fake.DotNet.Cli //"
//#load "./.fake/build.fsx/intellisense.fsx"

//open Fake.DotNet

//Target.create "Clean" (fun _ -> DotNet.exec id "clean" "" |> ignore)

//Target.create "Build" (fun _ -> DotNet.build id ".")

//Target.create "Run" (fun _ -> DotNet.exec id "run" "" |> ignore)

//open Fake.Core
//Target.runOrDefault "Build"
