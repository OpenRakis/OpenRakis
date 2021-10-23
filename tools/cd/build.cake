#tool dotnet:?package=GitVersion.Tool&version=5.7.0
#tool nuget:?package=gitreleasemanager&version=0.12.1
#tool nuget:?package=GitReleaseNotes&version=0.7.1

var target = Argument("target", "GitHub-Release");
var configuration = Argument("configuration", "Release");
var assemblyVersion = "1.0.0";
var solutionFile = "./DuneTools.sln";
var artefactsDir = "./Publish";

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .WithCriteria(c => HasArgument("rebuild"))
    .Does(() =>
{
    System.IO.Directory.Delete(artefactsDir);
    System.IO.Directory.CreateDirectory(artefactsDir);
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
        DotNetCorePublish("./DuneEdit2/DuneEdit2/DuneEdit2.csproj", new DotNetCorePublishSettings
        {
            Configuration = configuration,
            OutputDirectory = "./Publish/Win",
            SelfContained = true,
            PublishSingleFile = true,
            Runtime = "win-x64",
        });
        DotNetCorePublish("./DuneEdit2/DuneEdit2/DuneEdit2.csproj", new DotNetCorePublishSettings
        {
            Configuration = configuration,
            OutputDirectory = "./Publish/Mac",
            SelfContained = true,
            PublishSingleFile = true,
            Runtime = "osx-x64",
        });
        DotNetCorePublish("./DuneEdit2/DuneEdit2/DuneEdit2.csproj", new DotNetCorePublishSettings
        {
            Configuration = configuration,
            OutputDirectory = "./Publish/Linux",
            SelfContained = true,
            PublishSingleFile = true,
            Runtime = "linux-x64",
        });
        GenerateReleaseNotes();
    });

private void GenerateReleaseNotes()
{
    var releaseNotesExitCode = StartProcess(
    @"tools\GitReleaseNotes.0.7.1\tools\gitreleasenotes.exe", 
    new ProcessSettings { Arguments = $". /o {artefactsDir}/releasenotes.md" });
    if (string.IsNullOrEmpty(System.IO.File.ReadAllText($"{artefactsDir}/releasenotes.md")))
    {
        System.IO.File.WriteAllText($"{artefactsDir}/releasenotes.md", "No issues closed since last release");
    }
    if (releaseNotesExitCode != 0) throw new Exception("Failed to generate release notes");
}

Task("Zip-All")
    .IsDependentOn("Publish-All")
    .Does(() =>
    {
        Zip($"{artefactsDir}/Linux", "linux.zip");
        Zip($"{artefactsDir}/Mac", "mac.zip");
        Zip($"{artefactsDir}/Win", "win.zip");
    });

Task("GitHub-Release")
    .IsDependentOn("Zip-All")
    .Does(() =>
    {
        var token = EnvironmentVariable("GitHubCakeToken");
        var owner = "maximilien-noal";
        var repo = "OpenRakis";

        var assets = new List<string>();
        Information("Releasing packages:");
        foreach (var file in new string[]{"win.zip", "mac.zip", "linux.zip"})
        {
            Information(file);
            assets.Add(file);
        }
        GitReleaseManagerCreate(owner: owner, repository: repo, token: token, settings: new GitReleaseManagerCreateSettings
        {
            WorkingDirectory = ".",
            TargetCommitish = "master",
            Prerelease = false,
            Assets = string.Join(",", assets),
            InputFilePath = new FilePath($"{artefactsDir}/releasenotes.md"),
            Name = $"DuneEdit2-v{assemblyVersion}",
            LogFilePath = new FilePath($"{artefactsDir}/release-creation.log")
        });
        Information("Release created");
        GitReleaseManagerPublish(owner: owner, repository: repo, token: token, tagName: $"DuneEdit2-v{assemblyVersion}", settings: new GitReleaseManagerPublishSettings
        {
            LogFilePath = new FilePath($"{artefactsDir}/release-log.log")
        });
        Information("Release published");
    });

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
