using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class GameManager
{
    private static GameManager instance;

    public int checkpointIndex = 0;
    public enum Checkpoints
    {
        NOCHECKPOINT,
        STARTINGSTATUE,
        CEARINGSTATUE,
        CAMPSITESTATUE,
    }
    public Vector3 wendigoSaveLocation = new Vector3();
    public Vector3 playerSaveLocation = new Vector3();
    public Checkpoints currentCheckpoint;
    public GameObject player;
    public GameObject wendigo;
    public GameObject activeWendigo;
    public GameObject[] wendigoCreatures;
    public NavMeshAgent[] wendigoNMAs;
    public float[] wendigoLoadedX;
    public float[] wendigoLoadedY;
    public float[] wendigoLoadedZ;
    private float wLoadX = 0;
    private float wLoadY = 0;
    private float wLoadZ = 0;
    private float pLoadX = 0;
    private float pLoadY = 0;
    private float pLoadZ = 0;
    public int currentWendigo = 0;
    public int activeWendigoIndex;
    public bool gameSaved = false;
    public bool noCheckpoint = false;
    public bool interactWithStatue = false;
    public bool checkpointLoaded = false;
    public bool GameOver = false;
    public bool GamePaused = false;
    public bool atStatue = false;
    public bool atHidingSpot = false;
    public bool showSaveText = false;
    public bool wendigoChasing = false;
    public bool wendigoRoaming = false;
    public bool finishedChasing = false;
    public bool outOfPlayerView = true;

    public void DoGameOver(GameObject wendigoRef, GameObject playerRef)
    {
        wendigo = wendigoRef;
        player = playerRef;
        if (GameOver == false)
        {
            GameOver = true;
        }
        if (checkpointIndex == 0)
        {
            CheckForSaveFile();
        }
        if (checkpointIndex != 0)
        {
            noCheckpoint = false;
            //LoadCheckpointData(wendigoRef, playerRef);
        }
        else
        {
            noCheckpoint = true;
            Debug.Log("No Checkpoint Detected... Returning To Main Menu...");
        }
    }

    public void CheckForSaveFile()
    {
        int lineNumber = 16;
        string path = Application.persistentDataPath + "/SaveData.txt";
        Debug.Log(path);
        if (File.Exists(path) || File.Exists(Application.persistentDataPath + "/filesSaveData.txt"))
        {
            using (StreamReader sr = new StreamReader(path))
            {
                for (int i = 0; i <= lineNumber; i++)
                {
                    // Table of contents:
                    // Cases 0 -> 11 = positions of all of the Wendigos in the game.
                    // Cases 12 -> 14 = player positions.
                    // Case 15 = checkpoint index.
                    // Case 16 = active Wendigo index.

                    switch (i)
                    {
                        // Line 1: (1)Wendigo X Position.                         
                        case 0:                                                   
                            {                                                     
                                string lineContents = sr.ReadLine();              
                                float.TryParse(lineContents, out wLoadX);         
                                Debug.Log("Loaded Wendigo X Position: " + wLoadX);
                                wendigoLoadedX[0] = wLoadX;                       
                                break;                                            
                            }                                                     
                        // Line 2: (1)Wendigo Y Position.                         
                        case 1:                                                   
                            {                                                     
                                string lineContents = sr.ReadLine();              
                                float.TryParse(lineContents, out wLoadY);         
                                Debug.Log("Loaded Wendigo Y Position: " + wLoadY);
                                wendigoLoadedY[0] = wLoadY;                       
                                break;                                            
                            }                                                     
                        // Line 3: (1)Wendigo Z Position.                         
                        case 2:                                                   
                            {                                                     
                                string lineContents = sr.ReadLine();              
                                float.TryParse(lineContents, out wLoadZ);         
                                Debug.Log("Loaded Wendigo Z Position: " + wLoadZ);
                                wendigoLoadedZ[0] = wLoadZ;                       
                                break;                                            
                            }                                                     
                        // Line 4: (2)Wendigo X Position.                         
                        case 3:                                                   
                            {                                                     
                                string lineContents = sr.ReadLine();              
                                float.TryParse(lineContents, out wLoadX);         
                                Debug.Log("Loaded Wendigo X Position: " + wLoadX);
                                wendigoLoadedX[1] = wLoadX;                       
                                break;                                            
                            }                                                     
                        // Line 5: (2)Wendigo Y Position.                         
                        case 4:                                                   
                            {                                                     
                                string lineContents = sr.ReadLine();              
                                float.TryParse(lineContents, out wLoadY);         
                                Debug.Log("Loaded Wendigo Y Position: " + wLoadY);
                                wendigoLoadedY[1] = wLoadY;                       
                                break;                                            
                            }                                                     
                        // Line 6: (2)Wendigo Z Position.                         
                        case 5:                                                   
                            {                                                     
                                string lineContents = sr.ReadLine();              
                                float.TryParse(lineContents, out wLoadZ);         
                                Debug.Log("Loaded Wendigo Z Position: " + wLoadZ);
                                wendigoLoadedZ[1] = wLoadZ;                       
                                break;                                            
                            }                                                     
                        // Line 7: (3)Wendigo X Position.                         
                        case 6:                                                   
                            {                                                     
                                string lineContents = sr.ReadLine();              
                                float.TryParse(lineContents, out wLoadX);         
                                Debug.Log("Loaded Wendigo X Position: " + wLoadX);
                                wendigoLoadedX[2] = wLoadX;                       
                                break;                                            
                            }                                                     
                        // Line 8: (3)Wendigo Y Position.                         
                        case 7:                                                   
                            {                                                     
                                string lineContents = sr.ReadLine();              
                                float.TryParse(lineContents, out wLoadY);         
                                Debug.Log("Loaded Wendigo Y Position: " + wLoadY);
                                wendigoLoadedY[2] = wLoadY;                       
                                break;                                            
                            }                                                     
                        // Line 9: (3)Wendigo Z Position.                         
                        case 8:                                                   
                            {                                                     
                                string lineContents = sr.ReadLine();              
                                float.TryParse(lineContents, out wLoadZ);         
                                Debug.Log("Loaded Wendigo Z Position: " + wLoadZ);
                                wendigoLoadedZ[2] = wLoadZ;                       
                                break;                                            
                            }                                                     
                        // Line 10: (4)Wendigo X Position.                        
                        case 9:                                                   
                            {                                                     
                                string lineContents = sr.ReadLine();              
                                float.TryParse(lineContents, out wLoadX);         
                                Debug.Log("Loaded Wendigo X Position: " + wLoadX);
                                wendigoLoadedX[3] = wLoadX;                       
                                break;                                            
                            }                                                     
                        // Line 11: (4)Wendigo Y Position.                        
                        case 10:                                                  
                            {                                                     
                                string lineContents = sr.ReadLine();              
                                float.TryParse(lineContents, out wLoadY);         
                                Debug.Log("Loaded Wendigo Y Position: " + wLoadY);
                                wendigoLoadedY[3] = wLoadY;                       
                                break;                                            
                            }                                                     
                        // Line 12: (4)Wendigo Z Position.                        
                        case 11:                                                  
                            {                                                     
                                string lineContents = sr.ReadLine();              
                                float.TryParse(lineContents, out wLoadZ);         
                                Debug.Log("Loaded Wendigo Z Position: " + wLoadZ);
                                wendigoLoadedZ[3] = wLoadZ;                       
                                break;                                            
                            }                                                     
                        // Line 13: Player X Position.
                        case 12:
                            {
                                string lineContents = sr.ReadLine();
                                float.TryParse(lineContents, out pLoadX);
                                Debug.Log("Loaded Player X Position: " + pLoadX);
                                break;
                            }
                        // Line 14: Player Y Position.
                        case 13:
                            {
                                string lineContents = sr.ReadLine();
                                float.TryParse(lineContents, out pLoadY);
                                Debug.Log("Loaded Player Y Position: " + pLoadY);
                                break;
                            }
                        // Line 15: Player Z Position.
                        case 14:
                            {
                                string lineContents = sr.ReadLine();
                                float.TryParse(lineContents, out pLoadZ);
                                Debug.Log("Loaded Player Z Position: " + pLoadZ);
                                break;
                            }
                        // Line 16: Latest Checkpoint Index.
                        case 15:
                            {
                                string lineContents = sr.ReadLine();
                                int.TryParse(lineContents, out checkpointIndex);
                                Debug.Log("Loaded Checkpoint Index: " + checkpointIndex);
                                break;
                            }
                        case 16:
                            {
                                string lineContents = sr.ReadLine();
                                int.TryParse(lineContents, out activeWendigoIndex);
                                Debug.Log("Loaded Active Wendigo Index: " + activeWendigoIndex);
                                break;
                            }
                    }
                }
            }
        }
    }

    public void SaveCheckpointData(GameObject wendigoRef, GameObject playerRef)
    {
        //CheckForSaveFile();
        wendigo = wendigoRef;
        player = playerRef;
        gameSaved = false;
        string path = Application.persistentDataPath + "/SaveData.txt";
        StreamWriter w = new StreamWriter(path);
        for (int i = 0; i <= wendigoCreatures.Length - 1; i++)
        {
            if (wendigoCreatures[i].activeSelf == true)
            {
                // Is the active creature.
                activeWendigoIndex = i;
                break;
            }
            else
            {
                // Is not the active creature.
            }
        }
        for (int i = 0; i < wendigoCreatures.Length; i++)
        {
            w.WriteLine(wendigoCreatures[i].transform.position.x);
            w.WriteLine(wendigoCreatures[i].transform.position.y);
            w.WriteLine(wendigoCreatures[i].transform.position.z);
        }
        w.WriteLine(player.transform.position.x);
        w.WriteLine(player.transform.position.y);
        w.WriteLine(player.transform.position.z);
        w.WriteLine(checkpointIndex);
        w.WriteLine(activeWendigoIndex);
        w.Close();
        gameSaved = true;
    }

    public void LoadCheckpointData(GameObject wendigoRef, GameObject playerRef)
    {
        GameOver = false;
        checkpointLoaded = false;

        CheckForSaveFile();
        for (int i = 0; i < 4; i++)
        {
            wendigoNMAs[i] = wendigoCreatures[i].GetComponent<NavMeshAgent>();
        }
        NavMeshAgent wendigonma = wendigoRef.GetComponent<NavMeshAgent>();
        PlayerController pCont = playerRef.GetComponent<PlayerController>();


        pCont.enabled = false;
        for (int i = 0; i < wendigoCreatures.Length; i++)
        {
            wendigoNMAs[i].enabled = false;
        }

        wendigoCreatures[0].transform.position = new Vector3(wendigoLoadedX[0], wendigoLoadedY[0], wendigoLoadedZ[0]);
        wendigoCreatures[1].transform.position = new Vector3(wendigoLoadedX[1], wendigoLoadedY[1], wendigoLoadedZ[1]);
        wendigoCreatures[2].transform.position = new Vector3(wendigoLoadedX[2], wendigoLoadedY[2], wendigoLoadedZ[2]);
        wendigoCreatures[3].transform.position = new Vector3(wendigoLoadedX[3], wendigoLoadedY[3], wendigoLoadedZ[3]);
        //wendigoNMAs[activeWendigoIndex].enabled = true;

        for (int i = 0; i < wendigoCreatures.Length; i++)
        {
            wendigoNMAs[i].enabled = true;
        }

        activeWendigo = wendigoCreatures[activeWendigoIndex];

        playerSaveLocation = new Vector3(pLoadX, pLoadY, pLoadZ);
        player.transform.position = playerSaveLocation;
        pCont.enabled = true;

        checkpointLoaded = true;
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
                        checkpointIndex = 1;
                        Debug.Log("Checkpoint Updated - " + currentCheckpoint);
                    }
                    break;
                }
            case 2:
                {
                    if (currentCheckpoint != Checkpoints.CEARINGSTATUE)
                    {
                        currentCheckpoint = Checkpoints.CEARINGSTATUE;
                        checkpointIndex = 2;
                        Debug.Log("Checkpoint Updated - " + currentCheckpoint);
                    }
                    break;
                }
            case 3:
                {
                    if (currentCheckpoint != Checkpoints.CAMPSITESTATUE)
                    {
                        currentCheckpoint = Checkpoints.CAMPSITESTATUE;
                        checkpointIndex = 3;
                        Debug.Log("Checkpoint Updated - " + currentCheckpoint);
                    }
                    break;
                }
        }
    }

    public static GameManager Instance { get { if (instance == null) { instance = new GameManager(); } return instance; } }


}
