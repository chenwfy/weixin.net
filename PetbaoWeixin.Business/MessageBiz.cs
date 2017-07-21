using System;
using System.Xml;
using System.IO;
using PetbaoWeixin.Entities;
using PetbaoWeixin.Utility;
using PetbaoWeixin.Business.Message;
using CSharpLib.Common;
using CSharpLib.Common.Cache;

namespace PetbaoWeixin.Business
{
    /// <summary>
    /// 微信服务器转发用户消息处理逻辑类
    /// </summary>
    public static class MessageBiz
    {
        private static string ConfigFileRoot;
        private const string DefaultMessageConfigFileName = "WxMessage.config";
        private const string DefaultMessageConfigCacheKey = "DEFAULTMESSAGE";

        /// <summary>
        /// 
        /// </summary>
        static MessageBiz()
        {
            ConfigFileRoot = "MIAP.ConfigFileRoot".GetAppSetting();
        }

        /// <summary>
        /// 接收并回复经微信服务器转发过来的消息
        /// </summary>
        /// <param name="forwardMessage">经微信服务器转发的原始消息内容</param>
        /// <returns>回复的消息内容</returns>
        public static string ReceiveForwardMessageAndReply(this string forwardMessage)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(forwardMessage);
                XmlNode messageXmlRoot = xmlDoc.FirstChild;
                string messageType = messageXmlRoot["MsgType"].InnerText.FirstCharToUpper();
                string instanceName = string.Format("Receive{0}Message", messageType);
                if (messageType.Equals("Event"))
                {
                    string eventName = messageXmlRoot["Event"].InnerText.FirstCharToUpper();
                    instanceName = string.Format("Receive{0}{1}Message", messageType, eventName);
                }

                MessageBase messageInstance = string.Format("{0}.Message.{1}, {0}", typeof(MessageBiz).Namespace, instanceName).CreateInstance<MessageBase>();
                messageInstance.Build(messageXmlRoot);
                messageInstance.ExecuteLogic();
                return messageInstance.GetReply();
            }
            catch (Exception ex)
            {
                ex.Error();
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取默认消息配置内容
        /// </summary>
        /// <returns></returns>
        public static WxDefaultMessage GetWxDefaultMessage()
        {
            object cacheValue = Constants.PetbaoWeixinCacheName.GetCache(DefaultMessageConfigCacheKey);
            if (null == cacheValue)
            {
                SetWxDefaultMessageCache();
                cacheValue = Constants.PetbaoWeixinCacheName.GetCache(DefaultMessageConfigCacheKey);
            }
            if (null != cacheValue)
                return (WxDefaultMessage)cacheValue;
            
            return new WxDefaultMessage { WelComeMessage = string.Empty, DefaultReplyMessage = string.Empty };
        }

        /// <summary>
        /// 设置微信默认消息配置内容缓存
        /// </summary>
        private static void SetWxDefaultMessageCache()
        {
            string configFileName = string.Empty;
            WxDefaultMessage config = GetWxDefaultMessageFromFile(ref configFileName);
            if (null != config)
            {
                IExpiration expiration = new FileMonitorExpiration(configFileName);
                Constants.PetbaoWeixinCacheName.SetCache(DefaultMessageConfigCacheKey, config, expiration);
            }
        }

        /// <summary>
        /// 从配置文件获取微信默认消息配置内容
        /// </summary>
        /// <param name="configFileName"></param>
        /// <returns></returns>
        private static WxDefaultMessage GetWxDefaultMessageFromFile(ref string configFileName)
        {
            string configFile = Path.Combine(ConfigFileRoot.MapPath(), DefaultMessageConfigFileName);
            configFileName = configFile;
            if (File.Exists(configFile))
            {
                byte[] buffer = configFile.ReadFile();
                return buffer.ProtoBufDeserialize<WxDefaultMessage>();
            }
            return null;
        }
    }
}