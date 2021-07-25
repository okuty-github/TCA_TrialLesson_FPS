using System;
using SoftGear.Strix.Unity.Runtime;
using SoftGear.Strix.Unity.Runtime.Event;

namespace Strix
{
    public class StrixServerEvent
    {
        private Action<StrixNetworkConnectEventArgs> _masterSessionConnectedAction;
        private Action<StrixNetworkConnectFailedEventArgs> _masterSessionConnectFailedAction;
        private Action<StrixNetworkCloseEventArgs> _masterSessionConnectClosedAction;

        private Action<StrixNetworkConnectEventArgs> _roomSessionConnectedAction;
        private Action<StrixNetworkConnectFailedEventArgs> _roomSessionConnectFailedAction;
        private Action<StrixNetworkCloseEventArgs> _roomSessionConnectClosedAction;

        /// <summary>
        /// マスターセッション接続時のアクション
        /// </summary>
        public Action<StrixNetworkConnectEventArgs> MasterSessionConnectedAction => _masterSessionConnectedAction;
        /// <summary>
        /// マスターセッション接続失敗時のアクション
        /// </summary>
        public Action<StrixNetworkConnectFailedEventArgs> MasterSessionConnectFailedAction => _masterSessionConnectFailedAction;
        /// <summary>
        /// マスターセッション切断時のアクション
        /// </summary>
        public Action<StrixNetworkCloseEventArgs> MasterSessionConnectClosedAction => _masterSessionConnectClosedAction;

        /// <summary>
        /// ルーム続時のアクション
        /// </summary>
        public Action<StrixNetworkConnectEventArgs> RoomSessionConnectedAction => _roomSessionConnectedAction;
        /// <summary>
        /// ルーム接続失敗時のアクション
        /// </summary>
        public Action<StrixNetworkConnectFailedEventArgs> RoomSessionConnectFailedAction => _roomSessionConnectFailedAction;
        /// <summary>
        /// ルーム切断時のアクション
        /// </summary>
        public Action<StrixNetworkCloseEventArgs> RoomSessionConnectClosedAction => _roomSessionConnectClosedAction;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StrixServerEvent()
        {
        }

        /// <summary>
        /// セッションイベントへコールバックを設定
        /// </summary>
        public void InitializeEvent()
        {
            var strixNetwork = StrixNetwork.instance;

            var masterSession = strixNetwork.masterSession;             // マスターセッションへイベントを登録
            masterSession.Connected += MasterSessionConnected;          // 接続時のイベント追加
            masterSession.ConnectFailed += MasterSessionConnectFailed;  // 接続失敗時のイベント追加
            masterSession.Closed += MasterSessionConnectClosed;         // 切断時のイベント追加

            var roomSession = strixNetwork.roomSession;                 // ルームセッションのイベントを登録.
            roomSession.Connected += RoomSessionConnected;              // 接続時のイベント追加
            roomSession.ConnectFailed += RoomSessionConnectFailed;      // 接続失敗時のイベント追加
            roomSession.Closed += RoomSessionConnectClosed;             // 切断時のイベント追加
        }

        /// <summary>
        /// セッションイベントからコールバックを削除
        /// </summary>
        public void DeleteEvent()
        {
            var strixNetwork = StrixNetwork.instance;

            var masterSession = strixNetwork.masterSession;             // マスターセッションからイベントを削除
            masterSession.Connected -= MasterSessionConnected;          // 接続時のイベント削除
            masterSession.ConnectFailed -= MasterSessionConnectFailed;  // 接続失敗時のイベント削除
            masterSession.Closed -= MasterSessionConnectClosed;         // 切断時のイベント削除

            var roomSession = strixNetwork.roomSession;                 // ルームセッションからイベントを削除
            roomSession.Connected -= RoomSessionConnected;              // 接続時のイベント削除
            roomSession.ConnectFailed -= RoomSessionConnectFailed;      // 接続失敗時のイベント削除
            roomSession.Closed -= RoomSessionConnectClosed;             // 切断時のイベント削除
        }

        /// <summary>
        /// マスターセッション接続コールバック
        /// </summary>
        /// <param name="args"></param>
        private void MasterSessionConnected(StrixNetworkConnectEventArgs args)
        {
            _masterSessionConnectedAction?.Invoke(args);
        }

        /// <summary>
        /// マスターセッション接続失敗コールバック
        /// </summary>
        /// <param name="args"></param>
        private void MasterSessionConnectFailed(StrixNetworkConnectFailedEventArgs args)
        {
            _masterSessionConnectFailedAction?.Invoke(args);
        }

        /// <summary>
        /// マスターセッション切断時コールバック
        /// </summary>
        /// <param name="args"></param>
        private void MasterSessionConnectClosed(StrixNetworkCloseEventArgs args)
        {
            _masterSessionConnectClosedAction?.Invoke(args);
        }

        /// <summary>
        /// ルームセッション接続時コールバック
        /// </summary>
        /// <param name="args"></param>
        private void RoomSessionConnected(StrixNetworkConnectEventArgs args)
        {
            _roomSessionConnectedAction?.Invoke(args);
        }

        /// <summary>
        /// ルームセッション接続失敗時コールバック
        /// </summary>
        /// <param name="args"></param>
        private void RoomSessionConnectFailed(StrixNetworkConnectFailedEventArgs args)
        {
            _roomSessionConnectFailedAction?.Invoke(args);
        }

        /// <summary>
        /// ルームセッション切断時コールバック
        /// </summary>
        /// <param name="args"></param>
        private void RoomSessionConnectClosed(StrixNetworkCloseEventArgs args)
        {
            _roomSessionConnectClosedAction?.Invoke(args);
        }
    }
}
