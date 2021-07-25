using SoftGear.Strix.Client.Core;
using SoftGear.Strix.Client.Match.Room.Message;
using SoftGear.Strix.Client.Match.Room.Model;
using SoftGear.Strix.Client.Room;
using SoftGear.Strix.Client.Room.Message;
using SoftGear.Strix.Unity.Runtime;
using SoftGear.Strix.Unity.Runtime.Session;

namespace Strix
{
    public class StrixRoomEvent
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public StrixRoomEvent()
        {
        }

        /// <summary>
        /// �C�x���g�R�[���o�b�N�̏�����
        /// </summary>
        public void InitializeEvent()
        {
            var roomSession = StrixNetwork.instance.roomSession;
            roomSession.roomClient.RoomCreated += RoomCreated;                          // ���[���쐬�C�x���g�ɃR�[���o�b�N�ǉ�
            roomSession.roomClient.RoomSet += RoomSet;                                  // ���[�����X�V�C�x���g�ɃR�[���o�b�N�ǉ�
            roomSession.roomClient.RoomJoined += RoomJoined;                            // ���[���Q���C�x���g�ɃR�[���o�b�N�ǉ�
            roomSession.roomClient.RoomLeft += RoomLeft;                                // ���[���ގ��C�x���g�ɃR�[���o�b�N�ǉ�
            roomSession.roomClient.RoomDeleted += RoomDeleted;                          // ���[���폜�C�x���g�ɃR�[���o�b�N�ǉ�
            roomSession.roomClient.RoomJoinNotified += RoomJoinNotified;                // ���[���Q���C�x���g�ɃR�[���o�b�N�ǉ�
            roomSession.roomClient.RoomLeaveNotified += RoomLeaveNotified;              // ���[���ގ��C�x���g�ɃR�[���o�b�N�ǉ�
            roomSession.roomClient.RoomDeleteNotified += RoomDeleteNotified;            // ���[���폜�C�x���g�ɃR�[���o�b�N�ǉ�
            roomSession.roomClient.RoomSetNotified += RoomSetNotified;                  // ���[�����X�V�C�x���g�ɃR�[���o�b�N�ǉ�
            roomSession.roomClient.RoomSetMemberNotified += RoomSetMemberNotified;      // ���[�������o�[���X�V�C�x���g�ɃR�[���o�b�N�ǉ�
            roomSession.roomClient.RoomDirectRelayNotified += RoomDirectRelayNotified;  // �_�C���N�g�����[��M�C�x���g�ɃR�[���o�b�N�ǉ�
            roomSession.roomClient.RoomRelayNotified += RoomRelayNotified;              // �����[��M�C�x���g�ɃR�[���o�b�N�ǉ�
            roomSession.roomClient.MatchRoomKickNotified += MatchRoomKickNotified;      // �L�b�N�C�x���g�ɃR�[���o�b�N�ǉ�
        }

        /// <summary>
        /// �C�x���g�R�[���o�b�N�폜
        /// </summary>
        public void DeleteEvent()
        {
            var roomSession = StrixNetwork.instance.roomSession;
            roomSession.roomClient.RoomCreated -= RoomCreated;                          // ���[���쐬�C�x���g����R�[���o�b�N�폜
            roomSession.roomClient.RoomSet -= RoomSet;                                  // ���[�����X�V�C�x���g����R�[���o�b�N�폜
            roomSession.roomClient.RoomJoined -= RoomJoined;                            // ���[���Q���C�x���g����R�[���o�b�N�폜
            roomSession.roomClient.RoomLeft -= RoomLeft;                                // ���[���ގ��C�x���g����R�[���o�b�N�폜
            roomSession.roomClient.RoomDeleted -= RoomDeleted;                          // ���[���폜�C�x���g����R�[���o�b�N�폜
            roomSession.roomClient.RoomJoinNotified -= RoomJoinNotified;                // ���[���Q���C�x���g����R�[���o�b�N�폜
            roomSession.roomClient.RoomLeaveNotified -= RoomLeaveNotified;              // ���[���ގ��C�x���g����R�[���o�b�N�폜
            roomSession.roomClient.RoomDeleteNotified -= RoomDeleteNotified;            // ���[���폜�C�x���g����R�[���o�b�N�폜
            roomSession.roomClient.RoomSetNotified -= RoomSetNotified;                  // ���[�����X�V�C�x���g����R�[���o�b�N�폜
            roomSession.roomClient.RoomSetMemberNotified -= RoomSetMemberNotified;      // ���[�������o�[���X�V�C�x���g����R�[���o�b�N�폜
            roomSession.roomClient.RoomDirectRelayNotified -= RoomDirectRelayNotified;  // �_�C���N�g�����[��M�C�x���g����R�[���o�b�N�폜
            roomSession.roomClient.RoomRelayNotified -= RoomRelayNotified;              // �����[��M�C�x���g����R�[���o�b�N�폜
            roomSession.roomClient.MatchRoomKickNotified -= MatchRoomKickNotified;      // �L�b�N�C�x���g����R�[���o�b�N�폜
        }

        /// <summary>
        /// ���g�����[�����쐬�����ۂ̃R�[���o�b�N
        /// </summary>
        /// <param name="args"></param>
        private void RoomCreated(RoomCreatedEventArgs<CustomizableMatchRoom> args)
        {
            StrixManager.Instance.DebugInfo.DumpLog($"RoomCreatedEvent : {args.Room.GetName()}");
        }

        /// <summary>
        /// ���g�����[�������X�V�����ۂ̃R�[���o�b�N
        /// </summary>
        /// <param name="args"></param>
        private void RoomSet(RoomSetEventArgs<CustomizableMatchRoom> args)
        {
            StrixManager.Instance.DebugInfo.DumpLog($"RoomSetEvent : {args.Room.GetName()}");
        }

        /// <summary>
        /// ���g�����[���ɎQ�������ۂ̃R�[���o�b�N
        /// </summary>
        /// <param name="args"></param>
        private void RoomJoined(RoomJoinedEventArgs<CustomizableMatchRoom, CustomizableMatchRoomMember> args)
        {
            StrixManager.Instance.DebugInfo.DumpLog($"RoomJoinedEvent : {args.Member.GetName()}");
        }

        /// <summary>
        /// ���g�����[����ގ������ۂ̃R�[���o�b�N
        /// </summary>
        /// <param name="args"></param>
        private void RoomLeft(RoomLeftEventArgs<CustomizableMatchRoom, CustomizableMatchRoomMember> args)
        {
            StrixManager.Instance.DebugInfo.DumpLog($"RoomLeftEvent : {args.Member.GetName()}");
        }

        /// <summary>
        /// ���g�����[�����폜�����ۂ̃R�[���o�b�N
        /// </summary>
        /// <param name="args"></param>
        private void RoomDeleted(RoomDeletedEventArgs<CustomizableMatchRoom> args)
        {
            StrixManager.Instance.DebugInfo.DumpLog($"RoomDeletedEvent : {args.Room.GetName()}");
        }

        /// <summary>
        /// ���[���Q���ʒm�R�[���o�b�N
        /// </summary>
        /// <param name="args"></param>
        private void RoomJoinNotified(NotificationEventArgs<RoomJoinNotification<CustomizableMatchRoom>> args)
        {
            var str = ((CustomizableMatchRoomMember)args.Data.GetNewlyJoinedMember())?.GetName() ?? "Null";
            StrixManager.Instance.DebugInfo.DumpLog($"RoomJoinNotified : {str}");
        }

        /// <summary>
        /// ���[���ގ����ʒm�R�[���o�b�N
        /// </summary>
        /// <param name="args"></param>
        private void RoomLeaveNotified(NotificationEventArgs<RoomLeaveNotification<CustomizableMatchRoom>> args)
        {
            var str = ((CustomizableMatchRoomMember)args.Data.GetGoneMember())?.GetName() ?? "Null";
            StrixManager.Instance.DebugInfo.DumpLog($"RoomLeaveNotified : {str}");
        }

        /// <summary>
        /// ���[���폜���ʒm�R�[���o�b�N
        /// </summary>
        /// <param name="args"></param>
        private void RoomDeleteNotified(NotificationEventArgs<RoomDeleteNotification<CustomizableMatchRoom>> args)
        {
            StrixManager.Instance.DebugInfo.DumpLog($"RoomDeleteNotified : {args.Data}");
        }

        /// <summary>
        /// ���[�����X�V���ʒm�R�[���o�b�N
        /// </summary>
        /// <param name="args"></param>
        private void RoomSetNotified(NotificationEventArgs<RoomSetNotification<CustomizableMatchRoom>> args)
        {
            var str = ((CustomizableMatchRoom)args.Data.GetRoom())?.GetName() ?? "Null";
            StrixManager.Instance.DebugInfo.DumpLog($"RoomSetNotified : {str}");
        }

        /// <summary>
        /// ���[�������o�[���X�V���ʒm�R�[���o�b�N
        /// </summary>
        /// <param name="args"></param>
        private void RoomSetMemberNotified(NotificationEventArgs<RoomSetMemberNotification<CustomizableMatchRoomMember>> args)
        {
            var str = ((CustomizableMatchRoomMember)args.Data.GetMember())?.GetName() ?? "Null";
            StrixManager.Instance.DebugInfo.DumpLog($"RoomSetMemberNotified : {str}");
        }

        /// <summary>
        /// �_�C���N�g�����[���b�Z�[�W��M���ʒm�R�[���o�b�N
        /// </summary>
        /// <param name="args"></param>
        private void RoomDirectRelayNotified(NotificationEventArgs<RoomDirectRelayNotification> args)
        {
            var str = args.Data.GetMessageToRelay()?.ToString() ?? "Null";
            StrixManager.Instance.DebugInfo.DumpLog($"RoomDirectRelayNotified : {str}");
        }

        /// <summary>
        /// �����[���b�Z�[�W��M���ʒm�R�[���o�b�N
        /// </summary>
        /// <param name="args"></param>
        private void RoomRelayNotified(NotificationEventArgs<RoomRelayNotification> args)
        {
            var str = args.Data.GetMessageToRelay()?.ToString() ?? "Null";
            StrixManager.Instance.DebugInfo.DumpLog($"RoomRelayNotified : {str}");
        }

        /// <summary>
        /// ���[�������o�[�L�b�N���ʒm�R�[���o�b�N
        /// </summary>
        /// <param name="args"></param>
        private void MatchRoomKickNotified(NotificationEventArgs<MatchRoomKickNotification<CustomizableMatchRoom>> args)
        {
            StrixManager.Instance.DebugInfo.DumpLog($"MatchRoomKickNotified : {args.Data.GetRoomId()}");
        }
    }
}
