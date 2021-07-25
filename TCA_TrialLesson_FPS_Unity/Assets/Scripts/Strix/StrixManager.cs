using System;
using DesignPattern;
using SoftGear.Strix.Unity.Runtime;

namespace Strix
{
    public class StrixManager : Singleton<StrixManager>, IDisposable
    {
        #region Field
        private StrixDebugInfo _debugInfo;
        private StrixRoom _room;
        private StrixServer _server;

        private StrixServerEvent _serverEvent;
        private StrixRoomEvent _roomEvent;
        private StrixReplicaEvent _replicaEvent;
        #endregion

        #region Properaty
        public StrixDebugInfo DebugInfo => _debugInfo;
        public StrixRoom Room => _room;
        public StrixServer Server => _server;

        public StrixServerEvent ServerEvent => _serverEvent;
        public StrixRoomEvent RoomEvent => _roomEvent;
        public StrixReplicaEvent ReplicaEvent => _replicaEvent;
        #endregion

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public StrixManager() : base()
        {
            _debugInfo = new StrixDebugInfo();
            _room = new StrixRoom();
            _server = new StrixServer();
        }

        /// <summary>
        /// �j��
        /// </summary>
        public void Dispose()
        {
            StrixNetwork.instance.Destroy();
        }
    }
}
