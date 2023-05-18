using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple_Enemy : enemyctrl//紫色(カーブ突進の敵)の突撃部分の継承クラス
{
	bool firstflame = true;
	//反省点　命名が謎すぎて後から見返して何しているのかわかりずらくなった
	float x;
	float x1;
	float pos_x;
	float t1 = 0;
	public override void Attack()
	{
		//継承元クラスにあるStateが攻撃を示す状態になったら
		if (this.GetState() != STATE.attack) {
			firstflame = true;
			t1 = 90f;
			//振れ幅をランダムに取得
			x = Random.Range(2.5f,3.5f);
			//左右に振れる速度をランダムに取得
			x1 = Random.Range(0.25f, 0.5f);
			return; }
		if (firstflame)
		{
			//攻撃開始状態になったら一度だけ処理
			//突撃時のX座標を取得
			pos_x = transform.position.x;
			firstflame = false;
		}
		//横軸はCosで補完した位置に
		//縦軸はそのまま真下に
		this.transform.position = new Vector3(pos_x+(Mathf.Cos(t1*Mathf.Deg2Rad)*(x)),this.transform.position.y-(this.GetAtackSpeed()*Time.deltaTime),0f);
		t1 += x1*axis;
	}
}
