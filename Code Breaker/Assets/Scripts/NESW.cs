using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NESW : MonoBehaviour
{
    [SerializeField] private string code = "1231423"; //Code zum �ffnen 1231423
    [SerializeField] private string codeInput = ""; //Aktuelle Eingabe vom Spieler
    [SerializeField] private float maxInput = 7; //Maximale Eingabe vom Spieler
    [SerializeField] private float currentInput = 0; //Aktuelle Eingabe vom Spieler
    [SerializeField] private bool isComplete = false; //Wurde das R�tsel gel�st?

    [SerializeField] GameObject Jumpscare; //Jumpscare

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
    }

        private void Lose()
    {
        currentInput = 0; //Spielereingabe wird auf 0 gesetzt
        codeInput = ""; //Spielereingabe wird gel�scht
    }
}
