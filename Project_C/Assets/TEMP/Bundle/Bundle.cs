using System.Collections;
using UnityEngine;
using System.IO;

public class Bundle : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ILoad());
    }

    IEnumerator ILoad()
    {
        AssetBundleCreateRequest async = AssetBundle.LoadFromFileAsync(
            Path.Combine(Application.streamingAssetsPath, "slash"));
        //"slash" - asset�� ������ AssetBundle�� �̸��� ��Ȯ�� �Է��ؾ���

        if (async == null)
        {
            Debug.LogError("���� ���� ������ ã�� �� �����ϴ�: ");
            yield break;
        }

        yield return async;

        AssetBundle local = async.assetBundle;

        if (local == null)
            yield break;

        //������ ���������� ����ؾ���
        //<GameObject> - Object,GameObject,Matereal,Texture ���
        AssetBundleRequest asset = local.LoadAssetAsync<GameObject>("Skill_Slash");
        //"Skill_Slash" - assetbundle�� ���� asset�� �̸��� ��Ȯ�� �Է��ؾ���

        yield return asset;

        if (asset == null)
        {
            Debug.LogError("������ ã�� �� �����ϴ�: Skill_Slash");
        }

        var prefab = asset.asset as GameObject;
        //GameObject - �ʿ��� ������������ ��ȯ;
        Debug.Log(prefab);

        Instantiate(prefab);

        //GameObject obj = Instantiate(prefab);
        //obj.name = "LoadBundle";

        local.Unload(true);
    }
}