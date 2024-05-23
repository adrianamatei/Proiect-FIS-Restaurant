namespace Proiect_FIS_Restaurant
{
    partial class DetailsForm
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblIsPrice = new System.Windows.Forms.Label();
            this.lblIngredients = new System.Windows.Forms.Label();
            this.lblIsSpicy = new System.Windows.Forms.Label();
            this.lblIsVegetarian = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(233, 90);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(233, 125);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(49, 13);
            this.lblCategory.TabIndex = 1;
            this.lblCategory.Text = "Category";
            // 
            // lblIsPrice
            // 
            this.lblIsPrice.AutoSize = true;
            this.lblIsPrice.Location = new System.Drawing.Point(233, 161);
            this.lblIsPrice.Name = "lblIsPrice";
            this.lblIsPrice.Size = new System.Drawing.Size(31, 13);
            this.lblIsPrice.TabIndex = 2;
            this.lblIsPrice.Text = "Price";
            // 
            // lblIngredients
            // 
            this.lblIngredients.AutoSize = true;
            this.lblIngredients.Location = new System.Drawing.Point(233, 204);
            this.lblIngredients.Name = "lblIngredients";
            this.lblIngredients.Size = new System.Drawing.Size(59, 13);
            this.lblIngredients.TabIndex = 3;
            this.lblIngredients.Text = "Ingredients";
            // 
            // lblIsSpicy
            // 
            this.lblIsSpicy.AutoSize = true;
            this.lblIsSpicy.Location = new System.Drawing.Point(233, 244);
            this.lblIsSpicy.Name = "lblIsSpicy";
            this.lblIsSpicy.Size = new System.Drawing.Size(33, 13);
            this.lblIsSpicy.TabIndex = 4;
            this.lblIsSpicy.Text = "Spicy";
            // 
            // lblIsVegetarian
            // 
            this.lblIsVegetarian.AutoSize = true;
            this.lblIsVegetarian.Location = new System.Drawing.Point(233, 286);
            this.lblIsVegetarian.Name = "lblIsVegetarian";
            this.lblIsVegetarian.Size = new System.Drawing.Size(58, 13);
            this.lblIsVegetarian.TabIndex = 5;
            this.lblIsVegetarian.Text = "Vegetarian";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(281, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 31);
            this.label1.TabIndex = 7;
            this.label1.Text = "Details";
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(287, 339);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(192, 33);
            this.close.TabIndex = 8;
            this.close.Text = "Close";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // DetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.close);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblIsVegetarian);
            this.Controls.Add(this.lblIsSpicy);
            this.Controls.Add(this.lblIngredients);
            this.Controls.Add(this.lblIsPrice);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblName);
            this.Name = "DetailsForm";
            this.Text = "DetailsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblIsPrice;
        private System.Windows.Forms.Label lblIngredients;
        private System.Windows.Forms.Label lblIsSpicy;
        private System.Windows.Forms.Label lblIsVegetarian;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button close;
    }
}