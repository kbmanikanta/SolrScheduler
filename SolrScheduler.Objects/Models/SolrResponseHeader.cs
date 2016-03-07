using SolrScheduler.Interfaces.Models;

namespace SolrScheduler.Objects.Models {

    /// <summary>
    /// Class SolrResponseHeader.
    /// </summary>
    public class SolrResponseHeader : ISolrResponseHeader {

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the query time.
        /// </summary>
        /// <value>The query time.</value>
        public int QTime { get; set; }
    }
}