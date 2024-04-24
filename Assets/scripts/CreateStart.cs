using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateStart : MonoBehaviour
{   
    private SpriteRenderer spriteRenderer;
    public string scene;
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Title") {
            Debug.Log("Hello It Works");
            spriteRenderer.enabled = true;
        }
    }
    void OnMouseDown() {
        Debug.Log("Button Works");
        SceneManager.LoadScene(scene);
    }
}
