﻿using System;
using WeifenLuo.WinFormsUI.Docking;
using Yuanfeng.ExternalUnit.SerialCommPort.IDR;
using Yuanfeng.Log4netx;
using Yuanfeng.Smarty;

namespace Open.Yuanfeng.Windows.SerialPort
{
    public partial class SimpleIDRDoc : DockContent
    {
        public SimpleIDRDoc()
        {
            InitializeComponent();
        }
        private IProtoIDRController controller = new ProtoIDRController();
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                throw new Exception("Unfind someting.");

                int result = controller.Scan(new IDReadCompletedHandler((RicTextInfo member) =>
                 {
                     this.Invoke(new Action(() =>
                     {
                         if (member != null)
                         { this.RicContent.AppendText(member.ToString()); this.Photo.Image = member.Photo.ToBitmap(); }
                         this.btnOpen.Enabled = true;
                     }));
                 }), (int)this.Channel.Value, (int)this.LiveTimeOut.Value);

                if (result == 1) this.btnOpen.Enabled = false;
            }
            catch (Exception exception)
            {
                Logx.Instance().Error("打开身份证阅读器异常", exception);
            }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (controller.IsOpen) controller.Stop(); this.btnOpen.Enabled = true;
        }
    }
}