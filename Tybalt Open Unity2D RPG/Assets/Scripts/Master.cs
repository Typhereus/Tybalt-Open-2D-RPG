using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour
{
    [SerializeField] private WorldMapManager overmapManager;
    [SerializeField] private PlayerManager playerManager;

    private void Start()
    {
        overmapManager.InitializeWorldMap();
        playerManager.SetPlayerCurrentPosition(overmapManager.GrabCenter());
        overmapManager.CreateNewWorldMap(playerManager.GrabPlayerCurrentPosition());
    }
}
