// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator 1.0.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Azure.Commands.Relay.Models
{
    using Azure;
    using Management;
    using Relay;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Azure.Management.Relay.Models;

    /// <summary>
    /// Parameters supplied to the Patch Namespace operation.
    /// </summary>
    public partial class RelayNamespaceAttirbutesUpdateParameter
    {
        /// <summary>
        /// Initializes a new instance of the RelayNamespaceUpdateParameter
        /// class.
        /// </summary>
        public RelayNamespaceAttirbutesUpdateParameter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RelayNamespaceUpdateParameter
        /// class.
        /// </summary>
        /// <param name="tags">Resource tags</param>
        public RelayNamespaceAttirbutesUpdateParameter(IDictionary<string, string> tags = default(IDictionary<string, string>))
        {
            Tags = tags;
        }

        public RelayNamespaceAttirbutesUpdateParameter(RelayNamespaceUpdateParameter relaynamespaceupdateparameter)
        {
            Tags = relaynamespaceupdateparameter.Tags;
        }


        /// <summary>
        /// Static constructor for RelayNamespaceUpdateParameter class.
        /// </summary>
        static RelayNamespaceAttirbutesUpdateParameter()
        {
            Sku = new SkuAttributes();
        }

        /// <summary>
        /// Gets or sets resource tags
        /// </summary>
        [JsonProperty(PropertyName = "tags")]
        public IDictionary<string, string> Tags { get; set; }

        /// <summary>
        /// The sku of the created namespace
        /// </summary>
        [JsonProperty(PropertyName = "sku")]
        public static SkuAttributes Sku { get; private set; }

    }
}

