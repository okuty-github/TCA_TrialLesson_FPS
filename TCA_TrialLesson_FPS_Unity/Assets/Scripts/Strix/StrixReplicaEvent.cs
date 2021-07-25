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
        /// �R���X�g���N�^
        /// </summary>
        public StrixReplicaEvent()
        {
        }

        /// <summary>
        /// �C�x���g�R�[���o�b�N��������
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
        /// �C�x���g�̃R�[���o�b�N���폜
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
        /// �V�������v���J���쐬���ꂽ�Ƃ��̒ʒm�R�[���o�b�N
        /// </summary>
        /// <param name="notification"></param>
        private void ReplicaCreateNotified(NotificationEventArgs<ReplicaCreateNotification<Replica>> notification)
        {
            var data = notification.Data.GetReplica();
            //DumpLog("ReplicaCreateNotify", data.GetOwnerUid(), data.GetObjectType(), data.GetPrimaryKey(), data.GetRoomId(), data.GetProperties());
        }

        /// <summary>
        /// ���v���J�̃v���p�e�B���ύX���ꂽ�ۂ̒ʒm�R�[���o�b�N
        /// </summary>
        /// <param name="notification"></param>
        private void ReplicaSetNotified(NotificationEventArgs<ReplicaSetNotification<Replica>> notification)
        {
            // �ʒm�ʂ������̂ŃR�����g�A�E�g���Ă���܂��B
            // var data = notification.Data;
            //    DumpLog("ReplicaSetNotify",data.GetReplicaId(),data.GetProperties());
        }

        /// <summary>
        /// ���v���J���폜���ꂽ�ۂ̒ʒm�R�[���o�b�N
        /// </summary>
        /// <param name="notification"></param>
        private void ReplicaDeleteNotified(NotificationEventArgs<ReplicaDeleteNotification<Replica>> notification)
        {
            var data = notification.Data;
            //DumpLog("ReplicaDeleteNotify", data.GetReplicaId());
        }

        /// <summary>
        /// ���v���J�̏��L�����ύX���ꂽ�ۂ̒ʒm�R�[���o�b�N
        /// </summary>
        /// <param name="notification"></param>
        private void ReplicaChangeOwnerNotified(NotificationEventArgs<ReplicaChangeOwnerNotification<Replica>> notification)
        {
            var data = notification.Data;
            //DumpLog("ReplicaChangeOwnerNotify", data.GetOwnerUid(), data.GetReplicaId());
        }
    }
}
