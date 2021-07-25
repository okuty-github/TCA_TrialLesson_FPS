using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Client.Match.Room.Model;
using SoftGear.Strix.Unity.Runtime;

namespace Strix
{
    public class StrixRpc : StrixBehaviour
    {
        /// <summary>
        /// RPC�R�[���o�b�N
        /// </summary>
        public Action<StrixCustomRpcMessage, bool> OnCallRpcCallback { private get; set; }

        /// <summary>
        /// �J�n����
        /// </summary>
        private void Start()
        {
            //RpcSample rpcSample = FindObjectOfType<RpcSample>();
            //OnCallRpcCallback = rpcSample.DisplayMessage;
        }

        /// <summary>
        /// ���̃����o�[��RPC�Ăяo��
        /// </summary>
        public void RpcToOtherMembers(string message)
        {
            var self = StrixNetwork.instance.selfRoomMember;
            var messageData = new StrixCustomRpcMessage(type: "ToOtherMembers", sender: self.GetName(), timestamp: DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), message: message);
            RpcToOtherMembers(nameof(SendMessageRpc), messageData);
        }

        /// <summary>
        /// �S����RPC�Ăяo��
        /// </summary>
        public void RpcToAll(string message)
        {
            var self = StrixNetwork.instance.selfRoomMember;
            var messageData = new StrixCustomRpcMessage(type: "ToAll", sender: self.GetName(), timestamp: DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), message: message);
            RpcToAll(nameof(SendMessageRpc), messageData);
        }

        /// <summary>
        /// ���[���I�[�i�[��RPC�Ăяo��
        /// </summary>
        public void RpcToRoomOwner(string message)
        {
            var self = StrixNetwork.instance.selfRoomMember;
            var messageData = new StrixCustomRpcMessage(type: "ToRoomOwner", sender: self.GetName(), timestamp: DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), message: message);
            RpcToRoomOwner(nameof(SendMessageRpc), messageData);
        }

        /// <summary>
        /// �����o�[��RPC�Ăяo��
        /// </summary>
        /// <param name="member"></param>
        /// <param name="message"></param>
        public void RpcToMember(CustomizableMatchRoomMember member, string message)
        {
            var self = StrixNetwork.instance.selfRoomMember;
            var messageData = new StrixCustomRpcMessage(type: $"To{member.GetName()}", sender: self.GetName(), timestamp: DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), message: message);
            Rpc(member.GetUid(), nameof(SendMessageRpc), messageData);
        }

        /// <summary>
        /// RPC���b�Z�[�W���M
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        [StrixRpc]
        public void SendMessageRpc(StrixCustomRpcMessage message, StrixRpcContext context)
        {
            var isMineMessage = context.senderUid.Equals(StrixNetwork.instance.selfRoomMember.GetUid());
            OnCallRpcCallback?.Invoke(message, isMineMessage);
        }
    }
}
