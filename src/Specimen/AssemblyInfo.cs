// <copyright file="AssemblyInfo.cs" company="Specimen">
// Copyright (c) Specimen. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using NullGuard;

[assembly: CLSCompliant(false)]
[assembly: AllowPartiallyTrustedCallers]
[assembly: ComVisible(false)]
[assembly: NeutralResourcesLanguage("en")]
[assembly: InternalsVisibleTo("Specimen.Test")]
[assembly: NullGuard(ValidationFlags.AllPublic | ValidationFlags.NonPublic)]