using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using UnityEngine.EventSystems;

public class EquipmentWindow : MonoBehaviour, IPointerClickHandler
{
    public SerializedDictionary<EQUITMENT_TYPE, EquipmentItem> dicEquitmentItem = new SerializedDictionary<EQUITMENT_TYPE, EquipmentItem>();

    [SerializeField]
    SerializedDictionary<EQUITMENT_TYPE, EquipmentSlot> dicEquitmentSlot = new SerializedDictionary<EQUITMENT_TYPE, EquipmentSlot>();

    [SerializeField]
    InfoPlayer infoPlayer;

    Player player;

    private void Start()
    {
        player = Shared.GameMgr.PLAYER.GetComponent<Player>();
    }

    public void EquippedItem(EquipmentItem _equipmentItem)
    {
        dicEquitmentItem.Add(_equipmentItem.equimentType, _equipmentItem);
        dicEquitmentSlot[_equipmentItem.equimentType].InputEquipmentItem(_equipmentItem);
        player.ApplyEquipItem(_equipmentItem);
        infoPlayer.Refresh();
    }

    public void Unequip(EQUITMENT_TYPE _type)
    {
        player.ApplyEquipItem(dicEquitmentItem[_type], true);
        dicEquitmentItem.Remove(_type);
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
