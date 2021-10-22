var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .WithCriteria(c => HasArgument("rebuild"))
    .Does(() =>
{
    CleanDirectory($"./DuneEdit/bin/{configuration}");
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
        Configuration = configuration,
    });
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetCoreTest("./DuneTools.sln", new DotNetCoreTestSettings
    {
        Configuration = configuration,
    });
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
