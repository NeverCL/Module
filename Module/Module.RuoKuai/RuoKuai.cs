using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Module.RuoKuai
{
    public class RuoKuai
    {
        #region ctor
        private readonly string _username;
        private readonly string _password;
        private readonly string _typeid;
        private readonly string _softid = "36472";
        private readonly string _softkey = "4f0fc07c10574dcaa7f9b0391ee8e2bd";
        private readonly string _timeout;

        public RuoKuai(string username, string password, string typeid, int limit = 5, string timeout = "90")
        {
            _username = username;
            _password = password;
            _typeid = typeid;
            _timeout = timeout;
            //重要！
            //重要！根据线程实际使用情况调整。
            //修改HTTP请求默认连接数，默认是2。
            System.Net.ServicePointManager.DefaultConnectionLimit = limit;//64
        }

        /// <summary>
        /// 暂时私有化
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="typeid"></param>
        /// <param name="softid"></param>
        /// <param name="softkey"></param>
        /// <param name="limit"></param>
        /// <param name="timeout"></param>
        internal RuoKuai(string username, string password, string typeid, string softid, string softkey, int limit = 5, string timeout = "90")
        {
            _username = username;
            _password = password;
            _typeid = typeid;
            _softid = softid;
            _softkey = softkey;
            _timeout = timeout;
            //重要！
            //重要！根据线程实际使用情况调整。
            //修改HTTP请求默认连接数，默认是2。
            System.Net.ServicePointManager.DefaultConnectionLimit = limit;//64
        }
        #endregion

        public string Verify(byte[] data)
        {
            var param = new Dictionary<object, object>
                        {                
                            {"username",_username},
                            {"password",_password},
                            {"typeid",_typeid},
                            {"timeout",_timeout},
                            {"softid",_softid},
                            {"softkey",_softkey}
                        };
            var rst = RuoKuaiHttp.Post("http://api.ruokuai.com/create.xml", param, data);
            return GetResult(rst);
        }

        private string GetResult(string rst)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(rst)))
            {
                try
                {
                    var xml = new XmlSerializer(typeof(RuoKuaiRst)).Deserialize(ms) as RuoKuaiRst;
                    return xml != null ? xml.Result : rst;
                }
                catch (Exception)
                {
                    return rst;
                }
            }
        }

        public string Verify(string imgUrl)
        {
            var param = new Dictionary<object, object>
                        {                
                            {"username",_username},
                            {"password",_password},
                            {"typeid",_typeid},
                            {"timeout",_timeout},
                            {"softid",_softid},
                            {"softkey",_softkey},
                            {"imageurl",imgUrl}
                        };
            return GetResult(RuoKuaiHttp.Post("http://api.ruokuai.com/create.xml", param));
        }

    }
}
