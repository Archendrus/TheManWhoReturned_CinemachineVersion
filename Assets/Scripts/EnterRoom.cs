using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnterRoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera roomCamera = null;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            roomCamera.Priority = 15;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            roomCamera.Priority = 5;
        }
    }
}
