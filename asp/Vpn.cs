using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Threading;



namespace NETWORK
{
    class OpenVPNCon : IDisposable
    {
        public enum Signal
        {
            Hup,
            Term,
            Usr1,
            Usr2
        }

        private Socket sock;
        private const int bsize = 1024;
        private string openvpPath;
        private readonly Process prc = new();
        private static TcpListener server;
        private readonly string openvpEXE;

        public string Pid { get => SendCommand("pid"); }

        private void Run()
        {
            prc.StartInfo.CreateNoWindow = false;
            prc.EnableRaisingEvents = true;                  //event name
            prc.StartInfo.Arguments = $"--config {openvpPath} --service {"ovpnE"}";
            prc.StartInfo.Verb = "runas";
            prc.StartInfo.FileName = openvpEXE;
            prc.Start();
        }

        public OpenVPNCon(
            string host,
            int port,
            string ofile,
            string uid,
            string pwd,
            string ovpnpath = @"C:\Program Files\OpenVPN\bin\openvpn.exe"
        )
        {
            openvpEXE = ovpnpath;
            openvpPath = Path.Combine(Directory.GetCurrentDirectory(), ofile);
            var ovpnFileContent = File.ReadAllLines(ofile);

            var idx = Array.FindIndex(ovpnFileContent, x => x.StartsWith("management"));
            if (idx >= 0){
                ovpnFileContent[idx] = $"management {host} {port}";
            }
            else {
                var lastIdx = ovpnFileContent.Length - 1;
                var lastLine = ovpnFileContent[lastIdx];
                ovpnFileContent[lastIdx] = $"{lastLine}{Environment.NewLine}management {host} {port}";
            }
            var idx2 = Array.FindIndex(ovpnFileContent, x => x.StartsWith("auth-user-pass"));
            if (idx2 >= 0){
                var passFileName = Path.Combine(Path.GetTempPath(), "ovpnpass.txt").Replace(@"\", @"\\");
                File.WriteAllLines(passFileName, new string[] { uid, pwd });
                ovpnFileContent[idx2] = $"auth-user-pass {passFileName}";
                
            }
            File.WriteAllLines(ofile, ovpnFileContent);
            Run();
            server = new(port);
            server.Start();
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Connect(host, port);
            //SendGreeting();
            //SendCommand("log on all");
        }

        

        private void SendGreeting()
        {
            var bf = new byte[bsize];
            int rb = sock.Receive(bf, 0, bf.Length, SocketFlags.None);
            if (rb < 1)
            {
                throw new SocketException();
            }
        }
        public string Kill(string name)
        {
            return SendCommand($"kill {name}");
        }

        public string Kill(string host, int port)
        {
            return SendCommand($"kill {host}:{port}");
        }
        public string SendSignal(Signal sgn)
        {
            return SendCommand($"signal SIG{sgn.ToString().ToUpper()}");
        }
      
        private string SendCommand(string cmd)
        {
            sock.Send(Encoding.Default.GetBytes(cmd + "\r\n"));
            var bf = new byte[bsize];
            var sb = new System.Text.StringBuilder();
            int rb;
            string str = "";
            while (true)
            {
                Thread.Sleep(100);
                rb = sock.Receive(bf, 0, bf.Length, 0);
                str = Encoding.UTF8.GetString(bf).Replace("\0", "");
                if (rb < bf.Length)
                {
                    if (str.Contains("\r\nEND"))
                    {
                        var a = str.Substring(0, str.IndexOf("\r\nEND"));
                        sb.Append(a);
                    }
                    else if (str.Contains("SUCCESS: "))
                    {
                        var a = str.Replace("SUCCESS: ", "").Replace("\r\n", "");
                        sb.Append(a);
                    }
                    else if (str.Contains("ERROR: "))
                    {
                        var msg = str.Replace("ERROR: ", "").Replace("\r\n", "");
                        throw new ArgumentException(msg);
                    }
                    else
                        continue;
                    break;
                }
                else
                    sb.Append(str);
            }

            return sb.ToString();
        }

        public void Dispose(){
            if (sock  is not  null)
                if (openvpPath is not null)
                   SendSignal(Signal.Term);

            sock!.Dispose();
            server!.Stop();
            prc.Close();
        }

    }
}

