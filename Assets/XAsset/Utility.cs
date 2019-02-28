using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace XAsset {
    public class Utility {
        public const string AssetBundlesOutputPath = "AssetBundles";

#if UNITY_EDITOR
        private static int _activeBundleMode = -1;
        private const string ActiveBundleModeStr = "ActiveBundleMode";
        public static bool ActiveBundleMode {
            get {
                if (_activeBundleMode == -1)
                    _activeBundleMode = EditorPrefs.GetBool(ActiveBundleModeStr, true) ? 1 : 0;
                return _activeBundleMode != 0;
            }
            set {
                int newValue = value ? 1 : 0;
                if (newValue != _activeBundleMode) {
                    _activeBundleMode = newValue;
                    EditorPrefs.SetBool(ActiveBundleModeStr, value);
                }
            }
        }
#endif

        public static string GetPlatformName() {
#if UNITY_EDITOR
            return GetPlatformForAssetBundles(EditorUserBuildSettings.activeBuildTarget);
#else
            return GetPlatformForAssetBundles(Application.platform);
#endif
        }

        public static string GetPlatformForAssetBundles(RuntimePlatform platform) {
            switch (platform) {
                case RuntimePlatform.Android:
                    return "Android";
                case RuntimePlatform.IPhonePlayer:
                    return "iOS";
                case RuntimePlatform.tvOS:
                    return "tvOS";
                case RuntimePlatform.WebGLPlayer:
                    return "WebGL";
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    return "Windows";
                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.OSXEditor:
                    return "OSX";
            }
            return null;
        }

#if UNITY_EDITOR
        static string GetPlatformForAssetBundles(BuildTarget target) {

            switch (target) {
                case BuildTarget.Android:
                    return "Android";
                case BuildTarget.iOS:
                    return "iOS";
                case BuildTarget.tvOS:
                    return "tvOS";
                case BuildTarget.WebGL:
                    return "WebGL";
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    return "Windows";
#if UNITY_2017_3_OR_NEWER
                case BuildTarget.StandaloneOSX:
#else
                case BuildTarget.StandaloneOSXIntel:
                case BuildTarget.StandaloneOSXIntel64:
                case BuildTarget.StandaloneOSX:
#endif
                    return "OSX";
            }
            return null;
        }
#endif
    }
}