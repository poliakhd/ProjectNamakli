// <copyright file="ApiTestsCollection.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>21/06/2019 10:31 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using Xunit;

namespace ProjectNamakli.Api.Tests.Collections
{
    [CollectionDefinition("ApiTestsCollection")]
    public class ApiTestsCollection : ICollectionFixture<TestContext>
    {
    }
}