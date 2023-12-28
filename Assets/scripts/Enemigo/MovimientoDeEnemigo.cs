using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MovimientoDeEnemigo : MonoBehaviourPunCallbacks
{
    private Transform objetivo;
    public float speed;
    public bool debePerseguir;
    
    public void BuscarJugadorMasCercano()
    {
        if (PhotonNetwork.IsConnected)
        {
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
                        debePerseguir = true;
                    }
                }
                else
                {
                    Debug.Log("Jugador" + i + " no está instanciado");
                }
            }
        }
    }
    
    void Update()
    {
        if (!debePerseguir)
        {
            BuscarJugadorMasCercano();
        }
        if (debePerseguir)
        {
            transform.position = Vector2.MoveTowards(transform.position, objetivo.position, speed * Time.deltaTime);
        }
    }
}
