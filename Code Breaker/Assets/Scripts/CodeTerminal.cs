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

    [SerializeField] private TextMeshProUGUI codeText;

    private void Update()
    {
        //Wenn die Eingabe des Spieler größer gleich die maximale Eingabe des Spieler ist und das Rätsel nicht gelöst wurde dann:
        if(currentInput >= maxInput && !isComplete)
        {
            if (codeInput == code) //Wenn der eingegebene Code der gleiche ist wie der des Rätsels dann:
            {
                Debug.Log("Win");
            }
            else //Wenn der Code nicht richtig ist dann:
            {
                Lose(); //Startet Funktion
            }
        }

        UpdateUI();
    }

    public void TerminalInput(int input)
    {
        currentInput += 1;
        codeInput += input;
    }

        private void Lose()
    {
        currentInput = 0; //Spielereingabe wird auf 0 gesetzt
        codeInput = ""; //Spielereingabe wird gelöscht
        Debug.Log("Lose");
    }

    private void UpdateUI()
    {
        codeText.text = codeInput;
    }
}
