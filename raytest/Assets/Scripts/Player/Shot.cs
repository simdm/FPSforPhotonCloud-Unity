using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {
	
	public GameObject target;
	RaycastHit hit;
	public GameObject Pistol; 
	public int bullets = 13;
	public bool gunloading = false;
	public GameObject player;
	public CharacterMotor cm;
	
	void Awake(){
		
		cm = player.GetComponent<CharacterMotor>();
		
	}
	
	void Update () {
		
		if(Input.GetMouseButtonDown(0)){
			
				if(bullets<=0){
					StartCoroutine(Load(2.0f));
				}else{
				
				if(!gunloading){
					bullets--;
					Pistol.animation.Play("idle");
					Pistol.animation.Play("shot");
			
				Vector3 fwd = transform.TransformDirection(Vector3.forward);
				if (Physics.Raycast(transform.position, fwd, out hit , 500))
				{
					if(hit.transform.gameObject!=this.gameObject){
						hit.transform.gameObject.SendMessage("HitBullet");
					}
				}
				else
				{
					return;
				}
				}
			}
		}
		
		Pistol.animation.CrossFadeQueued("gunwalk");
		
		if(Input.GetKeyDown(KeyCode.R)){
			if(bullets!=13){
				print("reloading...");
				StartCoroutine(Load (2.0f));
			}
		}
		
		if(Input.GetKey(KeyCode.LeftShift)){
			cm.movement.maxForwardSpeed=8;
			cm.movement.maxBackwardsSpeed=8;
			cm.movement.maxSidewaysSpeed=8;
		}else{
			cm.movement.maxForwardSpeed=5;
			cm.movement.maxBackwardsSpeed=5;
			cm.movement.maxSidewaysSpeed=5;
		}
	
	}
	
	
	
	public IEnumerator Load(float time) {
		gunloading = true;
		Pistol.animation.Play("load");
		yield return new WaitForSeconds(time);
		Pistol.animation.CrossFadeQueued("idle");
		bullets=13;
		gunloading = false;
		print("reloaded");
	}

	
}
