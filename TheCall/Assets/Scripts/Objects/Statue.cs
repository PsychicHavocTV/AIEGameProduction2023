using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Statue : MonoBehaviour
{
    public bool canInteract = true;
    [SerializeField]
    private int statueIndex = 0;
    [SerializeField]
    private GameObject playerParentRef;
    [SerializeField]
    private GameObject wendigo;
    [SerializeField]
    private GameObject[] wendigoCreatures;
    [SerializeField]
    private CharacterController playerCharacterController;
    [SerializeField]
    private StatueInteract sI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController pc = playerParentRef.GetComponent<PlayerController>();
            pc.statueInteraction = sI;
            pc.statueInteraction.statueInteractSound = sI.statueInteractSound;
            Debug.Log("Player Can Now Interact.");
            if (GameManager.Instance.atStatue == false)
            {
                GameManager.Instance.atStatue = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player Can No Longer Interact");
            if (GameManager.Instance.atStatue == true)
            {
                GameManager.Instance.atStatue = false;
            }
        }
    }

    public void SaveCheckpoint()
    {
        Debug.Log("***SAVING GAME DATA***");
        GameManager.Instance.activeWendigo = wendigo;
        for (int i = 0; i <= wendigoCreatures.Length - 1; i++)
        {
            GameManager.Instance.wendigoCreatures[i] = wendigoCreatures[i];
        }
        GameManager.Instance.player = playerParentRef;
        GameManager.Instance.UpdateCheckpoint(statueIndex);
        GameManager.Instance.SaveCheckpointData(wendigo, playerParentRef);
        PlayerController pController;
        pController = playerParentRef.GetComponent<PlayerController>();
        if (pController.enabled == false)
        {
            pController.enabled = true;
        }
        GameManager.Instance.showSaveText = true;
    }




    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.player = playerParentRef;
    }

    void Update()
    {
        //if (wendigo != GameManager.Instance.activeWendigo && GameManager.Instance.activeWendigo != null)
        //{
        //    wendigo = GameManager.Instance.activeWendigo;
        //}
        if (GameManager.Instance.interactWithStatue == true)
        {
            GameManager.Instance.interactWithStatue = false;
            SaveCheckpoint();
        }
    }
}
