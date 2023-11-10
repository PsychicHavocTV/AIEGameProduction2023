using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private CharacterController playerCharacterController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player Can Now Interact.");
            GameManager.Instance.wendigo = wendigo;
            GameManager.Instance.player = playerParentRef;
            GameManager.Instance.UpdateCheckpoint(statueIndex);
            GameManager.Instance.SaveCheckpointData();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player Can No Longer Interact");
        }
    }




    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            playerCharacterController.enabled = false;
            GameManager.Instance.LoadCheckpointData();
            //GameManager.Instance.player.transform.position = new Vector3(GameManager.Instance.playerSaveLocation.x, GameManager.Instance.playerSaveLocation.y, GameManager.Instance.playerSaveLocation.z);
            playerCharacterController.enabled = true;
            //Debug.Log(GameManager.Instance.player.transform.position);
        }
    }
}
