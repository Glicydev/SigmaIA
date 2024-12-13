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
            this.target = new System.Windows.Forms.Panel();
            this.sigma = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // target
            // 
            this.target.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.target.Location = new System.Drawing.Point(217, 199);
            this.target.Name = "target";
            this.target.Size = new System.Drawing.Size(135, 128);
            this.target.TabIndex = 0;
            // 
            // sigma
            // 
            this.sigma.BackColor = System.Drawing.Color.Blue;
            this.sigma.Location = new System.Drawing.Point(416, 408);
            this.sigma.Name = "sigma";
            this.sigma.Size = new System.Drawing.Size(74, 71);
            this.sigma.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.sigma);
            this.Controls.Add(this.target);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel target;
        private System.Windows.Forms.Panel sigma;
    }
}

