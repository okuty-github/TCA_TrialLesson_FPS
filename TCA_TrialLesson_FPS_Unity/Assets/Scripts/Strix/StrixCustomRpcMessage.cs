using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strix
{
    /// <summary>
    /// Rpc���M�p�J�X�^���N���X
    /// </summary>
    [Serializable]
    public class StrixCustomRpcMessage
    {
        private string _type = "";
        private string _senderName = "";
        private string _timestamp = "";
        private string _message = "";

        public string Type => _type;
        public string SenderName => _senderName;
        public string TimeStamp => _timestamp;
        public string Message => _message;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sender"></param>
        /// <param name="timestamp"></param>
        /// <param name="message"></param>
        public StrixCustomRpcMessage(string type, string sender, string timestamp, string message)
        {
            _type = type;
            _senderName = sender;
            _timestamp = timestamp;
            _message = message;
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public StrixCustomRpcMessage()
        {
        }
    }
}
