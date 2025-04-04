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
 1. AssetBundle�� ������ Asset�� AssetBundle�� �̸��� ���� �� �Է�
 2. Editor�󿡼� Assets/AssetBundles�޴��� �̿��Ͽ�
    Assets�� AssetsBundle�� ����
 3. Bundle Script�� �̿��Ͽ� ���ϴ� Asset���� �ҷ��ͼ� ���
 */