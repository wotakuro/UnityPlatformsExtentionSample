# UnityPlatformsExtentionSample
Unity Platforms Extensionサンプル

## このサンプルについて
Platformsパッケージを利用したビルドでは、ビルドの過程のカスタムが変わりました。<br />
これを利用して、任意のパスにあるものをStreamingAssetsとしてコピーするサンプルになります。<br />

![alt text](Documentation~/image/menu.png)

※Platformsは 2021年1月現在は、preview提供ですので…<br />
ご利用は計画的に


## おまけ

従来のビルド方法で 同じようなことをする場合、ビルド前にStreamingAssets以下にデータを配置し、ビルド後に戻すという形になります。<br />
[MyStreamingAssets.cs](#Documentation~/MyStreamingAssets.cs)

Adrressableでは、ビルド前にファイルをコピーしてきて、ビルド後にファイル削除をしています。<br />
このサンプルでは速度重視でFile.Moveにしています。

