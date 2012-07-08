using UnityEngine;
using System.Collections;

public class FirstPersonNetwork2 : Photon.MonoBehaviour {

	MouseLook cameraScript2;
	Shot shotScript;

	void Awake () {
		
		cameraScript2 = this.GetComponent<MouseLook>();
		shotScript = this.GetComponent<Shot>();
		
		
		 if (photonView.isMine)
        {
            //MINE: local player, simply enable the local scripts
            cameraScript2.enabled = true;
            shotScript.enabled = true;
        }
        else
        {           
            cameraScript2.enabled = false;
			shotScript.enabled = false;
        }

        gameObject.name = gameObject.name + photonView.viewID.ID;
	
	}
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation); 
        }
        else
        {

        }
    }
	
	void Update () {
	
		//print ("2:"+photonView.isMine+gameObject.name);
		
	}
	
}
