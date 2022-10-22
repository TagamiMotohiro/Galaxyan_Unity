using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletctrl : MonoBehaviour
{
    public float speed;
    [SerializeField]
    string tagstring;
    float bullet_alive = 0f;
    SpriteRenderer thissprite;
    
    // Start is called before the first frame update
    void Start()
    {
        thissprite = this.GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        BulletTransForm();
        ColisionDirection();
    }
    void BulletTransForm()
    { 
        this.transform.position += this.transform.up*speed*Time.deltaTime;
        bullet_alive++;
        if (!thissprite.isVisible&&bullet_alive>5f)
        {
            Destroy(this.gameObject);
        }
        
    }
    void ColisionDirection()
    {
        GameObject[] enemy_List = GameObject.FindGameObjectsWithTag(tagstring);
        if (enemy_List == null) { return; }
        foreach (GameObject obj in enemy_List)
        {
            BulletColision(obj);
        }
    }
    void BulletColision(GameObject enemy)
    {
        enemyctrl e = enemy.GetComponent<enemyctrl>();
        Debug.Log(e.ReturnRudius());
        if (Mathf.Abs(this.transform.position.x - enemy.transform.position.x) < e.ReturnRudius()+0.1 &&
           Mathf.Abs(this.transform.position.y - enemy.transform.position.y) < e.ReturnRudius()+0.1)
        {
            Debug.Log("“–‚½‚Á‚½");
            enemy.SendMessage("Hit");
            Destroy(this.gameObject);
        }
    }
    public void SetTagString(string tag)
    {
        tagstring = tag;
    }
}
