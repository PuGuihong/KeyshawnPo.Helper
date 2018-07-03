namespace Dao.AdoNet.Winform
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbHardInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbHardInfo
            // 
            this.lbHardInfo.AutoSize = true;
            this.lbHardInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbHardInfo.Location = new System.Drawing.Point(0, 0);
            this.lbHardInfo.Name = "lbHardInfo";
            this.lbHardInfo.Size = new System.Drawing.Size(41, 12);
            this.lbHardInfo.TabIndex = 0;
            this.lbHardInfo.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 422);
            this.Controls.Add(this.lbHardInfo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbHardInfo;
    }
}

