using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using UnityEngine.EventSystems;

public class EquipmentWindow : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    SerializedDictionary<EQUITMENT_TYPE, EquipmentSlot> dicEquitmentSlot = new SerializedDictionary<EQUITMENT_TYPE, EquipmentSlot>();

    [SerializeField]
    InfoPlayer infoPlayer;
    Player Player;

    private void Start()
    {
        Player = Shared.GameMgr.PLAYER;
    }

    public void EquippedItem(EquipmentItem _equipmentItem)
    {
        if(Player == null)
        {
            Player = Shared.GameMgr.PLAYER;
        }
        Player.GetDicEquitmentItem().Add(_equipmentItem.EquimentType, _equipmentItem);
        dicEquitmentSlot[_equipmentItem.EquimentType].InputEquipmentItem(_equipmentItem);
        Player.ApplyEquipItem(_equipmentItem);
        infoPlayer.Refresh();
    }

    public void Unequip(EQUITMENT_TYPE _type)
    {
        Player.ApplyEquipItem(Player.GetDicEquitmentItem()[_type], true);
        Player.GetDicEquitmentItem().Remove(_type);
        infoPlayer.Refresh();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;
        EquipmentSlot slot = obj.transform.GetComponent<EquipmentSlot>();

        if (slot != null && slot.IsSlotItem() != null)
        {
            slot.OnClickSlot();
        }
    }
}
