using System.Collections.Generic;

namespace SolrScheduler.Interfaces.Models {
    /// <summary>
    /// Interface ISolrResponseObject
    /// </summary>
    public interface ISolrResponseObject {
        #region Properties 

        /// <summary>
        /// Gets or sets the response header.
        /// </summary>
        /// <value>The response header.</value>
        ISolrResponseHeader ResponseHeader { get; set; }

        /// <summary>
        /// Gets or sets the initialize arguments.
        /// </summary>
        /// <value>The initialize arguments.</value>
        List<object> InitArgs { get; set; }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        string Command { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        string Status { get; set; }

        /// <summary>
        /// Gets or sets the import response.
        /// </summary>
        /// <value>The import response.</value>
        string ImportResponse { get; set; }

        /// <summary>
        /// Gets or sets the response status.
        /// </summary>
        /// <value>The response status.</value>
        ISolrResponseStatus ResponseStatus { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ISolrResponseObject"/> is successful.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        bool Success { get; }

        #endregion
    }
}