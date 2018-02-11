using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : InteractionHandler
{

    public string sceneName;

    public override void HandleAction(GameObject source)
    {
        GameManager.instance.LoadScene(sceneName);
    }

}
