using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Vehicles.Car;

public class Drive : NetworkBehaviour {

    public GameObject player = null;
    private bool isActive = false;
    public static bool carDrive = false;

    private float defaultDrag = 0;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponentInChildren<Camera>().enabled = false;
        gameObject.GetComponentInChildren<CarController>().enabled = false;
        gameObject.GetComponentInChildren<CarUserControl>().enabled = false;
        LightOff();
        defaultDrag = gameObject.GetComponent<Rigidbody>().drag;
    }

    // Update is called once per frame
    void Update()
    {
        Switcher();
        ObjectDependencies();
    }

    void Dragging()
    {
        gameObject.GetComponent<Rigidbody>().drag += (float)0.05;
    }

    void DraggingOff()
    {
        gameObject.GetComponent<Rigidbody>().drag = defaultDrag;
    }

    void LightOff()
    {
        foreach (var v in GameObject.Find("FrontLights").GetComponentsInChildren<Light>())
        {
            v.enabled = false;
        }
    }

    void LightOn()
    {
        foreach (var v in GameObject.Find("FrontLights").GetComponentsInChildren<Light>())
        {
            v.enabled = true;
        }
    }

    void LightSwitch()
    {
        if (GameObject.Find("FrontLights").GetComponentsInChildren<Light>()[0].enabled)
        {
            LightOff();
        }
        else
        {
            LightOn();
        }

    }

    void Switcher()
    {
        player = GetLocalPlayer(player);

        if (Input.GetKeyUp(KeyCode.E) && player.GetComponent<PersonController>().isLocalPlayer &&
            Vector3.Distance(gameObject.transform.position, player.gameObject.transform.position) < 5)
        {
            if (!isActive)
            {
                player.SetActive(false);
                isActive = true;
                gameObject.GetComponentInChildren<Camera>().enabled = true;
                gameObject.GetComponentInChildren<CarController>().enabled = true;
                gameObject.GetComponentInChildren<CarUserControl>().enabled = true;
            }
            else
            {
                player.transform.position = new Vector3(
                    gameObject.transform.position.x - 3,
                    gameObject.transform.position.y + 1,
                    gameObject.transform.position.z);

                player.SetActive(true);
                isActive = false;
                gameObject.GetComponentInChildren<Camera>().enabled = false;
                gameObject.GetComponentInChildren<CarController>().enabled = false;
                gameObject.GetComponentInChildren<CarUserControl>().enabled = false;
            }
        }

        if (isActive)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Dragging();
            }
        }

        if (isActive)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                DraggingOff();
            }
        }

        if (isActive)
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                LightSwitch();
            }
        }
    }

    void ObjectDependencies()
    {
        if (isActive)
        {
            player = GetLocalPlayer(player);

            if (player.GetComponent<PersonController>().isLocalPlayer)
            {
                player.transform.position = gameObject.transform.position;
            }
        }
    }

    private GameObject GetLocalPlayer(GameObject origin)
    {
        GameObject p = null;
        foreach (var v in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (v != null && v.GetComponent<PersonController>() != null && v.GetComponent<PersonController>().isLocalPlayer)
            {
                p = v;
                break;
            }
        }
        if (p == null)
        {
            return origin;
        }
        return p;
    }
}