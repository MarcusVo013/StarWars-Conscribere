using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MeshSocket : MonoBehaviour
{
    public MeshSockets.SocketId soketId;
    public HumanBodyBones bone;

    public Vector3 offset;
    public Vector3 rotaion;

    Transform attachPoint;
    void Start()
    {
        Animator animator = GetComponentInParent<Animator>();
        attachPoint = new GameObject("socket" + soketId).transform;
        attachPoint.SetParent(animator.GetBoneTransform(bone));
        attachPoint.localPosition = offset;
        attachPoint.localRotation = Quaternion.Euler(rotaion);
    }

   public void Attach(Transform objectTranforms)
    {
        objectTranforms.SetParent(attachPoint, false);
    }
}
