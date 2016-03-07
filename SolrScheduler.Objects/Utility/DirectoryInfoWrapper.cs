using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using SolrScheduler.Interfaces.Utility;

namespace SolrScheduler.Objects.Utility {
    /// <summary>
    /// Class DirectoryInfoWrapper.
    /// </summary>
    public class DirectoryInfoWrapper : IDirectoryInfoWrapper {
        #region Constants

        /// <summary>
        /// The configuration key used for determining where data for the jobs is stored
        /// </summary>
        private const string DATA_DIRECTORY_KEY = "DATA_DIRECTORY";

        #endregion

        /// <summary>
        /// The log directory on the file system
        /// </summary>
        private DirectoryInfo _dataDirectory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryInfoWrapper"/> class.
        /// </summary>
        /// <param name="dataDirectory">The data directory.</param>
        public DirectoryInfoWrapper(DirectoryInfo dataDirectory = null) {
            _dataDirectory = dataDirectory;
        }

        /// <summary>
        /// Gets the file contents from directory.
        /// </summary>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">$Cannot find key: {DATA_DIRECTORY_KEY} in app.config.</exception>
        public IEnumerable<string> GetFileContentsFromDirectory() {
            const string FILE_TYPE_PATTERN = "*.json";

            if (_dataDirectory == null) {
                string confDirectory = ConfigurationManager.AppSettings[DATA_DIRECTORY_KEY];

                if (string.IsNullOrEmpty(confDirectory))
                    throw new ConfigurationErrorsException($"Cannot find key: {DATA_DIRECTORY_KEY} in app.config.");

                _dataDirectory = new DirectoryInfo(confDirectory);
            }

            FileInfo[] objectFiles = _dataDirectory.GetFiles(FILE_TYPE_PATTERN);
            return objectFiles.Select(x => File.ReadAllText(x.FullName));
        }
    }
}