using System;
using Newtonsoft.Json;
using SolrScheduler.Interfaces.Models;

namespace SolrScheduler.Objects.Models {
    /// <summary>
    /// Class SolrResponseStatus.
    /// </summary>
    public class SolrResponseStatus : ISolrResponseStatus {

        /// <summary>
        /// Gets or sets the total requests.
        /// </summary>
        /// <value>The total requests.</value>
        [JsonProperty("Total Requests made to DataSource")]
        public int TotalRequests { get; set; }

        /// <summary>
        /// Gets or sets the total rows.
        /// </summary>
        /// <value>The total rows.</value>
        [JsonProperty("Total Rows Fetched")]
        public int TotalRows { get; set; }

        /// <summary>
        /// Gets or sets the total documents processed.
        /// </summary>
        /// <value>The total documents processed.</value>
        [JsonProperty("Total Documents Processed")]
        public int TotalDocumentsProcessed { get; set; }

        /// <summary>
        /// Gets or sets the total documents skipped.
        /// </summary>
        /// <value>The total documents skipped.</value>
        [JsonProperty("Total Documents Skipped")]
        public int TotalDocumentsSkipped { get; set; }

        /// <summary>
        /// Gets or sets the full dump started.
        /// </summary>
        /// <value>The full dump started.</value>
        [JsonProperty("Full Dump Started")]
        public DateTime FullDumpStarted { get; set; }

        /// <summary>
        /// Gets or sets the time committed.
        /// </summary>
        /// <value>The time committed.</value>
        [JsonProperty("Committed")]
        public DateTime TimeCommitted { get; set; }

        /// <summary>
        /// Gets or sets the time optimized.
        /// </summary>
        /// <value>The time optimized.</value>
        [JsonProperty("Optimized")]
        public DateTime TimeOptimized { get; set; }

        /// <summary>
        /// Gets or sets the amount of time taken.
        /// </summary>
        /// <value>The amount of time taken.</value>
        [JsonProperty("Time taken")]
        public TimeSpan AmountOfTimeTaken { get; set; }
    }
}