﻿//-----------------------------------------------------------------------
// Copyright (c) .NET Foundation and Contributors. All rights reserved.
// See License.txt in the project root for license information.
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using OData.Neo.Core.Models.Tokens;

namespace OData.Neo.Core.Services.Foundations.Tokenizations
{
    public partial class TokenizationService : ITokenizationService
    {
        readonly char[] SeparatorChars = new char[] { '\'', ' ', '=', '\\' };

        public Token[] Tokenize(string rawQuery) =>
        TryCatch(() =>
        {
            ValidateOTokenQuery(rawQuery);

            return OTokenize(rawQuery, SeparatorChars).ToArray();
        });

        private static IEnumerable<Token> OTokenize(string rawQuery, char[] separatorChars)
        {
            string remainingRawQuery = rawQuery;
            Func<char, bool> NotSeparatorChar = c => !separatorChars.Contains(c);

            while (remainingRawQuery.Length > 0)
            {
                string returnValue = remainingRawQuery;
                string nextRemainingValue = string.Empty;

                var index = remainingRawQuery.IndexOfAny(separatorChars);
                if (index is not -1)
                {
                    int rangeIndex = GetRangeIndex(index);
                    Range currentRange = Range.EndAt(rangeIndex);
                    Range remainingRange = Range.StartAt(rangeIndex);

                    returnValue = remainingRawQuery[currentRange];
                    nextRemainingValue = remainingRawQuery[remainingRange];
                }

                remainingRawQuery = nextRemainingValue;

                var oTokenType = returnValue.Any(NotSeparatorChar)
                    ? TokenType.Word
                    : TokenType.Separator;

                yield return new Token(oTokenType, returnValue);
            }
        }

        private static int GetRangeIndex(in int index)
        {
            if (index > 0)
            {
                return index;
            }

            return 1;
        }
    }
}