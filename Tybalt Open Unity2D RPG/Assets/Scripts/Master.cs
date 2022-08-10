using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour
{
    [SerializeField] private WorldMapManager overmapManager;
    [SerializeField] private PlayerManager playerManager;

    private void Start()
    {
        overmapManager.InitializeOvermap();
        playerManager.SetPlayerCurrentPosition(overmapManager.GrabCenter());
        overmapManager.CreateNewOvermap(playerManager.GrabPlayerCurrentPosition());
    }
}
