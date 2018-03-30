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

namespace Microsoft.Azure.Commands.Sql.AutomaticTuning.Model
{
    /// <summary>
    /// Represents an Azure Sql Server Automatic Tuning Configuration
    /// </summary>
    public class AzureSqlServerAutomaticTuningModel
    {
        /// <summary>
        /// Gets or sets the resource id for this resource
        /// </summary>
        public string ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the name of the resource group
        /// </summary>
        public string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the name of the server
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// The actual state that the server is in
        /// Custom - don't inherit
        /// Auto - inherit from Azure defaults
        /// </summary>
        public AutomaticTuningServerMode? ActualState { get; set; }

        /// <summary>
        /// The desired state that the server should be in
        /// Custom - don't inherit
        /// Auto - inherit from Azure defaults
        /// </summary>
        public AutomaticTuningServerMode? DesiredState { get; set; }

        /// <summary>
        /// The actual state that the Force Plan option is in
        /// </summary>
        public AutomaticTuningOptionModeActual? ForceLastGoodPlanActualState { get; set; }

        /// <summary>
        /// The desired state that the Force Plan option should be in
        /// </summary>
        public AutomaticTuningOptionModeDesired? ForceLastGoodPlanDesiredState { get; set; }

        /// <summary>
        /// The actural state that the Create Index option is in
        /// </summary>
        public AutomaticTuningOptionModeActual? CreateIndexActualState { get; set; }

        /// <summary>
        /// The desired state that the Create Index option should be in
        /// </summary>
        public AutomaticTuningOptionModeDesired? CreateIndexDesiredState { get; set; }

        /// <summary>
        /// The actural state that the Drop Index option is in
        /// </summary>
        public AutomaticTuningOptionModeActual? DropIndexActualState { get; set; }

        /// <summary>
        /// The desired state that the Drop Index option should be in
        /// </summary>
        public AutomaticTuningOptionModeDesired? DropIndexDesiredState { get; set; }

    }
}
