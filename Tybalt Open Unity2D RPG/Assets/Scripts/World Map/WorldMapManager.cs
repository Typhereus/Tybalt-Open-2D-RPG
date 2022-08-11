using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapManager : MonoBehaviour
{
    [SerializeField] private WorldMapUI worldMapUI;
    [SerializeField] private WorldMap currentWorldMap;
    
    public void InitializeWorldMap()
    {
        currentWorldMap = new WorldMap(50, 50);
    }
    public Vector2IntSerializable GrabCenter()
    {
        return currentWorldMap.GrabStartCell();
    }
    public void CreateNewWorldMap(Vector2IntSerializable playerCurrentPosition)
    {       
        worldMapUI.StartNewWorldMap(currentWorldMap, playerCurrentPosition);
    }
}
