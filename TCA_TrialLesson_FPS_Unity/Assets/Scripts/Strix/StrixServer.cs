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
        /// �ڑ����
        /// </summary>
        public StrixConfig.CONNECT_STATE ConnectState => _connectState;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public StrixServer()
        {
        }

        /// <summary>
        /// �}�X�^�[�T�[�o�[�֐ڑ�
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
        /// �}�X�^�[�T�[�o�[�ւƂ̐ڑ���ؒf
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
        /// �}�X�^�[�T�[�o�[��Ԋm�F����
        /// </summary>
        public void CheckServerStatus(Action onSuccess, Action onFailed)
        {
            // �m�[�h���i�ғ����̃��[���T�[�o�[�䐔�j�m�F����
            StrixNetwork.instance.masterSession.nodeClient.GetCount(
                new GetCountMessage<CustomizableMatchServerNode>(),
                args =>
                {
                    // �m�[�h�������O�Ƃ��ďo��
                    if (args.Result.GetCount() > 0)
                    {
                        //�m�[�h����0�ȏ� = �m�[�h�����݂��� 
                        //DumpLog("MasterAndRoomServerWorking", args.Result.GetCount());
                        onSuccess?.Invoke();
                    }
                    else
                    {
                        // �m�[�h�����݂��Ȃ�
                        //DumpLog("MasterServerWorking");
                        onFailed?.Invoke();
                    }
                },
                args =>
                {
                    // �}�X�^�[�T�[�o�[���ғ����Ă��Ȃ�
                    //DumpLog("MasterServerNotWorking");
                    onFailed?.Invoke();
                });
        }
    }
}
