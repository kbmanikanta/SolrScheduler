namespace SolrScheduler.Interfaces.Solr {
    /// <summary>
    /// Interface ISolrOperationModel
    /// </summary>
    public interface ISolrOperationModel {
        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        string Server { get; set; }

        /// <summary>
        /// Gets or sets the core.
        /// </summary>
        /// <value>The core.</value>
        string Core { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ISolrOperationModel"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ISolrOperationModel"/> is clean.
        /// </summary>
        /// <value><c>true</c> if clean; otherwise, <c>false</c>.</value>
        bool Clean { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ISolrOperationModel"/> is commit.
        /// </summary>
        /// <value><c>true</c> if commit; otherwise, <c>false</c>.</value>
        bool Commit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ISolrOperationModel"/> is optimize.
        /// </summary>
        /// <value><c>true</c> if optimize; otherwise, <c>false</c>.</value>
        bool Optimize { get; set; }

        /// <summary>
        /// Gets or sets the recurrence start.
        /// </summary>
        /// <value>The recurrence start.</value>
        long RecurrenceStart { get; set; }

        /// <summary>
        /// Gets or sets the recurrence interval.
        /// </summary>
        /// <value>The recurrence interval.</value>
        long RecurrenceInterval { get; set; }

        /// <summary>
        /// Gets or sets the type of the import.
        /// </summary>
        /// <value>The type of the import.</value>
        string ImportType { get; set; }

        #endregion
    }
}