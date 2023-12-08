using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Flowchart fc;
    public PlayerObjectives objectives;

    public void SaveObjectives(PlayerObjectives objctvs)
    {
        fc.SendFungusMessage("LoadData");
        Debug.Log("Saving Objectives...");
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SaveObjectives(objectives);
    }
}
