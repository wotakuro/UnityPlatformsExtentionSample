using System.Collections.Generic;
using UnityEditor.Build;
using UnityEditor;
using UnityEditor.Build.Reporting;
using System.IO;

#if false 
// 通常のビルドプロセスで StreamingAssetsを利用する方法
namespace UTJ
{
    public class MyStreamingAssets : IPostprocessBuildWithReport, IPreprocessBuildWithReport
    {
        // データ移動
        protected struct DataMoveInfo{
            public BuildTarget target;
            public string path;

            // コンストラクタ
            public DataMoveInfo(BuildTarget t, string p)
            {
                this.target = t;
                this.path = p;
            }
        }


        public int callbackOrder => 0;

        // ビルド前に StreamingAssetsのファイルを変更します
        public void OnPreprocessBuild(BuildReport report)
        {
            var rules = CreateRules();

            foreach( var rule in rules)
            {
                if(rule.target == EditorUserBuildSettings.activeBuildTarget){
                    // 安全性より速度優先でMoveにしました。
                    // 安全性を優先するならコピーにして、その後削除です
                    Directory.Move(rule.path , "Assets/StreamingAssets/" + GetLastDirectory(rule.path) );
                }
            }
        }
        // ビルド後にtreamingAssetsを元に戻します
        public void OnPostprocessBuild(BuildReport report)
        {
            var rules = CreateRules();
            foreach (var rule in rules)
            {
                if (rule.target == EditorUserBuildSettings.activeBuildTarget)
                {
                    Directory.Move( "Assets/StreamingAssets/" + GetLastDirectory(rule.path) , rule.path);
                }
            }
        }

        // フォルダ名だけ抜き出しします
        private string GetLastDirectory(string dir)
        {
            int length = dir.Length;
            int last = dir.LastIndexOf('/');
            if (last != length - 1)
            {
                return dir.Substring(last + 1);
            }
            last = dir.LastIndexOf('/', length - 2);
            string newStr = dir.Substring(last + 1,length - last -2);
            return newStr;
        }

        // プラットフォーム別のルールをこちらに記述してください
        protected virtual List<DataMoveInfo> CreateRules()
        {
            List<DataMoveInfo> rules = new List<DataMoveInfo>();
            rules.Add(new DataMoveInfo(BuildTarget.StandaloneWindows, "AppendFile/Windows"));
            rules.Add(new DataMoveInfo(BuildTarget.StandaloneWindows64, "AppendFile/Windows"));
            rules.Add(new DataMoveInfo(BuildTarget.Android, "AppendFile/Android"));
            rules.Add(new DataMoveInfo(BuildTarget.iOS, "AppendFile/iOS"));
            return rules;
        }


    }
}
#endif