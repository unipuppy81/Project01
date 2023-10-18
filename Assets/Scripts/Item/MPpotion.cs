using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Consumable/Mana")]
public class MPpotion : ItemEffect
{
    public int manaHealing = 0;
    public override bool ExecuteRole()
    {
        Debug.Log("PlayerMP Add : " + manaHealing);

        return true;
    }
}
