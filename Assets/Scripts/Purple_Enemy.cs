using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple_Enemy : enemyctrl//���F(�J�[�u�ːi�̓G)�̓ˌ������̌p���N���X
{
	bool firstflame = true;
	//���ȓ_�@�������䂷���Čォ�猩�Ԃ��ĉ����Ă���̂��킩�肸�炭�Ȃ���
	float x;
	float x1;
	float pos_x;
	float t1 = 0;
	public override void Attack()
	{
		//�p�����N���X�ɂ���State���U����������ԂɂȂ�����
		if (this.GetState() != STATE.attack) {
			firstflame = true;
			t1 = 90f;
			//�U�ꕝ�������_���Ɏ擾
			x = Random.Range(2.5f,3.5f);
			//���E�ɐU��鑬�x�������_���Ɏ擾
			x1 = Random.Range(0.25f, 0.5f);
			return; }
		if (firstflame)
		{
			//�U���J�n��ԂɂȂ������x��������
			//�ˌ�����X���W���擾
			pos_x = transform.position.x;
			firstflame = false;
		}
		//������Cos�ŕ⊮�����ʒu��
		//�c���͂��̂܂ܐ^����
		this.transform.position = new Vector3(pos_x+(Mathf.Cos(t1*Mathf.Deg2Rad)*(x)),this.transform.position.y-(this.GetAtackSpeed()*Time.deltaTime),0f);
		t1 += x1*axis;
	}
}
