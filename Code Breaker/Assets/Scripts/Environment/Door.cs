using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsOpen = false; //Ist die T�r offen?
    public bool IsLocked = false; //Ist die T�r abgeschlossen?
    public bool isRotationDoor = false;

    [Header("Rotation Configss")]
    [SerializeField] private float RotationAmount = 90f; //Wert, welcher angibt, wie weit die T�r sich �ffnen soll
    [SerializeField] private float ForwardDirection = 0; //Nullpunkt f�r das Dot Product
    [SerializeField] private float RotationOpenSpeed = 1f; //T�r�ffnungsgeschwindigkeit
    [SerializeField] private float RotationCloseSpeed = 1f; //T�rschlie�ungsgeschwindigkeit
    [Header("Sliding Configs")]
    [SerializeField] private Vector3 SlideDirection = Vector3.back;
    [SerializeField] private float SlideAmount = 1.9f;
    [SerializeField] private float SlideSpeed;

    private Vector3 StartRotation; //Position der T�r
    private Vector3 StartPosition;

    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        //Position der T�r wird mit StartRotation gleich gesetzt, damit diese sp�ter leichter verwendet werden kann
        StartRotation = transform.rotation.eulerAngles;
        StartPosition = transform.position;
    }

    public void OpenClose()
    {
        if (!IsLocked)
        {
            if (!IsOpen)
            {
                if (AnimationCoroutine != null) //Wenn die AnimationCoroutine keinen Wert hat dann:
                {
                    StopCoroutine(AnimationCoroutine); //Coroutine wird angehalten
                }

                AnimationCoroutine = StartCoroutine(DoSlidingOpen());
            }
            else
            {
                AnimationCoroutine = StartCoroutine(DoSlidingClose());
            }
        }
    }

    public void Open(Vector3 UserPosition) //�ffnen der T�r
    {
        if (!IsLocked) //Wenn die T�r nicht abgeschlossen ist dann:
        {
            if (!IsOpen) //Wenn die T�r nicht offen ist dann:
            {
                if (AnimationCoroutine != null) //Wenn die AnimationCoroutine keinen Wert hat dann:
                {
                    StopCoroutine(AnimationCoroutine); //Coroutine wird angehalten
                }
            }

            if (isRotationDoor)
            {
                //Das Dot Product nimmt zwei Varibalen A und B. Diese werden miteinander multipliziert, um das Dot Product zu bekommen
                //Da das Dot Product als Vector3(x,y,z) berechnet wird, benutzt Unity folgenden Rechnung: Ax*Bx + Ay*By + Az*Bz
                //In diesem Fall wird transform.forward(Vorderseite der T�r) als A genommen und
                //B nimmt die Position des Spielers - die Position der T�r als normalized Vector (Werte von -1 bis 1)
                //Anhand dessen wird die Position des Spielers in Relation zu der Position des T�r berechnet
                float dot = Vector3.Dot(transform.forward, (UserPosition - transform.position).normalized);
                AnimationCoroutine = StartCoroutine(DoRotationOpen(dot)); //Startet die Coroutine und nimmt das Dot Product mit
            }
            else
            {
                AnimationCoroutine = StartCoroutine(DoSlidingOpen());
            }
        }
    }

    private IEnumerator DoRotationOpen(float ForwardAmount)
    {
        Quaternion startRotation = transform.rotation; //Anfangsrotation der T�r
        Quaternion endRotation; //Endrotation der T�r

        //Wenn das Dot Product gr��er gleich 0 ist dann:
        if (ForwardAmount >= ForwardDirection)
        {
            //Wird die Endrotation der T�r 90 grad minus der aktuellen Position gerechnet, damit die T�r sich vom Spieler weg �ffnet
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y - RotationAmount, 0));
        }
        else //Wenn das Dot Product kleiner gleich 0 ist dann:
        {
            //Wird die Endrotation der T�r 90 grad plus der aktuellen Position gerechnet, damit die T�r sich vom Spieler weg �ffnet
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y + RotationAmount, 0));
        }

        IsOpen = true; //T�r ist offen

        float time = 0; //Zeit ist gleich 0
        while (time < 1) //W�rend die Zeit kleiner als 1 ist:
        {
            //Slerp (spherical linear interpolation) ist �hnlich wie Lerp nur als Sph�re
            //Slerp (spherical linear interpolation) ist die Berechnung von Startpunkt a (startRotation), Endpunkt b (endRotation) und dem Prozentwert t (time)
            //Die Berechung f�r Lerp w�re damit: a + (b - a) * t
            //Da der Wert t (time) linear von 0 auf 1 (0% auf 100%) steigt, rotiert sich das Gameobject gleichm��ig von a nach b
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            //Die Zeitberechnung ergiebt sich aus der Zeit mal die Geschwindigkeit, mit welcher sich die T�r �ffnen soll
            time += Time.deltaTime * RotationOpenSpeed;
        }
    }

    private IEnumerator DoSlidingOpen()
    {
        Vector3 endPosition = StartPosition + SlideAmount * SlideDirection;
        Vector3 startPosition = transform.position;

        float time = 0;
        IsOpen = true;
        while(time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * SlideSpeed;
        }
    }

    public void Close() //Schlie�en der T�r
    {
        if (IsOpen) //Wenn die T�r offen ist dann:
        {
            if (AnimationCoroutine != null) //Wenn die AnimationCoroutine keinen Wert hat dann:
            {
                StopCoroutine(AnimationCoroutine); //Coroutine wird angehalten
            }

            if (isRotationDoor)
            {
                AnimationCoroutine = StartCoroutine(DoRotationClose()); //Startet die Coroutine
            }
            else
            {
                AnimationCoroutine = StartCoroutine(DoSlidingClose());
            }
        }
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRotation = transform.rotation; //Startrotation ist die aktuelle Position der T�r
        Quaternion endRotation = Quaternion.Euler(StartRotation); //Endrotation ist die anfangs Position der T�r

        IsOpen = false; //T�r ist geschlossen

        float time = 0; //Zeit ist gleich 0
        while (time < 1) //W�rend die Zeit kleiner als 1 ist:
        {
            //Slerp (spherical linear interpolation) ist �hnlich wie Lerp nur als Sph�re
            //Slerp (spherical linear interpolation) ist die Berechnung von Startpunkt a (startRotation), Endpunkt b (endRotation) und dem Prozentwert t (time)
            //Die Berechung f�r Lerp w�re damit: a + (b - a) * t
            //Da der Wert t (time) linear von 0 auf 1 (0% auf 100%) steigt, rotiert sich das Gameobject gleichm��ig von a nach b
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            //Die Zeitberechnung ergiebt sich aus der Zeit mal die Geschwindigkeit, mit welcher sich die T�r �ffnen soll
            time += Time.deltaTime * RotationCloseSpeed;
        }
    }

    private IEnumerator DoSlidingClose()
    {
        Vector3 endPosition = StartPosition;
        Vector3 startPosition = transform.position;

        IsOpen = false;

        float time = 0;
        while (time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * SlideSpeed;
        }
    }
}
