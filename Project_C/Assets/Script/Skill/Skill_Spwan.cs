using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using UnityEngine;

public class Skill_Spwan : Skill
{
    public string MonsterName;
    public int Count;
    public override void UseSkill()
    {
        base.UseSkill();
        SpwanMonster();
    }

    void SpwanMonster()
    {
        float Spacing = 6.0f;
        Vector3 Origin = Owner.transform.position;
        Vector3 Right = Owner.transform.right.normalized;

        int Half = Count / 2;
        for (int i = 0; i<Count; ++i)
        {
            float OffsetIndex = i - Half;

            if (Count % 2 == 0)
                OffsetIndex += 0.5f;

            Vector3 spawnPos = Origin + Right * (OffsetIndex * Spacing);

            PhotonNetwork.Instantiate($"Prefabs/Monster/{MonsterName}/{MonsterName}", spawnPos, Owner.transform.rotation, 0);
        }
        base.SkillEnd();
    }
}
