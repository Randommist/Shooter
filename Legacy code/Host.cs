using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;

public class Host : MonoBehaviour
{
    public int Port = 8900;
    private int _packageSize = 128;
    private Socket _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private Thread _accept;
    //private Thread _communication;

    private List<GameObject> _players = new List<GameObject>();
    public GameObject OtherPlayerPrefab;

    private string tempName;
    private Socket tempStream;
    private bool canCreate = false;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _listener.Bind(new IPEndPoint(IPAddress.Any, Port));
        _listener.Listen(5);
        _accept = new Thread(Accept);
        //_communication = new Thread(Communication);
        _accept.Start();
        //_communication.Start();
    }


    void Update()
    {
        foreach(GameObject player in _players)
        {
            if (player.GetComponent<PlayerData>().Stream.Available > 0)
            {
                string messageReceive = Receive(player.GetComponent<PlayerData>().Stream);
                string[] temp = new Regex(";").Split(messageReceive);
                Vector2 pos = new Vector2();
                pos.x = float.Parse(temp[0]);
                pos.y = float.Parse(temp[1]);
                player.transform.position = pos;
            }
            
        }

        if (canCreate)
        {
            canCreate = false;
            GameObject newPlayer = Instantiate(OtherPlayerPrefab);
            newPlayer.GetComponent<PlayerData>().Name = tempName;
            newPlayer.GetComponent<PlayerData>().Stream = tempStream;
            _players.Add(newPlayer);
        }


    }

    void Accept()
    {
        while (true)
        {
            Socket newClient = _listener.Accept();
            string message = Receive(newClient);

            bool isOverlap = false;
            foreach (GameObject player in _players)
            {
                if (player.GetComponent<PlayerData>().Name == message)
                {
                    isOverlap = true;
                }
            }

            if (!isOverlap)
            {
                //Player newPlayer = new Player(message,newClient);
                tempName = message;
                tempStream = newClient;
 
                canCreate = true;

                Debug.Log(message);
            }
            else
            {
                Send("Игрок с таким именем уже существует", newClient);
                Debug.LogWarning("Игрок с таким именем уже существует");
            }
            
            
        }
    }

    void Communication()
    {

    }

    string Receive(Socket stream)
    {
        //Получение. где первые 2 символа - длина
        byte[] buffer = new byte[_packageSize];
        stream.Receive(buffer);
        string message = Encoding.UTF8.GetString(buffer);
        int charCount;
        if (int.TryParse(message.Remove(2), out charCount))
        {
            message = message.Remove(0, 2);
            message = message.Remove(charCount);
            return message;
        }
        return null;
    }

    void Send(string message, Socket stream)
    {
        //Отправка. где первые 2 символа - длина
        byte[] buffer = new byte[_packageSize];
        string charCount = message.Length.ToString();
        if (message.Length > 99)
        {
            return;
        }
        if (message.Length < 10)
        {
            charCount = '0' + charCount;
        }
        string str = charCount + message;
        buffer = Encoding.UTF8.GetBytes(str);
        stream.Send(buffer, buffer.Length, SocketFlags.None);
    }

    private void OnApplicationQuit()
    {
        _accept.Abort();
        //_communication.Abort();
    }

    private void OnDestroy()
    {
        _accept.Abort();
        //_communication.Abort();
    }

}
