using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapManager : MonoBehaviour
{
    [SerializeField] private WorldMapUI overmapUI;
    [SerializeField] private WorldMap currentOvermap;
    
    public void InitializeOvermap()
    {
        currentOvermap = new WorldMap(50, 50);
    }
    public Vector2Serializable GrabCenter()
    {
        return currentOvermap.GrabStartCell();
    }
    public void CreateNewOvermap(Vector2Serializable playerCurrentPosition)
    {       
        overmapUI.StartNewOverMap(currentOvermap, playerCurrentPosition);
    }

}
