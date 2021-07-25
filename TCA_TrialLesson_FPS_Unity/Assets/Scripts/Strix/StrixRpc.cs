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
        /// RPCコールバック
        /// </summary>
        public Action<StrixCustomRpcMessage, bool> OnCallRpcCallback { private get; set; }

        /// <summary>
        /// 開始処理
        /// </summary>
        private void Start()
        {
            //RpcSample rpcSample = FindObjectOfType<RpcSample>();
            //OnCallRpcCallback = rpcSample.DisplayMessage;
        }

        /// <summary>
        /// 他のメンバーへRPC呼び出し
        /// </summary>
        public void RpcToOtherMembers(string message)
        {
            var self = StrixNetwork.instance.selfRoomMember;
            var messageData = new StrixCustomRpcMessage(type: "ToOtherMembers", sender: self.GetName(), timestamp: DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), message: message);
            RpcToOtherMembers(nameof(SendMessageRpc), messageData);
        }

        /// <summary>
        /// 全員へRPC呼び出し
        /// </summary>
        public void RpcToAll(string message)
        {
            var self = StrixNetwork.instance.selfRoomMember;
            var messageData = new StrixCustomRpcMessage(type: "ToAll", sender: self.GetName(), timestamp: DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), message: message);
            RpcToAll(nameof(SendMessageRpc), messageData);
        }

        /// <summary>
        /// ルームオーナーへRPC呼び出し
        /// </summary>
        public void RpcToRoomOwner(string message)
        {
            var self = StrixNetwork.instance.selfRoomMember;
            var messageData = new StrixCustomRpcMessage(type: "ToRoomOwner", sender: self.GetName(), timestamp: DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), message: message);
            RpcToRoomOwner(nameof(SendMessageRpc), messageData);
        }

        /// <summary>
        /// メンバーへRPC呼び出し
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
        /// RPCメッセージ送信
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
