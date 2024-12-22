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
            this.components = new System.ComponentModel.Container();
            this.target = new System.Windows.Forms.Panel();
            this.lblPoints = new System.Windows.Forms.Label();
            this.sigmaTimer = new System.Windows.Forms.Timer(this.components);
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
            // sigmaTimer
            // 
            this.sigmaTimer.Interval = 1;
            this.sigmaTimer.Tick += new System.EventHandler(this.sigmaTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.lblPoints);
            this.Controls.Add(this.target);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel target;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.Timer sigmaTimer;
    }
}

