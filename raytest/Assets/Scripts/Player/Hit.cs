using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {
	
	public int health=100;

	void HitBullet(){
		health-=10;
		if(health<=0){
			SendMessage("die");
		}
	}
	
	void die(){
		Destroy(gameObject);
	}

}
