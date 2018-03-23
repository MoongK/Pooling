using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour {

    InputField myInput;
    GameObject baseServer;
    GameObject BalloonRoom;
    Object Balloon;

    MyInformation Myinfo;
    ReceivedInformation Receivedinfo;
    string message;

	void Start () {
        myInput = transform.Find("InputField").GetComponent<InputField>(); // 과정 1. inputfield로부터 message 받기
        baseServer = GameObject.Find("BaseServer");
        Balloon = Resources.Load("Balloon");
        BalloonRoom = gameObject.transform.Find("Room").Find("Scroll View").Find("Viewport").Find("RoomContent").gameObject;
	}
	
	
	void Update ()
    {
        if (myInput.text != string.Empty)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            {
                message = myInput.text;
                MakeInfo(gameObject, message); // 과정 2-1. 내 정보 생성 후
                SendInfoToServer(); //  2-2. Base Server로 보내기
                myInput.text = string.Empty;
            }
        }
	} // Update


    public void MakeInfo(GameObject _gameObject, string _message) // 내 정보 생성
    {
        print("(Client)From : " + _gameObject);
        print("(Client)Said : " + _message);
        Myinfo = new MyInformation(_gameObject, _message);
    }

    public void SendInfoToServer() // Server로 정보 보내기
    {
        baseServer.GetComponent<BaseServer>().ReceivedClientInfo(Myinfo);
    }

    public void ReceiveInfoFromServer(ToSendInfo _InfoFromServer) // 서버가 보낸 정보 받기 , 말풍선 생성
    {
        Receivedinfo = new ReceivedInformation(_InfoFromServer.RecOb, _InfoFromServer.RecMes);
        print("(Client)(Received)From : " + Receivedinfo.ReceiveObject);
        print("(Client)(Received)Said : " + Receivedinfo.ReceiveMessage);

        MakeBalloon(Receivedinfo); // 말풍선
    }

    void MakeBalloon(ReceivedInformation _recievedClientInfo) // 풍선 생성기
    {
        GameObject realballoon;

        var balloon = Instantiate(Balloon, BalloonRoom.transform);
        realballoon = balloon as GameObject;
        realballoon.gameObject.transform.Find("Text").GetComponent<Text>().text = Receivedinfo.ReceiveMessage;
        //realballoon.gameObject.transform.Find("Name").GetComponent<Text>().text = Receivedinfo.ReceiveObject.name;

        if (_recievedClientInfo.ReceiveObject == gameObject) // 내 풍선일 때.
        {
            realballoon.GetComponent<Image>().color = Color.white;
            print("(Client)My information! (From) : " + Receivedinfo.ReceiveObject.name);
        }
        else // 상대 풍선일 때
        {
            realballoon.GetComponent<Image>().color = Color.yellow;
            print("(Client)Other information! (From) : " + gameObject.name);
        }
    }

} // MainClass

public class MyInformation // 자신 정보
{
    public GameObject Myobject { get; set; }
    public string Mymessage { get; set; }

    public MyInformation(GameObject _ob, string _mes)
    {
        Myobject = _ob;
        Mymessage = _mes;
    }
}

public class ReceivedInformation // Base Server로부터 받은 정보
{
    public GameObject ReceiveObject { get; set; }
    public string ReceiveMessage { get; set; }

    public ReceivedInformation(GameObject _resOb, string _resMes)
    {
        ReceiveObject = _resOb;
        ReceiveMessage = _resMes;
    }
}