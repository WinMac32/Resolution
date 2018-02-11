using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotionRefillInteraction : InteractionHandler
{
    private LotionManager lotionManager;

    public int lotionRefill = 40;

    public void Start()
    {
        lotionManager = GameManager.instance.lotionManager;
    }

    public override void HandleAction(GameObject source)
    {
        lotionManager.RefillLotion(lotionRefill);

        DestroyObject(source);
    }
}
