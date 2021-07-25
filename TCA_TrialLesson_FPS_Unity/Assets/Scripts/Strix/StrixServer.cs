using System;
using SoftGear.Strix.Unity.Runtime;
using SoftGear.Strix.Net.Logging;
using SoftGear.Strix.Unity.Runtime.Event;
using SoftGear.Strix.Client.Core.Message;
using SoftGear.Strix.Client.Match.Node.Model;

namespace Strix
{
    public class StrixServer
    {
        private StrixConfig.CONNECT_STATE _connectState;

        /// <summary>
        /// 接続状態
        /// </summary>
        public StrixConfig.CONNECT_STATE ConnectState => _connectState;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StrixServer()
        {
        }

        /// <summary>
        /// マスターサーバーへ接続
        /// </summary>
        /// <param name="logLebel"></param>
        /// <param name="applicationId"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="playerName"></param>
        public void Connect(Level logLebel, string applicationId, string host, int port, string playerName, Action onSuccess, Action onFailed)
        {
            _connectState = StrixConfig.CONNECT_STATE.CONNECTING;
            LogManager.Instance.Filter = logLebel;
            StrixNetwork.instance.applicationId = applicationId;
            StrixNetwork.instance.playerName = playerName;
            StrixNetwork.instance.ConnectMasterServer(host, port,
                args =>
                {
                    _connectState = StrixConfig.CONNECT_STATE.CONNECT;
                    onSuccess?.Invoke();
                },
                args =>
                {
                    _connectState = StrixConfig.CONNECT_STATE.CONNECT_FAILED;
                    onFailed?.Invoke();
                });
        }

        /// <summary>
        /// マスターサーバーへとの接続を切断
        /// </summary>
        public void Disconnect()
        {
            if (_connectState == StrixConfig.CONNECT_STATE.CONNECT)
            {
                StrixNetwork.instance.DisconnectMasterServer();
                _connectState = StrixConfig.CONNECT_STATE.DISCONNECT;
            }
        }

        /// <summary>
        /// マスターサーバー状態確認処理
        /// </summary>
        public void CheckServerStatus(Action onSuccess, Action onFailed)
        {
            // ノード数（稼働中のルームサーバー台数）確認処理
            StrixNetwork.instance.masterSession.nodeClient.GetCount(
                new GetCountMessage<CustomizableMatchServerNode>(),
                args =>
                {
                    // ノード数をログとして出力
                    if (args.Result.GetCount() > 0)
                    {
                        //ノード数が0以上 = ノードが存在する 
                        //DumpLog("MasterAndRoomServerWorking", args.Result.GetCount());
                        onSuccess?.Invoke();
                    }
                    else
                    {
                        // ノードが存在しない
                        //DumpLog("MasterServerWorking");
                        onFailed?.Invoke();
                    }
                },
                args =>
                {
                    // マスターサーバーが稼働していない
                    //DumpLog("MasterServerNotWorking");
                    onFailed?.Invoke();
                });
        }
    }
}
