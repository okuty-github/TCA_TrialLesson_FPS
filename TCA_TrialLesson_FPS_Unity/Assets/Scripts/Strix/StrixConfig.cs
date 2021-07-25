using SoftGear.Strix.Net.Logging;

namespace Strix
{
    public static class StrixConfig
    {
        #region SERVER
        /// <summary>
        /// サーバー接続状態
        /// </summary>
        public enum CONNECT_STATE
        {
            DISCONNECT,
            CONNECTING,
            CONNECT,
            CONNECT_FAILED
        };
        /// <summary>
        /// クラスターURL
        /// </summary>
        public const string HOST = "8b8de983b521b1522ca13ebb.game.strixcloud.net";
        /// <summary>
        /// ポート番号
        /// </summary>
        public const int PORT = 9122;
        /// <summary>
        /// アプリケーションID
        /// </summary>
        public const string APPLICATION_ID = "0d6299b9-2194-417f-be55-204dd808d113";
        /// <summary>
        /// ログレベル
        /// </summary>
        public const Level LOG_LEVEL = Level.INFO;
        #endregion

        #region ROOM
        /// <summary>
        /// デフォルトのルームの制限人数
        /// </summary>
        public const int DEFAULT_ROOM_CAPACITY = 10;
        /// <summary>
        /// デフォルトのルーム検索の数の上限
        /// </summary>
        public const int DEFAULT_SEARCH_ROOM_LIMIT = 10;
        /// <summary>
        /// デフォルトのルーム検索のオフセット値
        /// </summary>
        public const int DEFAULT_SEARCH_ROOM_OFFSET = 0;
        #endregion

        #region DEBUG
        /// <summary>
        /// ダンプメッセージの最大数
        /// </summary>
        public const int MAX_DUMP_MESSAGE = 1000;
        #endregion
    }
}
