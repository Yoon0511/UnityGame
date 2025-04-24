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
        //"slash" - asset에 지어준 AssetBundle의 이름을 정확히 입력해야함

        if (async == null)
        {
            Debug.LogError("에셋 번들 파일을 찾을 수 없습니다: ");
            yield break;
        }

        yield return async;

        AssetBundle local = async.assetBundle;

        if (local == null)
            yield break;

        //에셋의 데이터형을 명시해야함
        //<GameObject> - Object,GameObject,Matereal,Texture 등등
        AssetBundleRequest asset = local.LoadAssetAsync<GameObject>("Skill_Slash");
        //"Skill_Slash" - assetbundle로 만든 asset의 이름을 정확히 입력해야함

        yield return asset;

        if (asset == null)
        {
            Debug.LogError("에셋을 찾을 수 없습니다: Skill_Slash");
        }

        var prefab = asset.asset as GameObject;
        //GameObject - 필요한 데이터형으로 변환;
        Debug.Log(prefab);

        Instantiate(prefab);

        //GameObject obj = Instantiate(prefab);
        //obj.name = "LoadBundle";

        local.Unload(true);
    }
}