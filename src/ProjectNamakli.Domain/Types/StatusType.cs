// <copyright file="StatusType.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>18/06/2019 7:14 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

namespace ProjectNamakli.Domain.Types
{
    /// <summary>
    /// Status types
    /// </summary>
    public enum StatusType
    {
        /// <summary>
        /// Hold type
        /// </summary>
        Hold,

        /// <summary>
        /// Ongoing type
        /// </summary>
        Ongoing,

        /// <summary>
        /// Completed type
        /// </summary>
        Completed,

        /// <summary>
        /// Closed type
        /// </summary>
        Closed
    }
}