using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeInstruction : MonoBehaviour
{
    public static void Change(string instructionText)
    {
        var text = GameObject.FindWithTag("Instruction").GetComponent<TextMeshProUGUI>();
        text.SetText(instructionText);
        FindObjectOfType<AudioManager>().PlayAudio("Hint");
    }

    public static void ChangeNoSound(string instructionText)
    {
        var text = GameObject.FindWithTag("Instruction").GetComponent<TextMeshProUGUI>();
        text.SetText(instructionText);
    }
}
