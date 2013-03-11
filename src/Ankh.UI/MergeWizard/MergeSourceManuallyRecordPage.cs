// $Id: MergeSourceManuallyRecordPage.cs 8023 2010-04-01 21:28:52Z rhuijben $
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
using Ankh.UI.WizardFramework;

namespace Ankh.UI.MergeWizard
{
    public partial class MergeSourceManuallyRecordPage : MergeSourceBasePage
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MergeSourceManuallyRecordPage()
        {
            IsPageComplete = false;
            Text = MergeStrings.MergeSourceHeaderTitle;
            Description = MergeStrings.MergeSourceManuallyRecordPageHeaderMessage;
            InitializeComponent();
        }

        /// <see cref="Ankh.UI.MergeWizard.MergeSourceBasePage" />
        internal override MergeWizard.MergeType MergeType
        {
            get { return MergeWizard.MergeType.ManuallyRecord; }
        }

        protected override void OnPageChanging(WizardPageChangingEventArgs e)
        {
            base.OnPageChanging(e);

            Wizard.LogMode = Ankh.UI.SvnLog.LogMode.MergesEligible;
        }
    }
}