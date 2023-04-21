using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyctrl : MonoBehaviour
{
    public enum STATE//�X�e�[�^�X���`
    { 
        back,//�����̃|�W�V�����֋A�Ғ�
        idle,//�҂�
        takeof,//�ˌ��̂��߂ɑ��񂩂痣�E������������
        attack//�ˌ�
    }
    [SerializeField]
    GameObject bulletPrefub;
    [SerializeField]
    GameObject effect;
    [SerializeField]
    private STATE state;
    [SerializeField]
    private float attack_Speed;//�v���C���[�ɓˌ�����Ƃ��̑��x
    Transform[] flyobfj = new Transform[2];
    Vector3[] flyPoint = new Vector3[2];//��яo���̃|�C���g
    Vector3 p1;
    Vector3 Stert_Point;//�߂��Ă���n�_
    public float magazine;
    public int late;
    int cool_time;
    float now_Point;//�Ȑ��⊮�p�̐��l
    float ReturnPosition_Y=0;//�߂��Ă���ۂ�Y���W(�ǂꂾ���X�^�[�g�n�_���痣��Ă��邩)
    public float fly_Speed;//��яo���̃X�s�[�h
    bool flyed = false;//��������яo���Ă��邩�̔���
    protected float axis=1;
    GameObject maneger;
    GameObject player;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        //���X�̎擾
        //(�ŏ�����scene�ɒu�����ق��������ŃR�[�h�Ƃ��Ă̎��܂���������Ƃ͔c�����Ă��܂����A�ۑ萧��̕��j���炱�̂悤�ȏ��������Ă��܂�)
        maneger = this.transform.root.gameObject;
        anim = GetComponent<Animator>();
        attack_Speed = 3;
        player = GameObject.Find("player");
        p1 = this.transform.position;
        for (int i = 0; i < flyPoint.Length; i++)
        {
            flyobfj[i] = this.transform.GetChild(i).gameObject.transform;
            flyPoint[i] = flyobfj[i].position;
            if (this.transform.position.x < 0)
            {
                flyobfj[i].localPosition = new Vector3(-flyobfj[i].localPosition.x, flyobfj[i].localPosition.y, 0);
                flyPoint[i] = flyobfj[i].position;
                axis = -1;
            }
        }
    }

	void Update()
	{
        //��ї��|�C���g�̒�`
        Stert_Point = new Vector3(p1.x+maneger.transform.position.x,p1.y,p1.z);
        //�e��Ԃɍ��킹���s���̒�`
        if (this.state == STATE.attack && this.transform.position.y<-5)
        {
            this.state = STATE.back;
            this.transform.position = new Vector3(Stert_Point.x, 5, 0);
            this.transform.rotation = Quaternion.identity;
        }
        if (this.state == STATE.back)
        {
            this.magazine = 2;
            now_Point = 0;
            Return_HomePos();
        }
        if (state == STATE.takeof)
        {
            Fly();
        }
        if (state == STATE.attack)
        {
            looktoPlyer();
            BulletFire();
        }
        if (state == STATE.idle)
        {
            anim.SetBool("Idle", true);
        }
        else
        {
            anim.SetBool("Idle", false);
        }
        Attack();
    }
	void Return_HomePos()//�ˌ����I��萶���Ă����珊��̈ʒu�ɖ߂�
    {
        this.transform.position = new Vector3(Stert_Point.x,this.transform.position.y-ReturnPosition_Y,0);
        if (this.transform.position.y < Stert_Point.y)
        {
            //�ҋ@��Ԃɖ߂�
            this.state = STATE.idle;
            ReturnPosition_Y = 0;
        }
        //
        ReturnPosition_Y+=0.1f*Time.deltaTime;
    }
    private void Hit()//�e�����������Ƃ��ɌĂ΂��R�[���o�b�N
    {
        Instantiate(effect, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    protected void Fly()//����̗����ʒu���痣�E����яo��
    {
        if (this.state == STATE.takeof)
        {
            this.transform.position = GetCurve(flyPoint[0], flyPoint[1], now_Point);
            this.transform.rotation = Quaternion.AngleAxis(now_Point*(axis*180), Vector3.forward);
            now_Point+=fly_Speed*Time.deltaTime;
            if (now_Point >= 1)
            {
                //�U�������Ɉڍs
                this.state = STATE.attack;
            }
        }
    }
    void TakeOf()
    {
        this.state = STATE.takeof;
    }
    public virtual void Attack()//���ꂼ��̂����œˌ����s���I�[�o�[���C�h
    {
        
    }
    protected void looktoPlyer()//�v���C���[�̂ق�������������
    {
        this.transform.rotation = Quaternion.FromToRotation(Vector3.up, player.transform.position-this.transform.position);
    }

    Vector3 GetCurve(Vector3 a,Vector3 b,float t)//(��ї������p)�X�^�[�g�n�_��2�_�̃|�C���g����t%�n�_�̋Ȑ��⊮���o��
    {
        Vector3 result;
        Vector3 p0 = Vector3.Lerp(Stert_Point, a, t);
        Vector3 p1 = Vector3.Lerp(a, b, t);

        result = Vector3.Lerp(p0,p1,t);

        return result;
    }
    protected void BulletFire()
    {
        //�e�𐶐�
        if (cool_time >= late && magazine > 0)
        {
            GameObject g =
            Instantiate(bulletPrefub, this.transform.position, Quaternion.FromToRotation(Vector3.up, this.transform.up));
            //�e�C���X�^���X�̓�����Ώۂ�ݒ�
            g.GetComponent<bulletctrl>().SetTagString("Player");
            cool_time = 0;
            magazine--;
        }
        cool_time++;
    }
    //�Q�b�^�[���\�b�h
    public STATE GetState()
    {
        return state;
    }
    public float GetAtackSpeed()
    {
        return attack_Speed;
    }
}
