using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    [SerializeField]
    Inventory inventory;
    public Inventory GetInventory() { return inventory; }
}
