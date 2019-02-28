using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


namespace XAsset.Editor {
    public static class AssetsMenuItem {
        [MenuItem("XAsset/Copy Asset Path", false, 0)]
        static void CopyAssetPath() {
            if (EditorApplication.isCompiling) {
                return;
            }
            string path = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
            GUIUtility.systemCopyBuffer = path;
            Debug.Log(string.Format("systemCopyBuffer: {0}", path));
        }

        private const string RuntimeMode = "XAsset/Bundle Mode";
        [MenuItem(RuntimeMode, false, 100)]
        public static void ToggleRuntimeMode() {
            Utility.ActiveBundleMode = !Utility.ActiveBundleMode;
        }

        [MenuItem(RuntimeMode, true, 100)]
        public static bool ToggleRuntimeModeValidate() {
            Menu.SetChecked(RuntimeMode, Utility.ActiveBundleMode);
            return true;
        }

        private const string AssetsManifesttxt = "Assets/Manifest.txt";
        [MenuItem("XAsset/Build Manifest", false, 101)]
        public static void BuildAssetManifest() {
            if (EditorApplication.isCompiling) {
                return;
            }
            List<AssetBundleBuild> builds = BuildRule.GetBuilds(AssetsManifesttxt);
            BuildScript.BuildManifest(AssetsManifesttxt, builds);
        }

        [MenuItem("XAsset/Build AssetBundles", false, 102)]
        public static void BuildAssetBundles() {
            if (EditorApplication.isCompiling) {
                return;
            }
            List<AssetBundleBuild> builds = BuildRule.GetBuilds(AssetsManifesttxt);
            BuildScript.BuildManifest(AssetsManifesttxt, builds);
            BuildScript.BuildAssetBundles(builds);
        }

        [MenuItem("XAsset/Copy AssetBundles to StreamingAssets", false, 103)]
        public static void CopyAssetBundlesToStreamingAssets() {
            if (EditorApplication.isCompiling) {
                return;
            }
            BuildScript.CopyAssetBundlesTo(Path.Combine(Application.streamingAssetsPath, Utility.AssetBundlesOutputPath));

            AssetDatabase.Refresh();
        }

        [MenuItem("XAsset/Build Player", false, 104)]
        public static void BuildPlayer() {
            if (EditorApplication.isCompiling) {
                return;
            }
            BuildScript.BuildStandalonePlayer();
        }
    }
}