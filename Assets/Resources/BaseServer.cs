using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseServer : MonoBehaviour {

    ReceivedClient recievedInfo;
    ToSendInfo SendingInfo;
    GameObject[] AllUsers;

    void Start () {
        AllUsers = GameObject.FindGameObjectsWithTag("Users");
	}


    public void ReceivedClientInfo(MyInformation _client)
    {
        recievedInfo = new ReceivedClient(_client.Myobject, _client.Mymessage);
        print("(Server)From : " + recievedInfo.RecOb);
        print("(Server)Said : " + recievedInfo.RecMes);
        SendInfoToClient(recievedInfo);

    }

    void SendInfoToClient(ReceivedClient _clientInfo)
    {
        SendingInfo = new ToSendInfo(_clientInfo.RecOb, _clientInfo.RecMes);
        print("(Server)Sending OB : " + SendingInfo.RecOb);
        print("(Server)Sending Mes : " + SendingInfo.RecMes);

        foreach(GameObject ob in AllUsers)
        {
            ob.GetComponent<Client>().ReceiveInfoFromServer(SendingInfo);
        }
    }
}

public class ReceivedClient
{
    public GameObject RecOb { get; set; }
    public string RecMes { get; set; }

     public ReceivedClient(GameObject _ob, string _Mes)
     {
        RecOb = _ob;
        RecMes = _Mes;
     }
}

public class ToSendInfo
{
    public GameObject RecOb { get; set; }
    public string RecMes { get; set; }

    public ToSendInfo(GameObject _ob, string _Mes)
    {
        RecOb = _ob;
        RecMes = _Mes;
    }
}
