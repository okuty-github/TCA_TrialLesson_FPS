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
        /// コンストラクタ
        /// </summary>
        public StrixRoomEvent()
        {
        }

        /// <summary>
        /// イベントコールバックの初期化
        /// </summary>
        public void InitializeEvent()
        {
            var roomSession = StrixNetwork.instance.roomSession;
            roomSession.roomClient.RoomCreated += RoomCreated;                          // ルーム作成イベントにコールバック追加
            roomSession.roomClient.RoomSet += RoomSet;                                  // ルーム情報更新イベントにコールバック追加
            roomSession.roomClient.RoomJoined += RoomJoined;                            // ルーム参加イベントにコールバック追加
            roomSession.roomClient.RoomLeft += RoomLeft;                                // ルーム退室イベントにコールバック追加
            roomSession.roomClient.RoomDeleted += RoomDeleted;                          // ルーム削除イベントにコールバック追加
            roomSession.roomClient.RoomJoinNotified += RoomJoinNotified;                // ルーム参加イベントにコールバック追加
            roomSession.roomClient.RoomLeaveNotified += RoomLeaveNotified;              // ルーム退室イベントにコールバック追加
            roomSession.roomClient.RoomDeleteNotified += RoomDeleteNotified;            // ルーム削除イベントにコールバック追加
            roomSession.roomClient.RoomSetNotified += RoomSetNotified;                  // ルーム情報更新イベントにコールバック追加
            roomSession.roomClient.RoomSetMemberNotified += RoomSetMemberNotified;      // ルームメンバー情報更新イベントにコールバック追加
            roomSession.roomClient.RoomDirectRelayNotified += RoomDirectRelayNotified;  // ダイレクトリレー受信イベントにコールバック追加
            roomSession.roomClient.RoomRelayNotified += RoomRelayNotified;              // リレー受信イベントにコールバック追加
            roomSession.roomClient.MatchRoomKickNotified += MatchRoomKickNotified;      // キックイベントにコールバック追加
        }

        /// <summary>
        /// イベントコールバック削除
        /// </summary>
        public void DeleteEvent()
        {
            var roomSession = StrixNetwork.instance.roomSession;
            roomSession.roomClient.RoomCreated -= RoomCreated;                          // ルーム作成イベントからコールバック削除
            roomSession.roomClient.RoomSet -= RoomSet;                                  // ルーム情報更新イベントからコールバック削除
            roomSession.roomClient.RoomJoined -= RoomJoined;                            // ルーム参加イベントからコールバック削除
            roomSession.roomClient.RoomLeft -= RoomLeft;                                // ルーム退室イベントからコールバック削除
            roomSession.roomClient.RoomDeleted -= RoomDeleted;                          // ルーム削除イベントからコールバック削除
            roomSession.roomClient.RoomJoinNotified -= RoomJoinNotified;                // ルーム参加イベントからコールバック削除
            roomSession.roomClient.RoomLeaveNotified -= RoomLeaveNotified;              // ルーム退室イベントからコールバック削除
            roomSession.roomClient.RoomDeleteNotified -= RoomDeleteNotified;            // ルーム削除イベントからコールバック削除
            roomSession.roomClient.RoomSetNotified -= RoomSetNotified;                  // ルーム情報更新イベントからコールバック削除
            roomSession.roomClient.RoomSetMemberNotified -= RoomSetMemberNotified;      // ルームメンバー情報更新イベントからコールバック削除
            roomSession.roomClient.RoomDirectRelayNotified -= RoomDirectRelayNotified;  // ダイレクトリレー受信イベントからコールバック削除
            roomSession.roomClient.RoomRelayNotified -= RoomRelayNotified;              // リレー受信イベントからコールバック削除
            roomSession.roomClient.MatchRoomKickNotified -= MatchRoomKickNotified;      // キックイベントからコールバック削除
        }

        /// <summary>
        /// 自身がルームを作成した際のコールバック
        /// </summary>
        /// <param name="args"></param>
        private void RoomCreated(RoomCreatedEventArgs<CustomizableMatchRoom> args)
        {
            StrixManager.Instance.DebugInfo.DumpLog($"RoomCreatedEvent : {args.Room.GetName()}");
        }

        /// <summary>
        /// 自身がルーム情報を更新した際のコールバック
        /// </summary>
        /// <param name="args"></param>
        private void RoomSet(RoomSetEventArgs<CustomizableMatchRoom> args)
        {
            StrixManager.Instance.DebugInfo.DumpLog($"RoomSetEvent : {args.Room.GetName()}");
        }

        /// <summary>
        /// 自身がルームに参加した際のコールバック
        /// </summary>
        /// <param name="args"></param>
        private void RoomJoined(RoomJoinedEventArgs<CustomizableMatchRoom, CustomizableMatchRoomMember> args)
        {
            StrixManager.Instance.DebugInfo.DumpLog($"RoomJoinedEvent : {args.Member.GetName()}");
        }

        /// <summary>
        /// 自身がルームを退室した際のコールバック
        /// </summary>
        /// <param name="args"></param>
        private void RoomLeft(RoomLeftEventArgs<CustomizableMatchRoom, CustomizableMatchRoomMember> args)
        {
            StrixManager.Instance.DebugInfo.DumpLog($"RoomLeftEvent : {args.Member.GetName()}");
        }

        /// <summary>
        /// 自身がルームを削除した際のコールバック
        /// </summary>
        /// <param name="args"></param>
        private void RoomDeleted(RoomDeletedEventArgs<CustomizableMatchRoom> args)
        {
            StrixManager.Instance.DebugInfo.DumpLog($"RoomDeletedEvent : {args.Room.GetName()}");
        }

        /// <summary>
        /// ルーム参加通知コールバック
        /// </summary>
        /// <param name="args"></param>
        private void RoomJoinNotified(NotificationEventArgs<RoomJoinNotification<CustomizableMatchRoom>> args)
        {
            var str = ((CustomizableMatchRoomMember)args.Data.GetNewlyJoinedMember())?.GetName() ?? "Null";
            StrixManager.Instance.DebugInfo.DumpLog($"RoomJoinNotified : {str}");
        }

        /// <summary>
        /// ルーム退室時通知コールバック
        /// </summary>
        /// <param name="args"></param>
        private void RoomLeaveNotified(NotificationEventArgs<RoomLeaveNotification<CustomizableMatchRoom>> args)
        {
            var str = ((CustomizableMatchRoomMember)args.Data.GetGoneMember())?.GetName() ?? "Null";
            StrixManager.Instance.DebugInfo.DumpLog($"RoomLeaveNotified : {str}");
        }

        /// <summary>
        /// ルーム削除時通知コールバック
        /// </summary>
        /// <param name="args"></param>
        private void RoomDeleteNotified(NotificationEventArgs<RoomDeleteNotification<CustomizableMatchRoom>> args)
        {
            StrixManager.Instance.DebugInfo.DumpLog($"RoomDeleteNotified : {args.Data}");
        }

        /// <summary>
        /// ルーム情報更新時通知コールバック
        /// </summary>
        /// <param name="args"></param>
        private void RoomSetNotified(NotificationEventArgs<RoomSetNotification<CustomizableMatchRoom>> args)
        {
            var str = ((CustomizableMatchRoom)args.Data.GetRoom())?.GetName() ?? "Null";
            StrixManager.Instance.DebugInfo.DumpLog($"RoomSetNotified : {str}");
        }

        /// <summary>
        /// ルームメンバー情報更新時通知コールバック
        /// </summary>
        /// <param name="args"></param>
        private void RoomSetMemberNotified(NotificationEventArgs<RoomSetMemberNotification<CustomizableMatchRoomMember>> args)
        {
            var str = ((CustomizableMatchRoomMember)args.Data.GetMember())?.GetName() ?? "Null";
            StrixManager.Instance.DebugInfo.DumpLog($"RoomSetMemberNotified : {str}");
        }

        /// <summary>
        /// ダイレクトリレーメッセージ受信時通知コールバック
        /// </summary>
        /// <param name="args"></param>
        private void RoomDirectRelayNotified(NotificationEventArgs<RoomDirectRelayNotification> args)
        {
            var str = args.Data.GetMessageToRelay()?.ToString() ?? "Null";
            StrixManager.Instance.DebugInfo.DumpLog($"RoomDirectRelayNotified : {str}");
        }

        /// <summary>
        /// リレーメッセージ受信時通知コールバック
        /// </summary>
        /// <param name="args"></param>
        private void RoomRelayNotified(NotificationEventArgs<RoomRelayNotification> args)
        {
            var str = args.Data.GetMessageToRelay()?.ToString() ?? "Null";
            StrixManager.Instance.DebugInfo.DumpLog($"RoomRelayNotified : {str}");
        }

        /// <summary>
        /// ルームメンバーキック時通知コールバック
        /// </summary>
        /// <param name="args"></param>
        private void MatchRoomKickNotified(NotificationEventArgs<MatchRoomKickNotification<CustomizableMatchRoom>> args)
        {
            StrixManager.Instance.DebugInfo.DumpLog($"MatchRoomKickNotified : {args.Data.GetRoomId()}");
        }
    }
}
