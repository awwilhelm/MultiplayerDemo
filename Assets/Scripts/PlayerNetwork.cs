using UnityEngine;
using System.Collections;

public class PlayerNetwork : Photon.MonoBehaviour
{
    private GameObject playerCamera;
    private CameraLook cameraLookScript;
    private PlayerController playerControllerScript;

	// Use this for initialization
	void Awake () {
        playerCamera = transform.FindChild("Mesh").FindChild("PlayerCamera").transform.gameObject;
        cameraLookScript = playerCamera.GetComponent<CameraLook>();
        playerControllerScript = GetComponent<PlayerController>();

        if (photonView.isMine)
        {
            //MINE: local player, simply enable the local scripts
            cameraLookScript.enabled = true;
            playerControllerScript.enabled = true;
            playerCamera.SetActive(true);
        }
        else
        {
            playerCamera.SetActive(false);
            cameraLookScript.enabled = false;

            playerControllerScript.enabled = true;
            playerControllerScript.isControllable = false;
        }
        gameObject.name = gameObject.name + photonView.viewID;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //We own this player: send the others our data
            //stream.SendNext((int)playerControllerScript._characterState);
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            //Network player, receive data
            //playerControllerScript._characterState = (CharacterState)(int)stream.ReceiveNext();
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
        }
    }
}
