using System;
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
    public PlayerObjectives playerObjectives;
    public GameObject wendigo;
    private float wLoadX = 0;
    private float wLoadY = 0;
    private float wLoadZ = 0;
    private float pLoadX = 0;
    private float pLoadY = 0;
    private float pLoadZ = 0;
    public bool gameSaved = false;
    public bool checkpointLoaded = false;
    public bool GameOver = false;

    public static Dictionary<String, Int32> guidInstanceMapping = new Dictionary<String, int>();
    public static Dictionary<String, GameObject> guidObjectMapping = new Dictionary<String, GameObject>();

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
            LoadCheckpointData(wendigoRef, playerRef);
        }
        else
        {
            Debug.Log("No Checkpoint Detected... Returning To Main Menu...");
        }
    }

    public void CheckForSaveFile()
    {
        int lineNumber = 7;
        string path = Application.persistentDataPath + "/SaveData.txt";
        Debug.Log(path);
        if (File.Exists(path) || File.Exists(Application.persistentDataPath + "/filesSaveData.txt"))
        {
            using (StreamReader sr = new StreamReader(path))
            {
                for (int i = 0; i <= lineNumber; i++)
                {
                    switch (i)
                    {
                        // Line 1: Wendigo X Position.
                        case 0:
                            {
                                string lineContents = sr.ReadLine();
                                float.TryParse(lineContents, out wLoadX);
                                Debug.Log("Loaded Wendigo X Position: " + wLoadX);
                                break;
                            }
                        // Line 2: Wendigo Y Position.
                        case 1:
                            {
                                string lineContents = sr.ReadLine();
                                float.TryParse(lineContents, out wLoadY);
                                Debug.Log("Loaded Wendigo Y Position: " + wLoadY);
                                break;
                            }
                        // Line 3: Wendigo Z Position.
                        case 2:
                            {
                                string lineContents = sr.ReadLine();
                                float.TryParse(lineContents, out wLoadZ);
                                Debug.Log("Loaded Wendigo Z Position: " + wLoadZ);
                                break;
                            }
                        // Line 4: Player X Position.
                        case 3:
                            {
                                string lineContents = sr.ReadLine();
                                float.TryParse(lineContents, out pLoadX);
                                Debug.Log("Loaded Player X Position: " + pLoadX);
                                break;
                            }
                        // Line 5: Player Y Position.
                        case 4:
                            {
                                string lineContents = sr.ReadLine();
                                float.TryParse(lineContents, out pLoadY);
                                Debug.Log("Loaded Player Y Position: " + pLoadY);
                                break;
                            }
                        // Line 6: Player Z Position.
                        case 5:
                            {
                                string lineContents = sr.ReadLine();
                                float.TryParse(lineContents, out pLoadZ);
                                Debug.Log("Loaded Player Z Position: " + pLoadZ);
                                break;
                            }
                        // Line 7: Latest Checkpoint Index.
                        case 6:
                            {
                                string lineContents = sr.ReadLine();
                                int.TryParse(lineContents, out checkpointIndex);
                                Debug.Log("Loaded Checkpoint Index: " + checkpointIndex);
                                break;
                            }
                    }
                }
            }
        }
    }

    public void SaveCheckpointData(GameObject wendigoRef, GameObject playerRef, PlayerObjectives playerObjectivesRef)
    {
        //CheckForSaveFile();
        gameSaved = false;

        wendigo = wendigoRef;
        player = playerRef;
        playerObjectives = playerObjectivesRef;

        string path = Application.persistentDataPath + "/SaveData.txt";
        StreamWriter w = new StreamWriter(path);
        w.WriteLine(wendigo.transform.position.x);
        w.WriteLine(wendigo.transform.position.y);
        w.WriteLine(wendigo.transform.position.z);
        w.WriteLine(player.transform.position.x);
        w.WriteLine(player.transform.position.y);
        w.WriteLine(player.transform.position.z);
        w.WriteLine(checkpointIndex);
        w.Close();

        gameSaved = true;
    }

    public void LoadCheckpointData(GameObject wendigoRef, GameObject playerRef)
    {
        GameOver = false;
        checkpointLoaded = false;

        CheckForSaveFile();
        NavMeshAgent wendigonma = wendigoRef.GetComponent<NavMeshAgent>();

        wendigonma.enabled = false;
        wendigoSaveLocation = new Vector3(wLoadX, wLoadY, wLoadZ);
        wendigo.transform.position = wendigoSaveLocation;
        wendigonma.enabled = true;

        playerSaveLocation = new Vector3(pLoadX, pLoadY, pLoadZ);
        player.transform.position = playerSaveLocation;

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

    // https://www.gamedeveloper.com/programming/building-a-simple-system-for-persistence-with-unity
    public static void DeregisterGUID(String guid)
    {
        guidInstanceMapping.Remove(guid);
        guidObjectMapping.Remove(guid);
    }

    public static void RegisterInstanceGUID(String guid, Int32 instanceID)
    {
        if (!guidInstanceMapping.ContainsKey(guid))
            guidInstanceMapping.Add(guid, instanceID);
    }

    public static void RegisterObjectGUID(String guid, GameObject instance)
    {
        if (!guidObjectMapping.ContainsKey(guid))
            guidObjectMapping.Add(guid, instance);
    }

    public static Int32 GetInstanceID(String guid)
    {
        return guidInstanceMapping[guid];
    }

    public static GameManager Instance { get { if (instance == null) { instance = new GameManager(); } return instance; } }


}
