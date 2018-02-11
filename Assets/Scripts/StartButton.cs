using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {

	public void ClickToStart()
	{
        GameManager.instance.LoadScene("Desert Level");
    }
}
