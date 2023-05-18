using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletctrl : MonoBehaviour//�e�̓����蔻���Ԃ��N���X(�G��������)
{
    public float speed;
    [SerializeField]
    string TargetTag;//�����蔻���Ԃ��Ώۂ̃^�O�̕�����
    float bullet_alive = 0f;
    [SerializeField]
    SpriteRenderer MySpriteRenderer;
    //�G�̓����蔻���Ԃ��N���X�̃��X�g
    List<CollisionCtrl> enemyCollision_List=new List<CollisionCtrl>();

    // Start is called before the first frame update
    void Start()
    {
        //�G�̓����蔻���Ԃ��N���X���擾
        GameObject[] g = GameObject.FindGameObjectsWithTag(TargetTag);
        foreach (var Collision_Item in g)
        {
            enemyCollision_List.Add(Collision_Item.GetComponent<CollisionCtrl>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        BulletTransForm();
        ColisionDirection();
    }
    void BulletTransForm()
    {
        if (TargetTag == "Player")//�^�[�Q�b�g���v���C���[���G���Œe�̓����𕪊�
        {
            this.transform.position += Vector3.down * speed * Time.deltaTime;
        }
        else
        {
            this.transform.position += this.transform.up.normalized * speed * Time.deltaTime;
        }
        bullet_alive++;
        if (!MySpriteRenderer.isVisible&&bullet_alive>5f)
        {
            Destroy(this.gameObject);
        }
        
    }
    void ColisionDirection()//������ꂽ�Ώۂɒe���������Ă��邩�̔���
    {
        if (enemyCollision_List == null) { return; }
        foreach (CollisionCtrl Collision in enemyCollision_List)
        {
            BulletColision(Collision);
        }
    }
    void BulletColision(CollisionCtrl enemy)//���쓖���蔻��
    {
        if (Mathf.Abs(this.transform.position.x - enemy.transform.position.x) < enemy.ReturnRadius() &&
           Mathf.Abs(this.transform.position.y - enemy.transform.position.y) < enemy.ReturnRadius())
        {
            enemy.gameObject.GetComponent<enemyctrl>().Hit();
            Destroy(this.gameObject);
        }
    }
    public void SetTarget(string tag)//������Ώۂ�tag��p���Đݒ肷��ۂɎg�p���镶����̕ύX
    {
        TargetTag = tag;
    }
}
