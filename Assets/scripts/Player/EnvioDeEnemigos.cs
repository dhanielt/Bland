using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnvioDeEnemigos : MonoBehaviour
{
    private Dictionary<GameObject, string> enemigos = new Dictionary<GameObject, string>();
    private string jugadorLocal;

    void Start()
    {
        jugador();
    }

    [PunRPC]
    public void guardarEnemigo(string enemigoPrefab, Vector3 position, Quaternion rotation, string propietario, int numeroEnemigos, int objetivo)
    {
        GameObject enemigo;
        
        if (propietario != jugadorLocal)
        {
            GameObject prefab = Resources.Load(enemigoPrefab) as GameObject;
            enemigo = Instantiate(prefab, position, rotation);
            if (prefab != null)
            {
                
                numeroEnemigos += 1;
                enemigo.name = "enemigo" + numeroEnemigos;
                enemigo.GetComponent<MovimientoDeEnemigo>().setObjetivo(objetivo);
            }
            else
            {
                Debug.Log("no econtro Prefab");
            }
        }
        else
        {
            enemigo = GameObject.Find("enemigo" + numeroEnemigos);
        }

        enemigos.Add(enemigo, propietario);
    }

    public void jugador()
    {
        for (int i = 1; i <= PhotonNetwork.PlayerList.Length; i++)
        {
            if (GameObject.Find("jugador" + i).GetComponent<PhotonView>().IsMine)
            {
                Debug.Log("encontre jugador local");
                jugadorLocal = "jugador" + i;
                Debug.Log(jugadorLocal);
            }
        }
    }
}