using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public Save[] Saves;
    public Save currentSave;
    public GameObject Player;
    public bool changeSaveState;
    public int SaveState;

    private void Start()
    {
        if (changeSaveState)
        {
            SetSaveInt(SaveState);
        }


        int saveInt = PlayerPrefs.GetInt("SavePhase");

        currentSave = Saves[saveInt];

        for (int i = 0; i < currentSave.gameObjectsToActive.Length; i++)
        {
            currentSave.gameObjectsToActive[i].SetActive(true);
        }

        currentSave.Trigger.Invoke();

        if (currentSave.changeInstruction)
        {
            ChangeInstruction.ChangeNoSound(currentSave.Instruction);
        }

        Player.transform.position = currentSave.PlayerSpawnPosition;
        Player.transform.rotation = currentSave.PlayerSpawnRotation;
    }

    public static void SetSaveInt(int save)
    {
        PlayerPrefs.SetInt("SavePhase", save);
    }
}
