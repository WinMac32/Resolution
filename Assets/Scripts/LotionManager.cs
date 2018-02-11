using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotionManager : MonoBehaviour
{
    public delegate void OnUseLotion();
    public OnUseLotion onUseLotion;

    public float maxLotion;
    public float lotionStash;

    public bool UseLotion(float amount)
    {
        if (lotionStash > amount)
        {
            lotionStash -= amount;
            Debug.Log("Our lotion is now " + lotionStash);

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
        onUseLotion();
    }

    public void Start()
    {
    }
}
