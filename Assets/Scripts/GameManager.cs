using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }

    public LotionManager lotionManager;
    [SerializeField]
    private int _buildSceneIndexToLoad;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    void Start()
    {
        SceneManager.LoadScene(_buildSceneIndexToLoad);
    }

    void Update()
    {
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
