using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyLight : MonoBehaviour
{
    [SerializeField] private Light[] areaLights;
    [SerializeField] private Material[] stationLightMaterials;
    [SerializeField] private Material[] stationMaterials;
    [SerializeField] private Material[] roomMaterials;
    [SerializeField] private Door door;

    [Header("Colors")]
    [SerializeField] private Color emergencyLightColor;
    [SerializeField] private Color resetLightColor;
    [SerializeField] private Color emergencyMaterialColor;
    [SerializeField] private Color resetMaterialColor;
    [SerializeField] private Color resetRoomColor;

    [Header("Probes")]
    [SerializeField] private GameObject BlueProbes;
    [SerializeField] private GameObject PurpleProbes;

    private void OnDisable()
    {
        DisableEmergencyLight();
    }

    public void EnableEmergencyLight()
    {
        PurpleProbes.SetActive(true);
        BlueProbes.SetActive(false);

        for (int i = 0; i < areaLights.Length; i++)
        {
            areaLights[i].color = emergencyLightColor;
        }

        for (int i = 0; i < stationLightMaterials.Length; i++)
        {
            stationLightMaterials[i].SetColor("_EmissiveColor", emergencyMaterialColor * 40);
        }

        for (int i = 0; i < stationMaterials.Length; i++)
        {
            stationMaterials[i].SetColor("_EmissiveColor", emergencyMaterialColor * 6.6f);
        }

        for (int i = 0; i < roomMaterials.Length; i++)
        {
            roomMaterials[i].SetColor("_EmissiveColor", emergencyMaterialColor * 40f);
        }
    } 

    public void DisableLights()
    {
        for (int i = 0; i < areaLights.Length; i++)
        {
            areaLights[i].color = Color.black;
        }

        for (int i = 0; i < stationLightMaterials.Length; i++)
        {
            stationLightMaterials[i].SetColor("_EmissiveColor", Color.black);
        }

        for (int i = 0; i < stationMaterials.Length; i++)
        {
            stationMaterials[i].SetColor("_EmissiveColor", Color.black);
        }

        for (int i = 0; i < roomMaterials.Length; i++)
        {
            roomMaterials[i].SetColor("_EmissiveColor", Color.black);
        }
    }

    public void DisableEmergencyLight()
    {
        PurpleProbes.SetActive(false);
        BlueProbes.SetActive(true);

        for (int i = 0; i < areaLights.Length; i++)
        {
            areaLights[i].color = resetLightColor;
        }

        for (int i = 0; i < stationLightMaterials.Length; i++)
        {
            stationLightMaterials[i].SetColor("_EmissiveColor", resetMaterialColor * 40);
        }

        for (int i = 0; i < stationMaterials.Length; i++)
        {
            stationMaterials[i].SetColor("_EmissiveColor", resetMaterialColor * 6.6f);
        }

        for (int i = 0; i < roomMaterials.Length; i++)
        {
            roomMaterials[i].SetColor("_EmissiveColor", resetRoomColor * 40f);
        }
    }

    private IEnumerator TriggerEmergencyLight()
    {
        door.IsLocked = true;
        var Audio = FindObjectOfType<AudioManager>();
        Audio.PlayAudio("Station_Blackout");
        Audio.PlayAudio("Quill_Scare");
        DisableLights();
        yield return new WaitForSeconds(7);
        EnableEmergencyLight();
        yield return new WaitForSeconds(.5f);
        Audio.PlayAudio("Dieter_Blackout");
        yield return new WaitForSeconds(Audio.ReturnClipLength("Dieter_Blackout"));
        Audio.PlayAudio("Quill_Blackout");
        yield return new WaitForSeconds(Audio.ReturnClipLength("Quill_Blackout") + .5f);
        ChangeInstruction.Change("gehe zu raum r3");
        door.IsLocked = false;
    }

    public void EnablePower()
    {
        StartCoroutine(TriggerNormalLight());
    }

    private IEnumerator TriggerNormalLight()
    {
        DisableLights();
        yield return new WaitForSeconds(1);
        FindObjectOfType<AudioManager>().PlayAudio("Station_PowerOn");
        DisableEmergencyLight();
        ChangeInstruction.ChangeNoSound("");
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TriggerEmergencyLight());
            SaveManager.SetSaveInt(2);
            var box = GetComponent<BoxCollider>();
            box.enabled = false;
        }
    }
}
