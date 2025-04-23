using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    public Dictionary<Buff, float> DicBuffs = new Dictionary<Buff, float>();
    bool IsRunningCoroutine = false;

    public void AddBuff(Buff _buff)
    {
        if (DicBuffs.ContainsKey(_buff))
        {
            DicBuffs[_buff] = _buff.Duration;
        }
        else
        {
            _buff.ApplyBuff();
            DicBuffs.Add(_buff, _buff.Duration);
        }
        if (IsRunningCoroutine == false)
        {
            StartCoroutine(IBuffCoroutine());
        }
    }
    public void RemoveBuff(Buff _buff) { DicBuffs.Remove(_buff); }
    public void ClearBuff() { DicBuffs.Clear(); }
    public Dictionary<Buff, float> GetDicBuff() { return DicBuffs; }

    IEnumerator IBuffCoroutine()
    {
        IsRunningCoroutine = true;
        while (true)
        {
            UpdateBuff();
            yield return null;

            if (DicBuffs.Count == 0)
            {
                IsRunningCoroutine = false;
                yield break;
            }
        }
    }
    public void UpdateBuff()
    {
        foreach (var buff in DicBuffs.ToList())
        {
            buff.Key.UpdateBuff();
            DicBuffs[buff.Key] -= Time.deltaTime; // 남은 시간 감소
            if (buff.Value <= 0)
            {
                buff.Key.EndBuff();
                DicBuffs.Remove(buff.Key); // 삭제
            }
        }
    }
       
    public void ApplyBuff()
    {
        foreach (var buff in DicBuffs)
        {
            buff.Key.ApplyBuff();
        }
    }

    public void EndBuff()
    {
        foreach (var buff in DicBuffs)
        {
            buff.Key.EndBuff();
        }
    }
}
