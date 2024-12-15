namespace iaQTable
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.target = new System.Windows.Forms.Panel();
            this.lblPoints = new System.Windows.Forms.Label();
            this.sigmaImg = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.sigmaImg)).BeginInit();
            this.SuspendLayout();
            // 
            // target
            // 
            this.target.BackColor = System.Drawing.Color.Lime;
            this.target.Location = new System.Drawing.Point(235, 235);
            this.target.Name = "target";
            this.target.Size = new System.Drawing.Size(130, 130);
            this.target.TabIndex = 2;
            // 
            // lblPoints
            // 
            this.lblPoints.AutoSize = true;
            this.lblPoints.Location = new System.Drawing.Point(11, 13);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(35, 13);
            this.lblPoints.TabIndex = 3;
            this.lblPoints.Text = "label1";
            // 
            // sigmaImg
            // 
            this.sigmaImg.BackColor = System.Drawing.Color.Transparent;
            this.sigmaImg.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sigmaImg.BackgroundImage")));
            this.sigmaImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sigmaImg.Location = new System.Drawing.Point(433, 254);
            this.sigmaImg.Name = "sigmaImg";
            this.sigmaImg.Size = new System.Drawing.Size(70, 70);
            this.sigmaImg.TabIndex = 4;
            this.sigmaImg.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.sigmaImg);
            this.Controls.Add(this.lblPoints);
            this.Controls.Add(this.target);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.sigmaImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel target;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.PictureBox sigmaImg;
    }
}

