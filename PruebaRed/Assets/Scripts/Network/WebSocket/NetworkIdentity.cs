using Project.Utility.Attributes;
using SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project.Networking
{
    public class NetworkIdentity : MonoBehaviour
    {
        [Header("Helpful Values")]
        [SerializeField]
        [GreyOut]
        private string alias;
        [SerializeField]
        [GreyOut]
        private bool isControlling = false;

        private SocketIOComponent socket;


        void Awake()
        {
            isControlling = false;
        }

        // Update is called once per frame
        public void SetControllerID(string ID)
        {
            alias = ID;
            isControlling = (MultiplayerIO.ClientAlias == ID) ? true : false;
        }
        public void SetSocketReference(SocketIOComponent Socket)
        {
            socket = Socket;
        }
        public string GetID()
        {
            return alias;
        }
        public bool IsControlling()
        {
            return isControlling;
        }
        public SocketIOComponent GetSocket()
        {
            return socket;
        }
    }
}
