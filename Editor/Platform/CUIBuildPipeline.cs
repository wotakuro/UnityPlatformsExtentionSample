using System.IO;
using Unity.Build;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace UTJ{
    public class CUIBuildPipeline
    {
        // コマンドラインからのビルド用
        public static void Build()
        {
            BuildWithAssetFile(GetCUIOptionValue("-build-asset-path"));
        }

        // 引数に渡されたオプションから取得します
        private static string GetCUIOptionValue(string option)
        {
            var args = System.Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; ++i)
            {
                if (args[i] == option && i < args.Length-1)
                {
                    return args[i + 1];
                }
            }
            return null;
        }
        // ファイルのビルドを行います
        private static Unity.Build.BuildResult BuildWithAssetFile(string path)
        {
            var config = AssetDatabase.LoadAssetAtPath<BuildConfiguration>(path);
            if( config == null) {
                UnityEngine.Debug.LogError("not Found file " + path);
                return null; 
            }
            if( !config.CanBuild())
            {
                UnityEngine.Debug.LogError("cannot Build file " + path);
                return null;
            }
            var report = config.Build();
            return report;
        }
    }
}
