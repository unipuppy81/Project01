using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public AudioClip TownSound;


    void Start()
    {
        SoundManager.Instance.PlaySound(TownSound);
    }
}
