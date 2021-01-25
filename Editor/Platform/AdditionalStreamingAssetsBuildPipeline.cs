using System;
using Unity.Build.Classic;
using Unity.Build;
using System.IO;

namespace UTJ
{
    // StreamingAssetsに追加するデータ
    public class AdditionaStreamingAssets : IBuildComponent {
        public string []additionalPaths;
    }

    // ビルドのカスタム
    public class AdditionalStreamingAssetsBuildPipeline : ClassicBuildPipelineCustomizer
    {
        // 利用されるコンポーネント
        public override Type[] UsedComponents { get; } =
        {
            typeof(AdditionaStreamingAssets)
        };

        // Deployするファイルの指定
        public override void RegisterAdditionalFilesToDeploy(Action<string, string> registerAdditionalFileToDeploy)
        {
            var conf = GetConfig();
            foreach( var path in conf.additionalPaths)
            {
                RegisterDirectoryFile(path, registerAdditionalFileToDeploy);
            }
        }

        // ディレクトリ下にあるファイルを指定
        private void RegisterDirectoryFile(string dir, Action<string, string>  regist)
        {
            var paths = System.IO.Directory.GetFileSystemEntries(dir,"*",
                System.IO.SearchOption.AllDirectories);
            foreach( var path in paths)
            {
                string dstFile = path.Substring(dir.Length+1);
                string dest = Path.Combine( StreamingAssetsDirectory, dstFile);
                regist(path, dest);
            }
        }

        // もしビルドをする前にすることがあるなら…
        public override void OnBeforeBuild()
        {
            base.OnBeforeBuild();
        }

        // コンフィグファイルの取得
        private AdditionaStreamingAssets GetConfig()
        {
            return Context.GetComponentOrDefault<AdditionaStreamingAssets>();
        }
    }
}
