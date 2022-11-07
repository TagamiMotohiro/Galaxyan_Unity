using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purple_Enemy : enemyctrl
{
	bool firstflame = true;
	Vector3 atackPos;
	Vector3 atackvector;
	public float amplitude=0.3f;
	float t1 = 1;
	public override void Attack()
	{
		if (firstflame)
		{
			//“ËŒ‚‚·‚éƒxƒNƒgƒ‹‚ðŒˆ’è
			atackPos = GameObject.Find("player").transform.position;
			atackvector = (atackPos - this.transform.position).normalized;
			firstflame = false;
		}
		this.transform.position += new Vector3(atackvector.x*Mathf.Cos(2 * Mathf.PI *0.5f* Time.time), atackvector.y * Time.deltaTime, 0);
	}
}
