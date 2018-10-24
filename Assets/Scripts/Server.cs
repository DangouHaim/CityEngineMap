using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Server : NetworkBehaviour {

    public GameObject playerPrefab;

    void Start()
    {
        if(isServer)
        {
            CmdCreateCar();
        }
    }

    [Command]
    void CmdCreateCar()
    {
        var car = (GameObject)Instantiate(
             playerPrefab,
             GameObject.FindGameObjectWithTag("CarSpawnPoint").transform.position,
             Quaternion.identity);
        NetworkServer.SpawnWithClientAuthority(car, connectionToClient);
    }
}
