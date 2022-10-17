using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerctrl : MonoBehaviour
{
    public float speed;
    float horizontal=0;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }
    void PlayerMove()
    {
        
        Mathf.Clamp(horizontal += Input.GetAxis("Horizontal") * speed * Time.deltaTime,-8.6f,8.6f);
        this.transform.position = new Vector3(horizontal, -4, 0);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet, this.transform.position, Quaternion.identity).
            GetComponent<bulletctrl>().SetTagString("Enemy");
        }
    }
    
}
