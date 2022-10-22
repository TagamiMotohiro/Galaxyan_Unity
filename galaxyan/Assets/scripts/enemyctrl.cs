using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyctrl : MonoBehaviour
{
    public enum STATE//�X�e�[�^�X���`
    { 
        idle,//�҂�
        takeof,//�ˌ��̂��߂ɑ��񂩂痣�E������������
        attack//�����オ�����̂��ɓˌ�
    }
    public STATE state;
    Vector3[] flyPoint = new Vector3[1];//��яo���̃|�C���g
    Vector3 Stert_Point;//�߂��Ă���n�_
    Vector3 fly_Terget;//�ˌ��Ώےn�_
    float now_Point;//�Ȑ��⊮�p�̐��l
    float rudius;//���g�̑傫��
    public float fly_Speed;//��яo���̃X�s�[�h
    bool flyed = false;//��������яo���Ă��邩�̔���
    GameObject bulletPrefub;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        rudius = 0.25f;
        Stert_Point = this.transform.position;
        for (int i = 0; i < flyPoint.Length; i++)
        {
            flyPoint[i] = this.transform.GetChild(i).gameObject.transform.position;
        }
        fly_Terget = flyPoint[0];
        //new Vector2(this.transform.localScale.x/2,this.transform.localScale.y/2);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (state==STATE.takeof)
        {
            looktoPlyer();
            Fly();
        }
        if (state == STATE.attack)
        {
            looktoPlyer();
            Attack();
        }
    }
    void Return_HomePos()//�ˌ����I��萶���Ă����珊��̈ʒu�ɖ߂�
    {

    }
    public float ReturnRudius()
    {
        return rudius;
    }
    private void Hit()//�e�����������Ƃ��ɌĂ΂��R�[���o�b�N
    {
        Destroy(this.gameObject);
    }
    private void Fly()//����̗����ʒu���痣�E����яo��
    {
        if (Mathf.Abs(this.transform.position.magnitude - flyPoint[0].magnitude) < 0.1f && !flyed)
        {
            this.state = STATE.attack;
        }
        this.transform.position = Vector3.Slerp(Stert_Point, fly_Terget, now_Point);
        now_Point += 1 * Time.deltaTime * fly_Speed;
    }
    public virtual void Attack()//���ꂼ��̂����œˌ����s���I�[�o�[���C�h
    {
        
    }
    private void looktoPlyer()//�v���C���[�̂ق�������������
    {
        this.transform.rotation = Quaternion.FromToRotation(Vector3.up, player.transform.position);
    }
}
