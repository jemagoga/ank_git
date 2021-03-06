// $Id: DiffToolWindowControl.cs 10071 2011-02-06 17:09:56Z sander $
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Ankh.Scc.UI;
using Ankh.UI.Services;
using Microsoft.VisualStudio.Shell.Interop;
using System.Collections.ObjectModel;
using System.IO;
using Ankh.Diff.DiffUtils;

namespace Ankh.UI.DiffWindow
{
    public partial class DiffToolWindowControl : AnkhToolWindowControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiffControl"/> class.
        /// </summary>
        public DiffToolWindowControl()
        {
            InitializeComponent();
        }

        IAnkhPackage Package
        {
            get { return Context.GetService<IAnkhPackage>(); }
        }

        /// <summary>
        /// Called when the frame is created
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFrameCreated(EventArgs e)
        {
            base.OnFrameCreated(e);

            ToolWindowHost.CommandContext = AnkhId.DiffMergeContextGuid;
            ToolWindowHost.KeyboardContext = AnkhId.DiffMergeContextGuid;
            
        }

        protected override void OnLoad(EventArgs e)
        {
            ToolStripRenderer renderer = null;
            System.Windows.Forms.Design.IUIService ds = Context.GetService<System.Windows.Forms.Design.IUIService>();
            if (ds != null)
            {
                renderer = ds.Styles["VsToolWindowRenderer"] as ToolStripRenderer;
            }

            if (renderer != null)
                diffControl1.ToolStripRenderer = renderer;

            base.OnLoad(e);
        }

        int _nFrame;
        protected override void OnFrameClose(EventArgs e)
        {
            base.OnFrameClose(e);

            OnClose();
        }

        protected override void OnFrameShow(FrameEventArgs e)
        {
            base.OnFrameShow(e);

            switch (e.Show)
            {
                case __FRAMESHOW.FRAMESHOW_Hidden:
                case __FRAMESHOW.FRAMESHOW_DestroyMultInst:
                case __FRAMESHOW.FRAMESHOW_WinClosed:
                    OnClose();
                    break;
            }
        }

        void OnClose()
        {
            Clear();

            if (_nFrame >= 0)
            {
                Context.GetService<IAnkhDiffHandler>().ReleaseDiff(_nFrame);
                _nFrame = -1;
            }
        }

        private void Clear()
        {
            //throw new NotImplementedException();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch(keyData)
            {
                case Keys.Escape:
                    CloseToolWindow();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CloseToolWindow()
        {
            Package.CloseToolWindow(AnkhToolWindow.Diff, _nFrame, __FRAMECLOSE.FRAMECLOSE_NoSave);
        }

        private void GetFileLines(string strA, string strB, out Collection<string> A, out Collection<string> B)
        {
            A = File.Exists(strA) ? Functions.GetFileTextLines(strA) : Functions.GetStringTextLines(string.Empty);
            B = File.Exists(strB) ? Functions.GetFileTextLines(strB) : Functions.GetStringTextLines(string.Empty);
        }

        public void Reset(int n, AnkhDiffArgs args)
        {
            _nFrame = n;
            Clear();

            Collection<string> A, B;
            GetFileLines(args.BaseFile, args.MineFile, out A, out B);
            TextDiff Diff = new TextDiff(HashType.HashCode, false, false);
            EditScript Script = Diff.Execute(A, B);

            string strCaptionA = args.BaseTitle ?? Path.GetFileName(args.BaseFile);
            string strCaptionB = args.MineTitle ?? Path.GetFileName(args.MineFile);
            //Ankh.Diff.FileName fnA = new Ankh.Diff.FileName(mine);
            //Ankh.Diff.FileName fnB = new Ankh.Diff.FileName(theirs);
            diffControl1.SetData(A, B, Script, strCaptionA, strCaptionB);

            ToolWindowHost.Title = Path.GetFileName(args.MineFile) + " - Diff";
        }
    }
}
