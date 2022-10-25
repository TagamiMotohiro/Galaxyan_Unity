using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerctrl : MonoBehaviour
{
    float radius;
    public float speed;
    float horizontal=0;
    public GameObject bullet;
    public GameObject b;
    // Start is called before the first frame update
    void Start()
    {
        radius = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        CollisionEnemy();
    }
    void PlayerMove()
    {
        horizontal += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        if (horizontal < -8.6) { horizontal = -8.6f; }
        if (horizontal > 8.6) { horizontal = 8.6f; }
        this.transform.position = new Vector3(horizontal, -4, 0);
        if (Input.GetKeyDown(KeyCode.Space)&&b==null)
        {
            b = Instantiate(bullet, this.transform.position, Quaternion.identity);
            b.GetComponent<bulletctrl>().SetTagString("Enemy");
        }
    }
    void CollisionEnemy()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemys)
        {
           var c = e.GetComponent<CollisionCtrl>();
            if (Mathf.Abs(e.transform.position.x - this.transform.position.x) > (this.radius + c.ReturnRadius()))
            { return; }
            if (Mathf.Abs(e.transform.position.y - this.transform.position.y) > (this.radius + c.ReturnRadius()))
            { return; }
           this.Hit();
        }
    }
    void Hit()
    {
        this.gameObject.SetActive(false);
    }
}
