using System;

namespace DesignPattern
{
    /// <summary>
    /// �V���O���g��
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : class, IDisposable, new()
    {
        /// <summary>
        /// �C���X�^���X
        /// </summary>
        protected static T _instance;

        /// <summary>
        /// �C���X�^���X�̎擾�i����͐����j
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        protected Singleton()
        {
        }

        /// <summary>
        /// �C���X�^���X�̔j��
        /// </summary>
        public static void DeleteInstance()
        {
            _instance?.Dispose();
            _instance = null;
        }
    }
}
