using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CodeTerminal : MonoBehaviour
{
    [SerializeField] private string code = "1846"; //Code zum öffnen 1231423
    [SerializeField] private string codeInput = ""; //Aktuelle Eingabe vom Spieler
    [SerializeField] private float maxInput = 4; //Maximale Eingabe vom Spieler
    [SerializeField] private float currentInput = 0; //Aktuelle Eingabe vom Spieler
    [SerializeField] private bool isComplete = false; //Wurde das Rätsel gelöst?
    [SerializeField] private float waitTime = 1;

    [Header("Turbine")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip EngineSoundFixed;
    [SerializeField] private Animator TurbineAnimator;

    [SerializeField] private TextMeshProUGUI codeText;
    [SerializeField] private GameObject Checkmark;
    [SerializeField] private GameObject Cross;

    private void Update()
    {
        UpdateUI();
    }

    public void CheckInput()
    {
        if (codeInput == code) //Wenn der eingegebene Code der gleiche ist wie der des Rätsels dann:
        {
            codeText.gameObject.SetActive(false);
            Checkmark.SetActive(true);
            TurbineAnimator.SetTrigger("Fix");
            source.Stop();
            source.PlayOneShot(EngineSoundFixed);
            Debug.Log("Win");
        }
        else //Wenn der Code nicht richtig ist dann:
        {
            StartCoroutine(Lose()); //Startet Funktion
        }
    }

    public void TerminalInput(int input)
    {
        if (currentInput < maxInput)
        {
            currentInput += 1;
            codeInput += input;
        }
    }

    IEnumerator Lose()
    {
        codeText.gameObject.SetActive(false);
        Cross.SetActive(true);
        FindObjectOfType<AudioManager>().PlayAudio("HackGame_Fail");
        currentInput = 0; //Spielereingabe wird auf 0 gesetzt
        codeInput = ""; //Spielereingabe wird gelöscht
        yield return new WaitForSeconds(waitTime);
        codeText.gameObject.SetActive(true);
        Cross.SetActive(false);
        Debug.Log("Lose");
        yield return null;
    }

    private void UpdateUI()
    {
        codeText.text = codeInput;
    }
}
