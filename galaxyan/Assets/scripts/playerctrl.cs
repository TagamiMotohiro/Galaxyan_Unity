using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class playerctrl : MonoBehaviour
{
    float radius;
    public float speed;
    float horizontal=0;
    public List<GameObject> enemy_List;
    public GameObject bullet;
    public GameObject b;
    public GameObject player_Expro;
    public Sprite bulletEnpty;
    public Sprite bulletFull;
    SpriteRenderer myRend;
    
    // Start is called before the first frame update
    void Start()
    {
        myRend = this.gameObject.GetComponent<SpriteRenderer>();
        radius = 0.25f;
        enemy_List = GameObject.FindGameObjectsWithTag("Enemy").ToList();
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
        if (horizontal < -3.5f) { horizontal = -3.5f; }
        if (horizontal > 3.5f) { horizontal = 3.5f; }
        this.transform.position = new Vector3(horizontal, -4, 0);
        if (Input.GetKeyDown(KeyCode.Space)&&b==null)
        {
            b = Instantiate(bullet, this.transform.position, Quaternion.identity);
            b.GetComponent<bulletctrl>().SetTagString("Enemy");
        }
        if (b == null)
        { myRend.sprite = bulletFull; }
        else
        { myRend.sprite = bulletEnpty; }
    }
    void CollisionEnemy()
    {
        enemy_List.RemoveAll(a=>a==null);
        foreach (GameObject e in enemy_List)
        {
            var c = e.GetComponent<CollisionCtrl>();
            if (Mathf.Abs(e.transform.position.x - transform.position.x) > (this.radius + c.ReturnRadius()))
            { continue; }
            if (Mathf.Abs(e.transform.position.y - transform.position.y) > (this.radius + c.ReturnRadius()))
            { continue; }
            if (Mathf.Abs(e.transform.position.z - transform.position.z) > (this.radius + c.ReturnRadius()))
            { continue; }
            this.Hit();
        }
    }
    void Hit()
    {
        Instantiate(player_Expro,this.transform.position,Quaternion.identity);
        this.gameObject.SetActive(false);
    }
}
