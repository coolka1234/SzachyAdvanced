namespace SzachyAdvanced
{
    partial class PromotionPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PromotionPanel));
            this.buttonRook = new System.Windows.Forms.Button();
            this.buttonSkoczek = new System.Windows.Forms.Button();
            this.buttonGoniec = new System.Windows.Forms.Button();
            this.buttonQrolowa = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonRook
            // 
            this.buttonRook.BackColor = System.Drawing.Color.Transparent;
            this.buttonRook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonRook.Location = new System.Drawing.Point(0, 0);
            this.buttonRook.Name = "buttonRook";
            this.buttonRook.Size = new System.Drawing.Size(175, 210);
            this.buttonRook.TabIndex = 0;
            this.buttonRook.UseVisualStyleBackColor = false;
            this.buttonRook.Click += new System.EventHandler(this.buttonRook_Click);
            // 
            // buttonSkoczek
            // 
            this.buttonSkoczek.BackColor = System.Drawing.Color.Transparent;
            this.buttonSkoczek.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonSkoczek.Location = new System.Drawing.Point(175, 0);
            this.buttonSkoczek.Name = "buttonSkoczek";
            this.buttonSkoczek.Size = new System.Drawing.Size(175, 210);
            this.buttonSkoczek.TabIndex = 1;
            this.buttonSkoczek.UseVisualStyleBackColor = false;
            this.buttonSkoczek.Click += new System.EventHandler(this.buttonSkoczek_Click);
            // 
            // buttonGoniec
            // 
            this.buttonGoniec.BackColor = System.Drawing.Color.Transparent;
            this.buttonGoniec.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonGoniec.Location = new System.Drawing.Point(350, 0);
            this.buttonGoniec.Name = "buttonGoniec";
            this.buttonGoniec.Size = new System.Drawing.Size(175, 210);
            this.buttonGoniec.TabIndex = 2;
            this.buttonGoniec.UseVisualStyleBackColor = false;
            this.buttonGoniec.Click += new System.EventHandler(this.buttonGoniec_Click);
            // 
            // buttonQrolowa
            // 
            this.buttonQrolowa.BackColor = System.Drawing.Color.Transparent;
            this.buttonQrolowa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonQrolowa.Location = new System.Drawing.Point(525, 0);
            this.buttonQrolowa.Name = "buttonQrolowa";
            this.buttonQrolowa.Size = new System.Drawing.Size(175, 210);
            this.buttonQrolowa.TabIndex = 3;
            this.buttonQrolowa.UseVisualStyleBackColor = false;
            this.buttonQrolowa.Click += new System.EventHandler(this.buttonQrolowa_Click);
            // 
            // PromotionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Linen;
            this.ClientSize = new System.Drawing.Size(704, 211);
            this.Controls.Add(this.buttonQrolowa);
            this.Controls.Add(this.buttonGoniec);
            this.Controls.Add(this.buttonSkoczek);
            this.Controls.Add(this.buttonRook);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PromotionPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choose pawn to promote";
            this.Load += new System.EventHandler(this.PromotionPanel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRook;
        private System.Windows.Forms.Button buttonSkoczek;
        private System.Windows.Forms.Button buttonGoniec;
        private System.Windows.Forms.Button buttonQrolowa;
    }
}