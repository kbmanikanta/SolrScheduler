using System;
using Newtonsoft.Json;

namespace SolrScheduler.Interfaces.Models {
    /// <summary>
    /// Interface ISolrResponseStatus
    /// </summary>
    public interface ISolrResponseStatus {

        /// <summary>
        /// Gets or sets the total requests.
        /// </summary>
        /// <value>The total requests.</value>
        [JsonProperty("Total Requests made to DataSource")]
        int TotalRequests { get; set; }

        /// <summary>
        /// Gets or sets the total rows.
        /// </summary>
        /// <value>The total rows.</value>
        [JsonProperty("Total Rows Fetched")]
        int TotalRows { get; set; }

        /// <summary>
        /// Gets or sets the total documents processed.
        /// </summary>
        /// <value>The total documents processed.</value>
        [JsonProperty("Total Documents Processed")]
        int TotalDocumentsProcessed { get; set; }

        /// <summary>
        /// Gets or sets the total documents skipped.
        /// </summary>
        /// <value>The total documents skipped.</value>
        [JsonProperty("Total Documents Skipped")]
        int TotalDocumentsSkipped { get; set; }

        /// <summary>
        /// Gets or sets the full dump started.
        /// </summary>
        /// <value>The full dump started.</value>
        [JsonProperty("Full Dump Started")]
        DateTime FullDumpStarted { get; set; }

        /// <summary>
        /// Gets or sets the time committed.
        /// </summary>
        /// <value>The time committed.</value>
        [JsonProperty("Committed")]
        DateTime TimeCommitted { get; set; }

        /// <summary>
        /// Gets or sets the time optimized.
        /// </summary>
        /// <value>The time optimized.</value>
        [JsonProperty("Optimized")]
        DateTime TimeOptimized { get; set; }

        /// <summary>
        /// Gets or sets the amount of time taken.
        /// </summary>
        /// <value>The amount of time taken.</value>
        [JsonProperty("Time taken")]
        TimeSpan AmountOfTimeTaken { get; set; }
    }
}