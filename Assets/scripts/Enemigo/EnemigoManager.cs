using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System;
using Photon.Pun.Demo.PunBasics;

public class EnemigoManager : MonoBehaviour
{
    public GameObject enemigoPrefab;
    private int numeroEnemigos;
    public Transform[] spawnPoint = new Transform[6];
    private System.Random generadorAleatorio = new System.Random();
    
    void Start()
    {
        numeroEnemigos = 0;
        StartCoroutine(instanciarEnemigoPrimeraves());
    }

    IEnumerator instanciarEnemigoPrimeraves()
    {
        yield return new WaitUntil(() => PhotonNetwork.PlayerList.Length != 0);
        crearInstancia();
        yield return new WaitForSeconds(2f);
        jugador();
    }

    public void crearInstancia()
    {
            
            int spawn = generadorAleatorio.Next(0, 6);
            GameObject nuevoEnemigo = Instantiate(enemigoPrefab, spawnPoint[spawn].position, spawnPoint[spawn].rotation);
            numeroEnemigos += 1;
            nuevoEnemigo.name = "enemigo" + numeroEnemigos;
            
    }

    public void jugador()
    {
        for (int i = 1; i <= PhotonNetwork.PlayerList.Length; i++)
        {
            if (GameObject.Find("jugador" + i).GetComponent<PhotonView>().IsMine)
            {
                Debug.Log("jugador" + i + " es el local");
            }
        }
    }
    
    
    void Update()
    {
        
    }
}
