﻿//-----------------------------------------------------------------------
// Copyright (c) .NET Foundation and Contributors. All rights reserved.
// See License.txt in the project root for license information.
//-----------------------------------------------------------------------

using System;
using FluentAssertions;
using InternalMock.Extensions;
using OData.Neo.Core.Models.OTokens;
using OData.Neo.Core.Models.OTokens.Exceptions;
using Xunit;

namespace OData.Neo.Core.Tests.Unit.Services.Foundations.OTokenizations
{
    public partial class OTokenizationServiceTests
    {
        [Fact]
        public void ShouldThrowServiceExceptionOnTokenizationIfServiceErrorOccurs()
        {
            // given
            OToken[] someOTokens =
                CreateRandomOTokens();

            var serviceException = new Exception();

            var failedOTokenServiceException =
                new FailedOTokenServiceException(
                    serviceException);

            var expectedOTokenServiceException =
                new OTokenServiceException(
                    failedOTokenServiceException);

            this.tokenizationService.Mock(
                methodName: "ValidateOTokens")
                    .Throws(serviceException);

            // when
            Action toknizationAction = () =>
                this.tokenizationService.OTokenize(
                    someOTokens);

            OTokenServiceException actualOTokenValidationException =
                Assert.Throws<OTokenServiceException>(
                    toknizationAction);

            // then
            actualOTokenValidationException.Should()
                .BeEquivalentTo(expectedOTokenServiceException);

            this.tokenizationService.ClearAllOtherCalls();
        }
    }
}
