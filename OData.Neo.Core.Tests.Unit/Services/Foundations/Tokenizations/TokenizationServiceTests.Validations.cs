﻿//-----------------------------------------------------------------------
// Copyright (c) .NET Foundation and Contributors. All rights reserved.
// See License.txt in the project root for license information.
//-----------------------------------------------------------------------

using System;
using OData.Neo.Core.Models.OTokens.Exceptions;
using Xunit;


namespace OData.Neo.Core.Tests.Unit.Services.Foundations.Tokenizations
{
    public partial class TokenizationServiceTests
    {
        [Fact]
        public void ShouldThrowServiceExceptionOnTokenizeIfExceptionOccurs()
        {
            // given
            string someQuery = GetRandomWordValue();
            var exception = new Exception();

            var failedOTokenServiceException =
                new FailedOTokenServiceException(exception);

            var OTokenServiceException =
                new OTokenServiceException(failedOTokenServiceException);

            // when
            Action tokenizationAction = () =>
                this.tokenizationService.Tokenize(someQuery);

            // then
            Assert.Throws<OTokenServiceException>(tokenizationAction);
        }
    }
}