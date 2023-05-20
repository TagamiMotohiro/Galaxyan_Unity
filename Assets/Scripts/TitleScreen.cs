using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ƒ^ƒCƒgƒ‹‚©‚ç‚Ì‘JˆÚ
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("MainGame");
        }
    }
}
