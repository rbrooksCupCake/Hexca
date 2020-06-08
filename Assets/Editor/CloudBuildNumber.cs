
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;
using UnityEngine.UI;

public class CloudBuildNumber : MonoBehaviour
{

    public static string MajorVersionNumStr;
    public static string MinorVersionNumStr;
    public static string InternalVersionNumStr;

#if UNITY_CLOUD_BUILD
    public static void OnPreExportIOS(UnityEngine.CloudBuild.BuildManifestObject manifest)
    {
        Debug.Log("UnityCloudBuild - OnPreExportIOS Started");
        string[] versionString = PlayerSettings.bundleVersion.Split('.');
        Debug.Log("Major Version: " + versionString[0]);
        MajorVersionNumStr = versionString[0];
        Debug.Log("Minor Version: " + versionString[1]);
        MinorVersionNumStr = versionString[1];
        Debug.Log("Internal Version: " + versionString[2]);
        InternalVersionNumStr = manifest.GetValue("buildNumber", "unknown");
        PlayerSettings.bundleVersion = MajorVersionNumStr + "." + MinorVersionNumStr +"."+ InternalVersionNumStr;
        versionString = PlayerSettings.bundleVersion.Split('.');
        Debug.Log("Internal Version: " + versionString[2]);
        Debug.Log("UnityCloudBuild - OnPreExportIOS Finished");
        Debug.Log("UnityCloudBuild - New Bundle Version: " + PlayerSettings.bundleVersion);
    }
#endif



    // Output the build size or a failure depending on BuildPlayer.


    [MenuItem("Build/Build iOS")]
    public static void MyBuild()
    {
        Debug.Log("Unity Build - Started");

        string[] versionString = PlayerSettings.bundleVersion.Split('.');
        Debug.Log("Major Version: " + versionString[0]);
        MajorVersionNumStr = versionString[0];
        Debug.Log("Minor Version: " + versionString[1]);
        MinorVersionNumStr = versionString[1];
        Debug.Log("Internal Version: " + versionString[2]);
        InternalVersionNumStr = (int.Parse(versionString[2]) + 1).ToString();
        PlayerSettings.bundleVersion = MajorVersionNumStr + "." + MinorVersionNumStr + "." + InternalVersionNumStr;
        versionString = PlayerSettings.bundleVersion.Split('.');
        Debug.Log("Internal Version: " + versionString[2]);
        Debug.Log("Unity Build - New Bundle Version: " + PlayerSettings.bundleVersion);

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/Main.unity" };
        buildPlayerOptions.locationPathName = "iOSBuild/"+"v."+PlayerSettings.bundleVersion+"/";
        buildPlayerOptions.target = BuildTarget.iOS;


        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }
}






