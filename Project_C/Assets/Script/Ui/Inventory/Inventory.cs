using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SimpleJSON;
using Unity.VisualScripting;

public class Inventory : MonoBehaviour, IPointerClickHandler
{
    public int NUM_MAX_ITEM = 25;
    public GameObject ITEMSLOT;
    public GameObject PARENTGRID;
    public Text GOLDTEXT;
    Character Owner;

    [SerializeField]
    List<ItemBase> items = new List<ItemBase>();

    [SerializeField]
    List<InvenSlot> slots = new List<InvenSlot>();

    [SerializeField]
    GameObject Ui;

    [SerializeField]
    List<GameObject> SortButtons = new List<GameObject>();
    [SerializeField]
    GameObject SortOptionButton;

    bool[] IsSortOderByAsc = new bool[]{ false,false,false}; //0:���, 1:�̸�, 2:����
    bool IsExpanded = false; // �ɼ� Ȱ��ȭ ���̸� false
    bool IsSortOptionAnimation = false;

    [SerializeField]
    Dropdown SortDropdown;
    void Awake()
    {
        if (items.Count == 0)
        {
            for (int i = 0; i < NUM_MAX_ITEM; ++i)
            {
                InvenSlot instSlot = Instantiate(ITEMSLOT, PARENTGRID.transform).GetComponent<InvenSlot>();
                slots.Add(instSlot);
            }
        }
        Refresh();
    }
    void Init()
    {
        if (items.Count == 0)
        {
            for (int i = 0; i < NUM_MAX_ITEM; ++i)
            {
                InvenSlot instSlot = Instantiate(ITEMSLOT, PARENTGRID.transform).GetComponent<InvenSlot>();
                slots.Add(instSlot);
            }
        }
        Refresh();
    }

    public void Refresh()
    {
        int i = 0;
        for (; i < items.Count; ++i)
        {
            slots[i].InputItem(items[i]);
            //slots[i].Refresh(items[i]);
        }
        for(;i<NUM_MAX_ITEM;++i)
        {
            slots[i].InputItem(null);
            //slots[i].Refresh(null);
        }

        UpdateGoldText();
    }

    public void AddItem(ItemBase _item)
    {
        if(items.Count < NUM_MAX_ITEM)
        {
            if (Owner != null)
            {
                _item.Owner = Owner;
            }
            items.Add(_item);
            Refresh();
        }
    }

    public void DeleteItem(ItemBase _item)
    {
        items.Remove(_item);
        //_item.ReleaseObject();
        Refresh();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;
        InvenSlot slot = obj.transform.GetComponent<InvenSlot>();

        if (slot != null && slot.IsSlotItem() != null)
        {
            slot.OnClickSlot();
        }
    }

    public void SetOwner(Character _owner) { Owner = _owner; }
    public Character GetOwner() { return Owner; }
    public List<ItemBase> GetItems() { return items; }
    public void UpdateGoldText()
    {
        Player player = Owner as Player;
        if(player != null)
        {
            string Gold = player.GetGold().ToString();

            //���� ���
            GOLDTEXT.text = "<color=#FFD700><b>" + Gold + "</b></color>";
        }
    }

    public void OpenUi()
    {
        Ui.SetActive(true);
        Shared.SoundMgr.PlaySFX("UI_NOTIFICATION");
    }

    public void SortByGrade()
    {
        // ��޺� �������� ����
        if (IsSortOderByAsc[(int)SORT_TYPE.GRADE])
        {
            // �̹� �������� ���ĵǾ� �ִٸ� ������������ ����
            IsSortOderByAsc[(int)SORT_TYPE.GRADE] = false;
            items.Sort((a, b) => a.Grade.CompareTo(b.Grade));
        }
        else
        {
            // �������� ���ķ� ����
            IsSortOderByAsc[(int)SORT_TYPE.GRADE] = true;
            items.Sort((a, b) => b.Grade.CompareTo(a.Grade));
        }
        Refresh();
    }

    public void SortByName()
    {
        // �̸��� �������� ����
        if (IsSortOderByAsc[(int)SORT_TYPE.NAME])
        {
            // �̹� �������� ���ĵǾ� �ִٸ� ������������ ����
            IsSortOderByAsc[(int)SORT_TYPE.NAME] = false;
            items.Sort((a, b) => string.Compare(b.ItemName, a.ItemName));
        }
        else
        {
            // �������� ���ķ� ����
            IsSortOderByAsc[(int)SORT_TYPE.NAME] = true;
            items.Sort((a, b) => string.Compare(a.ItemName, b.ItemName));
        }
        Refresh();
    }

    public void SortByUseType()
    {
        // �̸��� �������� ����
        if (IsSortOderByAsc[(int)SORT_TYPE.NAME])
        {
            // �̹� �������� ���ĵǾ� �ִٸ� ������������ ����
            IsSortOderByAsc[(int)SORT_TYPE.NAME] = false;
            items.Sort((a, b) => b.ItemType.CompareTo(a.ItemType));
        }
        else
        {
            // �������� ���ķ� ����
            IsSortOderByAsc[(int)SORT_TYPE.NAME] = true;
            items.Sort((a, b) => a.ItemType.CompareTo(b.ItemType));
        }
        Refresh();
    }

    public void OnSortOption()
    {
        if(IsSortOptionAnimation == false)
        {
            StartCoroutine(IAnimationSortBtns());
        }
    }

    IEnumerator IAnimationSortBtns()
    {
        IsSortOptionAnimation = true;
        float t = 0.0f;
        float Duration = 0.3f;
       
        while (t < Duration)
        {
            t += Time.deltaTime;
            for (int i = 0; i < SortButtons.Count; ++i)
            {
                Vector3 basePos = SortOptionButton.transform.position;

                Vector3 from, to;

                if (!IsExpanded)
                {
                    // �ɼ� Open - ������ ����
                    from = basePos;
                    to = basePos + new Vector3(0, (i + 1) * -40f, 0);
                }
                else
                {
                    // �ɼ� Close - �ٽ� ���̴� ����
                    from = basePos + new Vector3(0, (i + 1) * -40f, 0);
                    to = basePos;
                }

                SortButtons[i].transform.position = Vector3.Lerp(from, to, t / Duration);
            }
            yield return null;
        }

        IsExpanded = !IsExpanded;
        IsSortOptionAnimation = false;
    }

    public void OnDropdownValueChanged(int _type)
    {
        SORT_TYPE Type = (SORT_TYPE)SortDropdown.value;
        // ���� �ɼ� ��ư Ŭ�� ��, �ش� Ÿ������ ����
        switch (Type)
        {
            case SORT_TYPE.GRADE:
                SortByGrade();
                break;
            case SORT_TYPE.NAME:
                SortByName();
                break;
            case SORT_TYPE.TYPE:
                SortByUseType();
                break;
            default:
                break;
        }
    }
    public InventoryJson ToJsonData()
    {
        InventoryJson json = new InventoryJson();

        Player player = Owner as Player;
        if (player != null)
        {
            json.Gold = player.GetGold();
        }

        foreach (ItemBase item in items)
        {
            json.ListItemId.Add(item.Id);
        }

        return json;
    }

    public void ApplyJsonData(InventoryJson _json)
    {
        for(int i = 0;i<_json.ListItemId.Count;++i)
        {
            int itemid = _json.ListItemId[i];
            AddItem(Shared.DataMgr.GetItem(itemid));
        }

        Player player = Owner as Player;
        if (player != null)
        {
            player.SetGold(_json.Gold);
        }
    }
}
