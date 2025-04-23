using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuffUi : MonoBehaviour
{
    Player Player;
    List<KeyValuePair<Buff,Image>> BuffKvpList = new List<KeyValuePair<Buff, Image>>();
    bool IsRunningCoroutine = false;
    public GameObject BUFFICON_PREFAP;
    Character Target = null;

    private void Start()
    {
        Player = Shared.GameMgr.PLAYER;
    }

    public void AddBuff(Buff _buff, Character _Target)
    {
        if(Target == null)
        {
            Target = _Target;
        }

        GameObject obj = Instantiate(BUFFICON_PREFAP);
        obj.transform.SetParent(transform);
        obj.GetComponent<Image>().sprite = Shared.GameMgr.GetSpriteAtlas("Skill_Icons", _buff.BuffIconName);
        KeyValuePair<Buff, Image> kvp = new KeyValuePair<Buff, Image>(_buff, obj.GetComponent<Image>());
        BuffKvpList.Add(kvp);

        if (BuffKvpList.Count > 0)
        {
            if (IsRunningCoroutine == false)
            {
                StartCoroutine(IBuffUiCoroutine());
            }
        }
    }

    IEnumerator IBuffUiCoroutine()
    {
        IsRunningCoroutine = true;
        while (true)
        {
            for (int i = BuffKvpList.Count - 1; i >= 0; i--)
            {
                float time = 0.0f;
                //bool IsGetBuff = Player.BuffSystem.GetDicBuff().TryGetValue(BuffKvpList[i].Key, out time);
                bool IsGetBuff = Target.BuffSystem.GetDicBuff().TryGetValue(BuffKvpList[i].Key, out time);

                if (IsGetBuff)
                {
                    BuffKvpList[i].Value.fillAmount = time / BuffKvpList[i].Key.Duration;

                    if (time <= 0.1f)
                    {
                        Destroy(BuffKvpList[i].Value.gameObject);
                        BuffKvpList.Remove(BuffKvpList[i]);
                    }
                }
            }

            //foreach (var buff in BuffKvpList)
            //{
            //    float time = 0.0f;
            //
            //    bool IsGetBuff = Player.BuffSystem.GetDicBuff().TryGetValue(buff.Key, /out /time);
            //
            //    if(IsGetBuff)
            //    {
            //        buff.Value.fillAmount = time / buff.Key.Duration;
            //
            //        if(time <= 0.1f)
            //        {
            //            BuffKvpList.Remove(buff);
            //        }
            //    }
            //}
            if (BuffKvpList.Count <= 0)
            {
                IsRunningCoroutine = false;
                yield break;
            }
            yield return null;
        }
    }

    public void UiReset()
    {
        if(IsRunningCoroutine)
        {
            StopCoroutine(IBuffUiCoroutine());
        }
        
        for (int i = BuffKvpList.Count - 1; i >= 0; i--)
        {
            Destroy(BuffKvpList[i].Value.gameObject);
            BuffKvpList.Remove(BuffKvpList[i]);
        }

        BuffKvpList.Clear();
    }

    public void SetTarget(Character _target)
    {
        UiReset();

        Target = _target;
        foreach(Buff buff in Target.BuffSystem.GetDicBuff().Keys)
        {
            AddBuff(buff, Target);
        }
    }

    public int GetTargetBuffCount() { return BuffKvpList.Count; }
}
