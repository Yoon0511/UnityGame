using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;

public partial class GameMgr
{
    public void MonsterSpawn()
    {
        List<Monster> Remove = new List<Monster>();
        for (int i = 0; i < ListMonster.Count; ++i)
        {
            string path = null;
            if (GetMonsterPath(ListMonster[i],out path))
            {
                PhotonNetwork.Instantiate(path, ListMonster[i].transform.position, Quaternion.identity, 0);
                Remove.Add(ListMonster[i]);
            }
        }

        foreach (var monster in Remove)
        {
            ListMonster.Remove(monster);
            Destroy(monster.gameObject);
        }
    }

    public bool GetMonsterPath(Monster _monster,out string _path)
    {
        _path = null;
        switch ((MONSTER_ID)_monster.GetId())
        {
            case MONSTER_ID.GOLEM:
                {
                    _path = "Prefabs/Monster/Golem/" + _monster.name;
                    return true;
                }
            case MONSTER_ID.WOLF:
                {
                    Wolf wolf = null;
                    if(_monster.TryGetComponent<Wolf>(out wolf))
                    {
                        if(wolf.GetIsLeader())
                        {
                            _path = "Prefabs/Monster/Wolf/Wolf_leader";
                            return true;
                        }
                        else
                        {
                            _path = "Prefabs/Monster/Wolf/Wolf";
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            default:
                return false;
        }
    }

    public void RemoveLocalMonster()
    {
        for (int i = ListMonster.Count - 1; i >= 0; i--)
        {
            GameObject monster = ListMonster[i].gameObject;
            ListMonster.RemoveAt(i);
            Destroy(monster);
        }
    }
}
