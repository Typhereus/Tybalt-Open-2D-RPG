using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerCharacterData playerCharacterData;

    public void StartNewPlayer()
    {
        playerCharacterData = new PlayerCharacterData();
    }
    public void SetPlayerCurrentPosition(Vector2Serializable vector2Serializable) 
    {
        playerCharacterData.currentPosition = vector2Serializable;
    }
    public Vector2Serializable GrabPlayerCurrentPosition()
    {
        return playerCharacterData.currentPosition;
    }
}
