﻿//-----------------------------------------------------------------------
// Copyright (c) .NET Foundation and Contributors. All rights reserved.
// See License.txt in the project root for license information.
//-----------------------------------------------------------------------

using Xeptions;

namespace OData.Neo.Core.Models.Orchestrations.OQueries.Exceptions
{
    public class NullQueryOQueryOrchestrationException : Xeption
    {
        public NullQueryOQueryOrchestrationException()
            : base("Query Expression is null.")
        { }
    }
}
