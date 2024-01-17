using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MeshSockets : MonoBehaviour
{
    public enum SocketId 
    { 
        Spine
    }
    Dictionary<SocketId, MeshSocket> socketMap = new Dictionary<SocketId, MeshSocket>();
    void Start()
    {
        MeshSocket[] sockets = GetComponentsInChildren<MeshSocket>();
        foreach(var socket in sockets) { socketMap[socket.soketId] = socket; }
    }
    public void Attach (Transform objectTranform, SocketId socketId)
    {
        socketMap[socketId].Attach(objectTranform);
    }
}
