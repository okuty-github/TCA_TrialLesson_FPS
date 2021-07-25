using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Unity.Runtime;

namespace Strix
{
    public class StrixDebugInfo
    {
        private LinkedList<string> _dumpLogList;
        private int _dumpIndex;

        public LinkedList<string> DumpLogList => _dumpLogList;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public StrixDebugInfo()
        {
            _dumpLogList = new LinkedList<string>();
        }

        /// <summary>
        /// �_���v���O
        /// </summary>
        /// <param name="message"></param>
        public void DumpLog(string message)
        {
            _dumpLogList.AddLast(message);
            if (_dumpLogList.Count > StrixConfig.MAX_DUMP_MESSAGE)
            {
                _dumpLogList.RemoveFirst();
            }
        }
    }
}
