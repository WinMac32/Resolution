using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public LotionManager lotionManager;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        lotionManager = new LotionManager();
    }

    void Start()
    {
        SceneManager.LoadScene("TestLevel");
    }

    void Update()
    {
    }
}
