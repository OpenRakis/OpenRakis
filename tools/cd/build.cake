var target = Argument("target", "Zip-All");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .WithCriteria(c => HasArgument("rebuild"))
    .Does(() =>
{
    CleanDirectory($"./DuneEdit2/bin/{configuration}");
    CleanDirectory($"./DuneImpactor/bin/{configuration}");
    CleanDirectory($"./DuneExtractor/bin/{configuration}");
    CleanDirectory($"./LipsExtractor/bin/{configuration}");
    CleanDirectory($"./LipsInjector/bin/{configuration}");
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetCoreBuild("./DuneTools.sln", new DotNetCoreBuildSettings
    {
        Configuration = configuration
    });
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetCoreTest("./DuneTools.sln", new DotNetCoreTestSettings
    {
        Configuration = configuration,
        NoBuild = true
    });
});

Task("Publish-All")
    .IsDependentOn("Test")
    .Does(() =>
{
    DotNetCorePublish("./DuneTools.sln", new DotNetCorePublishSettings
    {
        Configuration = configuration,
        OutputDirectory = "./Publish/Win",
        SelfContained = true,
        PublishSingleFile = true,
        Runtime = "win-x64",
    });
    DotNetCorePublish("./DuneTools.sln", new DotNetCorePublishSettings
    {
        Configuration = configuration,
        OutputDirectory = "./Publish/Mac",
        SelfContained = true,
        PublishSingleFile = true,
        Runtime = "osx-x64",
    });
    DotNetCorePublish("./DuneTools.sln", new DotNetCorePublishSettings
    {
        Configuration = configuration,
        OutputDirectory = "./Publish/Linux",
        SelfContained = true,
        PublishSingleFile = true,
        Runtime = "linux-x64",
    });
});

Task("Zip-All")
    .IsDependentOn("Publish-All")
    .Does(() =>
{
    Zip("./Publish", "publish.zip");
});

Task("Publish-GitHub-Release")
    .Does(() =>
{
    GitReleaseManagerAddAssets(parameters.GitHub.UserName, parameters.GitHub.Password, "cake-build", "cake", parameters.Version.Milestone, parameters.Paths.Files.ZipArtifactPathDesktop.ToString());
    GitReleaseManagerAddAssets(parameters.GitHub.UserName, parameters.GitHub.Password, "cake-build", "cake", parameters.Version.Milestone, parameters.Paths.Files.ZipArtifactPathCoreClr.ToString());
    GitReleaseManagerClose(parameters.GitHub.UserName, parameters.GitHub.Password, "cake-build", "cake", parameters.Version.Milestone);
})

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
