// <copyright file="ApiBaseTests.cs" company="10Apps">
// Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>Daniil Poliakh</author>
// <date>27/01/2018 12:26 PM</date>
// <summary>
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

namespace ProjectNamakli.Api.Tests.Controllers
{
    public class BaseApiTests
    {
        public readonly TestContext Context;

        public BaseApiTests(TestContext context)
        {
            Context = context;
        }
    }
}