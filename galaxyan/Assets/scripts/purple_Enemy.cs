using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purple_Enemy : enemyctrl
{
	bool firstflame = true;
	GameObject atackPos;
	Vector3 atackvector;
	Vector3 curvevector;
	float x;
	float pos_x;
	float t1 = 0;
	public override void Attack()
	{
		if (this.state != STATE.attack) {
			firstflame = true;
			t1 = 90f;
			x = Random.Range(2.5f,3.5f);
			return; }
		if (firstflame)
		{
			pos_x = transform.position.x;
			firstflame = false;
			Debug.Log("firstpos=" + pos_x.ToString());
		}
		Debug.Log((Mathf.Cos(t1*Mathf.Deg2Rad)));
		this.transform.position = new Vector3(pos_x+(Mathf.Cos(t1*Mathf.Deg2Rad)*x),   this.transform.position.y-(this.attack_Speed*Time.deltaTime),0f);
		t1 += (0.5f);//��ā@�����_���ŕύX�@����̎��Ԃ܂Ŕ{���ύX
	}
}
