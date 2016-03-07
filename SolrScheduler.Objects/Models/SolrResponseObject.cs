using System.Collections.Generic;
using SolrScheduler.Interfaces.Models;

namespace SolrScheduler.Objects.Models {
    /// <summary>
    /// Class SolrResponseObject.
    /// </summary>
    public class SolrResponseObject : ISolrResponseObject {

        /// <summary>
        /// Initializes a new instance of the <see cref="SolrResponseObject"/> class.
        /// This constructor is necessary for JSON.NET to deserialize the interfaces
        /// to the appropriate concrete classes.
        /// </summary>
        /// <param name="responseHeader">The response header.</param>
        /// <param name="responseStatus">The response status.</param>
        public SolrResponseObject(SolrResponseHeader responseHeader, SolrResponseStatus responseStatus) {
            ResponseHeader = responseHeader;
            ResponseStatus = responseStatus;
        }

        /// <summary>
        /// Gets or sets the response header.
        /// </summary>
        /// <value>The response header.</value>
        public ISolrResponseHeader ResponseHeader { get; set; }

        /// <summary>
        /// Gets or sets the initialize arguments.
        /// </summary>
        /// <value>The initialize arguments.</value>
        public List<object> InitArgs { get; set; }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        public string Command { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the import response.
        /// </summary>
        /// <value>The import response.</value>
        public string ImportResponse { get; set; }

        /// <summary>
        /// Gets or sets the response status.
        /// </summary>
        /// <value>The response status.</value>
        public ISolrResponseStatus ResponseStatus { get; set; }

        /// <summary>
        /// Gets or sets the success.
        /// </summary>
        /// <value>The success.</value>
        public bool Success => Status == "idle";
    }
}