using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyctrl : MonoBehaviour
{
    float radius;
    // Start is called before the first frame update
    void Start()
    {
        radius = 0.5f/2;//new Vector2(this.transform.localScale.x/2,this.transform.localScale.y/2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float ReturnRudius()
    {
        return radius;
    }
	private void Hit()
	{
        Destroy(this.gameObject);
	}
}
