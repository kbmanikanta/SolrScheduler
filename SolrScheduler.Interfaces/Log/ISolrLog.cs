using System;
using System.Collections.Generic;
using SolrScheduler.Interfaces.Enumerations;
using SolrScheduler.Interfaces.Models;

namespace SolrScheduler.Interfaces.Log {
    /// <summary>
    /// Interface ISolrLog
    /// </summary>
    public interface ISolrLog {
        #region Properties

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        string Value { get; }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <value>The values.</value>
        IEnumerable<string> Values { get; }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when [on log action].
        /// </summary>
        event Action<string> OnLogAction;

        #endregion

        #region Stack Actions

        /// <summary>
        /// Appends the action.
        /// </summary>
        /// <param name="timeStampStarted">The time stamp started.</param>
        /// <param name="timeStampFinished">The time stamp finished.</param>
        /// <param name="operationName">Name of the operation.</param>
        /// <param name="responseObject">The response object.</param>
        void AppendSolrAction(DateTime timeStampStarted, DateTime timeStampFinished, string operationName, ISolrResponseObject responseObject);

        /// <summary>
        /// Appends the cancellation.
        /// </summary>
        /// <param name="operationName">Name of the operation.</param>
        /// <param name="action">The action.</param>
        void AppendJobAction(string operationName, SolrLogAction action);

        /// <summary>
        /// Appends the reload.
        /// </summary>
        void AppendReload();

        #endregion

        #region Log Writing

        /// <summary>
        /// Writes the log to the file.
        /// </summary>
        /// <param name="operationName">Name of the operation.</param>
        /// <param name="logItem">The log item.</param>
        void WriteLog(string operationName, string logItem);

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="operationName">Name of the operation.</param>
        /// <returns>System.String.</returns>
        string GetFileName(string operationName);

        #endregion
    }
}