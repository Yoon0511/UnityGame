using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character
{
    [SerializeField]
    Transform PetPos;

    Pet Pet;

    public Vector3 GetPetPos()
    {
        return PetPos.position;
    }

    public void EquipPet(Pet _pet)
    {
        Pet = _pet;
        Pet.transform.SetParent(PetPos, false);
        Pet.transform.localPosition = new Vector3(0, 0, 0);
        Pet.Equip();
    }

    public void UnEquipPet()
    {
        Pet.UnEquip();
        Destroy(Pet.gameObject);
        Pet = null;
    }
    public Pet GetPet()
    {
        return Pet;
    }
}
