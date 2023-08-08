using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    [SerializeField] GameObject Cursor1;
    [SerializeField] GameObject Cursor2;

    private GameObject cur1 = null;
    private GameObject cur2 = null;


    public bool cursor01Changed = false;
    public bool cursor02Changed = false;

    void Start()
    {
         cur1 = Instantiate(Cursor1,new Vector3(0, 0, 0), Quaternion.identity);
         //cur2 = Instantiate(Cursor2,new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

        if (cursor01Changed) { cur1.SetActive(true); }
        else { cur1.SetActive(false); }


        if (cursor02Changed) { Cursor2.SetActive(true); }
        else { Cursor2.SetActive(false); }


    }
}
