using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillageCollider : MonoBehaviour
{
    public string Name;

    public void OnTriggerEnter(Collider other)
    {
        bool check = Shared.GameMgr.IsCheckCharacterType(other, (int)CHARACTER_TYPE.PLAYER);
        if(check)
        {
            Player Player;
            other.transform.TryGetComponent<Player>(out Player);
            if(Player != null)
            {
                if(Player.GetPVIsMine())
                {
                    VillageName villageName = Shared.PoolMgr.GetObject("VillageName").GetComponent<VillageName>();
                    villageName.Init(Name);
                }
            }
        }
    }
}
