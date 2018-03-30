// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.Azure.Commands.Common.Authentication;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.Common.Authentication.Models;
using Microsoft.Azure.Commands.Sql.Common;
using Microsoft.Azure.Management.Internal.Resources;
using Microsoft.Azure.Management.Sql;
using Microsoft.Azure.Management.Sql.Models;
using Microsoft.Azure.Management.Sql.LegacySdk;
using Microsoft.Rest.Azure;
using Microsoft.WindowsAzure.Management.Storage;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Azure.Management.Sql.LegacySdk.Models;

namespace Microsoft.Azure.Commands.Sql.AutomaticTuning.Services
{
    /// <summary>
    /// This class is responsible for all the REST communication with the audit REST endpoints
    /// </summary>
    public class AzureSqlAutomaticTuningCommunicator
    {
        /// <summary>
        /// The Sql client to be used by this end points communicator
        /// </summary>
        private static Management.Sql.SqlManagementClient SqlClient { get; set; }

        /// <summary>
        /// The old version of Sql client to be used by this end points communicator
        /// </summary>
        public Management.Sql.LegacySdk.SqlManagementClient LegacySqlClient { get; set; }

        /// <summary>
        /// Gets or set the Azure subscription
        /// </summary>
        private static IAzureSubscription Subscription { get; set; }

        /// <summary>
        /// Gets or sets the Azure profile
        /// </summary>
        public IAzureContext Context { get; set; }

        /// <summary>
        /// Creates a communicator for Azure Sql Database and Server Automatic Tuning Configuration
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="subscription"></param>
        public AzureSqlAutomaticTuningCommunicator(IAzureContext context)
        {
            Context = context;
            if (context.Subscription != Subscription)
            {
                Subscription = context.Subscription;
                SqlClient = null;
            }
        }

        /// <summary>
        /// Gets the Azure Sql Server Automatic Tuning Configuration
        /// </summary>
        public Management.Sql.Models.ServerAutomaticTuning GetServerConfiguration(string resourceGroupName, string serverName)
        {
            return GetCurrentSqlClient().ServerAutomaticTuning.Get(resourceGroupName, serverName);
        }

        /// <summary>
        /// Updates the Azure Sql Server Automatic Tuning Configuration
        /// </summary>
        public Management.Sql.Models.ServerAutomaticTuning UpdateServerConfiguration(string resourceGroupName, string serverName, ServerAutomaticTuning parameters)
        {
            return GetCurrentSqlClient().ServerAutomaticTuning.Update(resourceGroupName, serverName, parameters);
        }

        /// <summary>
        /// Gets the Azure Sql Database Automatic Tuning Configuration
        /// </summary>
        public Management.Sql.Models.DatabaseAutomaticTuning GetDatabaseConfiguration(string resourceGroupName, string serverName, string databaseName)
        {
            return GetCurrentSqlClient().DatabaseAutomaticTuning.Get(resourceGroupName, serverName, databaseName);
        }

        /// <summary>
        /// Updates the Azure Sql Database Automatic Tuning Configuration
        /// </summary>
        public Management.Sql.Models.DatabaseAutomaticTuning UpdateDatabaseConfiguration(string resourceGroupName, string serverName, string databaseName, DatabaseAutomaticTuning parameters)
        {
            return GetCurrentSqlClient().DatabaseAutomaticTuning.Update(resourceGroupName, serverName, databaseName, parameters);
        }

        /// <summary>
        /// Retrieve the SQL Management client for the currently selected subscription, adding the session and request
        /// id tracing headers for the current cmdlet invocation.
        /// </summary>
        /// <returns>The SQL Management client for the currently selected subscription.</returns>
        private Management.Sql.LegacySdk.SqlManagementClient GetLegacySqlClient()
        {
            // Get the SQL management client for the current subscription
            if (LegacySqlClient == null)
            {
                LegacySqlClient = AzureSession.Instance.ClientFactory.CreateClient<Management.Sql.LegacySdk.SqlManagementClient>(Context, AzureEnvironment.Endpoint.ResourceManager);
            }
            return LegacySqlClient;
        }

        /// <summary>
        /// Retrieve the SQL Management client for the currently selected subscription, adding the session and request
        /// id tracing headers for the current cmdlet invocation.
        /// </summary>
        /// <returns>The SQL Management client for the currently selected subscription.</returns>
        private Management.Sql.SqlManagementClient GetCurrentSqlClient()
        {
            // Get the SQL management client for the current subscription
            if (SqlClient == null)
            {
                SqlClient = AzureSession.Instance.ClientFactory.CreateArmClient<Management.Sql.SqlManagementClient>(Context, AzureEnvironment.Endpoint.ResourceManager);
            }
            return SqlClient;
        }
    }
}
