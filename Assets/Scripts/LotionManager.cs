using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotionManager : MonoBehaviour
{
    public delegate void OnUseLotion();
    public OnUseLotion onUseLotion;
    public delegate void OnRefillLotion();
    public OnRefillLotion onRefillLotion;

    public float maxLotion;
    public float lotionStash;

    public bool UseLotion(float amount)
    {
        if (lotionStash > amount)
        {
            lotionStash -= amount;

            if (onUseLotion != null)
            {
                onUseLotion();
            }

            return true;
        }
        return false;
    }

    public void RefillLotion(float amount)
    {
        lotionStash += amount;
        
        if (lotionStash > maxLotion)
        {
            lotionStash = maxLotion;
        }
        
        if (onRefillLotion != null)
        {
            onRefillLotion();
        }
    }

    public void Start()
    {
    }
}
