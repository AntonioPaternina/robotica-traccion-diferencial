using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento : MonoBehaviour
{
    Vector3 pos;
    Vector3 rot;
    public float r;
    public float b;
    public float velocidad;
    // Use this for initialization
    void Start()
    {
        pos = this.transform.position;
        rot = this.transform.eulerAngles;
        r = 3;
        b = 15;
    }

    // Update is called once per frame
    void Update()
    {     
        calcularNuevasPosiciones(6, 0);
    }

    void calcularNuevasPosiciones(float voltajeIzquierda, float voltajeDerecha)
    {
        float omegaIzquierda = voltajeAVelocidadAngular(voltajeIzquierda);
        float omegaDerecha = voltajeAVelocidadAngular(voltajeDerecha);

        pos.x += deltaX(omegaIzquierda, omegaDerecha);
        pos.z += deltaZ(omegaIzquierda, omegaDerecha);
        this.transform.position = pos;

        rot.y += radianesAGrados(deltaPhi(omegaIzquierda, 0));
        this.transform.eulerAngles = rot;
    }

    float deltaPhi(float omegaIzquierda, float omegaDerecha)
    {
        float deltaPhi = (r / b) * (omegaIzquierda - omegaDerecha) * (Time.deltaTime);
        Debug.Log("deltaPhi=" + deltaPhi);
        return deltaPhi;
    }

    float deltaX(float omegaIzquierda, float omegaDerecha)
    {
        float deltaX = (b / 2) * ((omegaDerecha + omegaIzquierda) / (omegaDerecha - omegaIzquierda))
            * (Mathf.Cos(gradosARadianes(rot.y)) -
            Mathf.Cos(gradosARadianes(rot.y) + deltaPhi(omegaIzquierda, omegaDerecha)));
        Debug.Log("deltaX=" + deltaX);
        return -deltaX;
    }

    float deltaZ(float omegaIzquierda, float omegaDerecha)
    {
        float deltaZ = (b / 2) * ((omegaDerecha + omegaIzquierda) / (omegaDerecha - omegaIzquierda))
            * (Mathf.Sin(gradosARadianes(rot.y) + deltaPhi(omegaIzquierda, omegaDerecha)) 
            - Mathf.Sin(gradosARadianes(rot.y)));
        Debug.Log("deltaZ=" + deltaZ);
        return -deltaZ;
    }

    float voltajeAVelocidadAngular(float voltaje)
    {
        float velocidadAngular = (4f / 3f) * Mathf.PI * voltaje;
        return velocidadAngular;
    }

    float radianesAGrados(float anguloEnRadianes)
    {
        return anguloEnRadianes * (360f / (2 * Mathf.PI));
    }

    float gradosARadianes(float anguloEnGrados)
    {
        return anguloEnGrados * ((2 * Mathf.PI) / 360f);
    }
}
