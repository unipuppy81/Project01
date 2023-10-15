using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Consumable/Health")]
public class HPpotion : ItemEffect
{

    public int healingPoint = 0;
    public override bool ExecuteRole()
    {
        Debug.Log("PlayerHP Add : " + healingPoint);

        return true;
    }
}
