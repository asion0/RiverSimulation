namespace RiverSimulationApplication
{
    partial class ResultTimeSelForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.ok = new System.Windows.Forms.Button();
            this.timeLsb = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.clearBtn = new System.Windows.Forms.Button();
            this.selectAllChk = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "選取時間：";
            // 
            // ok
            // 
            this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ok.Location = new System.Drawing.Point(139, 325);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(96, 32);
            this.ok.TabIndex = 6;
            this.ok.Text = "完成";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // timeLsb
            // 
            this.timeLsb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLsb.FormattingEnabled = true;
            this.timeLsb.ItemHeight = 12;
            this.timeLsb.Location = new System.Drawing.Point(14, 70);
            this.timeLsb.Name = "timeLsb";
            this.timeLsb.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.timeLsb.Size = new System.Drawing.Size(197, 244);
            this.timeLsb.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 302);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "秒";
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(111, 12);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(100, 31);
            this.clearBtn.TabIndex = 9;
            this.clearBtn.Text = "清除";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // selectAllChk
            // 
            this.selectAllChk.AutoSize = true;
            this.selectAllChk.Location = new System.Drawing.Point(14, 20);
            this.selectAllChk.Name = "selectAllChk";
            this.selectAllChk.Size = new System.Drawing.Size(48, 16);
            this.selectAllChk.TabIndex = 10;
            this.selectAllChk.Text = "全選";
            this.selectAllChk.UseVisualStyleBackColor = true;
            this.selectAllChk.CheckedChanged += new System.EventHandler(this.selectAllChk_CheckedChanged);
            // 
            // ResultTimeSelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 361);
            this.Controls.Add(this.selectAllChk);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.timeLsb);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.label1);
            this.Name = "ResultTimeSelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "選取時間";
            this.Load += new System.EventHandler(this.ResultTimeSelForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.ListBox timeLsb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.CheckBox selectAllChk;
    }
}