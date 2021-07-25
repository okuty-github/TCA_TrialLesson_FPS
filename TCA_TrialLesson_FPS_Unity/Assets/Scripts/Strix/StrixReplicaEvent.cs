using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Net.Logging;
using SoftGear.Strix.Unity.Runtime;
using SoftGear.Strix.Unity.Runtime.Session;
using SoftGear.Strix.Client.Core;
using SoftGear.Strix.Client.Replica.Message;
using SoftGear.Strix.Client.Replica.Model;

namespace Strix
{
    public class StrixReplicaEvent
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StrixReplicaEvent()
        {
        }

        /// <summary>
        /// イベントコールバックを初期化
        /// </summary>
        public void InitializeEvent()
        {
            var roomSession = StrixNetwork.instance.roomSession;
            roomSession.replicaClient.ReplicaCreateNotified += ReplicaCreateNotified;
            roomSession.replicaClient.ReplicaSetNotified += ReplicaSetNotified;
            roomSession.replicaClient.ReplicaDeleteNotified += ReplicaDeleteNotified;
            roomSession.replicaClient.ReplicaChangeOwnerNotified += ReplicaChangeOwnerNotified;
        }

        /// <summary>
        /// イベントのコールバックを削除
        /// </summary>
        public void RemoveEvent()
        {
            var roomSession = StrixNetwork.instance.roomSession;
            roomSession.replicaClient.ReplicaCreateNotified -= ReplicaCreateNotified;
            roomSession.replicaClient.ReplicaSetNotified -= ReplicaSetNotified;
            roomSession.replicaClient.ReplicaDeleteNotified -= ReplicaDeleteNotified;
            roomSession.replicaClient.ReplicaChangeOwnerNotified -= ReplicaChangeOwnerNotified;
        }

        /// <summary>
        /// 新しいレプリカが作成されたときの通知コールバック
        /// </summary>
        /// <param name="notification"></param>
        private void ReplicaCreateNotified(NotificationEventArgs<ReplicaCreateNotification<Replica>> notification)
        {
            var data = notification.Data.GetReplica();
            //DumpLog("ReplicaCreateNotify", data.GetOwnerUid(), data.GetObjectType(), data.GetPrimaryKey(), data.GetRoomId(), data.GetProperties());
        }

        /// <summary>
        /// レプリカのプロパティが変更された際の通知コールバック
        /// </summary>
        /// <param name="notification"></param>
        private void ReplicaSetNotified(NotificationEventArgs<ReplicaSetNotification<Replica>> notification)
        {
            // 通知量が多いのでコメントアウトしてあります。
            // var data = notification.Data;
            //    DumpLog("ReplicaSetNotify",data.GetReplicaId(),data.GetProperties());
        }

        /// <summary>
        /// レプリカが削除された際の通知コールバック
        /// </summary>
        /// <param name="notification"></param>
        private void ReplicaDeleteNotified(NotificationEventArgs<ReplicaDeleteNotification<Replica>> notification)
        {
            var data = notification.Data;
            //DumpLog("ReplicaDeleteNotify", data.GetReplicaId());
        }

        /// <summary>
        /// レプリカの所有権が変更された際の通知コールバック
        /// </summary>
        /// <param name="notification"></param>
        private void ReplicaChangeOwnerNotified(NotificationEventArgs<ReplicaChangeOwnerNotification<Replica>> notification)
        {
            var data = notification.Data;
            //DumpLog("ReplicaChangeOwnerNotify", data.GetOwnerUid(), data.GetReplicaId());
        }
    }
}
