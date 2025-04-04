using UnityEngine;
using UnityEditor;
using System.IO;

public class Bundle : MonoBehaviour
{
    [MenuItem("Assets/AssetBundles")]
    static void BuildAssetBundles()
    {
        string dir = "Assets/StreamingAssets";

        if(!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(dir);
        }

        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None,
            EditorUserBuildSettings.activeBuildTarget);
    }
}

/*
 1. AssetBundle로 추출할 Asset의 AssetBundle의 이름을 생성 후 입력
 2. Editor상에서 Assets/AssetBundles메뉴를 이용하여
    Assets을 AssetsBundle로 추출
 3. Bundle Script를 이용하여 원하는 Asset으로 불러와서 사용
 */