using System.Collections.Generic;

namespace SolrScheduler.Interfaces.Utility {
    /// <summary>
    /// Interface IDirectoryInfoWrapper
    /// </summary>
    public interface IDirectoryInfoWrapper {

        /// <summary>
        /// Gets the file contents from directory.
        /// </summary>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        IEnumerable<string> GetFileContentsFromDirectory();
    }
}