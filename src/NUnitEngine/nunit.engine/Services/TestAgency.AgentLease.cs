﻿// ***********************************************************************
// Copyright (c) 2020 Charlie Poole, Rob Prouse
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

#if NETFRAMEWORK
using System;

namespace NUnit.Engine.Services
{
    public partial class TestAgency
    {
        private sealed class AgentLease : IAgentLease
        {
            private readonly TestAgency _agency;
            private readonly ITestAgent _remoteAgent;

            public AgentLease(TestAgency agency, Guid id, ITestAgent remoteAgent)
            {
                _agency = agency;
                Id = id;
                _remoteAgent = remoteAgent;
            }

            public Guid Id { get; }

            public ITestEngineRunner CreateRunner(TestPackage package)
            {
                return _remoteAgent.CreateRunner(package);
            }

            public void Dispose()
            {
                _agency.Release(Id, _remoteAgent);
            }
        }
    }
}
#endif