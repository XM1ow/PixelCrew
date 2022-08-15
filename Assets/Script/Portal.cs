using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.tag = "Portal";
    }

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
