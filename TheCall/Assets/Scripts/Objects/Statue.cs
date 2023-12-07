using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public StatueInteract interaction;
    public bool canInteract = true;
    [SerializeField]
    private int statueIndex = 0;
    [SerializeField]
    private GameObject playerParentRef;
    [SerializeField]
    private GameObject wendigo;
    [SerializeField]
    private CharacterController playerCharacterController;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController pc = playerParentRef.GetComponent<PlayerController>();
            pc.statueInteractions = interaction;
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
            PlayerController pc = playerParentRef.GetComponent<PlayerController>();
            pc.statueInteractions = null;
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
        GameManager.Instance.wendigo = wendigo;
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
    }

    void Update()
    {
        if (GameManager.Instance.interactWithStatue == true)
        {
            GameManager.Instance.interactWithStatue = false;
            SaveCheckpoint();
        }
    }
}
