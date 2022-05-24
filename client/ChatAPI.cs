using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;

namespace client
{
    internal class ChatAPI
    {
        internal class User
        {
            public string id { get; set; }
            public string username { get; set; }
        }

        internal class Chat
        {
            public string chat_id { get; set; }
            public string name { get; set; }
            public User[] users { get; set; }
        }

        internal class Message
        {
            public string id { get; set; }
            public string user_id { get; set; }
            public string username { get; set; }
            public string text { get; set; }
        }

        internal class UserAuth
        {
            public string user_id { get; set; }
            public string token { get; set; }
        }

        public static UserAuth LoadAuth(string path)
        {
            if (!File.Exists(path))
                throw new Exception("Auth file with path \"" + path + "\" does not exist.");

            FileStream fs = File.Open(path, FileMode.Open);
            byte[] buff = new byte[4096];
            string content = "";
            int read = 0;

            while((read = fs.Read(buff, 0, buff.Length)) > 0)
                content += Encoding.ASCII.GetString(buff, 0, read);

            fs.Close();
            fs.Dispose();

            UserAuth auth = JsonConvert.DeserializeObject<UserAuth>(content);

            if (auth == null || auth.user_id.Length <= 0 || auth.token.Length <= 0)
                throw new Exception("Could not load auth info.");

            return auth;
        }

        public static void SaveAuth(string path, UserAuth auth)
        {
            string json = JsonConvert.SerializeObject(auth);
            File.WriteAllText(path, json);
        }

        private static string fetchText(string url)
        {
            string body = "";

            WebRequest req = WebRequest.Create(url);
            req.Method = "GET";

            WebResponse res = req.GetResponse();
            int status = (int)((HttpWebResponse)res).StatusCode;

            if (status != 200)
            {
                res.Dispose();
                req.Abort();
                throw new Exception("Status code is " + status);
            }

            Stream s = res.GetResponseStream();

            byte[] buff = new byte[4096];
            int read = 0;

            while((read = s.Read(buff, 0, buff.Length)) != 0)
                body += Encoding.UTF8.GetString(buff, 0, read);

            res.Dispose();
            s.Dispose();
            req.Abort();
            buff = null;

            return body;
        }

        private static string postData(string url, Dictionary<string, string> urlData)
        {
            string response = "";
            Stream dataS = new FormUrlEncodedContent(urlData).ReadAsStreamAsync().GetAwaiter().GetResult();

            WebRequest req = WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = dataS.Length;
            
            Stream s = req.GetRequestStream();

            byte[] buff = new byte[4096];
            int read = 0;

            while ((read = dataS.Read(buff, 0, buff.Length)) > 0)
                s.Write(buff, 0, read);

            s.Close();

            WebResponse res = req.GetResponse();
            int status = (int)((HttpWebResponse)res).StatusCode;

            if (status == 200)
            {
                Stream resS = res.GetResponseStream();

                while ((read = resS.Read(buff, 0, buff.Length)) > 0)
                    response += Encoding.ASCII.GetString(buff, 0, read);
            }

            dataS.Dispose();
            s.Dispose();
            req.Abort();
            res.Dispose();
            buff = null;

            if (status != 200)
            {
                res.Dispose();
                req.Abort();
                throw new Exception("Status code is " + status);
            }

            return response;
        }

        public static void SendMessage(string url, string text, string chatId, string userId, string token) => 
            postData($"{url}/sendMessage.php", new Dictionary<string, string>()
            {
                { "text", text },
                { "chat", chatId },
                { "user", userId },
                { "token", token }
            });

        public static List<Chat> GetChats(string url, string userId, string token)
        {
            string res = fetchText($"{url}/getChats.php?user={userId}&token={token}");
            return JsonConvert.DeserializeObject<List<Chat>>(res);
        }

        public static List<Message> GetMessages(string url, string chatId, string userId, string token)
        {
            string res = fetchText($"{url}/getMessages.php?user={userId}&chat={chatId}&token={token}");
            return JsonConvert.DeserializeObject<List<Message>>(res);
        }

        public static UserAuth RegisterUser(string url, string username, string password)
        {
            try
            {
                string res = postData($"{url}/register.php", new Dictionary<string, string>()
                {
                    { "username", username },
                    { "password", password }
                });

                return JsonConvert.DeserializeObject<UserAuth>(res);
            } catch
            {
                return null;
            }
        }

        public static UserAuth LoginUser(string url, string username, string password)
        {
            try
            {
                string res = postData($"{url}/login.php", new Dictionary<string, string>()
                {
                    { "username", username },
                    { "password", password }
                });

                return JsonConvert.DeserializeObject<UserAuth>(res);
            }
            catch
            {
                return null;
            }
        }

        public static bool CreateChat(string url, string name, string userId, string token)
        {
            try
            {
                string res = postData($"{url}/createChat.php", new Dictionary<string, string>()
                {
                    { "name", name },
                    { "user", userId },
                    { "token", token }
                });

                return res != "false";
            }
            catch
            {
                return false;
            }
        }
    }
}
