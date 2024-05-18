using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviourPun
{
    UnityStandardAssets.Vehicles.Car.CarController carController;
    float horizontal;
    float vertical;
    float drift;

    void Start()
    {
        if (!photonView.IsMine) return;

        GameManager.Instance.SetAutoCamTarget(transform);

        carController = GetComponent<UnityStandardAssets.Vehicles.Car.CarController>();

    }

    void Update()
    {
        if (!photonView.IsMine) return;

        if (GameManager.Instance.gameState != GameState.RacingStart)
        {
            carController.Move(0, 0, 0, 0);
            return;
        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        drift = Input.GetAxis("Jump");

        GameManager.Instance.SetAutoCamTarget(transform);
        carController.Move(horizontal, vertical, vertical, drift);


    }
}
