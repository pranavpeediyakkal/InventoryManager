using log4net;
using System;
using System.IO;

namespace InventoryManager.Helper
{
    public class Logger
    {
        #region Enum
        public enum LogType
        {
            DEBUG,
            INFO,
            WARN,
            ERROR,
        }
        #endregion

        #region Member variables
        private static ILog m_log;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ApplicationPath">Application path</param>
        public static void Initialize(string ApplicationPath)
        {
            string folderPath = Path.Combine(ApplicationPath, "App_Data");
            if (Directory.Exists(folderPath) == false)
            {
                Directory.CreateDirectory(folderPath);
            }

            string logFilePath = Path.Combine(folderPath, $"{DateTime.Now.ToString("yyyyMMdd")}.log");
            GlobalContext.Properties["LogFileName"] = logFilePath;

            log4net.Config.XmlConfigurator.Configure(new FileInfo(Path.Combine(ApplicationPath, "Log4Net.config")));
            m_log = LogManager.GetLogger("InventoryManager");
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Write a log message
        /// </summary>
        /// <param name="type">Type of the log</param>
        /// <param name="message">Log message</param>
        /// <param name="ex">Exception</param>
        public static void WriteLog(LogType type, string message, Exception ex = null)
        {
            if (m_log == null)
            {
                string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"{DateTime.Now.ToString("yyyyMMdd")}.log");
                Initialize(logFilePath);
            }

            switch (type)
            {
                case LogType.DEBUG:
                    m_log.Debug(message);
                    break;

                case LogType.INFO:
                    m_log.Info(message);
                    break;

                case LogType.WARN:
                    m_log.Warn(message);
                    break;

                case LogType.ERROR:
                    m_log.Error(message, ex);
                    break;
            }
        } 
        #endregion
    }
}