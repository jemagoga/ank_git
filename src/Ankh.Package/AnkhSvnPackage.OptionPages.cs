// $Id: AnkhSvnPackage.OptionPages.cs 6572 2009-03-29 13:15:24Z rhuijben $
//
// Copyright 2008-2009 The AnkhSVN Project
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

using System;
using System.Collections.Generic;
using System.Text;
using Ankh.VSPackage.Attributes;
using Microsoft.VisualStudio.Shell;

namespace Ankh.VSPackage
{
    [ProvideOptionPage(typeof(UserToolsSettingsPage), "Source Control", "Subversion User Tools", 106, 108, false)]
    [ProvideToolsOptionsPageVisibility("Source Control", "Subversion User Tools", AnkhId.SccProviderId)]
    [ProvideOptionPage(typeof(EnvironmentSettingsPage), "Source Control", "Subversion", 106, 107, false)]
    [ProvideToolsOptionsPageVisibility("Source Control", "Subversion", AnkhId.SccProviderId)]
    partial class AnkhSvnPackage
    {
    }
}
