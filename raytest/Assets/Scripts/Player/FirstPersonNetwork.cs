using UnityEngine;
using System.Collections;

public class FirstPersonNetwork : Photon.MonoBehaviour {
	
	MouseLook cameraScript;
	MouseLook cameraScript2;
	CharacterMotor controllerScript;
	Shot shotScript;
	public GameObject camera;

	void Awake () {
		
		cameraScript = GetComponent<MouseLook>();
		cameraScript2 = camera.GetComponent<MouseLook>();
		controllerScript = GetComponent<CharacterMotor>();
		shotScript = GetComponent<Shot>();
		
		 if (photonView.isMine)
        {
            this.camera.camera.enabled=true;
            cameraScript.enabled = true;
			cameraScript2.enabled = true;
			controllerScript.canControl = true;
			shotScript.enabled=true;
        }
        else
        {           
			this.camera.camera.enabled=false;
            cameraScript.enabled = false;
			cameraScript2.enabled = false;
			controllerScript.canControl = false;
			shotScript.enabled=false;
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
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }
    }

    private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
    private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this

    void Update()
    {
        if (!photonView.isMine)
        {
            //Update remote player (smooth this, this looks good, at the cost of some accuracy)
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 5);
			print ("your turn"+gameObject.name+photonView.isMine);
		}

    }
}



