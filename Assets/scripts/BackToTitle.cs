using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToTitle : MonoBehaviour
{
    public string scene;
    void OnMouseDown() {
        Debug.Log("Hello");
        SceneManager.LoadScene(scene);
    }
}
