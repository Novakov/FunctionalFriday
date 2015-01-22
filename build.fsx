#r "packages/FAKE.Core.3.14.7/tools/FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile
open Fake.ZipHelper

Target "Default" (fun _ ->
    log "Defualt target"
)


Target "Clean" (fun _ ->
    let buildParams (def:MSBuildParams) = { def with Targets = ["Clean"] }
    
    build buildParams "FunctionalFriday.sln"
)

Target "GenerateVersionInfo" (fun _ ->
    let version = (getBuildParamOrDefault "version" "1.0.0-dev")

    CreateCSharpAssemblyInfo ("SolutionAssemblyInfo.cs" |> FullName) [
        Attribute.Product "FunctionFriday Fake Demo"
        Attribute.Description "Sample application which uses FAKE to build itself"   
        Attribute.Version version
        Attribute.InformationalVersion version        
        Attribute.Metadata ("Commit", (Git.Information.getCurrentSHA1 "."))
    ]
)

Target "Build" (fun _ ->
    let buildParams (def:MSBuildParams) = { def with 
                                                Targets = ["Build"]
                                                Verbosity = Some(MSBuildVerbosity.Minimal)
                                                FileLoggers = Some([
                                                                        { 
                                                                            Number = 1; 
                                                                            Filename = Some("build.log"); 
                                                                            Verbosity = Some(Normal); 
                                                                            Parameters = None
                                                                        }
                                                                    ])
                                                Properties = [
                                                                "Configuration", "Release"
                                                                ]
                                                }
    
    build buildParams "FunctionalFriday.sln"
)

Target "Tests" (fun _ ->
    let testParams (def:NUnitParams) = { def with ShowLabels = true }

    [ "Tests/bin/Release/Tests.dll" ]
     |> NUnit testParams
)

Target "Package" (fun _ ->
    let zipBase = "FakeBuild" @@ "bin" @@ "Release"
    CreateZip zipBase "BuildOutput.zip" "" DefaultZipLevel false (!! (zipBase @@ "**.*"))
)

// build dependencies
"Clean"
    ==> "GenerateVersionInfo"
    =?> ("Build", (hasBuildParam "RunTests"))
    ==> "Tests"
    ==> "Package"
    ==> "Default"

RunTargetOrDefault "Default"