using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CodeTerminal : MonoBehaviour
{
    [SerializeField] private string code = "1846"; //Code zum �ffnen 1231423
    [SerializeField] private string codeInput = ""; //Aktuelle Eingabe vom Spieler
    [SerializeField] private float maxInput = 4; //Maximale Eingabe vom Spieler
    [SerializeField] private float currentInput = 0; //Aktuelle Eingabe vom Spieler
    [SerializeField] private bool isComplete = false; //Wurde das R�tsel gel�st?

    [SerializeField] private TextMeshProUGUI codeText;

    private void Update()
    {
        //Wenn die Eingabe des Spieler gr��er gleich die maximale Eingabe des Spieler ist und das R�tsel nicht gel�st wurde dann:
        if(currentInput >= maxInput && !isComplete)
        {
            if (codeInput == code) //Wenn der eingegebene Code der gleiche ist wie der des R�tsels dann:
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
        codeInput = ""; //Spielereingabe wird gel�scht
        Debug.Log("Lose");
    }

    private void UpdateUI()
    {
        codeText.text = codeInput;
    }
}
