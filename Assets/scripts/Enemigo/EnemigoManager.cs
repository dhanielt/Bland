using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System;


public class EnemigoManager : MonoBehaviour
{
    public GameObject enemigoPrefab;
    private int numeroEnemigos;
    public Transform[] spawnPoint = new Transform[6];
    private System.Random generadorAleatorio = new System.Random();
    private PhotonView mandarInformacion;
    private string jugadorLocal;
    
    void Start()
    {
        numeroEnemigos = 0;
        StartCoroutine(instanciarEnemigoPrimeraves());
    }

    private IEnumerator instanciarEnemigoPrimeraves()
    {
        yield return new WaitUntil(() => PhotonNetwork.PlayerList.Length != 0);
        yield return new WaitForSeconds(1f);
        jugador();
        crearInstancia();
        StartCoroutine(oleadas());
        Debug.Log("se termino la corrutina");
    }

    private IEnumerator oleadas()
    {
        while (numeroEnemigos <= 10)
        {
            yield return new WaitForSeconds(2f);
            crearInstancia();
            yield return null;
        }
    }
    
    public void crearInstancia()
    {
            int spawn = generadorAleatorio.Next(0, 6);
            GameObject nuevoEnemigo = Instantiate(enemigoPrefab, spawnPoint[spawn].position, spawnPoint[spawn].rotation);
            numeroEnemigos += 1;
            nuevoEnemigo.name = "enemigo" + numeroEnemigos;
            mandarInformacion.RPC("guardarEnemigo", RpcTarget.All, enemigoPrefab.name, spawnPoint[spawn].position, spawnPoint[spawn].rotation, jugadorLocal, numeroEnemigos, nuevoEnemigo.GetComponent<MovimientoDeEnemigo>().getObjetivo());
    }
    
    public void jugador()
    {
        for (int i = 1; i <= PhotonNetwork.PlayerList.Length; i++)
        {
            if (GameObject.Find("jugador" + i).GetComponent<PhotonView>().IsMine)
            {
                jugadorLocal = "jugador" + i;
                mandarInformacion = GameObject.Find("jugador" + i).GetComponent<PhotonView>();
            }
        }
    }
    
    void Update()
    {
        
    }
}
