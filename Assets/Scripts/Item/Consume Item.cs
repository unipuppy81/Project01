using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumeItemType
{
    HealthPotion,
    ManaPotion,
    Etc
}

public class ConsumeItem : MonoBehaviour
{
    public ConsumeItemType consumeType;

}
