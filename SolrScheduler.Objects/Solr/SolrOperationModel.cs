using Newtonsoft.Json;
using SolrScheduler.Interfaces.Solr;

namespace SolrScheduler.Objects.Solr {
    /// <summary>
    /// Class SolrOperationModel.
    /// </summary>
    public class SolrOperationModel : ISolrOperationModel {

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        [JsonProperty(Required = Required.Always)]
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the core.
        /// </summary>
        /// <value>The core.</value>
        [JsonProperty(Required = Required.Always)]
        public string Core { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:SolrScheduler.Interfaces.ISolrOperationModel" /> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        [JsonProperty(Required = Required.Always)]
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:SolrScheduler.Interfaces.ISolrOperationModel" /> is clean.
        /// </summary>
        /// <value><c>true</c> if clean; otherwise, <c>false</c>.</value>
        [JsonProperty(Required = Required.Always)]
        public bool Clean { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:SolrScheduler.Interfaces.ISolrOperationModel" /> is commit.
        /// </summary>
        /// <value><c>true</c> if commit; otherwise, <c>false</c>.</value>
        [JsonProperty(Required = Required.Always)]
        public bool Commit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:SolrScheduler.Interfaces.ISolrOperationModel" /> is optimize.
        /// </summary>
        /// <value><c>true</c> if optimize; otherwise, <c>false</c>.</value>
        [JsonProperty(Required = Required.Always)]
        public bool Optimize { get; set; }

        /// <summary>
        /// Gets or sets the type of the import.
        /// </summary>
        /// <value>The type of the import.</value>
        [JsonProperty(Required = Required.Always)]
        public string ImportType { get; set; }

        /// <summary>
        /// Gets or sets the recurrence start.
        /// </summary>
        /// <value>The recurrence start.</value>
        [JsonProperty(Required = Required.Always)]
        public long RecurrenceStart { get; set; }

        /// <summary>
        /// Gets or sets the recurrence interval.
        /// </summary>
        /// <value>The recurrence interval.</value>
        [JsonProperty(Required = Required.Always)]
        public long RecurrenceInterval { get; set; }
    }
}