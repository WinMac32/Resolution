using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHandler : InteractionHandler
{
    public override void handleAction()
    {
        Debug.Log("DEBUG INTERACTION");
    }
}
