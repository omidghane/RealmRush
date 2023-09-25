using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFix : MonoBehaviour
{
    [SerializeField] Tower towerDefense;
    [SerializeField] bool isPlacable;
    public bool IsPlacable { get { return isPlacable; } }

    void OnMouseDown()
    {
        if(isPlacable)
        {
            bool isPlaced = towerDefense.create_tower(towerDefense, transform.position);
            isPlacable = !isPlaced;
        }    
    }
}