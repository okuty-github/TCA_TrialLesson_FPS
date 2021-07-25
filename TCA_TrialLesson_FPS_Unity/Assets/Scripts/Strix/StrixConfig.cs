using SoftGear.Strix.Net.Logging;

namespace Strix
{
    public static class StrixConfig
    {
        #region SERVER
        /// <summary>
        /// �T�[�o�[�ڑ����
        /// </summary>
        public enum CONNECT_STATE
        {
            DISCONNECT,
            CONNECTING,
            CONNECT,
            CONNECT_FAILED
        };
        /// <summary>
        /// �N���X�^�[URL
        /// </summary>
        public const string HOST = "8b8de983b521b1522ca13ebb.game.strixcloud.net";
        /// <summary>
        /// �|�[�g�ԍ�
        /// </summary>
        public const int PORT = 9122;
        /// <summary>
        /// �A�v���P�[�V����ID
        /// </summary>
        public const string APPLICATION_ID = "0d6299b9-2194-417f-be55-204dd808d113";
        /// <summary>
        /// ���O���x��
        /// </summary>
        public const Level LOG_LEVEL = Level.INFO;
        #endregion

        #region ROOM
        /// <summary>
        /// �f�t�H���g�̃��[���̐����l��
        /// </summary>
        public const int DEFAULT_ROOM_CAPACITY = 10;
        /// <summary>
        /// �f�t�H���g�̃��[�������̐��̏��
        /// </summary>
        public const int DEFAULT_SEARCH_ROOM_LIMIT = 10;
        /// <summary>
        /// �f�t�H���g�̃��[�������̃I�t�Z�b�g�l
        /// </summary>
        public const int DEFAULT_SEARCH_ROOM_OFFSET = 0;
        #endregion

        #region DEBUG
        /// <summary>
        /// �_���v���b�Z�[�W�̍ő吔
        /// </summary>
        public const int MAX_DUMP_MESSAGE = 1000;
        #endregion
    }
}
