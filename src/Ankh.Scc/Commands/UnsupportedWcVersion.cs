// $Id: UnsupportedWcVersion.cs 7184 2009-08-12 13:31:02Z rhuijben $
//
// Copyright 2009 The AnkhSVN Project
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
using Ankh.Commands;
using Ankh.UI;

namespace Ankh.Scc.Commands
{
    [Command(AnkhCommand.NotifyWcToNew, AlwaysAvailable=true)]
    class UnsupportedWcVersion : ICommandHandler
    {

        public void OnUpdate(CommandUpdateEventArgs e)
        {
        }

        bool _skip;
        public void OnExecute(CommandEventArgs e)
        {
            if(_skip) // Only show this message once!
                return;

            _skip = true;
            AnkhMessageBox mb = new AnkhMessageBox(e.Context);
            mb.Show(string.Format(Resources.UnsupportedWorkingCopyFound, e.Argument));
        }
    }
}
