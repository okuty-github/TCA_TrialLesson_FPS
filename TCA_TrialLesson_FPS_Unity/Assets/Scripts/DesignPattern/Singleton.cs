using System;

namespace DesignPattern
{
    /// <summary>
    /// シングルトン
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : class, IDisposable, new()
    {
        /// <summary>
        /// インスタンス
        /// </summary>
        protected static T _instance;

        /// <summary>
        /// インスタンスの取得（初回は生成）
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
        /// コンストラクタ
        /// </summary>
        protected Singleton()
        {
        }

        /// <summary>
        /// インスタンスの破棄
        /// </summary>
        public static void DeleteInstance()
        {
            _instance?.Dispose();
            _instance = null;
        }
    }
}
