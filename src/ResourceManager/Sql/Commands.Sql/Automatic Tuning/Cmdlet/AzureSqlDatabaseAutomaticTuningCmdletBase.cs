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

using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.Common.Authentication.Models;
using Microsoft.Azure.Commands.Sql.Common;
using Microsoft.Azure.Commands.Sql.AutomaticTuning.Model;
using Microsoft.Azure.Commands.Sql.AutomaticTuning.Services;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using System.Linq;

namespace Microsoft.Azure.Commands.Sql.AutomaticTuning.Cmdlet
{
    public abstract class AzureSqlDatabaseAutomaticTuningCmdletBase : AzureSqlCmdletBase<IEnumerable<AzureSqlDatabaseAutomaticTuningModel>, AzureSqlAutomaticTuningAdapter>
    {
        /// <summary>
        /// Gets or sets the name of the server to use.
        /// </summary>
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The name of the Azure SQL Server to use.")]
        [ValidateNotNullOrEmpty]
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the database to use.
        /// </summary>
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The name of the Azure SQL Database to use.")]
        [ValidateNotNullOrEmpty]
        public string DatabaseName { get; set; }

        /// <summary>
        /// Initializes the adapter
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        protected override AzureSqlAutomaticTuningAdapter InitModelAdapter(IAzureSubscription subscription)
        {
            return new AzureSqlAutomaticTuningAdapter(DefaultProfile.DefaultContext);
        }
    }
}