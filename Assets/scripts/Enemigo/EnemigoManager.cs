using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using System.Linq;
using Unity.Mathematics;


public class EnemigoManager : MonoBehaviour
{
    public GameObject[] enemigoPrefab;
    private int numeroEnemigos;
    public float tiempoGeneracion;
    public Transform[] spawnPoints;
    private System.Random generadorAleatorio = new System.Random();
    private PhotonView mandarInformacion;
    private string jugadorLocal;
    private float tiempoTranscurrido;
    private bool generar;
    
    void Start()
    {
        generar = false;
        numeroEnemigos = 0;
        StartCoroutine(instanciarEnemigoPrimeraves());
    }

    private IEnumerator instanciarEnemigoPrimeraves()
    {
        yield return new WaitUntil(() => PhotonNetwork.PlayerList.Length == 2);
        yield return new WaitForSeconds(1f);
        jugador();
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
        if (numeroEnemigos >= 10)
        {
            generar = false;
        }

        int tipoEnemigo = generadorAleatorio.Next(0, enemigoPrefab.Length);
        Vector2 spawnAleatorio = spawnPoints[generadorAleatorio.Next(0, spawnPoints.Length)].position;
        GameObject nuevoEnemigo = Instantiate(enemigoPrefab[tipoEnemigo], spawnAleatorio, Quaternion.identity);
        numeroEnemigos += 1;
        nuevoEnemigo.name = "enemigo" + numeroEnemigos;
        mandarInformacion.RPC("guardarEnemigo", RpcTarget.All, enemigoPrefab[tipoEnemigo].name, spawnAleatorio, jugadorLocal, numeroEnemigos, nuevoEnemigo.GetComponent<MovimientoDeEnemigo>().getObjetivo());
    }
    
    public void jugador()
    {
        for (int i = 1; i <= PhotonNetwork.PlayerList.Length; i++)
        {
            if (GameObject.Find("jugador" + i).GetComponent<PhotonView>().IsMine)
            {
                jugadorLocal = "jugador" + i;
                mandarInformacion = GameObject.Find("jugador" + i).GetComponent<PhotonView>();
                generar = true;
            }
        }
    }

    private void Update()
    {
        tiempoTranscurrido += Time.deltaTime;
    
        if (tiempoGeneracion <= tiempoTranscurrido && generar)
        {
            Debug.Log("crea instancia");
            tiempoTranscurrido = 0;
            crearInstancia();
        }
    }
}
