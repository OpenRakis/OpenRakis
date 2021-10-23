#tool dotnet:?package=GitVersion.Tool&version=5.7.0
#tool nuget:?package=gitreleasemanager&version=0.12.1
#tool "nuget:?package=GitReleaseNotes&version=0.7.1"

var target = Argument("target", "GitHub-Release");
var configuration = Argument("configuration", "Release");
var assemblyVersion = "1.0.0";
var solutionFile = "./DuneTools.sln";
var artifactsDir = "./Publish";

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .WithCriteria(c => HasArgument("rebuild"))
    .Does(() =>
{
    CleanDirectory(artifactsDir);
    System.IO.Directory.CreateDirectory(artifactsDir);
    CleanDirectory($"./DuneEdit2/bin/{configuration}");
    CleanDirectory($"./DuneImpactor/bin/{configuration}");
    CleanDirectory($"./DuneExtractor/bin/{configuration}");
    CleanDirectory($"./LipsExtractor/bin/{configuration}");
    CleanDirectory($"./LipsInjector/bin/{configuration}");
});

Task("SemVer")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        var gitVersionSettings = new GitVersionSettings
        {
            NoFetch = true,
            UpdateAssemblyInfo = true,
        };
        var gitVersion = GitVersion(gitVersionSettings);
        assemblyVersion = gitVersion.AssemblySemVer;
        Information($"AssemblySemVer: {assemblyVersion}");
    });

Task("Build")
    .IsDependentOn("SemVer")
    .Does(() =>
    {
        DotNetCoreBuild(solutionFile, new DotNetCoreBuildSettings
        {
            Configuration = configuration,
            MSBuildSettings = new DotNetCoreMSBuildSettings()
                .SetVersion(assemblyVersion)
                .WithProperty("FileVersion", assemblyVersion)
                .WithProperty("InformationalVersion", assemblyVersion)
        });
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        DotNetCoreTest(solutionFile, new DotNetCoreTestSettings
        {
            Configuration = configuration,
            NoBuild = true
        });
    });

Task("Publish-All")
    .IsDependentOn("Test")
    .Does(() =>
    {
        DotNetCorePublish(solutionFile, new DotNetCorePublishSettings
        {
            Configuration = configuration,
            OutputDirectory = "./Publish/Win",
            SelfContained = true,
            PublishSingleFile = true,
            Runtime = "win-x64",
        });
        DotNetCorePublish(solutionFile, new DotNetCorePublishSettings
        {
            Configuration = configuration,
            OutputDirectory = "./Publish/Mac",
            SelfContained = true,
            PublishSingleFile = true,
            Runtime = "osx-x64",
        });
        DotNetCorePublish(solutionFile, new DotNetCorePublishSettings
        {
            Configuration = configuration,
            OutputDirectory = "./Publish/Linux",
            SelfContained = true,
            PublishSingleFile = true,
            Runtime = "linux-x64",
        });
    });

private void GenerateReleaseNotes()
{
        var releaseNotesExitCode = StartProcess(
        @"tools\GitReleaseNotes\tools\gitreleasenotes.exe", 
        new ProcessSettings { Arguments = $". /o {artifactsDir}/releasenotes.md" });
    if (string.IsNullOrEmpty(System.IO.File.ReadAllText($"{artifactsDir}/releasenotes.md")))
    {
        System.IO.File.WriteAllText($"{artifactsDir}/releasenotes.md", "No issues closed since last release");
    }
    if (releaseNotesExitCode != 0) throw new Exception("Failed to generate release notes");
}

Task("Zip-All")
    .IsDependentOn("Publish-All")
    GenerateReleaseNotes();
    .Does(() =>
    {
        Zip($"{artifactsDir}/Linux", "publish-linux.zip");
        Zip($"{artifactsDir}/Mac", "publish-mac.zip");
        Zip($"{artifactsDir}/Windows", "publish-win.zip");
    });

Task("GitHub-Release")
    .IsDependentOn("Zip-All")
    .Does(() =>
    {
        var username = EnvironmentVariable("GitHubUserName");
        var password = EnvironmentVariable("GitHubUserName");
        var owner = "maximilien-noal";
        var repo = "OpenRakis";

        // Build a string containing paths to NuGet packages
        var assets = new List<string>();
        Information("Releasing packages:");
        foreach (var file in new string[]{"publish-win.zip", "publish-linux.zip", "publish-mac.zip", $"{artefactsDir}/releasenotes.md"})
        {
            Information(file);
            assets.Add(file);
        }
        GitReleaseManagerCreate(username, password, owner, repo, new GitReleaseManagerCreateSettings
        {
            Prerelease = false,
            Assets = string.Join(",", assets),
            TargetCommitish = "develop",
            InputFilePath = new FilePath($"{artefactsDir}/releasenotes.md"),
            Name = $"DuneTools-{assemblyVersion}"
        });
        GitReleaseManagerPublish(username, password, owner, repo, publishVersion);
    });

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
