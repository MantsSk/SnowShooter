using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

#if UNITY_PS5
using PS5 = UnityEditor.PS5;
#elif UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT
using UnityEditor.GameCore;
#endif

class AutoBuilder
{
    private const string ApplicationIdentifier = "com.unity3d.unity-metrics.test";

    [MenuItem ("AutoBuilder/PREPARE")]
    static void PREPARE ()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene(), "Assets/MainScene.unity");
        var editorBuildSettingsScenes = new List<EditorBuildSettingsScene> { new EditorBuildSettingsScene("Assets/MainScene.unity", true) };
        EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
    }

    [MenuItem ("AutoBuilder/Android APK ARM64")]
    static void Build_Android_APK_ARM64 ()
    {
        PREPARE();
        EditorUserBuildSettings.buildAppBundle = false;
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, ApplicationIdentifier);
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
        BuildPipeline.BuildPlayer(BuildPlayerOptions(BuildTarget.Android));
    }

    [MenuItem ("AutoBuilder/Android APK X86")]
    static void Build_Android_APK_X86()
    {
        PREPARE();
        EditorUserBuildSettings.buildAppBundle = false;
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, ApplicationIdentifier);
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.X86;
        BuildPipeline.BuildPlayer(BuildPlayerOptions(BuildTarget.Android));
    }

    [MenuItem ("AutoBuilder/Android APK X8664")]
    static void Build_Android_APK_X8664()
    {
        PREPARE();
        EditorUserBuildSettings.buildAppBundle = false;
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, ApplicationIdentifier);
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.X86_64;
        BuildPipeline.BuildPlayer(BuildPlayerOptions(BuildTarget.Android));
    }

    [MenuItem ("AutoBuilder/Android APK ARMv7")]
    static void Build_Android_APK_ARMv7()
    {
        PREPARE();
        EditorUserBuildSettings.buildAppBundle = false;
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, ApplicationIdentifier);
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7;
        BuildPipeline.BuildPlayer(BuildPlayerOptions(BuildTarget.Android));
    }

    [MenuItem ("AutoBuilder/Android AAB")]
    static void Build_Android_AAB ()
    {
        PREPARE();
        EditorUserBuildSettings.buildAppBundle = true;
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, ApplicationIdentifier);
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
        BuildPipeline.BuildPlayer(BuildPlayerOptions(BuildTarget.Android));
    }

    [MenuItem ("AutoBuilder/iOS")]
    static void Build_iOS()
    {
        PREPARE();
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, ApplicationIdentifier);
        BuildPipeline.BuildPlayer(BuildPlayerOptions(BuildTarget.iOS));
    }

    [MenuItem ("AutoBuilder/Windows")]
    static void Build_Windows ()
    {
        PREPARE();
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, ApplicationIdentifier);
        BuildPipeline.BuildPlayer(BuildPlayerOptions(BuildTarget.StandaloneWindows64));
    }

    [MenuItem ("AutoBuilder/macOS")]
    static void Build_MacOS ()
    {
        PREPARE();
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, ApplicationIdentifier);
        BuildPipeline.BuildPlayer(BuildPlayerOptions(BuildTarget.StandaloneOSX));
    }
    
    [MenuItem ("AutoBuilder/WebGL_Development")]
    static void Build_WebGL_Development ()
    {
        PREPARE();
        EditorUserBuildSettings.development = true;
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.WebGL, ApplicationIdentifier);
        BuildPipeline.BuildPlayer(BuildPlayerOptions(BuildTarget.WebGL));
    }

    
    [MenuItem ("AutoBuilder/WebGL_DiskSize")]
    static void Build_WebGL_DiskSize ()
    {
        PREPARE();
        EditorUserBuildSettings.SetPlatformSettings(BuildPipeline.GetBuildTargetName(BuildTarget.WebGL), "CodeOptimization", "disksize");
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.WebGL, ApplicationIdentifier);
        BuildPipeline.BuildPlayer(BuildPlayerOptions(BuildTarget.WebGL));
    }

    
    [MenuItem ("AutoBuilder/WebGL_RuntimeSpeed")]
    static void Build_WebGL_RuntimeSpeed ()
    {
        PREPARE();
        EditorUserBuildSettings.SetPlatformSettings(BuildPipeline.GetBuildTargetName(BuildTarget.WebGL), "CodeOptimization", "runtimespeed");
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.WebGL, ApplicationIdentifier);
        BuildPipeline.BuildPlayer(BuildPlayerOptions(BuildTarget.WebGL));
    }
    
    [MenuItem ("AutoBuilder/WebGL_BuildTime")]
    static void Build_WebGL_BuildTime ()
    {
        PREPARE();
        EditorUserBuildSettings.SetPlatformSettings(BuildPipeline.GetBuildTargetName(BuildTarget.WebGL), "CodeOptimization", "buildtimes");
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.WebGL, ApplicationIdentifier);
        BuildPipeline.BuildPlayer(BuildPlayerOptions(BuildTarget.WebGL));
    }

    [MenuItem ("AutoBuilder/PS4")]
    static void Build_PS4 ()
    {
        PREPARE();
        EditorUserBuildSettings.forceInstallation = true;
        EditorUserBuildSettings.ps4BuildSubtarget = PS4BuildSubtarget.Package;
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.PS4, ApplicationIdentifier);
        BuildPipeline.BuildPlayer(BuildPlayerOptions(BuildTarget.PS4));
    }

#if UNITY_PS5
    [MenuItem ("AutoBuilder/PS5")]
    static void Build_PS5 ()
    {
        PREPARE();
        EditorUserBuildSettings.forceInstallation = true;
#if UNITY_2019_4
        EditorUserBuildSettings.ps5BuildSubtarget = PS5BuildSubtarget.Package;
#else
        PS5.PlayerSettings.buildSubtarget = PS5.PS5BuildSubtarget.Package;
#endif
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.PS5, ApplicationIdentifier);
        BuildPipeline.BuildPlayer(BuildPlayerOptions(BuildTarget.PS5));
    }

#elif UNITY_GAMECORE_XBOXONE
    [MenuItem("AutoBuilder/XboxOne")]
    static void Build_XboxOne()
    {
        PREPARE();
        GameCoreXboxOneSettings.GetInstance().BuildSubtarget = GameCoreBuildSubtarget.Master;
        GameCoreXboxOneSettings.GetInstance().DeploymentMethod = GameCoreDeployMethod.Package;
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.GameCoreXboxOne, ApplicationIdentifier);
        BuildPipeline.BuildPlayer(BuildPlayerOptions(BuildTarget.GameCoreXboxOne));
    }

#elif UNITY_GAMECORE_SCARLETT
    [MenuItem("AutoBuilder/XboxSeries")]
    static void Build_XboxSeries()
    {
        PREPARE();
        GameCoreScarlettSettings.GetInstance().BuildSubtarget = GameCoreBuildSubtarget.Master;
        GameCoreScarlettSettings.GetInstance().DeploymentMethod = GameCoreDeployMethod.Package;
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.GameCoreXboxSeries, ApplicationIdentifier);
        BuildPipeline.BuildPlayer(BuildPlayerOptions(BuildTarget.GameCoreXboxSeries));
    }
#endif


    private static BuildPlayerOptions BuildPlayerOptions(BuildTarget buildTarget, BuildOptions buildOptions = BuildOptions.None)
    {
        var buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(scene => scene.path).ToArray(),
            locationPathName = "Players",
            target = buildTarget,
            options = buildOptions
        };
        return buildPlayerOptions;
    }

    private static string GetArg(string name)
    {
        var args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == name && args.Length > i + 1)
            {
                return args[i + 1];
            }
        }
        return null;
    }
}
