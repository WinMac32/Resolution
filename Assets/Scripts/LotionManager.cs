using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotionManager : MonoBehaviour
{
    public float maxLotion;
    public float lotionStash;

    public bool UseLotion(float amount)
    {
        if (lotionStash > amount)
        {
            lotionStash -= amount;
            Debug.Log("Our lotion is now " + lotionStash);
            return true;
        }
        return false;
    }

    public void Start()
    {
    }
}
