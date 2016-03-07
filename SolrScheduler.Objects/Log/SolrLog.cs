using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using SolrScheduler.Interfaces.Enumerations;
using SolrScheduler.Interfaces.Log;
using SolrScheduler.Interfaces.Models;

namespace SolrScheduler.Objects.Log {
    /// <summary>
    /// Class SolrLog.
    /// </summary>
    public class SolrLog : ISolrLog {
        #region Constants

        /// <summary>
        /// The configuration key used for determining where to store logs
        /// </summary>
        private const string LOG_DIRECTORY_KEY = "LOG_DIRECTORY";

        /// <summary>
        /// The configuration key used for determining how many logs to display
        /// </summary>
        private const string LOG_COUNT_DISPLAY_KEY = "LOG_GUI_DISPLAY_COUNT";

        /// <summary>
        /// The amount of log items to display to the user
        /// </summary>
        private const int LOG_COUNT_DISPLAY_FALLBACK = 500;

        #endregion

        #region Private Variables

        /// <summary>
        /// The FILO stack, which we use to store the log strings
        /// </summary>
        private readonly Stack<string> _logCollection = new Stack<string>();

        /// <summary>
        /// The flag which determines whether or not to write to the file system
        /// </summary>
        private readonly bool _outputToFile;

        /// <summary>
        /// The log directory on the file system
        /// </summary>
        private DirectoryInfo _logDirectory;

        /// <summary>
        /// Controls how many log items to display
        /// </summary>
        private int _logDisplayCount = LOG_COUNT_DISPLAY_FALLBACK;

        #endregion

        #region Event Registration

        /// <summary>
        /// Occurs when [on log action].
        /// </summary>
        public event Action<string> OnLogAction;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value => string.Join("\r\n", _logCollection);

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        public IEnumerable<string> Values => _logCollection.Take(_logDisplayCount);

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SolrLog" /> class.
        /// </summary>
        /// <param name="logDirectory">The log directory.</param>
        /// <param name="outputToFile">The output to file.</param>
        public SolrLog(DirectoryInfo logDirectory = null, bool outputToFile = true) {
            _outputToFile = outputToFile;

            if (!_outputToFile)
                return;

            SetLoggingDirectory(logDirectory);
            SetLoggingDisplayCount();
        }

        #endregion

        #region Stack Methods

        /// <summary>
        /// Appends the action to the log stack.
        /// </summary>
        /// <param name="timeStampStarted">The time stamp started.</param>
        /// <param name="timeStampFinished">The time stamp finished.</param>
        /// <param name="operationName">Name of the operation.</param>
        /// <param name="response">The response.</param>
        public void AppendSolrAction(DateTime timeStampStarted, DateTime timeStampFinished, string operationName, ISolrResponseObject response) {
            string timeElapsed = $"{(timeStampFinished - timeStampStarted).TotalMilliseconds:0.0000}";

            StringBuilder entry = new StringBuilder();
            entry.Append($"{DateTime.Now}");
            entry.Append(response.Success ? "\tSuccess" : "\tFailed");
            entry.Append($"\t{operationName}");
            entry.Append($"\t{timeElapsed} ms\t");

            if (!string.IsNullOrEmpty(response.ImportResponse))
                entry.Append($"\t{response.ImportResponse}");


            _logCollection.Push(entry.ToString());

            if (_outputToFile)
                WriteLog(operationName, entry.ToString());

            OnLogAction?.Invoke(null);
        }


        /// <summary>
        /// Appends the cancellation to the log stack.
        /// </summary>
        /// <param name="operationName">Name of the operation.</param>
        /// <param name="action">The action.</param>
        public void AppendJobAction(string operationName, SolrLogAction action) {
            StringBuilder entry = new StringBuilder();
            entry.Append($"{DateTime.Now}");
            entry.Append($"\t{action}");
            entry.Append($"\t{operationName}");

            _logCollection.Push(entry.ToString());
            OnLogAction?.Invoke(null);
        }

        /// <summary>
        /// Appends the reload.
        /// </summary>
        public void AppendReload() {
            StringBuilder entry = new StringBuilder();
            entry.Append($"{DateTime.Now}");
            entry.Append("\tReloaded all Solr jobs");

            _logCollection.Push(entry.ToString());
            OnLogAction?.Invoke(null);
        }

        #endregion

        #region Writing Methods

        /// <summary>
        /// Writes the log.
        /// </summary>
        /// <param name="operationName">Name of the operation.</param>
        /// <param name="logItem">The log item.</param>
        public void WriteLog(string operationName, string logItem) {
            string file = GetFileName(operationName);
            using (var writer = File.AppendText(_logDirectory.FullName + file)) {
                writer.WriteLine(logItem);
            }
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="operationName">Name of the operation.</param>
        /// <returns>System.String.</returns>
        public string GetFileName(string operationName) {
            return $"{operationName}-{DateTime.Now.ToString("yyy-MM-dd")}.txt";
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the logging directory.
        /// </summary>
        /// <param name="logDirectory">The log directory.</param>
        /// <exception cref="ConfigurationErrorsException">$Cannot find key: {LOG_DIRECTORY_KEY} in app.config.</exception>
        private void SetLoggingDirectory(DirectoryInfo logDirectory = null) {
            if (logDirectory == null) {
                string confDig = ConfigurationManager.AppSettings[LOG_DIRECTORY_KEY];

                if (string.IsNullOrEmpty(confDig))
                    throw new ConfigurationErrorsException($"Cannot find key: {LOG_DIRECTORY_KEY} in app.config.");

                if (!Directory.Exists(confDig))
                    Directory.CreateDirectory(confDig);

                _logDirectory = new DirectoryInfo(confDig);
                return;
            }

            _logDirectory = logDirectory;
        }

        /// <summary>
        /// Sets the logging display count.
        /// </summary>
        private void SetLoggingDisplayCount() {
            string confDisplayString = ConfigurationManager.AppSettings[LOG_COUNT_DISPLAY_KEY];
            _logDisplayCount = string.IsNullOrEmpty(confDisplayString) ? LOG_COUNT_DISPLAY_FALLBACK : Convert.ToInt32(confDisplayString);
        }

        #endregion
    }
}