using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MovimientoDeEnemigo : MonoBehaviourPunCallbacks
{
    private Transform objetivo;
    public float speed;
    public bool debePerseguir;
    void Start()
    {
    }
    
    public void BuscarJugadorMasCercano()
    {
        if (PhotonNetwork.IsConnected)
        {
            objetivo = null;
            float distanciaMasCercana = float.MaxValue;

            if (PhotonNetwork.PlayerList.Length == 0)
            {
                Debug.Log("Servidor vacío");
                return;
            }

            foreach (var jugador in PhotonNetwork.PlayerList)
            {
                // Encuentra el PhotonView asociado al jugador
                PhotonView photonView = PhotonView.Find(jugador.UserId);

                if (photonView != null)
                {
                    // Obtén el ViewID del PhotonView
                    int viewID = photonView.ViewID;

                    // Encuentra el GameObject asociado al jugador
                    GameObject jugadorObject = photonView.gameObject;

                    if (jugadorObject != null)
                    {
                        float distancia = Vector3.Distance(transform.position, jugadorObject.transform.position);

                        if (distancia < distanciaMasCercana)
                        {
                            distanciaMasCercana = distancia;
                            objetivo = jugadorObject.transform;
                        }
                    }
                    else
                    {
                        Debug.Log("No se pudo asignar un objeto para el jugador con ViewID: " + viewID);
                    }
                }
                else
                {
                    Debug.Log("No se encontró PhotonView para el jugador con UserId: " + jugador.UserId);
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
            Debug.Log("movi el enemigo");
            transform.position = Vector2.MoveTowards(transform.position, objetivo.position, speed * Time.deltaTime);
        }
    }
}
