
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TableMgr
{
    //public TableCharacter Character = new TableCharacter();
    public TableItem Item = new TableItem();
    public void Init()
    {
#if UNITY_EDITOR
        //Character.Init_CSV("Character", 1, 0);
#else
        Character.Init_Binary("Character");
#endif
        Item.Init_CSV(1, 0);
    }

    public void Save()
    {
        //Character.Save_Binary("Character");

#if UNITY_EDITOR
        //AssetDatabase.Refresh();
#endif
    }
}
