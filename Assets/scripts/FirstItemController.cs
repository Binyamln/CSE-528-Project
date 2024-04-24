using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstItemController : MonoBehaviour
{
   private void OnCollect(Collider2D collision) 
   {
        if(collision.tag == "Player") {
            Destroy(gameObject);
        }
   }
}
