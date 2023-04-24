using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsOpen = false; //Ist die Tür offen?
    public bool IsLocked = false; //Ist die Tür abgeschlossen?
    public bool isRotationDoor = false;

    [Header("Rotation Configss")]
    [SerializeField] private float RotationAmount = 90f; //Wert, welcher angibt, wie weit die Tür sich öffnen soll
    [SerializeField] private float ForwardDirection = 0; //Nullpunkt für das Dot Product
    [SerializeField] private float RotationOpenSpeed = 1f; //Türöffnungsgeschwindigkeit
    [SerializeField] private float RotationCloseSpeed = 1f; //Türschließungsgeschwindigkeit
    [Header("Sliding Configs")]
    [SerializeField] private Vector3 SlideDirection = Vector3.back;
    [SerializeField] private float SlideAmount = 1.9f;
    [SerializeField] private float SlideSpeed;

    private Vector3 StartRotation; //Position der Tür
    private Vector3 StartPosition;

    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        //Position der Tür wird mit StartRotation gleich gesetzt, damit diese später leichter verwendet werden kann
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

    public void Open(Vector3 UserPosition) //Öffnen der Tür
    {
        if (!IsLocked) //Wenn die Tür nicht abgeschlossen ist dann:
        {
            if (!IsOpen) //Wenn die Tür nicht offen ist dann:
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
                //In diesem Fall wird transform.forward(Vorderseite der Tür) als A genommen und
                //B nimmt die Position des Spielers - die Position der Tür als normalized Vector (Werte von -1 bis 1)
                //Anhand dessen wird die Position des Spielers in Relation zu der Position des Tür berechnet
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
        Quaternion startRotation = transform.rotation; //Anfangsrotation der Tür
        Quaternion endRotation; //Endrotation der Tür

        //Wenn das Dot Product größer gleich 0 ist dann:
        if (ForwardAmount >= ForwardDirection)
        {
            //Wird die Endrotation der Tür 90 grad minus der aktuellen Position gerechnet, damit die Tür sich vom Spieler weg öffnet
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y - RotationAmount, 0));
        }
        else //Wenn das Dot Product kleiner gleich 0 ist dann:
        {
            //Wird die Endrotation der Tür 90 grad plus der aktuellen Position gerechnet, damit die Tür sich vom Spieler weg öffnet
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y + RotationAmount, 0));
        }

        IsOpen = true; //Tür ist offen

        float time = 0; //Zeit ist gleich 0
        while (time < 1) //Wärend die Zeit kleiner als 1 ist:
        {
            //Slerp (spherical linear interpolation) ist ähnlich wie Lerp nur als Sphäre
            //Slerp (spherical linear interpolation) ist die Berechnung von Startpunkt a (startRotation), Endpunkt b (endRotation) und dem Prozentwert t (time)
            //Die Berechung für Lerp wäre damit: a + (b - a) * t
            //Da der Wert t (time) linear von 0 auf 1 (0% auf 100%) steigt, rotiert sich das Gameobject gleichmäßig von a nach b
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            //Die Zeitberechnung ergiebt sich aus der Zeit mal die Geschwindigkeit, mit welcher sich die Tür öffnen soll
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

    public void Close() //Schließen der Tür
    {
        if (IsOpen) //Wenn die Tür offen ist dann:
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
        Quaternion startRotation = transform.rotation; //Startrotation ist die aktuelle Position der Tür
        Quaternion endRotation = Quaternion.Euler(StartRotation); //Endrotation ist die anfangs Position der Tür

        IsOpen = false; //Tür ist geschlossen

        float time = 0; //Zeit ist gleich 0
        while (time < 1) //Wärend die Zeit kleiner als 1 ist:
        {
            //Slerp (spherical linear interpolation) ist ähnlich wie Lerp nur als Sphäre
            //Slerp (spherical linear interpolation) ist die Berechnung von Startpunkt a (startRotation), Endpunkt b (endRotation) und dem Prozentwert t (time)
            //Die Berechung für Lerp wäre damit: a + (b - a) * t
            //Da der Wert t (time) linear von 0 auf 1 (0% auf 100%) steigt, rotiert sich das Gameobject gleichmäßig von a nach b
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            //Die Zeitberechnung ergiebt sich aus der Zeit mal die Geschwindigkeit, mit welcher sich die Tür öffnen soll
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
