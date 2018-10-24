using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class PersonController : NetworkBehaviour {

    public GameObject bullet = null;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            GetComponentInChildren<Camera>().enabled = false;
            GetComponent<FirstPersonController>().enabled = false;
            GetComponent<PersonController>().enabled = false;
            Debug.Log("is not local");
            enabled = false;
        }
        else
        {
            transform.position = GameObject.Find("StartPoint").transform.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        Controller();
    }

    

    void Controller()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            LightSwitch();
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            CmdFire();
        }
    }

    [Command]
    void CmdFire()
    {
        // This [Command] code is run on the server!
        // create the bullet object locally
        var b = (GameObject)Instantiate(
             bullet,
             transform.position + transform.forward,
             Quaternion.identity);

        // spawn the bullet on the clients
        NetworkServer.SpawnWithClientAuthority(b, connectionToClient);

        // when the bullet is destroyed on the server it will automaticaly be destroyed on clients
        //Destroy(b, 2.0f);
    }

    void LightOff()
    {
        foreach (var v in GameObject.Find("Flashlight").GetComponentsInChildren<Light>())
        {
            v.enabled = false;
        }
    }

    void LightOn()
    {
        foreach (var v in GameObject.Find("Flashlight").GetComponentsInChildren<Light>())
        {
            v.enabled = true;
        }
    }

    void LightSwitch()
    {
        if (GameObject.Find("Flashlight").GetComponentsInChildren<Light>()[0].enabled)
        {
            LightOff();
        }
        else
        {
            LightOn();
        }

    }
}
