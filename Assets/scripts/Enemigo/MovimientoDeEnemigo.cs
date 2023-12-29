using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MovimientoDeEnemigo : MonoBehaviourPunCallbacks
{
    private Transform objetivo;
    public float speed;
    private int jugador;
    public bool moverse;
    void Start()
    {
        StartCoroutine(BuscarJugadorMasCercano());
    }
    
    IEnumerator BuscarJugadorMasCercano()
    {
        yield return new WaitUntil(() => PhotonNetwork.PlayerList.Length != 0);
        yield return new WaitForSeconds(1f);
        
        objetivo = null; 
        float distanciaMasCercana = float.MaxValue;
        
        for (int i = 1; i <= PhotonNetwork.PlayerList.Length; i++) 
        { 
            GameObject jugadorObject = GameObject.Find("jugador" + i);
            
            if (jugadorObject != null) 
            { 
                float distancia = Vector3.Distance(transform.position, jugadorObject.transform.position);
                
                if (distancia < distanciaMasCercana) 
                { 
                    distanciaMasCercana = distancia; 
                    objetivo = jugadorObject.transform;
                    jugador = i;
                }
            }
            else 
            { 
                Debug.Log("Jugador" + i + " no está instanciado"); 
            } 
        }

        moverse = true;
    }

    public int getObjetivo()
    {
        return jugador;
    }

    public void setObjetivo(int jugador)
    {
        this.jugador = jugador;
        GameObject jugadorObject = GameObject.Find("jugador" + jugador);
        objetivo = jugadorObject.transform;
    }
    
    void Update()
    {
        if (moverse)
        {
            transform.position = Vector2.MoveTowards(transform.position, objetivo.position, speed * Time.deltaTime);
        }
    }
}
