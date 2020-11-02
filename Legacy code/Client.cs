using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

public class Client : MonoBehaviour
{
    public GameObject MyPlayer;

    private IPAddress _serverIP;
    private Socket _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private int _packageSize = 128;

    public string Connect(string serverIP, int serverPort, string PlayerName)
    {
        DontDestroyOnLoad(gameObject);
        if(IPAddress.TryParse(serverIP, out _serverIP))
        {
            try
            {
                _server.Connect(new IPEndPoint(_serverIP, serverPort));
                Send(PlayerName, _server);
                return "OK";
            }
            catch
            {
                return "Ошибка подключения";
            }
        }
        else
        {
            return "Неверный IP";
        }

    }

    void Update()
    {
        if(MyPlayer != null)
        {
            Vector2 position = MyPlayer.transform.position;
            string messageSend = position.x + ";" + position.y;
            Send(messageSend, _server);
            Debug.Log(messageSend);
        }
        else
        {
            MyPlayer = GameObject.Find("Player");
        }
    }

    void Send(string message, Socket stream)
    {
        //Кодирование. где первые 2 символа - длина
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

}
