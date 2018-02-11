using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {

    public string sceneName;
    public Image image;
    public Sprite pushedDown;
    public Sprite noTwinkle;
    public Sprite hasTwinkle;
    private float _lastTime;

    public void ClickToStart()
	{
        image.sprite = pushedDown;
        GameManager.instance.LoadScene(sceneName);
    }

    void Update()
    {
        if (Time.time >= _lastTime + 1f)
        {
            if (image.sprite == hasTwinkle)
            {
                image.sprite = noTwinkle;
            }
            else
            {
                image.sprite = hasTwinkle;
            }
            _lastTime = Time.time;
        }
    }
}
