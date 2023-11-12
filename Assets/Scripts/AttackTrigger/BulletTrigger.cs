using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    public bool IsAttack = false;

    private Player player;

    // Use this for initialization
    void Start()
    {
        player = GameSceneManager.Instance.Player;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            IsAttack = true;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
            IsAttack = false;
    }
}
