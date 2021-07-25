using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SoftGear.Strix.Client.Core;
using SoftGear.Strix.Client.Match.Room.Message;
using SoftGear.Strix.Client.Match.Room.Model;
using SoftGear.Strix.Client.Room;
using SoftGear.Strix.Client.Room.Message;
using SoftGear.Strix.Net.Logging;
using SoftGear.Strix.Unity.Runtime;
using SoftGear.Strix.Unity.Runtime.Session;

namespace Strix
{
    public class StrixRelay : MonoBehaviour
    {
        public StrixRelay()
        {
        }

        /// <summary>
        /// リレーメッセージの送信
        /// </summary>
        /// <param name="message"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onFailed"></param>
        public void SendRelayMessage(string message, Action onSuccess, Action onFailed)
        {
            StrixNetwork.instance.SendRoomRelay(message,
            sendRelayArgs =>
            {
                onSuccess?.Invoke();
            },
            failureArgs =>
            {
                onFailed?.Invoke();
            });
        }

        /// <summary>
        /// リレーメッセージの送信
        /// </summary>
        /// <param name="target"></param>
        /// <param name="message"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onFailed"></param>
        public void SendRelayMessage(CustomizableMatchRoomMember target, string message, Action onSuccess, Action onFailed)
        {
            if (target != null)
            {
                StrixNetwork.instance.SendRoomDirectRelay(target.GetUid(), message,
                    sendRelayArgs =>
                    {
                        onSuccess?.Invoke();
                    },
                    failureArgs =>
                    {
                        onFailed?.Invoke();
                    });
            }
            else
            {
                onFailed?.Invoke();
            }
        }
    }
}
