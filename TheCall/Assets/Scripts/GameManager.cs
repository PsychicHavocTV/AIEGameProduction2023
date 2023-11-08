using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager instance;

    public int checkpointIndex = 0;
    public enum Checkpoints
    {
        STARTINGSTATUE,
        CEARINGSTATUE,
        CAMPSITESTATUE,
    }

    public Vector3 playerSaveLocation = new Vector3();

    public Checkpoints currentCheckpoint;

    public void SaveCheckpointData()
    {

    }

    public void LoadCheckpointData()
    {

    }

    public void UpdateCheckpoint(int checkpoint)
    {
        switch (checkpoint)
        {
            case 1:
                {
                    if (currentCheckpoint != Checkpoints.STARTINGSTATUE)
                    {
                        currentCheckpoint = Checkpoints.STARTINGSTATUE;
                        Debug.Log("Checkpoint Updated - " + currentCheckpoint);
                    }
                    break;
                }
            case 2:
                {
                    if (currentCheckpoint != Checkpoints.CEARINGSTATUE)
                    {
                        currentCheckpoint = Checkpoints.CEARINGSTATUE;
                        Debug.Log("Checkpoint Updated - " + currentCheckpoint);
                    }
                    break;
                }
            case 3:
                {
                    if (currentCheckpoint != Checkpoints.CAMPSITESTATUE)
                    {
                        currentCheckpoint = Checkpoints.CAMPSITESTATUE;
                        Debug.Log("Checkpoint Updated - " + currentCheckpoint);
                    }
                    break;
                }
        }
    }

    public static GameManager Instance { get { if (instance == null) { instance = new GameManager(); } return instance; } }


}
