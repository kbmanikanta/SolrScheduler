namespace SolrScheduler.Interfaces.Models {
    /// <summary>
    /// Interface ISolrResponseHeader
    /// </summary>
    public interface ISolrResponseHeader {
        #region Properties

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        int Status { get; set; }

        /// <summary>
        /// Gets or sets the query time.
        /// </summary>
        /// <value>The query time.</value>
        int QTime { get; set; }
        #endregion
    }
}