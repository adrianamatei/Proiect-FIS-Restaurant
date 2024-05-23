namespace Proiect_FIS_Restaurant
{
    partial class MenuForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListView listViewMenu;
        private System.Windows.Forms.Button btnWelcome;
        private System.Windows.Forms.Button btnCart;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.Button btnOrder;

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
            this.listViewMenu = new System.Windows.Forms.ListView();
            this.btnWelcome = new System.Windows.Forms.Button();
            this.btnCart = new System.Windows.Forms.Button();
            this.btnDetails = new System.Windows.Forms.Button();
            this.btnOrder = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // listViewMenu
            // 
            this.listViewMenu.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            new System.Windows.Forms.ColumnHeader() { Text = "Name" },
            new System.Windows.Forms.ColumnHeader() { Text = "Category" },
            new System.Windows.Forms.ColumnHeader() { Text = "Price" },
            new System.Windows.Forms.ColumnHeader() { Text = "Ingredients" },
            new System.Windows.Forms.ColumnHeader() { Text = "Is Spicy" },
            new System.Windows.Forms.ColumnHeader() { Text = "Is Vegetarian" },
            new System.Windows.Forms.ColumnHeader() { Text = "Is Available" }});
            this.listViewMenu.FullRowSelect = true;
            this.listViewMenu.HideSelection = false;
            this.listViewMenu.Location = new System.Drawing.Point(12, 12);
            this.listViewMenu.Name = "listViewMenu";
            this.listViewMenu.Size = new System.Drawing.Size(776, 426);
            this.listViewMenu.TabIndex = 0;
            this.listViewMenu.UseCompatibleStateImageBehavior = false;
            this.listViewMenu.View = System.Windows.Forms.View.Details;

            // 
            // btnWelcome
            // 
            this.btnWelcome.Location = new System.Drawing.Point(12, 450);
            this.btnWelcome.Name = "btnWelcome";
            this.btnWelcome.Size = new System.Drawing.Size(100, 30);
            this.btnWelcome.TabIndex = 1;
            this.btnWelcome.Text = "Welcome";
            this.btnWelcome.UseVisualStyleBackColor = true;
            this.btnWelcome.Click += new System.EventHandler(this.btnWelcome_Click);

            // 
            // btnCart
            // 
            this.btnCart.Location = new System.Drawing.Point(118, 450);
            this.btnCart.Name = "Cart";
            this.btnCart.Size = new System.Drawing.Size(100, 30);
            this.btnCart.TabIndex = 2;
            this.btnCart.Text = "Add to Cart";
            this.btnCart.UseVisualStyleBackColor = true;
            this.btnCart.Click += new System.EventHandler(this.Cart_Click);

            // 
            // btnDetails
            // 
            this.btnDetails.Location = new System.Drawing.Point(224, 450);
            this.btnDetails.Name = "details";
            this.btnDetails.Size = new System.Drawing.Size(100, 30);
            this.btnDetails.TabIndex = 3;
            this.btnDetails.Text = "Details";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.details_Click);

            // 
            // btnOrder
            // 
            this.btnOrder.Location = new System.Drawing.Point(330, 450);
            this.btnOrder.Name = "order";
            this.btnOrder.Size = new System.Drawing.Size(100, 30);
            this.btnOrder.TabIndex = 4;
            this.btnOrder.Text = "Place Order";
            this.btnOrder.UseVisualStyleBackColor = true;
            this.btnOrder.Click += new System.EventHandler(this.order_Click);

            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.listViewMenu);
            this.Controls.Add(this.btnWelcome);
            this.Controls.Add(this.btnCart);
            this.Controls.Add(this.btnDetails);
            this.Controls.Add(this.btnOrder);
            this.Name = "MenuForm";
            this.Text = "MenuForm";
            
            this.ResumeLayout(false);
        }

        #endregion
    }
}
