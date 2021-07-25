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
        /// �}�X�^�[�Z�b�V�����ڑ����̃A�N�V����
        /// </summary>
        public Action<StrixNetworkConnectEventArgs> MasterSessionConnectedAction => _masterSessionConnectedAction;
        /// <summary>
        /// �}�X�^�[�Z�b�V�����ڑ����s���̃A�N�V����
        /// </summary>
        public Action<StrixNetworkConnectFailedEventArgs> MasterSessionConnectFailedAction => _masterSessionConnectFailedAction;
        /// <summary>
        /// �}�X�^�[�Z�b�V�����ؒf���̃A�N�V����
        /// </summary>
        public Action<StrixNetworkCloseEventArgs> MasterSessionConnectClosedAction => _masterSessionConnectClosedAction;

        /// <summary>
        /// ���[�������̃A�N�V����
        /// </summary>
        public Action<StrixNetworkConnectEventArgs> RoomSessionConnectedAction => _roomSessionConnectedAction;
        /// <summary>
        /// ���[���ڑ����s���̃A�N�V����
        /// </summary>
        public Action<StrixNetworkConnectFailedEventArgs> RoomSessionConnectFailedAction => _roomSessionConnectFailedAction;
        /// <summary>
        /// ���[���ؒf���̃A�N�V����
        /// </summary>
        public Action<StrixNetworkCloseEventArgs> RoomSessionConnectClosedAction => _roomSessionConnectClosedAction;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public StrixServerEvent()
        {
        }

        /// <summary>
        /// �Z�b�V�����C�x���g�փR�[���o�b�N��ݒ�
        /// </summary>
        public void InitializeEvent()
        {
            var strixNetwork = StrixNetwork.instance;

            var masterSession = strixNetwork.masterSession;             // �}�X�^�[�Z�b�V�����փC�x���g��o�^
            masterSession.Connected += MasterSessionConnected;          // �ڑ����̃C�x���g�ǉ�
            masterSession.ConnectFailed += MasterSessionConnectFailed;  // �ڑ����s���̃C�x���g�ǉ�
            masterSession.Closed += MasterSessionConnectClosed;         // �ؒf���̃C�x���g�ǉ�

            var roomSession = strixNetwork.roomSession;                 // ���[���Z�b�V�����̃C�x���g��o�^.
            roomSession.Connected += RoomSessionConnected;              // �ڑ����̃C�x���g�ǉ�
            roomSession.ConnectFailed += RoomSessionConnectFailed;      // �ڑ����s���̃C�x���g�ǉ�
            roomSession.Closed += RoomSessionConnectClosed;             // �ؒf���̃C�x���g�ǉ�
        }

        /// <summary>
        /// �Z�b�V�����C�x���g����R�[���o�b�N���폜
        /// </summary>
        public void DeleteEvent()
        {
            var strixNetwork = StrixNetwork.instance;

            var masterSession = strixNetwork.masterSession;             // �}�X�^�[�Z�b�V��������C�x���g���폜
            masterSession.Connected -= MasterSessionConnected;          // �ڑ����̃C�x���g�폜
            masterSession.ConnectFailed -= MasterSessionConnectFailed;  // �ڑ����s���̃C�x���g�폜
            masterSession.Closed -= MasterSessionConnectClosed;         // �ؒf���̃C�x���g�폜

            var roomSession = strixNetwork.roomSession;                 // ���[���Z�b�V��������C�x���g���폜
            roomSession.Connected -= RoomSessionConnected;              // �ڑ����̃C�x���g�폜
            roomSession.ConnectFailed -= RoomSessionConnectFailed;      // �ڑ����s���̃C�x���g�폜
            roomSession.Closed -= RoomSessionConnectClosed;             // �ؒf���̃C�x���g�폜
        }

        /// <summary>
        /// �}�X�^�[�Z�b�V�����ڑ��R�[���o�b�N
        /// </summary>
        /// <param name="args"></param>
        private void MasterSessionConnected(StrixNetworkConnectEventArgs args)
        {
            _masterSessionConnectedAction?.Invoke(args);
        }

        /// <summary>
        /// �}�X�^�[�Z�b�V�����ڑ����s�R�[���o�b�N
        /// </summary>
        /// <param name="args"></param>
        private void MasterSessionConnectFailed(StrixNetworkConnectFailedEventArgs args)
        {
            _masterSessionConnectFailedAction?.Invoke(args);
        }

        /// <summary>
        /// �}�X�^�[�Z�b�V�����ؒf���R�[���o�b�N
        /// </summary>
        /// <param name="args"></param>
        private void MasterSessionConnectClosed(StrixNetworkCloseEventArgs args)
        {
            _masterSessionConnectClosedAction?.Invoke(args);
        }

        /// <summary>
        /// ���[���Z�b�V�����ڑ����R�[���o�b�N
        /// </summary>
        /// <param name="args"></param>
        private void RoomSessionConnected(StrixNetworkConnectEventArgs args)
        {
            _roomSessionConnectedAction?.Invoke(args);
        }

        /// <summary>
        /// ���[���Z�b�V�����ڑ����s���R�[���o�b�N
        /// </summary>
        /// <param name="args"></param>
        private void RoomSessionConnectFailed(StrixNetworkConnectFailedEventArgs args)
        {
            _roomSessionConnectFailedAction?.Invoke(args);
        }

        /// <summary>
        /// ���[���Z�b�V�����ؒf���R�[���o�b�N
        /// </summary>
        /// <param name="args"></param>
        private void RoomSessionConnectClosed(StrixNetworkCloseEventArgs args)
        {
            _roomSessionConnectClosedAction?.Invoke(args);
        }
    }
}
