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

using Microsoft.Azure.Management.Sql.Models;
using Microsoft.Azure.Commands.Sql.Database.Model;
using Microsoft.Azure.Commands.Sql.AutomaticTuning.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Sql.AutomaticTuning.Cmdlet
{
    /// <summary>
    /// Cmdlet to update an Azure Sql Server Automatic Tuning Configuration
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "AzureRmSqlServerAutomaticTuningConfiguration", SupportsShouldProcess = true,
        ConfirmImpact = ConfirmImpact.Medium)]
    public class SetAzureSqlServerAutomaticTuningConfiguration : AzureSqlServerAutomaticTuningCmdletBase
    {
        /// <summary>
        /// The desired state that the server should be in.
        /// Custom - don't inherit
        /// Auto - inherit from Azure defaults
        /// </summary>
        [Parameter(Mandatory = false,
            Position = 2,
            HelpMessage = "The desired state that the server should be in.")]
        [ValidateNotNullOrEmpty]
        [PSDefaultValue(Value=AutomaticTuningServerMode.Unspecified)]
        public AutomaticTuningServerMode? DesiredState { get; set; }

        /// <summary>
        /// The desired state that the Force Plan option should be in.
        /// </summary>
        [Parameter(Mandatory = false,
            HelpMessage = "The desired state that the Force Plan option should be in.")]
        public AutomaticTuningOptionModeDesired? ForceLastGoodPlanDesiredState { get; set; }

        /// <summary>
        /// The desired state that the Create Index option should be in.
        /// </summary>
        [Parameter(Mandatory = false,
            HelpMessage = "The desired state that the Create Index option should be in.")]
        public AutomaticTuningOptionModeDesired? CreateIndexDesiredState { get; set; }

        /// <summary>
        /// The desired state that the Drop Index option should be in.
        /// </summary>
        [Parameter(Mandatory = false,
            HelpMessage = "The desired state that the Drop Index option should be in.")]
        public AutomaticTuningOptionModeDesired? DropIndexDesiredState { get; set; }

        /// <summary>
        /// Gets or sets whether or not to run this cmdlet in the background as a job.
        /// </summary>
        [Parameter(Mandatory = false, HelpMessage = "Run cmdlet in the background")]
        public SwitchParameter AsJob { get; set; }

        /// <summary>
        /// Overriding to add warning message
        /// </summary>
        public override void ExecuteCmdlet()
        {
            base.ExecuteCmdlet();
        }

        /// <summary>
        /// Get the entities from the service
        /// </summary>
        /// <returns>The list of entities</returns>
        protected override IEnumerable<AzureSqlServerAutomaticTuningModel> GetEntity()
        {
            return new List<AzureSqlServerAutomaticTuningModel>() {
                ModelAdapter.GetServerSettings(this.ResourceGroupName, this.ServerName)
            };
        }

        /// <summary>
        /// Create the model from user input
        /// </summary>
        /// <param name="model">Model retrieved from service</param>
        /// <returns>The model that was passed in</returns>
        protected override IEnumerable<AzureSqlServerAutomaticTuningModel> ApplyUserInputToModel(IEnumerable<AzureSqlServerAutomaticTuningModel> model)
        {
            AzureSqlServerAutomaticTuningModel serverSettings = model.First();

            if (DesiredState != null && DesiredState != AutomaticTuningServerMode.Unspecified)
            {
                serverSettings.DesiredState = DesiredState;
            }

            if (ForceLastGoodPlanDesiredState != null)
            {
                serverSettings.ForceLastGoodPlanDesiredState = ForceLastGoodPlanDesiredState;
            }
            if (CreateIndexDesiredState != null)
            {
                serverSettings.CreateIndexDesiredState = CreateIndexDesiredState ;
            }
            if (DropIndexDesiredState != null)
            {
                serverSettings.DropIndexDesiredState = DropIndexDesiredState;
            }

            return model;
        }

        /// <summary>
        /// Update the server
        /// </summary>
        /// <param name="entity">The output of apply user input to model</param>
        /// <returns>The input entity</returns>
        protected override IEnumerable<AzureSqlServerAutomaticTuningModel> PersistChanges(IEnumerable<AzureSqlServerAutomaticTuningModel> entity)
        {
            return new List<AzureSqlServerAutomaticTuningModel>() {
                ModelAdapter.UpdateServerConfiguration(entity.First())
            };
        }
    }
}
