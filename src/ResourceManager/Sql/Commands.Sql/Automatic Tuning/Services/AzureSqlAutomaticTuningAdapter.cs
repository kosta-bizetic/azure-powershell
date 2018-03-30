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

using Microsoft.Azure.Commands.Common.Authentication.Models;
using Microsoft.Azure.Commands.Sql.AutomaticTuning.Model;
using Microsoft.Azure.Commands.Sql.Services;
using Microsoft.Azure.Management.Sql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;

namespace Microsoft.Azure.Commands.Sql.AutomaticTuning.Services
{
    /// <summary>
    /// Adapter for Automatic Tuning operations
    /// </summary>
    public class AzureSqlAutomaticTuningAdapter
    {
        /// <summary>
        /// Gets or sets the AzureSqlAutomaticTuningCommunicator which has all the needed management clients
        /// </summary>
        private AzureSqlAutomaticTuningCommunicator Communicator { get; set; }

        /// <summary>
        /// Gets or sets the Azure profile
        /// </summary>
        public IAzureContext Context { get; set; }

        /// <summary>
        /// Gets or sets the Azure Subscription
        /// </summary>
        private IAzureSubscription _subscription { get; set; }

        /// <summary>
        /// Constructs an Automatic Tuning adapter
        /// </summary>
        /// <param name="profile">The current azure profile</param>
        /// <param name="subscription">The current azure subscription</param>
        public AzureSqlAutomaticTuningAdapter(IAzureContext context)
        {
            _subscription = context.Subscription;
            Context = context;
            Communicator = new AzureSqlAutomaticTuningCommunicator(Context);
        }

        /// <summary>
        /// Gets an Azure Sql Server Automatic Tuning Configuration.
        /// </summary>
        /// <param name="resourceGroupName">The name of the resource group</param>
        /// <param name="serverName">The name of the Azure Sql Server</param>
        /// <returns>The Azure Sql Database ElasticPool object</returns>
        internal AzureSqlServerAutomaticTuningModel GetServerSettings(string resourceGroupName, string serverName)
        {
            var resp = Communicator.GetServerConfiguration(resourceGroupName, serverName);
            return CreateAutomaticTuningServerModelFromResponse(resourceGroupName, serverName, resp);
        }

        /// <summary>
        /// Updates an Azure Sql Server Automatic Tuning Configuration.
        /// </summary>
        /// <param name="model">The input parameters for the update operation</param>
        /// <returns>The upserted Azure Sql Database ElasticPool</returns>
        internal AzureSqlServerAutomaticTuningModel UpdateServerConfiguration(AzureSqlServerAutomaticTuningModel model)
        {
            var resp = Communicator.UpdateServerConfiguration(model.ResourceGroupName, model.ServerName, new Management.Sql.Models.ServerAutomaticTuning
            {
                DesiredState = model.DesiredState,
                Options = new Dictionary<string, AutomaticTuningServerOptions>()
                    {
                        {"forceLastGoodPlan", new AutomaticTuningServerOptions(model.ForceLastGoodPlanDesiredState) },
                        {"createIndex", new AutomaticTuningServerOptions(model.CreateIndexDesiredState) },
                        {"dropIndex", new AutomaticTuningServerOptions(model.DropIndexDesiredState) }
                    }
            });

            return CreateAutomaticTuningServerModelFromResponse(model.ResourceGroupName, model.ServerName, resp);
        }

        /// <summary>
        /// Gets an Azure Sql Database Automatic Tuning Configuration.
        /// </summary>
        /// <param name="resourceGroupName">The name of the resource group</param>
        /// <param name="serverName">The name of the Azure Sql Server</param>
        /// <param name="databaseName">The name of the Azure Sql Database</param>
        /// <returns></returns>
        internal AzureSqlDatabaseAutomaticTuningModel GetDatabaseSettings(string resourceGroupName, string serverName, string databaseName)
        {
            var resp = Communicator.GetDatabaseConfiguration(resourceGroupName, serverName, databaseName);
            return CreateAutomaticTuningDatabaseModelFromResponse(resourceGroupName, serverName, databaseName, resp);
        }

        /// <summary>
        /// Updates an Azure Sql Database Automatic Tuning Configuration.
        /// </summary>
        /// <param name="model">The input parameters for the update operation</param>
        /// <returns>The upserted Azure Sql Database ElasticPool</returns>
        internal AzureSqlDatabaseAutomaticTuningModel UpdateDatabaseSettings(AzureSqlDatabaseAutomaticTuningModel model)
        {
            var resp = Communicator.UpdateDatabaseConfiguration(model.ResourceGroupName, model.ServerName, model.DatabaseName, new Management.Sql.Models.DatabaseAutomaticTuning
            {
                DesiredState = model.DesiredState,
                Options = new Dictionary<string, AutomaticTuningOptions>()
                    {
                        { "forceLastGoodPlan", new AutomaticTuningOptions(model.ForceLastGoodPlanDesiredState) },
                        { "createIndex", new AutomaticTuningOptions(model.CreateIndexDesiredState) },
                        { "dropIndex", new AutomaticTuningOptions(model.DropIndexDesiredState) }
                    }
            });

            return CreateAutomaticTuningDatabaseModelFromResponse(model.ResourceGroupName, model.ServerName, model.DatabaseName, resp);
        }

        /// <summary>
        /// Converts a DatabaseAutomaticTuning model to the corresponding powershell database object.
        /// </summary>
        /// <param name="resourceGroupName">The name of the resource group</param>
        /// <param name="serverName">The name of the Azure Sql Server</param>
        /// <param name="databaseName">The name of the Azure Sql Database</param>
        /// <param name="databaseSettings">The service response</param>
        /// <returns>The converted model</returns>
        private AzureSqlDatabaseAutomaticTuningModel CreateAutomaticTuningDatabaseModelFromResponse(string resourceGroupName, string serverName, string databaseName, Management.Sql.Models.DatabaseAutomaticTuning databaseSettings)
        {
            AzureSqlDatabaseAutomaticTuningModel model = new AzureSqlDatabaseAutomaticTuningModel
            {
                ResourceId = databaseSettings.Id,
                ResourceGroupName = resourceGroupName,
                ServerName = serverName,
                DatabaseName = databaseName,
                ActualState = databaseSettings.ActualState,
                DesiredState = databaseSettings.DesiredState,
                ForceLastGoodPlanActualState = databaseSettings.Options["forceLastGoodPlan"].ActualState,
                ForceLastGoodPlanDesiredState = databaseSettings.Options["forceLastGoodPlan"].DesiredState,
                CreateIndexActualState = databaseSettings.Options["createIndex"].ActualState,
                CreateIndexDesiredState = databaseSettings.Options["createIndex"].DesiredState,
                DropIndexActualState = databaseSettings.Options["dropIndex"].ActualState,
                DropIndexDesiredState = databaseSettings.Options["dropIndex"].DesiredState
            };

            return model;
        }

        /// <summary>
        /// Converts a ServerAutomaticTuning model to the corresponding powershell server object.
        /// </summary>
        /// <param name="resourceGroupName">The resource group the server is in</param>
        /// <param name="serverName">The name of the Azure Sql Database Server</param>
        /// <param name="serverSettings">The service response</param>
        /// <returns>The converted model</returns>
        private AzureSqlServerAutomaticTuningModel CreateAutomaticTuningServerModelFromResponse(string resourceGroupName, string serverName, Management.Sql.Models.ServerAutomaticTuning serverSettings)
        {
            AzureSqlServerAutomaticTuningModel model = new AzureSqlServerAutomaticTuningModel
            {
                ResourceId = serverSettings.Id,
                ResourceGroupName = resourceGroupName,
                ServerName = serverName,
                ActualState = serverSettings.ActualState,
                DesiredState = serverSettings.DesiredState,
                ForceLastGoodPlanActualState = serverSettings.Options["forceLastGoodPlan"].ActualState,
                ForceLastGoodPlanDesiredState = serverSettings.Options["forceLastGoodPlan"].DesiredState,
                CreateIndexActualState = serverSettings.Options["createIndex"].ActualState,
                CreateIndexDesiredState = serverSettings.Options["createIndex"].DesiredState,
                DropIndexActualState = serverSettings.Options["dropIndex"].ActualState,
                DropIndexDesiredState = serverSettings.Options["dropIndex"].DesiredState
            };

            return model;
        }
    }
}
