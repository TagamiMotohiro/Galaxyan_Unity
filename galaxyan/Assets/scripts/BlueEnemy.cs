using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemy : enemyctrl
{
	
	bool firstflame=true;
	Vector3 atackPos;
	Vector3 atackvector;
	public override void Attack()
	{
		if (firstflame)
		{	
			//“ËŒ‚‚·‚éƒxƒNƒgƒ‹‚ðŒˆ’è
			atackPos = GameObject.Find("player").transform.position;
			atackvector = (atackPos - this.transform.position).normalized;
			firstflame = false;
		}
		this.transform.position += atackvector * Time.deltaTime * attack_Speed;
	}
}
