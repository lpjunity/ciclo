using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     
        
        
    }
    
    public void AnimateBasicMove(GameObject animObj)
    {
        Vector3 position = animObj.transform.position;  
        LeanTween.move(animObj, position + new Vector3(50, 0, 0), 5f).setEaseInCirc();
    }

}
