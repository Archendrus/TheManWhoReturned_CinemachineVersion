using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnterRoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera roomCamera;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            roomCamera.Priority = 15;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            roomCamera.Priority = 5;
        }
    }
}
